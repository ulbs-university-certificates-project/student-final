using student_final.Registers.Models;

namespace student_final.QR.Services.Interfaces;

public interface IQRCommandService
{
    string GenerateAndSaveQRCode(Certificate certificate);

    void DeleteQRCode(string qrName);
}