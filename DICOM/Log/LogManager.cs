// Copyright (c) 2012-2015 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

namespace Dicom.Log
{
    public abstract class LogManager
    {
        static LogManager()
        {
            Default = new ConsoleLogManager();
        }

        public static LogManager Default { get; set; }

        public abstract Logger GetLogger(string name);
    }
}
