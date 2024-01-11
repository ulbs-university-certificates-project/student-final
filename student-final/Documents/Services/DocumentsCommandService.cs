using student_final.Certificates.Models;
using student_final.Documents.Services.Interfaces;
using student_final.QR.Services.Interfaces;
using student_final.System.Constants;
using Xceed.Document.NET;
using Xceed.Words.NET;

namespace student_final.Documents.Services;

public class DocumentsCommandService : IDocumentsCommandService
{
    private IQRCommandService _qrCommandService;

    public DocumentsCommandService(IQRCommandService qrCommandService)
    {
        _qrCommandService = qrCommandService;
    }
    
    public string CreateCertificateDocument(Certificate certificate)
    {
        string certificateName = CreateUninterpolatedCertificateAfterTemplate(certificate.Nume);
        string qrName = _qrCommandService.GenerateAndSaveQRCode(certificate);
        
        InterpolateTemplate(certificateName, qrName, certificate);
        _qrCommandService.DeleteQRCode(qrName);

        return certificateName;
    }
    
    public void DeleteCertificateDocument(string certificateName)
    {
        File.Delete(Constants.DOCUMENT_OUTPUT_PATH + certificateName);
    }
    
    #region PRIVATE_METHODS
    
    private void InterpolateTemplate(string certificateName, string qrName, Certificate certificate)
    {
        Dictionary<string, string> fieldMap = new Dictionary<string, string>
        {
            { "NRADEVERINTA", $"{certificate.NrAdeverinta}" },
            { "DATA", $"{certificate.Data.ToString(Constants.DATE_FORMAT_DOT)}" },
            { "NUME", $"{certificate.Nume}" },
            { "SECTIE", $"{certificate.Sectie}" },
            { "AN", $"{certificate.An}" },
            { "MOTIV", $"{certificate.Motiv}" }
        };

        using (DocX document = DocX.Load(Constants.DOCUMENT_OUTPUT_PATH + certificateName))
        {
            // Replacing fields (nr. adeverinta, nume, etc.)
            foreach (Paragraph paragraph in document.Paragraphs)
            {
                foreach (string field in fieldMap.Keys)
                {
                    StringReplaceTextOptions options = new StringReplaceTextOptions
                    {
                        SearchValue = $"[{field}]",
                        NewValue = fieldMap.GetValueOrDefault(field)
                    };
                    
                    // Using StringReplaceTextOptions because ReplaceText() with multiple parameters is obsolete.
                    paragraph.ReplaceText(options);
                }
            }
        
            // Inserting QR Code
            var qrParagraph = document.Paragraphs.FirstOrDefault(p => p.Text.Contains("[QR]"))!;
        
            qrParagraph.RemoveText(0); // 0 = Starting index (Removes (text length - index) characters)
        
            Image image = document.AddImage(Constants.QR_OUTPUT_PATH + qrName);
            Picture picture = image.CreatePicture(100, 100);
        
            qrParagraph.InsertPicture(picture);
        
            // Saving and closing document
            document.SaveAs(Constants.DOCUMENT_OUTPUT_PATH + certificateName);
        }
    }
    
    private string CreateUninterpolatedCertificateAfterTemplate(string studentName)
    {
        string certificateName = GenerateDocumentName(studentName);
        
        File.Copy(Constants.DOCUMENT_TEMPLATE, Constants.DOCUMENT_OUTPUT_PATH + certificateName);

        return certificateName;
    }

    private string GenerateDocumentName(string studentName)
    {
        string certificateName = $"Certificate - {studentName}.docx";
        
        if (!File.Exists(Constants.DOCUMENT_OUTPUT_PATH + certificateName)) 
            return certificateName;

        int index = 1;
        do
        {
            certificateName = $"Certificate - {studentName} ({index}).docx";
            index++;
        } while (File.Exists(Constants.DOCUMENT_OUTPUT_PATH + certificateName));
        
        return certificateName;
    }
    
    #endregion
}