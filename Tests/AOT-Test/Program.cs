using System;
using FellowOakDicom;

class Program
{
    static void Main()
    {
        var dataset = new DicomDataset
        {
            { DicomTag.PatientName, "Test^Patient" },
            { DicomTag.PatientID, "12345" },
            { DicomTag.SOPInstanceUID, DicomUID.Generate() }
        };

        var file = new DicomFile(dataset);
        Console.WriteLine($"Created DICOM file with Patient: {dataset.GetString(DicomTag.PatientName)}");
    }
}
