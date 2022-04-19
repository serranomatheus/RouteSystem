using Microsoft.AspNetCore.Http;
using OfficeOpenXml;

namespace MVCRouteSystem.Services
{
	public class ReaderFile
	{
        public static string[,] ReadFile(IFormFile pathFile)
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            using (ExcelPackage package = new(pathFile.OpenReadStream()))
            {
                ExcelWorksheet worksheet = package.Workbook.Worksheets[0];
                int colCount = worksheet.Dimension.End.Column;
                int rowCount = worksheet.Dimension.End.Row;
                var routes = new string[rowCount, colCount];
                for (int row = 1; row <= rowCount; row++)
                {
                    for (int col = 1; col <= colCount; col++)
                    {
                        routes[row - 1, col - 1] = worksheet.Cells[row, col].Value.ToString();
                    }
                }
                return routes;
            }
        }
    }
}
