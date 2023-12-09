// Copyright (c) 2012-2023 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).
#nullable disable

using System;
using System.Threading.Tasks;

namespace FellowOakDicom.Network
{
    public sealed class DicomServerRegistration : IDisposable
    {
        private IDicomServerRegistry Registry { get; }
        
        public IDicomServer DicomServer { get; }

        public Task Task { get; }

        public DicomServerRegistration(IDicomServerRegistry registry, IDicomServer dicomServer, Task task)
        {
            Registry = registry ?? throw new ArgumentNullException(nameof(registry));
            DicomServer = dicomServer ?? throw new ArgumentNullException(nameof(dicomServer));
            Task = task ?? throw new ArgumentNullException(nameof(task));
        }

        public void Dispose() => Registry.Unregister(this);
    }
}