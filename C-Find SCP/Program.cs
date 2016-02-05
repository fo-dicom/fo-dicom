using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dicom.Network;

namespace C_Find_SCP
{
    class Program
    {
        static void Main(string[] args)
        {
            DicomServer<FindSCP> cfindServer = new DicomServer<FindSCP>(104);
            DicomServer<FindSCP> cfindServer2 = new DicomServer<FindSCP>(1110);
            DicomServer<FindSCP> cfindServer3 = new DicomServer<FindSCP>(1111);
            Console.WriteLine("Press <return> to end...");
            Console.ReadKey();
        }
    }
}
