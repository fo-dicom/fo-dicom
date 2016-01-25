namespace Wado.Models
{
    /// <summary>
    /// IDicomImageFinderService implementation for demo purposes
    /// </summary>
    public class TestDicomImageFinderService : IDicomImageFinderService
    {

        public string GetImageByInstanceUid(string instanceUid)
        {
            //always returns the same image ;)
            return System.Web.Hosting.HostingEnvironment.MapPath(@"~/TestFiles/1.2.840.113564.192168156.2012101911104793780.1003000225002.dcm");
        }
    }
}