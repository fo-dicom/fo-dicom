// Copyright (c) 2012-2019 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

using System;
using System.Collections.Generic;
using System.Data;
#if !__IOS__ && !__ANDROID__ && !NETSTANDARD
using System.Data.Odbc;
#endif
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
    public class DatabaseQueryTransformRule : IDicomTransformRule
    {

        #region Private Members

        #endregion

        #region Public Constructor

        public DatabaseQueryTransformRule()
        {
            ConnectionType = DatabaseType.MsSql;
            Output = new List<DicomTag>();
            Parameters = new List<DicomTag>();
        }

        public DatabaseQueryTransformRule(
            string connectionString,
            DatabaseType dbType,
            string query,
            DicomTag[] outputTags,
            DicomTag[] paramTags)
        {
            ConnectionString = connectionString;
            ConnectionType = dbType;
            Query = query;
            Output = new List<DicomTag>(outputTags);
            Parameters = new List<DicomTag>(paramTags);
        }

      #endregion

      #region Public Properties

      public string ConnectionString { get; set; }

      public DatabaseType ConnectionType { get; set; }

      public string Query { get; set; }

      public List<DicomTag> Output { get; set; }

      public List<DicomTag> Parameters { get; set; }

      #endregion

      #region Public Methods

      public void Transform(DicomDataset dataset, DicomDataset modifiedAttributesSequenceItem = null)
        {
            IDbConnection connection = null;

            try
            {
                if (ConnectionType == DatabaseType.MsSql) connection = new SqlConnection(ConnectionString);
#if !__IOS__ && !__ANDROID__ && !NETSTANDARD
                else if (ConnectionType == DatabaseType.Odbc) connection = new OdbcConnection(ConnectionString);
#endif
                using (IDbCommand command = connection.CreateCommand())
                {
                    command.Connection = connection;
                    command.CommandText = Query;

                    for (int i = 0; i < Parameters.Count; i++)
                    {
                        var str = dataset.TryGetString(Parameters[i], out var dummy) ? dummy : String.Empty;
                        SqlParameter prm = new SqlParameter($"@{i}", str);
                        command.Parameters.Add(prm);
                    }

                    connection.Open();

                    if (Output.Count == 0)
                    {
                        command.ExecuteNonQuery();
                    }
                    else
                    {
                        using (IDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                for (int i = 0; i < Output.Count; i++)
                                {
                                    dataset.CopyTo(modifiedAttributesSequenceItem, Output[i]);
                                    string str = reader.GetString(i);
                                    dataset.AddOrUpdate(Output[i], str);
                                }
                            }
                        }
                    }

                    connection.Close();

                    connection = null;
                }
            }
            finally
            {
                if (connection != null)
                {
                    if (connection.State == ConnectionState.Closed || connection.State == ConnectionState.Broken) connection.Close();
                    connection.Dispose();
                }
            }
        }


        #endregion
    }
}
