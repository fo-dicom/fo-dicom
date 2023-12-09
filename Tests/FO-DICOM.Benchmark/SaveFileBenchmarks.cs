// Copyright (c) 2012-2023 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).
#nullable disable

using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using BenchmarkDotNet.Attributes;

namespace FellowOakDicom.Benchmark
{
    [MemoryDiagnoser]
    public class SaveFileBenchmarks
    {
        private readonly DicomFile _dicomFile;
        private readonly FileInfo _output;

        public SaveFileBenchmarks()
        {
            var rootPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

            _dicomFile = DicomFile.Open(Path.Combine(rootPath, "Data\\MG.dcm"));
            _output = new FileInfo(Path.Combine(rootPath, "Data\\MG_copy.dcm"));
            _output.Delete();
        }

        [Benchmark]
        public void Save()
        {
            _dicomFile.Save(_output.FullName);
            _output.Delete();
        }

        [Benchmark]
        public async Task SaveAsync()
        {
            await _dicomFile.SaveAsync(_output.FullName);
            _output.Delete();
        }
    }
}
