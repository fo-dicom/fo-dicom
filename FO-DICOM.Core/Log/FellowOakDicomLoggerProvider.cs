// Copyright (c) 2012-2023 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).
#nullable disable

using Microsoft.Extensions.Logging;
using System;

namespace FellowOakDicom.Log
{
    [Obsolete("Fellow Oak DICOM now supports Microsoft.Extensions.Logging")]
    internal class FellowOakDicomLoggerProvider: ILoggerProvider
    {
        private readonly ILogManager _logManager;

        public FellowOakDicomLoggerProvider(ILogManager logManager)
        {
            _logManager = logManager ?? throw new ArgumentNullException(nameof(logManager));
        }

        public Microsoft.Extensions.Logging.ILogger CreateLogger(string categoryName)
        {
            var logger = _logManager.GetLogger(categoryName);
            
            return new FellowOakDicomLogger(logger);
        }
        
        public void Dispose() {
            
        }
    }
}