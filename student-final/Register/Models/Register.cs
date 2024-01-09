using DocumentFormat.OpenXml.Spreadsheet;

namespace student_final.Register.Models
{
    internal class Register
    {

        private int _number;
        private string _date;
        private string _firstName;
        private string _lastName;
        private int _studyYear;
        private string _programme;
        private List<string> _availableProgrammes = new List<string> {
            "Calculatoare",
            "ISM",
            "TI"
        };
        private string _reason;
        private List<string> _availableReasons = new List<string> {
            "CNAS",
            "burse",
            "locul de munca",
            "cazare camin"
        };

        //incerc sa pun numarul adeverintei
        public Register()
        {
            _number = 1;
            _date = GetDate();
            _firstName = "";
            _lastName = "";
            _studyYear = 1;
            _programme = "";
            _reason = "";
        }

        public Register(string firstName, string lastName, int studyYear, int programmeIndex, int reasonIndex, ExcelReadAndWriteClass? xl = null)
        {
            // in cazul in care tabelul nu are nicio valoare inserata (doar header-ul tabelului)
            if (xl!.CountRows() <= 1 || xl == null!)
            {
                _number = 1;
                _date = GetDate();
                _firstName = firstName;
                _lastName = lastName;
                _studyYear = studyYear;

                // programme selection by given index
                if (programmeIndex < _availableProgrammes.LongCount())
                {
                    _programme = _availableProgrammes[programmeIndex];
                }
                else
                {
                    _programme = "";
                }

                // reason selection by given index
                if (reasonIndex < _availableReasons.LongCount())
                {
                    _reason = _availableReasons[reasonIndex];
                }
                else
                {
                    _reason = "";
                }
            }
            else // daca tabelul are deja valori puse in tabel
            {
                _number = GetLastCertificateNumber(xl) + 1;
                _date = GetDate();
                _firstName = firstName;
                _lastName = lastName;
                _studyYear = studyYear;

                // programme selection by given index
                if (programmeIndex < _availableProgrammes.LongCount())
                {
                    _programme = _availableProgrammes[programmeIndex];
                }
                else
                {
                    _programme = "";
                }

                // reason selection by given index
                if (reasonIndex < _availableReasons.LongCount())
                {
                    _reason = _availableReasons[reasonIndex];
                }
                else
                {
                    _reason = "";
                }
            }
        }

        public object[] GetObjectForRegistru(Register register)
        {
            object[] objectArray = new object[]
            {
                register._number,
                register._date,
                register._firstName,
                register._lastName,
                register._studyYear,
                register._programme,
                register._reason
            };

            return objectArray;
        }

        public string GetDate()
        {
            DateTime currentDate = DateTime.Today.Date;
            string formattedDate = currentDate.ToString("dd/MM/yyyy");

            return formattedDate;
        }

        public int GetLastCertificateNumber(ExcelReadAndWriteClass exelDocument)
        {
            return int.Parse(exelDocument.GetCellValue(exelDocument.getLastRow().Elements<Cell>().First()));
        }
    }
}
