using Microsoft.Office.Interop.Excel;
using RawMaterialProcessing.Model;
using System;
using System.Collections.Generic;
using System.IO;

namespace RawMaterialProcessing.Service
{
    class Excel
    {
        Application app;
        public Workbook wb;
        public Worksheet worksheet;

        public Excel()
        {
            app = new Application();

        }

        public bool OpenWorksheet(String path)
        {
            wb?.Close(0);
            
            wb = app.Workbooks.Open(path, Type.Missing, Type.Missing, Type.Missing, Type.Missing,
                Type.Missing, Type.Missing, Type.Missing, Type.Missing,
                Type.Missing, Type.Missing, Type.Missing, Type.Missing,
                Type.Missing, Type.Missing);

            worksheet = wb.Worksheets[1];
            return worksheet != null ? true : false;
        }

        public bool CreateWorksheet(String path)
        {
            if (!System.IO.File.Exists(path))
            {
                wb = app.Workbooks.Add(1);
                wb.SaveAs(path);
            }

            return OpenWorksheet(path);
        }

        public List<string> Read ()
        {
            var lastCell = worksheet.Cells.SpecialCells(XlCellType.xlCellTypeLastCell);
            List<string> data = new List<string>();
            string exelData = null;
            for (int i = 2; i <= (int)lastCell.Row; i++)
            {
                for (int j = 1; j <= (int)lastCell.Column; j++)
                {
                    exelData += worksheet.Cells[i, j].Value.ToString();
                    if (j != (int)lastCell.Column) exelData += ";";

                }
                data.Add(exelData);
                exelData = null;
            }
            
            return data;
        }

        public void WritePlan(List<Plan> plans, List<Nomenclatures> nomenclatureses, string path)
        {
            if (this.CreateWorksheet(Path.GetFullPath(path)))
            {
                    worksheet.Cells[1, 1].Value = "Nomenclature";
                    worksheet.Cells[1, 2].Value = "Machine";
                    worksheet.Cells[1, 3].Value = "start_time";
                    worksheet.Cells[1, 4].Value = "end_time";
                    int i = 2;
                    foreach (var plan in plans)
                    {
                        var planToXls = plan.PlanToExcelString(nomenclatureses);
                        string[] data = planToXls.Split(';');
                        for (int j = 1; j <= 4; j++)
                        {
                            worksheet.Cells[i, j].Value = data[j - 1];
                        }

                        i++;

                    }
                    wb.Save();
            }
        }

        public void close(string path)
        {
            wb?.Close(0,path);
            app?.Quit();
        }
    }
}
