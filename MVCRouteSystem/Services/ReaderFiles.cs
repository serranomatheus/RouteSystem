using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;
using OfficeOpenXml;

namespace MVCRouteSystem.Services
{
    public class ReaderFiles
    {
        public static (List<List<string>>,List<string>,List<string>,int) ReaderFileXlsx(IFormFile pathFile, string routeCity)
        {
            List<string> servicesList = new List<string>();
            List<string> headerList = new List<string>();
            List<List<string>> routeFile = new List<List<string>>();
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            using (ExcelPackage package = new(pathFile.OpenReadStream()))
            {
                ExcelWorksheet worksheet = package.Workbook.Worksheets[0];
                int columnCount = worksheet.Dimension.End.Column;
                int rowCount = worksheet.Dimension.End.Row;
                int columnCep = 0;
                int columnService = 0;
                int columnCity = 0;
                for (int row = 1; row <= columnCount; row++)
                {
                    if (worksheet.Cells[1, row].Value.ToString().ToLower().Replace(" ", "").Equals("cep"))
                    {
                        columnCep = row - 1;
                    }

                    if (worksheet.Cells[1, row].Value.ToString().ToLower().Equals("cidade"))
                    {
                        columnCity = row - 1;
                    }

                    if (worksheet.Cells[1, row].Value.ToString().ToLower().Replace("ç", "c").Equals("servico"))
                    {
                       columnService = row - 1;
                    }
                }

                worksheet.Cells[2, 1, rowCount, columnCount].Sort(columnCep, false);

                for (int row = 1; row <= rowCount; row++)
                {
                    List<string> rowList = new List<string>();
                    for (int col = 1; col <= columnCount; col++)
                    {
                        var value = worksheet.Cells[row, col].Value ?? "";
                        rowList.Add(value.ToString());
                    }
                    routeFile.Add(rowList);
                }

                headerList = routeFile[0];

                for (int i = 0; i < routeFile.Count; i++)
                {
                    
                    routeFile.Remove(routeFile.Find(route => route[columnCity].ToLower() != routeCity.ToLower()));
                   
                }

                for (int i  = 0; i < routeFile.Count; i++ )
                {
                    servicesList.Add(routeFile[i][columnService].ToString());
                    
                }

               servicesList = servicesList.Distinct().ToList();


                return (routeFile,servicesList,headerList, columnService);
            }
        }
    }
}
