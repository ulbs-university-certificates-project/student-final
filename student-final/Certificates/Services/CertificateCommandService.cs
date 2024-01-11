using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using student_final.Certificates.Models;
using student_final.System.Constants;
using student_final.Certificates.Services.Interfaces;

namespace student_final.Certificates.Services;

public class CertificateCommandService : ICertificateCommandService
{
    public string CreateCertificateDocument(Certificate certificate)
    {
        string certificateName = CreateUninterpolatedCertificateAfterTemplate(certificate.Nume);
        
        InterpolateTemplate(certificateName, certificate);

        return certificateName;
    }
    
    #region PRIVATE_METHODS
    
    private void InterpolateTemplate(string certificateName, Certificate certificate)
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
        
        WordprocessingDocument document =
            WordprocessingDocument.Open(Constants.CERTIFICATE_OUTPUT_PATH + certificateName, true);

        Body body = document.MainDocumentPart!.Document.Body!;

        foreach (Text text in body.Descendants<Text>())
        {
            foreach (string field in fieldMap.Keys)
            {
                if (text.Text.Contains($"{field}"))
                {
                    text.Text = text.Text.Replace($"{field}", fieldMap.GetValueOrDefault(field));
                }
            }
        }
        
        document.MainDocumentPart.Document.Save();
        document.Dispose();
    }
    
    private string CreateUninterpolatedCertificateAfterTemplate(string studentName)
    {
        string certificateName = GenerateCertificateName(studentName);
        
        File.Copy(Constants.CERTIFICATE_TEMPLATE, Constants.CERTIFICATE_OUTPUT_PATH + certificateName);

        return certificateName;
    }

    private string GenerateCertificateName(string studentName)
    {
        string certificateName = $"Certificate - {studentName}.docx";
        
        if (!File.Exists(Constants.CERTIFICATE_OUTPUT_PATH + certificateName)) 
            return certificateName;

        int index = 1;
        do
        {
            certificateName = $"Certificate - {studentName} ({index}).docx";
            index++;
        } while (File.Exists(Constants.CERTIFICATE_OUTPUT_PATH + certificateName));
        
        return certificateName;
    }
    
    #endregion
}