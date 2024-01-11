using System.Drawing;
using System.Linq.Expressions;
using AutoMapper;
using Newtonsoft.Json;
using student_final.Certificates.Models;
using student_final.QR.Services.Interfaces;
using student_final.Registers.Models;
using student_final.System.Constants;
using ZXing;
using ZXing.QrCode;
using ZXing.Windows.Compatibility;

namespace student_final.QR.Services;

public class QRCommandService : IQRCommandService
{
    private BarcodeWriter<Bitmap> _writer;
    
    public QRCommandService()
    {
        QrCodeEncodingOptions options = new QrCodeEncodingOptions
        {
            Width = 300,
            Height = 300,
            Margin = 0
        };

        _writer = new BarcodeWriter
        {
            Format = BarcodeFormat.QR_CODE,
            Renderer = new BitmapRenderer(),
            Options = options
        };
    }

    public string GenerateAndSaveQRCode(Certificate certificate)
    {
        string jsonString = JsonConvert.SerializeObject(certificate);
        
        Bitmap qrCode = _writer.WriteAsBitmap(jsonString);
        string qrName = GenerateRandomQRName();
        
        qrCode.Save(Constants.QR_OUTPUT_PATH + qrName);
        return qrName;
    }

    public void DeleteQRCode(string qrName)
    {
        File.Delete(Constants.QR_OUTPUT_PATH + qrName);
    }

    private string GenerateRandomQRName()
    {
        Random random = new Random();
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";
        
        char[] randomChars = new char[12];
        string name;

        do
        {
            for (int i = 0; i < 12; i++)
            {
                randomChars[i] = chars[random.Next(chars.Length)];
            }

            name = new string(randomChars);
            name += ".png";

        } while (File.Exists(Constants.QR_OUTPUT_PATH + name));

        return name;
    }
}