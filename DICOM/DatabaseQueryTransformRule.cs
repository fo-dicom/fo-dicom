using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Odbc;
using System.Data.SqlClient;

namespace Dicom
{
    public enum DatabaseType
    {
        Odbc,
        MsSql,
        DB2
    }

    /// <summary>
    /// Updates a DICOM dataset based on a database query.
    /// </summary>
    public class DatabaseQueryTransformRule : IDicomTransformRule {
        #region Private Members
        private string _connectionString;
        private DatabaseType _dbType;
        private string _query;
        private List<DicomTag> _output;
        private List<DicomTag> _params;
        #endregion

        #region Public Constructor
        public DatabaseQueryTransformRule() {
            _dbType = DatabaseType.MsSql;
            _output = new List<DicomTag>();
            _params = new List<DicomTag>();
        }

        public DatabaseQueryTransformRule(string connectionString, DatabaseType dbType, string query, DicomTag[] outputTags, DicomTag[] paramTags) {
            _connectionString = connectionString;
            _dbType = dbType;
            _query = query;
            _output = new List<DicomTag>(outputTags);
            _params = new List<DicomTag>(paramTags);
        }
        #endregion

        #region Public Properties
        public string ConnectionString {
            get { return _connectionString; }
            set { _connectionString = value; }
        }

        public DatabaseType ConnectionType {
            get { return _dbType; }
            set { _dbType = value; }
        }

        public string Query {
            get { return _query; }
            set { _query = value; }
        }

        public List<DicomTag> Output {
            get { return _output; }
            set { _output = value; }
        }

        public List<DicomTag> Parameters {
            get { return _params; }
            set { _params = value; }
        }
        #endregion

        #region Public Methods
        public void Transform(DicomDataset dataset, DicomDataset modifiedAttributesSequenceItem = null) {
            IDbConnection connection = null;

            try {
                if (_dbType == DatabaseType.Odbc)
                    connection = new OdbcConnection(_connectionString);
                else if (_dbType == DatabaseType.MsSql)
                    connection = new SqlConnection(_connectionString);

                using (IDbCommand command = connection.CreateCommand()) {
                    command.Connection = connection;
                    command.CommandText = _query;

                    for (int i = 0; i < _params.Count; i++) {
                        var str = dataset.Get<string>(_params[i], -1, String.Empty);
                        SqlParameter prm = new SqlParameter(String.Format("@{0}", i), str);
                        command.Parameters.Add(prm);
                    }

                    connection.Open();

                    if (_output.Count == 0) {
                        command.ExecuteNonQuery();
                    } else {
                        using (IDataReader reader = command.ExecuteReader()) {
                            if (reader.Read()) {
                                for (int i = 0; i < _output.Count; i++) {
                                    dataset.CopyTo(modifiedAttributesSequenceItem, _output[i]);
                                    string str = reader.GetString(i);
                                    dataset.Add(_output[i], str);
                                }
                            }
                        }
                    }

                    connection.Close();

                    connection = null;
                }
            } finally {
                if (connection != null) {
                    if (connection.State == ConnectionState.Closed || connection.State == ConnectionState.Broken)
                        connection.Close();
                    connection.Dispose();
                }
            }
        }

        public override string ToString() {
            return base.ToString();
        }
        #endregion
    }
}