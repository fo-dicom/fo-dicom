namespace DICOM__Unit_Tests_
{
  using System;
  using System.Collections.ObjectModel;
  using System.Linq;
  using System.Reflection;
  using System.Text;

  using Dicom;
  using Dicom.IO.Buffer;

  using Xunit;

  public class DicomVRTest
  {
    private static string[] AllVRs = new[] { "AE", "AS", "AT", "CS", "DA", "DS", "DT", "FD", "FL", "IS", "LO", "LT", "OB", "OD", "OF", "OW", "PN", "SH", "SL", "SQ", "SS", "ST", "TM", "UC", "UI", "UL", "UN", "UR", "US", "UT" };
    
    [Fact]
    public void All_Defined_VRs_Can_Roundtrip()
    {
      foreach (var vr in
        typeof(DicomVR).GetProperties(BindingFlags.Static | BindingFlags.Public)
          .Where(pi => pi.PropertyType == typeof(DicomVR))
          .Select(pi => (DicomVR)pi.GetValue(pi, new object[0])))
      {
        Assert.True(DicomVR.Parse(vr.Code) == vr);
      }
    }

    [Fact]
    public void Unknown_VRs_are_unknown()
    {
      Assert.Throws<DicomDataException>(() => DicomVR.Parse("XX"));
    }

    [Fact]
    public void All_Known_VRs_Exist()
    {
      foreach (var vr in AllVRs)
      {
        Assert.True(DicomVR.Parse(vr).Code == vr);
        Assert.True(DicomVR.Parse(vr).ToString() == vr);
        DicomVR parsedVr;
        Assert.True(DicomVR.TryParse(vr, out parsedVr));
        Assert.True(parsedVr.Code == vr);
        Assert.False(DicomVR.TryParse(vr + "X", out parsedVr));
        Assert.False(DicomVR.TryParse(vr + " ", out parsedVr));
        Assert.False(DicomVR.TryParse(" " + vr, out parsedVr));
      }

      // "NONE" is a special case, so let's make it so here as well.
      Assert.True(DicomVR.Parse("NONE") == DicomVR.NONE);
    }

    [Fact]
    public void All_VRs_should_have_one_and_only_one_matching_DicomElement_subclasses()
    {
      var codes = new Collection<string>();
      var types =
        Assembly.GetAssembly(typeof(DicomElement))
          .GetTypes()
          .Where(t => (t.IsSubclassOf(typeof(DicomElement)) || t == typeof(DicomSequence)) && !t.IsAbstract);

      var mbb = new MemoryByteBuffer(new byte[0]);

      foreach (var type in types)
      {
        DicomItem item;
        var ctor = type.GetConstructor(new[] { typeof(DicomTag), typeof(IByteBuffer) });
        if (ctor != null)
        {
          item = (DicomItem)ctor.Invoke(new object[] { new DicomTag(0, 0), mbb });
        }
        else
        {
          ctor = type.GetConstructor(new[] { typeof(DicomTag), typeof(Encoding), typeof(IByteBuffer) });
          if (ctor != null)
          {
            item = (DicomItem)ctor.Invoke(new object[] { new DicomTag(0, 0), Encoding.ASCII, mbb });
          }
          else
          {
            ctor = type.GetConstructor(new[] { typeof(DicomTag), typeof(DicomDataset[]) });

            Assert.NotNull(ctor);

            item = (DicomItem)ctor.Invoke(new object[] { new DicomTag(0, 0), new DicomDataset[0] });
          }
        }

        Assert.DoesNotContain(item.ValueRepresentation.Code, codes);
        codes.Add(item.ValueRepresentation.Code);
      }

      foreach (var vr in AllVRs)
      {
        Assert.Contains(vr, codes);
      }
    }
  }
}
