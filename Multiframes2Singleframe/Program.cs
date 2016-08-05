using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Multiframes2Singleframe
{
    class Program
    {
        static void Main(string[] args)
        {
            string srcFile = @"multi-frame.dcm";
            var singleFrames = Multiframes2SingleframeHelper.ExtractMultiframes2Singleframe(srcFile);
            foreach (var frame in singleFrames)
            {
                Console.WriteLine(string.Format("{0}文件的单帧图像{1}", srcFile, frame));
            }
            Console.ReadKey();
        }
    }
}
