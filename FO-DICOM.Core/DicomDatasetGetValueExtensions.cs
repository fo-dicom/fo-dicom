using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FellowOakDicom
{
    public static class DicomDatasetGetValueExtensions
    {

        public static string GetSingleValue(this DicomDataset dataset, DicomTagAE tag)
        {
            return dataset.GetElem(tag).StringValue;
        }

        public static string[] GetValues(this DicomDataset dataset, DicomTagAEs tag)
        {
            return dataset.GetElem(tag).StringValues;
        }

        public static string GetSingleValue(this DicomDataset dataset, DicomTagAS tag)
        {
            return dataset.GetElem(tag).StringValue;
        }

        public static string[] GetValues(this DicomDataset dataset, DicomTagASs tag)
        {
            return dataset.GetElem(tag).StringValues;
        }

        public static DicomTag GetSingleValue(this DicomDataset dataset, DicomTagAT tag)
        {
            return dataset.GetElem(tag).Values[0];
        }

        public static DicomTag[] GetValues(this DicomDataset dataset, DicomTagATs tag)
        {
            return dataset.GetElem(tag).Values;
        }

        public static string GetSingleValue(this DicomDataset dataset, DicomTagCS tag)
        {
            return dataset.GetElem(tag).StringValue;
        }

        public static string[] GetValues(this DicomDataset dataset, DicomTagCSs tag)
        {
            return dataset.GetElem(tag).StringValues;
        }

        public static DateTime GetSingleValue(this DicomDataset dataset, DicomTagDA tag)
        {
            return dataset.GetElem(tag).DateValues[0];
        }

        public static DateTime[] GetValues(this DicomDataset dataset, DicomTagDAs tag)
        {
            return dataset.GetElem(tag).DateValues;
        }

        public static decimal GetSingleValue(this DicomDataset dataset, DicomTagDS tag)
        {
            return dataset.GetElem(tag).Values[0];
        }

        public static decimal[] GetValues(this DicomDataset dataset, DicomTagDSs tag)
        {
            return dataset.GetElem(tag).Values;
        }

        public static DateTime GetSingleValue(this DicomDataset dataset, DicomTagDT tag)
        {
            return dataset.GetElem(tag).DateValues[0];
        }

        public static DateTime[] GetValues(this DicomDataset dataset, DicomTagDTs tag)
        {
            return dataset.GetElem(tag).DateValues;
        }

        public static double GetSingleValue(this DicomDataset dataset, DicomTagFD tag)
        {
            return dataset.GetElem(tag).Values[0];
        }

        public static double[] GetValues(this DicomDataset dataset, DicomTagFDs tag)
        {
            return dataset.GetElem(tag).Values;
        }

        public static float GetSingleValue(this DicomDataset dataset, DicomTagFL tag)
        {
            return dataset.GetElem(tag).Values[0];
        }

        public static float[] GetValues(this DicomDataset dataset, DicomTagFLs tag)
        {
            return dataset.GetElem(tag).Values;
        }

        public static int GetSingleValue(this DicomDataset dataset, DicomTagIS tag)
        {
            return dataset.GetElem(tag).Values[0];
        }

        public static int[] GetValues(this DicomDataset dataset, DicomTagISs tag)
        {
            return dataset.GetElem(tag).Values;
        }

        public static string GetSingleValue(this DicomDataset dataset, DicomTagLO tag)
        {
            return dataset.GetElem(tag).StringValue;
        }

        public static string[] GetValues(this DicomDataset dataset, DicomTagLOs tag)
        {
            return dataset.GetElem(tag).StringValues;
        }

        public static string GetSingleValue(this DicomDataset dataset, DicomTagLT tag)
        {
            return dataset.GetElem(tag).StringValue;
        }

        public static byte[] GetValues(this DicomDataset dataset, DicomTagOB tag)
        {
            return dataset.GetElem(tag).Values;
        }

        public static double[] GetValues(this DicomDataset dataset, DicomTagOD tag)
        {
            return dataset.GetElem(tag).Values;
        }

        public static float[] GetValues(this DicomDataset dataset, DicomTagOF tag)
        {
            return dataset.GetElem(tag).Values;
        }

        public static uint[] GetValues(this DicomDataset dataset, DicomTagOL tag)
        {
            return dataset.GetElem(tag).Values;
        }

        public static ulong[] GetValues(this DicomDataset dataset, DicomTagOV tag)
        {
            return dataset.GetElem(tag).Values;
        }

        public static ushort[] GetValues(this DicomDataset dataset, DicomTagOW tag)
        {
            return dataset.GetElem(tag).Values;
        }

        //public static DicomPersonName GetSingleValue(this DicomDataset dataset, DicomTagPN tag)
        //{
        //    return dataset.GetElem(tag).StringValue;
        //}

        //public static DicomPersonName[] GetValues(this DicomDataset dataset, DicomTagPNs tag)
        //{
        //    return dataset.GetElem(tag).StringValues;
        //}


        /*

        DicomTagPN
        DicomTagPNs
        DicomTagSH
        DicomTagSHs
        DicomTagSL
        DicomTagSLs
        DicomTagSQ
        DicomTagSQs
        DicomTagSS
        DicomTagSSs
        DicomTagST
        DicomTagSTs
        DicomTagSVs
        DicomTagTM
        DicomTagTMs
        DicomTagUC
        DicomTagUCs

                public static DicomUID GetSingleValue(this DicomDataset dataset, DicomTagUI tag)
                {
                    return dataset.GetElem(tag).Values[0];
                }

                public static DicomUID[] GetValues(this DicomDataset dataset, DicomTagUIs tag)
                {
                    return dataset.GetElem(tag).Values;
                }


        DicomTagUL
        DicomTagULs
        DicomTagUN
        DicomTagUR
        DicomTagURs
        DicomTagUS
        DicomTagUSs
        DicomTagUT
        DicomTagUVs


                */


    }
}
