// Copyright (c) 2012-2023 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).
#nullable disable

using System;
using System.Collections.Generic;

namespace FellowOakDicom
{

    public class DicomVM
    {
        private static IDictionary<string, DicomVM> _vm = new Dictionary<string, DicomVM>();

        public static readonly DicomVM VM_1 = DicomVM.Parse("1");

        public static readonly DicomVM VM_1_2 = DicomVM.Parse("1-2");

        public static readonly DicomVM VM_1_3 = DicomVM.Parse("1-3");

        public static readonly DicomVM VM_1_8 = DicomVM.Parse("1-8");

        public static readonly DicomVM VM_1_32 = DicomVM.Parse("1-32");

        public static readonly DicomVM VM_1_99 = DicomVM.Parse("1-99");

        public static readonly DicomVM VM_1_n = DicomVM.Parse("1-n");

        public static readonly DicomVM VM_2 = DicomVM.Parse("2");

        public static readonly DicomVM VM_2_n = DicomVM.Parse("2-n");

        public static readonly DicomVM VM_2_2n = DicomVM.Parse("2-2n");

        public static readonly DicomVM VM_3 = DicomVM.Parse("3");

        public static readonly DicomVM VM_3_n = DicomVM.Parse("3-n");

        public static readonly DicomVM VM_3_3n = DicomVM.Parse("3-3n");

        public static readonly DicomVM VM_4 = DicomVM.Parse("4");

        public static readonly DicomVM VM_6 = DicomVM.Parse("6");

        public static readonly DicomVM VM_16 = DicomVM.Parse("16");

        public DicomVM(int minimum, int maximum, int multiplicity)
        {
            Minimum = minimum;
            Maximum = maximum;
            Multiplicity = multiplicity;
        }

        private DicomVM()
        {
            Minimum = 1;
            Maximum = 1;
            Multiplicity = 1;
        }

        public int Minimum { get; private set; }

        public int Maximum { get; private set; }

        public int Multiplicity { get; private set; }

        public override string ToString()
        {
            if (Minimum == Maximum) return Minimum.ToString();

            if (Maximum == int.MaxValue)
            {
                if (Multiplicity > 1) return string.Format(@"{0}-{1}n", Minimum, Multiplicity);
                else return string.Format(@"{0}-n", Minimum);
            }

            return string.Format(@"{0}-{1}", Minimum, Maximum);
        }

        public static DicomVM Parse(string s)
        {
            try
            {
                DicomVM vm = null;
                if (_vm.TryGetValue(s, out vm)) return vm;

                string[] parts = s.Split(new[] { "-", " ", "or" }, StringSplitOptions.RemoveEmptyEntries);

                if (parts.Length == 1)
                {
                    vm = new DicomVM();
                    vm.Minimum = int.Parse(parts[0]);
                    vm.Maximum = vm.Minimum;
                    vm.Multiplicity = vm.Minimum;
                }
                else
                {
                    vm = new DicomVM
                    {
                        Minimum = int.Parse(parts[0]),
                        Multiplicity = 1
                    };

                    if (parts[1].IndexOf("n", StringComparison.OrdinalIgnoreCase) >= 0)
                    {
                        vm.Maximum = int.MaxValue;
                        parts[1] = parts[1].TrimEnd('n', 'N');

                        if (parts[1].Length > 0) vm.Multiplicity = int.Parse(parts[1]);
                    }
                    else
                    {
                        vm.Maximum = int.Parse(parts[1]);
                    }
                }

                _vm.Add(s, vm);

                return vm;
            }
            catch (Exception e)
            {
                throw new DicomDataException("Error parsing value multiplicty ['" + s + "']", e);
            }
        }
    }
}
