// Copyright (c) 2012-2026 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).
#nullable disable

using BenchmarkDotNet.Attributes;

namespace FellowOakDicom.Benchmark
{
    [MemoryDiagnoser]
    public class DicomValidationBenchmark
    {
        private const string AE = "MY_AE_TITLE";
        private const string AS = "030Y";
        private const string CS = "ORIGINAL";
        private const string DA = "20260513";
        private const string DS = "1.234567";
        private const string DT = "20260513120000.000000+0100";
        private const string IS = "12345";
        private const string LO = "Sample long string value";
        private const string LT = "Sample long text value with multiple words.";
        private const string PN = "Doe^John^^Mr^Jr";
        private const string SH = "Short";
        private const string ST = "Sample short text.";
        private const string TM = "120000.000000";
        private const string UI = "1.2.840.10008.5.1.4.1.1.2";
        private const string TZ = "+0100";

        [Benchmark]
        public void ValidateAE() => DicomValidation.ValidateAE(AE);

        [Benchmark]
        public void ValidateAS() => DicomValidation.ValidateAS(AS);

        [Benchmark]
        public void ValidateCS() => DicomValidation.ValidateCS(CS);

        [Benchmark]
        public void ValidateDA() => DicomValidation.ValidateDA(DA);

        [Benchmark]
        public void ValidateDS() => DicomValidation.ValidateDS(DS);

        [Benchmark]
        public void ValidateDT() => DicomValidation.ValidateDT(DT);

        [Benchmark]
        public void ValidateIS() => DicomValidation.ValidateIS(IS);

        [Benchmark]
        public void ValidateLO() => DicomValidation.ValidateLO(LO);

        [Benchmark]
        public void ValidateLT() => DicomValidation.ValidateLT(LT);

        [Benchmark]
        public void ValidatePN() => DicomValidation.ValidatePN(PN);

        [Benchmark]
        public void ValidateSH() => DicomValidation.ValidateSH(SH);

        [Benchmark]
        public void ValidateST() => DicomValidation.ValidateST(ST);

        [Benchmark]
        public void ValidateTM() => DicomValidation.ValidateTM(TM);

        [Benchmark]
        public void ValidateUI() => DicomValidation.ValidateUI(UI);

        [Benchmark]
        public void ValidateTimezoneOffset() => DicomValidation.ValidateTimezoneOffset(TZ);

        [Benchmark]
        public void ValidateAllOnce()
        {
            DicomValidation.ValidateAE(AE);
            DicomValidation.ValidateAS(AS);
            DicomValidation.ValidateCS(CS);
            DicomValidation.ValidateDA(DA);
            DicomValidation.ValidateDS(DS);
            DicomValidation.ValidateDT(DT);
            DicomValidation.ValidateIS(IS);
            DicomValidation.ValidateLO(LO);
            DicomValidation.ValidateLT(LT);
            DicomValidation.ValidatePN(PN);
            DicomValidation.ValidateSH(SH);
            DicomValidation.ValidateST(ST);
            DicomValidation.ValidateTM(TM);
            DicomValidation.ValidateUI(UI);
            DicomValidation.ValidateTimezoneOffset(TZ);
        }
    }
}
