using System.Diagnostics;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;

namespace student_final.Register.Models
{
    internal class ExcelReadAndWriteClass
    {
        private static SpreadsheetDocument _doc;
        private static bool _isEditable = true;
        private static WorkbookPart _workbookPart;
        public WorksheetPart _worksheetPart;
        private static SheetData _sheetData;
        private static SharedStringTable _sharedStringTable;
        private static SharedStringTablePart _sharedStringPart;
        public Sheet _sheet;
        /// private static string str = File.ReadAllText(@"C:\proiecteCsharp\xmlCode.txt");

        public ExcelReadAndWriteClass(string docPath)
        {

            _doc = SpreadsheetDocument.Open(docPath, _isEditable);
            _workbookPart = _doc.WorkbookPart;
            _sheet = _workbookPart.Workbook.Sheets.Elements<Sheet>().First();  //or GetFirstChild<Sheet>(); or ElementAt(0)  ///GETS THE FIRST SHEET IN THE ORDER THEY ARE PLACED IN
            _worksheetPart = (WorksheetPart)_workbookPart.GetPartById(_sheet.Id);
            _sheetData = _worksheetPart.Worksheet.Elements<SheetData>().First();
            if (_workbookPart.SharedStringTablePart is null)
            {
                _workbookPart.AddNewPart<SharedStringTablePart>();
                _workbookPart.Workbook.Save();
                _sharedStringPart = _workbookPart.SharedStringTablePart;
                _sharedStringPart.SharedStringTable = new SharedStringTable();
                _sharedStringPart.SharedStringTable.AppendChild(new SharedStringItem(new DocumentFormat.OpenXml.Spreadsheet.Text("default")));
                _sharedStringPart.SharedStringTable.Save();
                _sharedStringTable = _sharedStringPart.SharedStringTable;
            }
            else
            {
                _sharedStringPart = _workbookPart.SharedStringTablePart;
                _sharedStringTable = _sharedStringPart.SharedStringTable;
            }

            /* 
             * For editing multiple sheets *
             * IEnumerator<OpenXmlElement> sheetEnum = _workbookPart.Workbook.Sheets.GetEnumerator();
             * sheetEnum.MoveNext();
             * sheet = (Sheet)sheetEnum.Current;
            */
        }

        public Cell GetCellByReference(string reference)
        {
            Debug.Assert(!string.IsNullOrEmpty(reference));
            string thisCellRef;
            foreach (Row row in _sheetData.Elements<Row>())
            {
                foreach (Cell cell in row.Elements<Cell>())
                {
                    thisCellRef = cell.CellReference;
                    if (thisCellRef == reference)
                        return cell;
                }
            }
            return null;
        }

        public Cell GetCellByPosition(Row selectedRow, int position)
        {
            Debug.Assert(position > -1 && position < selectedRow.Count());
            return selectedRow.Elements<Cell>().ElementAt(position);
        }

        public string GetCellValue(Cell cell)
        {
            string cellValue = cell.InnerText;

            if (cell.DataType != null && cell.DataType.Value == CellValues.SharedString)
                if (_sharedStringPart != null)
                {
                    int sharedStringIndex = int.Parse(cellValue);   //cell value of a string cell is the index of the string in the shared string table
                    return _sharedStringPart.SharedStringTable.Elements<SharedStringItem>().ElementAt(sharedStringIndex).InnerText;
                }
            return cellValue;
        }

        public void WriteSheetInConsole()
        {
            Debug.Assert(_sheetData.Elements<Row>().Count() != 0, "THERE ARE NO ELEMENTS INSIDE THE SHEET");

            int contor = 0;

            string cellValue;

            foreach (Row row in _sheetData.Elements<Row>())
            {
                foreach (Cell cell in row.Elements<Cell>())
                {
                    contor++;
                    cellValue = GetCellValue(cell);
                    Console.WriteLine($"{cell.DataType.Value}     {cell.CellReference}: {cellValue}");
                }
                Console.WriteLine();
            }

            Console.WriteLine("NUMBER OF CELLS INSERTED IN THE SHEET: " + contor);
            Console.WriteLine();
        }

        private void ChangeCellValue(Cell cell, string newValue)
        {
            string cellValue = cell.InnerText;

            if (cell.DataType != null && cell.DataType.Value == CellValues.SharedString)
            {
                IEnumerable<SharedStringItem> cellStringCollection = _sharedStringTable.Elements<SharedStringItem>();
                int strIndex = int.Parse(cellValue);

                if (cellStringCollection.Where(cellString => cellString.InnerText == newValue).Count() == 0)
                {
                    _sharedStringTable.AppendChild(new SharedStringItem(new DocumentFormat.OpenXml.Spreadsheet.Text(newValue)));
                    _sharedStringTable.Save();
                }
                int newIndex = cellStringCollection.TakeWhile(cellString => cellString.InnerText != newValue).Count();
                cell.CellValue = new CellValue(newIndex);
            }
            else
                cell.CellValue = new CellValue(int.Parse(newValue));
            _worksheetPart.Worksheet.Save();
        }


        // NOTE: These "Get" and "Has" methods don't account for multiple entries with the same values
        //       They will return the first instance that meets the criterion
        public Row GetRowByCNP(string CNP)
        {
            Debug.Assert(!string.IsNullOrEmpty(CNP));

            string cellValue;
            Cell cell;

            foreach (Row row in _sheetData.Elements<Row>())
            {
                cell = row.Elements<Cell>().ElementAt(1);
                cellValue = GetCellValue(cell);
                if (cellValue == CNP)
                {
                    Console.WriteLine($"Entry {cell.CellReference.Value[1]} selected.");
                    return row;
                }

            }

            Console.WriteLine("!!!!!! There is no entry with the given CNP to select !!!!!!");
            //Debug.Assert(false);
            return null;
        }

        public bool HasRowWithCNP(string CNP)
        {
            Debug.Assert(!string.IsNullOrEmpty(CNP));

            string cellValue;
            Cell cell;

            foreach (Row row in _sheetData.Elements<Row>())
            {
                cell = row.Elements<Cell>().ElementAt(1);
                cellValue = GetCellValue(cell);
                if (cellValue == CNP)
                {
                    Console.WriteLine("Entry found.");
                    return true;
                }

            }
            Console.WriteLine("!!! There is no entry with the given CNP !!!");
            return false;
        }

        public Row GetRowByFullName(string FullName)
        {
            Debug.Assert(!string.IsNullOrEmpty(FullName));

            string cellValue;
            Cell cell;

            foreach (Row row in _sheetData.Elements<Row>())
            {
                cell = row.Elements<Cell>().ElementAt(0);
                cellValue = GetCellValue(cell);
                if (cellValue == FullName)
                {
                    Console.WriteLine($"Entry {cell.CellReference.Value[1]} selected.");
                    return row;
                }

            }
            Console.WriteLine("!!!!!! There is no entry with the given name to select !!!!!!");
            Debug.Assert(false);
            return null;
        }

        public bool HasRowWithFullName(string FullName)
        {
            Debug.Assert(!string.IsNullOrEmpty(FullName));

            string cellValue;
            Cell cell;

            foreach (Row row in _sheetData.Elements<Row>())
            {
                cell = row.Elements<Cell>().ElementAt(0);
                cellValue = GetCellValue(cell);
                if (cellValue == FullName)
                {
                    Console.WriteLine("Entry found.");
                    return true;
                }

            }
            Console.WriteLine("!!! There is no entry with the given name !!!");
            return false;
        }

        public Row GetRowByColumnNoEntry(int colNum, string entryToFind)
        {
            Debug.Assert(colNum > -1);
            Debug.Assert(entryToFind != null);

            string cellValue;
            IEnumerable<Cell> rowCells;
            Cell cell;

            foreach (Row row in _sheetData.Elements<Row>())
            {
                rowCells = row.Elements<Cell>();
                if (rowCells.Count() < colNum)
                    continue;
                cell = rowCells.ElementAt(colNum);
                cellValue = GetCellValue(cell);
                if (cellValue == entryToFind)
                {
                    Console.WriteLine($"Entry {cell.CellReference.Value[1]} selected.");
                    return row;
                }

            }
            Console.WriteLine($"!!!!!! There is no entry on column {colNum} with the given value to select !!!!!!");
            return null;
        }

        public bool HasRowWithColumnNoEntry(int colNum, string entryToFind)
        {
            Debug.Assert(colNum > -1);
            Debug.Assert(!string.IsNullOrEmpty(entryToFind));

            string cellValue;
            IEnumerable<Cell> rowCells;
            Cell cell;

            foreach (Row row in _sheetData.Elements<Row>())
            {
                rowCells = row.Elements<Cell>();
                if (rowCells.Count() < colNum)
                    continue;
                cell = rowCells.ElementAt(colNum);
                cellValue = GetCellValue(cell);
                if (cellValue == entryToFind)
                {
                    Console.WriteLine("Entry found.");
                    return true;
                }

            }
            Console.WriteLine($"!!! There is no entry on column {colNum} with the given value !!!");
            return false;
        }

        public Row getRowByIndex(int? index)
        {
            Debug.Assert(index.HasValue);
            Debug.Assert(index > -1);

            return _sheetData.Elements<Row>().ElementAt(index.Value);
        }

        public Row getLastRow()
        {
            return _sheetData.Elements<Row>().LastOrDefault();
        }

        public void InsertRow(object[] cellEntry)
        {
            Row lastRow = _sheetData.Elements<Row>().LastOrDefault();
            string indexOfCell;

            if (lastRow != null)
            {
                _sheetData.InsertAfter(new Row() { RowIndex = (lastRow.RowIndex + 1) }, lastRow);
                indexOfCell = "A" + (lastRow.RowIndex + 1).ToString();
            }
            else
            {
                _sheetData.InsertAt(new Row() { RowIndex = 0 }, 0);
                indexOfCell = "A1";
            }
            _worksheetPart.Worksheet.Save();
            Cell exampleCell = _sheetData.Elements<Row>().First().Elements<Cell>().First();
            lastRow = _sheetData.Elements<Row>().LastOrDefault();

            Cell newCell;

            for (int i = 0; i < cellEntry.Length; i++)
            {
                newCell = (Cell)exampleCell.CloneNode(true);

                if (cellEntry[i].GetType() == typeof(Int32) || cellEntry[i].GetType() == typeof(Int16))
                {
                    newCell.DataType.Value = CellValues.Number;
                    newCell.CellValue = new CellValue((int)cellEntry[i]);
                }
                else
                {
                    newCell.DataType.Value = CellValues.SharedString;
                    ChangeCellValue(newCell, cellEntry[i].ToString());
                }

                newCell.CellReference.Value = indexOfCell;
                lastRow.InsertAfter(newCell, lastRow.LastChild);
                indexOfCell = indexOfCell.Replace(indexOfCell[0], Convert.ToChar(indexOfCell[0] + 1));
                _worksheetPart.Worksheet.Save();
            }
            _doc.Save();
        }

        public int CountRows()
        {
            return _sheetData.Elements<Row>().Count();
        }
    }
}
