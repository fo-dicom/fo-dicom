using FellowOakDicom.IO.Buffer;
using System;
using System.Collections.Generic;

namespace FellowOakDicom
{
    public class DicomDataset2: DicomDataset, IDisposable
    {
        public void Dispose()
        {
            var stack = new Stack<DicomDataset>();

            stack.Push(this);

            while (stack.Count > 0)
            {
                var dataset = stack.Pop();
                
                foreach (var item in dataset)
                {
                    switch (item)
                    {
                        case DicomElement { Buffer: RentedMemoryByteBuffer rentedMemoryByteBuffer }:
                            rentedMemoryByteBuffer.Dispose();
                            break;
                        case DicomSequence sequence:
                            {
                                foreach (var sequenceItem in sequence.Items)
                                {
                                    stack.Push(sequenceItem);
                                }

                                break;
                            }
                    }
                }    
            }
        }
    }
}