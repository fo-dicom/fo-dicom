using System;
namespace Unity.IO.Compression {

    internal interface IDeflater : IDisposable {
        bool NeedsInput();
        void SetInput(byte[] inputBuffer, int startIndex, int count);
        int GetDeflateOutput(byte[] outputBuffer);
        bool Finish(byte[] outputBuffer, out int bytesRead);
    }
}
