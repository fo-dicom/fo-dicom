using System;

namespace FellowOakDicom.Network
{
    public class NetworkStreamCreationOptions
    {
        public string Host { get; set; }
        public int Port { get; set; }
        public bool UseTls { get; set; }
        public bool NoDelay { get; set; }
        public bool IgnoreSslPolicyErrors { get; set; }
        
        public TimeSpan Timeout { get; set; }
    }
}