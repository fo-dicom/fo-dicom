// Copyright (c) 2012-2026 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).
#nullable disable

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FellowOakDicom
{
    public class PersonName
    {

        private string _raw;

        public PersonName(string dicomPersonName)
        {

        }

        /// <summary> Family name or empty string. </summary>
        public string Last { get; }

        /// <summary> Given name or empty string. </summary>
        public string First { get; }

        /// <summary> Middle name or empty string. </summary>
        public string Middle { get; }

        /// <summary> Name prefix or empty string. </summary>
        public string Prefix { get; }

        /// <summary> Name suffix or empty string. </summary>
        public string Suffix { get; }

    }
}
