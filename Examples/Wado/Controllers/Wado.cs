using System;
using System.Collections;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Cors;
using Dicom;
using Dicom.Imaging;
using Dicom.Imaging.Codec;
using Dicom.IO;
using Wado.Models;

namespace Wado.Controllers
{
    /// <summary>
    /// main controller for wado implementation
    /// </summary>
    /// <remarks>the current implementation is incomplete</remarks>
    /// <remarks>more infos in the official specification : ftp://medical.nema.org/medical/dicom/2009/09_18pu.pdf </remarks>
    [EnableCors(origins: "*", headers: "*", methods: "GET")] //allows access by any host
    [RoutePrefix("wado")]
    public class WadoUriController : ApiController
    {
        #region consts

        /// <summary>
        /// string representation of the dicom content type
        /// </summary>
        private const string AppDicomContentType = "application/dicom";

        /// <summary>
        /// string representation of the jpeg content type
        /// </summary>
        private const string JpegImageContentType = "image/jpeg";

        #endregion

        #region fields

        /// <summary>
        /// service used to retrieve images by instance Uid
        /// </summary>
        private readonly IDicomImageFinderService _dicomImageFinderService;

        #endregion

        #region constructors

        /// <summary>
        /// Initialize a new instance of WadoUriController
        /// </summary>
        public WadoUriController()
        {
            //Put your own IDicomImageFinderService implementation here for real world scenarios
            _dicomImageFinderService = new TestDicomImageFinderService();
        }

        /// <summary>
        /// Testing purposes constructor if you want to inject a custom IDicomImageFinderService
        /// in unit tests
        /// </summary>
        /// <param name="dicomImageFinderService"></param>
        public WadoUriController(IDicomImageFinderService dicomImageFinderService)
        {
            _dicomImageFinderService = dicomImageFinderService;
        }

        #endregion

        #region methods

        /// <summary>
        /// main wado method
        /// </summary>
        /// <param name="requestMessage">web request</param>
        /// <param name="requestType">always equals to wado in current wado specification, may change in the future</param>
        /// <param name="studyUID">study instance UID</param>
        /// <param name="seriesUID">serie instance UID</param>
        /// <param name="objectUID">instance UID</param>
        /// <param name="contentType">The value shall be a list of MIME types, separated by a "," character, and potentially associated with relative degree of preference, as specified in IETF RFC2616. </param>
        /// <param name="charset">character set of the object to be retrieved.</param>
        /// <param name="transferSyntax">The Transfer Syntax to be used within the DICOM image object, as specified in PS 3.6</param>
        /// <param name="anonymize">if value is "yes", indicates that we should anonymize object.
        /// The Server may return an error if it either cannot or refuses to anonymize that object</param>
        /// <returns></returns>
        [Route("")]
        public async Task<HttpResponseMessage> GetStudyInstances(HttpRequestMessage requestMessage, string requestType,
            string studyUID, string seriesUID, string objectUID, string contentType = null, string charset = null,
            string transferSyntax = null, string anonymize = null)
        {

            //we do not handle anonymization
            if (anonymize == "yes")
                return requestMessage.CreateErrorResponse(HttpStatusCode.NotAcceptable,
                    String.Format("anonymise is not supported on the server", contentType));

            //we extract the content types from contentType value
            string[] contentTypes;
            bool canParseContentTypeParameter = ExtractContentTypesFromContentTypeParameter(contentType,
                out contentTypes);

            if (!canParseContentTypeParameter)
                return requestMessage.CreateErrorResponse(HttpStatusCode.NotAcceptable,
                    String.Format("contentType parameter (value: {0}) cannot be parsed", contentType));


            //8.1.5 The Web Client shall provide list of content types it supports in the "Accept" field of the GET method. The
            //value of the contentType parameter of the request shall be one of the values specified in that field. 
            string[] acceptContentTypesHeader =
                requestMessage.Headers.Accept.Select(header => header.MediaType).ToArray();

            // */* means that we accept everything for the content Header
            bool acceptAllTypesInAcceptHeader = acceptContentTypesHeader.Contains("*/*");
            bool isRequestedContentTypeCompatibleWithAcceptContentHeader = acceptAllTypesInAcceptHeader ||
                                                                           contentTypes == null ||
                                                                           acceptContentTypesHeader.Intersect(
                                                                               contentTypes).Any();

            if (!isRequestedContentTypeCompatibleWithAcceptContentHeader)
            {
                return requestMessage.CreateErrorResponse(HttpStatusCode.NotAcceptable,
                    String.Format("content type {0} is not compatible with types specified in  Accept Header",
                        contentType));
            }

            //6.3.2.1 The MIME type shall be one on the MIME types defined in the contentType parameter, preferably the most
            //desired by the Web Client, and shall be in any case compatible with the ‘Accept’ field of the GET method.
            //Note: The HTTP behavior is that an error (406 – Not Acceptable) is returned if the required content type cannot
            //be served. 
            string[] compatibleContentTypesByOrderOfPreference =
                GetCompatibleContentTypesByOrderOfPreference(contentTypes,
                    acceptContentTypesHeader);

            //if there is no type that can be handled by our server, we return an error
            if (compatibleContentTypesByOrderOfPreference != null
                && !compatibleContentTypesByOrderOfPreference.Contains(JpegImageContentType)
                && !compatibleContentTypesByOrderOfPreference.Contains(AppDicomContentType))
            {
                return requestMessage.CreateErrorResponse(HttpStatusCode.NotAcceptable,
                    String.Format("content type(s) {0} cannot be served",
                        String.Join(" - ", compatibleContentTypesByOrderOfPreference)
                        ));
            }

            //we now need to handle the case where contentType is not specified, but in this case, the default value
            //depends on the image, so we need to open it

            string dicomImagePath = _dicomImageFinderService.GetImageByInstanceUid(objectUID);

            if (dicomImagePath == null)
            {
                return requestMessage.CreateErrorResponse(HttpStatusCode.NotFound, "no image found");
            }

            try
            {
                IOManager.SetImplementation(DesktopIOManager.Instance);

                DicomFile dicomFile = await DicomFile.OpenAsync(dicomImagePath);

                string finalContentType = PickFinalContentType(compatibleContentTypesByOrderOfPreference, dicomFile);

                return ReturnImageAsHttpResponse(dicomFile, 
                    finalContentType, transferSyntax);
            }
            catch (Exception ex)
            {
                Trace.TraceError("exception when sending image: " + ex.ToString());

                return requestMessage.CreateErrorResponse(HttpStatusCode.InternalServerError, "server internal error");
            }
        }


        /// <summary>
        /// returns dicomFile in the content type given by finalContentType in a HttpResponseMessage.
        /// If content type is dicom, transfer syntax must be set to the given transferSyntax parameter.
        /// </summary>
        /// <param name="dicomFile"></param>
        /// <param name="finalContentType"></param>
        /// <param name="transferSyntax"></param>
        /// <returns></returns>
        private HttpResponseMessage ReturnImageAsHttpResponse(DicomFile dicomFile, string finalContentType, string transferSyntax)
        {
            MediaTypeHeaderValue header = null;
            Stream streamContent = null;

            if (finalContentType == JpegImageContentType)
            {
                DicomImage image = new DicomImage(dicomFile.Dataset);
                Bitmap bmp = image.RenderImage(0).As<Bitmap>();

                //When an image/jpeg MIME type is returned, the image shall be encoded using the JPEG baseline lossy 8
                //bit Huffman encoded non-hierarchical non-sequential process ISO/IEC 10918. 
                //TODO Is it the case with default Jpeg format from Bitmap?
                header = new MediaTypeHeaderValue(JpegImageContentType);
                streamContent = new MemoryStream();
                bmp.Save(streamContent, ImageFormat.Jpeg);
                streamContent.Seek(0, SeekOrigin.Begin);
            }
            else if (finalContentType == AppDicomContentType)
            {
                //By default, the transfer syntax shall be
                //"Explicit VR Little Endian".
                //Note: This implies that retrieved images are sent un-compressed by default.
                DicomTransferSyntax requestedTransferSyntax = DicomTransferSyntax.ExplicitVRLittleEndian;

                if (transferSyntax != null)
                    requestedTransferSyntax = GetTransferSyntaxFromString(transferSyntax);

                bool transferSyntaxIsTheSameAsSourceFile =
                    dicomFile.FileMetaInfo.TransferSyntax == requestedTransferSyntax;

                //we only change transfer syntax if we need to
                DicomFile dicomFileToStream;
                if (!transferSyntaxIsTheSameAsSourceFile)
                {
                    dicomFileToStream = dicomFile.ChangeTransferSyntax(requestedTransferSyntax);
                }
                else
                {
                    dicomFileToStream = dicomFile;
                }


                header = new MediaTypeHeaderValue(AppDicomContentType);
                streamContent = new MemoryStream();
                dicomFileToStream.Save(streamContent);
                streamContent.Seek(0, SeekOrigin.Begin);
            }


            HttpResponseMessage result = new HttpResponseMessage(HttpStatusCode.OK);
            result.Content = new StreamContent(streamContent);
            result.Content.Headers.ContentType = header;
            return result;
        }

        /// <summary>
        /// Choose the final content type given compatibleContentTypesByOrderOfPreference and dicomFile
        /// </summary>
        /// <param name="compatibleContentTypesByOrderOfPreference"></param>
        /// <param name="dicomFile"></param>
        /// <returns></returns>
        private static string PickFinalContentType(string[] compatibleContentTypesByOrderOfPreference, DicomFile dicomFile)
        {
            string chosenContentType = null;
            int nbFrames = dicomFile.Dataset.Get<int>(DicomTag.NumberOfFrames);

            //if compatibleContentTypesByOrderOfPreference is null,
            //it means we must choose a default content type based on image content:
            //  *Single Frame Image Objects
            //      If the contentType parameter is not present in the request, the response shall contain an image/jpeg MIME
            //      type, if compatible with the ‘Accept’ field of the GET method. 
            //  *Multi Frame Image Objects
            //      If the contentType parameter is not present in the request, the response shall contain a application/dicom
            //      MIME type. 

            //not sure if this is how we distinguish multi frame objects?
            bool isMultiFrame = nbFrames > 1;
            bool chooseDefaultValue = compatibleContentTypesByOrderOfPreference == null;
            if (chooseDefaultValue)
            {
                if (isMultiFrame)
                {
                    chosenContentType = AppDicomContentType;
                }
                else
                {
                    chosenContentType = JpegImageContentType;
                }
            }
            else
            {
                //we need to take the compatible one
                chosenContentType = compatibleContentTypesByOrderOfPreference
                    .Intersect(new[] {AppDicomContentType, JpegImageContentType})
                    .First();
            }
            return chosenContentType;
        }

        /// <summary>
        /// extract content type values (may have multiple values according to IETF RFC2616)
        /// </summary>
        /// <param name="contentType">contentype string from wado request</param>
        /// <param name="contentTypes">extracted content types</param>
        /// <returns>false if there is a parse error, else true</returns>
        private static bool ExtractContentTypesFromContentTypeParameter(string contentType, out string[] contentTypes)
        {
            //8.1.5 MIME type of the response 
            //The value shall be a list of MIME types, separated by a "," character, and potentially associated with
            //relative degree of preference, as specified in IETF RFC2616. 
            //so we must split the string

            contentTypes = null;
            if (contentType != null && contentType.Contains(","))
            {
                contentTypes = contentType.Split(',');
            }
            else if (contentType == null)
            {
                contentTypes = null;
            }
            else
            {
                contentTypes = new[] {contentType};
            }

            //we now need to parse each type which follows the RFC2616 syntax
            //it also extracts parameters like jpeg quality but we discard it because we don't need them for now
            try
            {
                if (contentType != null)
                {
                    contentTypes =
                        contentTypes.Select(contentTypeString => MediaTypeHeaderValue.Parse(contentTypeString))
                            .Select(mediaTypeHeader => mediaTypeHeader.MediaType).ToArray();
                }
            }
            catch (FormatException)
            {
                {
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// Get the compatible content types from the Accept Header, by order of preference
        /// </summary>
        /// <param name="contentTypes"></param>
        /// <param name="acceptContentTypesHeader"></param>
        /// <returns>
        /// compatible types by order of preference
        /// if contentTypes==null, returns null
        /// </returns>
        private static string[] GetCompatibleContentTypesByOrderOfPreference(
            string[] contentTypes, string[] acceptContentTypesHeader)
        {
            //je vérifie tout d'abord que parmis les types demandés, il y en a bien un que je gère.
            //je dois prendre l'intersection des types demandés et acceptés et les trier par ordre de préférence

            bool acceptAllTypesInAcceptHeader = acceptContentTypesHeader.Contains("*/*");

            string[] compatibleContentTypesByOrderOfPreference = null;
            if (acceptAllTypesInAcceptHeader)
            {
                compatibleContentTypesByOrderOfPreference = contentTypes;
            }
            //null represent the default value
            else if (contentTypes == null)
            {
                compatibleContentTypesByOrderOfPreference = null;
            }
            else
            {
                //intersect should preserve order (so it's already sorted by order of preference)
                compatibleContentTypesByOrderOfPreference = acceptContentTypesHeader.Intersect(contentTypes).ToArray();
            }
            return compatibleContentTypesByOrderOfPreference;
        }

        /// <summary>
        /// Converts string dicom transfert syntax to DicomTransferSyntax enumeration
        /// </summary>
        /// <param name="transferSyntax"></param>
        /// <returns></returns>
        private DicomTransferSyntax GetTransferSyntaxFromString(string transferSyntax)
        {
            try
            {
                return DicomParseable.Parse<DicomTransferSyntax>(transferSyntax);
            }
            catch (Exception)
            {
                //if we have an error, this probably means syntax is not supported
                //so according to 8.2.11 in spec, we use default ExplicitVRLittleEndian
                return DicomTransferSyntax.ExplicitVRLittleEndian;
            }
        }

        #endregion
    }
}
