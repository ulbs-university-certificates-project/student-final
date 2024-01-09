using System.Globalization;
using OfficeOpenXml;
using student_final.Students.Models;
using student_final.System.Constants;

namespace student_final.Registers.Models;

public class Register
{
    private ExcelPackage _package;
    private ExcelWorksheet _worksheet;
    private int _lastId;

    public Register()
    {
        _package = new ExcelPackage(new FileInfo(Constants.EXCEL_REGISTER_PATH));
        _worksheet = _package.Workbook.Worksheets[0];
        GetLastId();
    }

    public IEnumerable<CertificateObject> GetCertificates()
    {
        List<CertificateObject> certificateObjects = new List<CertificateObject>();

        int rowCount = _worksheet.Dimension.Rows;
        
        for (int i = 2; i <= rowCount; i++)
        {
            CertificateObject certificateObject = new CertificateObject
            {
                NrAdeverinta = Convert.ToInt32(_worksheet.Cells[i, 1].Value),
                Data = DateTime.ParseExact(_worksheet.Cells[i, 2].Value.ToString()!, Constants.DATE_FORMAT, CultureInfo.InvariantCulture),
                Nume = _worksheet.Cells[i, 3].Value.ToString()!,
                An = Convert.ToInt32(_worksheet.Cells[i, 4].Value),
                Sectie = _worksheet.Cells[i, 5].Value.ToString()!,
                Motiv = _worksheet.Cells[i, 6].Value.ToString()!
            };  
            
            certificateObjects.Add(certificateObject);
        }

        return certificateObjects;
    }
    
    public CertificateObject GetCertificateByNrAdeverinta(int nrAdeverinta)
    {
        List<CertificateObject> certificates = GetCertificates().ToList();

        return certificates.FirstOrDefault(certificate => certificate.NrAdeverinta == nrAdeverinta)!;
    }

    public CertificateObject CreateCertificate(Student student, string motiv)
    {
        _lastId++;
        CertificateObject certificateObject = new CertificateObject
        {
            NrAdeverinta = _lastId,
            Data = DateTime.Now,
            Nume = student.Nume,
            An = student.An,
            Sectie = student.Sectie,
            Motiv = motiv
        };
        
        InsertRow(certificateObject);
        return certificateObject;
    }
    
    #region PRIVATE_METHODS
    
    private void InsertRow(CertificateObject certificate)
    {
        int newRow = _worksheet.Dimension.Rows + 1;

        _worksheet.Cells[newRow, 1].Value = certificate.NrAdeverinta;
        _worksheet.Cells[newRow, 2].Value = certificate.Data.ToString(Constants.DATE_FORMAT);
        _worksheet.Cells[newRow, 3].Value = certificate.Nume;
        _worksheet.Cells[newRow, 4].Value = certificate.An;
        _worksheet.Cells[newRow, 5].Value = certificate.Sectie;
        _worksheet.Cells[newRow, 6].Value = certificate.Motiv;
        
        _package.Save();
    }
    
    private void GetLastId()
    {
        _lastId = Convert.ToInt32(_worksheet.Cells[_worksheet.Dimension.Rows, 1].Value);
    }
    
    #endregion
}