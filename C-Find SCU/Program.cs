using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dicom.Network;
using System.Threading;

namespace C_Find_SCU
{
    class Program
    {
        static void Main(string[] args)
        {
            MultiClient2OneServer(1);
            //OneClient2MultiServers(new int[] { 104, 1110, 1111 });
            Console.ReadKey();
        }
        static void OneClient2MultiServers(int[] ports)
        {
            DicomClient client = new DicomClient();
            foreach (var port in ports)
            {

            }

        }
        static void MultiClient2OneServer(int count)
        {
            for (int i = 0; i <count; ++i)
                ThreadPool.QueueUserWorkItem(ThreadProcess, i);
            Thread.Sleep(0);
        }
        static void ThreadProcess(object state)
        {
            string clientID = state.ToString();
            DicomCFindRequest cfindReq = DicomCFindRequest.CreatePatientQuery("1111", "test");
            cfindReq.OnResponseReceived += (req, rsp) =>
            {
                System.Console.WriteLine(rsp.ToString()+"ClientID="+clientID);
            };
            DicomClient client = new DicomClient();
            client.AddRequest(cfindReq);
            client.Send("127.0.0.1", 104, false, "FINDSCU", "FINDSCP");

        }
    }
}
