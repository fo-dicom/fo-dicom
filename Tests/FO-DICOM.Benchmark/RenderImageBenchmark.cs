// Copyright (c) 2012-2023 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).
#nullable disable

using BenchmarkDotNet.Attributes;
using FellowOakDicom.Imaging;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace FellowOakDicom.Benchmark
{

    [MemoryDiagnoser]
    [ThreadingDiagnoser]
    public class RenderImageBenchmark
    {

        private readonly string _rootpath;

        private DicomDataset _loadedTomo;


        public RenderImageBenchmark()
        {
            _rootpath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        }


        [GlobalSetup]
        public void Setup()
        {
            new DicomSetupBuilder()
                .RegisterServices(s => s
                .AddFellowOakDicom()
                .AddTranscoderManager<FellowOakDicom.Imaging.NativeCodec.NativeTranscoderManager>())
                .Build();

            var tomofilename = Path.Combine(_rootpath, "Data\\CT0070.dcm");
            _loadedTomo = DicomFile.Open(tomofilename).Dataset;
        }

        [Benchmark]
        public void RenderFirstFrameOfWSI()
        {
            var filename = Path.Combine(_rootpath, "Data\\multiframe.dcm");
            var image = new DicomImage(filename);
            var rendered = image.RenderImage(0);
        }

        [Benchmark]
        public void RenderFourFramesOfWSI()
        {
            var filename = Path.Combine(_rootpath, "Data\\multiframe.dcm");
            var image = new DicomImage(filename);
            var rendered = image.RenderImage(0);
            rendered = image.RenderImage(5);
            rendered = image.RenderImage(50);
            rendered = image.RenderImage(130);
        }

        [Benchmark]
        public void RenderFirstFrameOfTomo()
        {
            var filename = Path.Combine(_rootpath, "Data\\CT0070.dcm");
            var image = new DicomImage(filename);
            var rendered = image.RenderImage(0);
        }

        [Benchmark]
        public void RenderFourFramesOfTomo()
        {
            var filename = Path.Combine(_rootpath, "Data\\CT0070.dcm");
            var image = new DicomImage(filename);
            var rendered = image.RenderImage(0);
            rendered = image.RenderImage(5);
            rendered = image.RenderImage(25);
            rendered = image.RenderImage(50);
        }


        [Benchmark]
        public async Task RenderFramesOfTomoSimultanousAsync()
        {
            var image = new DicomImage(_loadedTomo);

            await Task.WhenAll(
                Enumerable.Range(0, image.NumberOfFrames)
                .Select(i =>
               {
                   return Task.Run(() => image.RenderImage(i));
               }
                ));
        }


    }
}
