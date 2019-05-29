using BenchmarkDotNet.Running;

namespace DICOM.Benchmarks.Desktop
{
    public class Program
    {
        public static void Main(string[] args)
        {
            BenchmarkRunner.Run<DicomClientBenchmarks>();
        }
    }
}
