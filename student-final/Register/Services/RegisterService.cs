using DocumentFormat.OpenXml.Spreadsheet;
using student_final.Register.Models;
using student_final.Register.Services.Interfaces;
using student_final.System.Constants;

namespace student_final.Register.Services;

public class RegisterService : IRegisterService
{
    private ExcelReadAndWriteClass _excel;
    private ILogger<RegisterService> _logger;

    public RegisterService(ILogger<RegisterService> logger)
    {
        _excel = new ExcelReadAndWriteClass(Constants.EXCEL_REGISTER_PATH);
        _logger = logger;
    }

    public string LogCells()
    {
        Row row = _excel.getLastRow();
        string log = "";
        foreach (Cell cell in row.Elements<Cell>())
        {
            log += _excel.GetCellValue(cell);
            _logger.LogInformation(_excel.GetCellValue(cell));
        }

        return log;
    }
}