using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using OfficeOpenXml;
using RouteSystemMVC.Models;

namespace RouteSystemMVC.Services
{
    public  class ReaderFile
    {
        public static List<Route> ReadFile(string pathFile)
        {
            var routes = new List<Route>();
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            using (ExcelPackage package = new(pathFile))
            {
                ExcelWorksheet worksheet = package.Workbook.Worksheets[0];
                int colCount = worksheet.Dimension.End.Column;
                int rowCount = worksheet.Dimension.End.Row;
                for (int row = 2; row <= rowCount; row++)
                {
                    var route = new Route();

                    route.Data = worksheet.Cells[row, 1].Value.ToString();
                    route.Stats = worksheet.Cells[row, 2].Value.ToString();
                    route.Auditado = worksheet.Cells[row, 3].Value.ToString();
                    route.CopReverteu = worksheet.Cells[row, 4].Value.ToString();
                    route.Log = worksheet.Cells[row, 5].Value.ToString();
                    route.Pdf = worksheet.Cells[row, 6].Value.ToString();
                    route.Foto = worksheet.Cells[row, 7].Value.ToString();
                    route.Contrato = worksheet.Cells[row, 8].Value.ToString();
                    route.Wo = worksheet.Cells[row, 9].Value.ToString();
                    route.Os = worksheet.Cells[row, 10].Value.ToString();
                    route.Assinante = worksheet.Cells[row, 11].Value.ToString();
                    route.Tecnicos = worksheet.Cells[row, 12].Value.ToString();
                    route.Login = worksheet.Cells[row, 13].Value.ToString();
                    route.Matricula = worksheet.Cells[row, 14].Value.ToString();
                    route.Cop = worksheet.Cells[row, 15].Value.ToString();
                    route.UltimoAlterar = worksheet.Cells[row, 16].Value.ToString();
                    route.Local = worksheet.Cells[row, 17].Value.ToString();
                    route.PontoCasaApt = worksheet.Cells[row, 18].Value.ToString();
                    route.Cidade = worksheet.Cells[row, 19].Value.ToString();
                    route.Base = worksheet.Cells[row, 20].Value.ToString();
                    route.Horario = worksheet.Cells[row, 21].Value.ToString();
                    route.Segmento = worksheet.Cells[row, 22].Value.ToString();
                    route.Servico = worksheet.Cells[row, 23].Value.ToString();
                    route.TipoServico = worksheet.Cells[row, 24].Value.ToString();
                    route.TipoOs = worksheet.Cells[row, 25].Value.ToString();
                    route.GrupoServico = worksheet.Cells[row, 26].Value.ToString();
                    route.Endereco = worksheet.Cells[row, 27].Value.ToString();
                    route.Numero = worksheet.Cells[row, 28].Value.ToString();
                    route.Complemento = worksheet.Cells[row, 29].Value.ToString();
                    route.Cep = worksheet.Cells[row, 30].Value.ToString();
                    route.Node = worksheet.Cells[row, 31].Value.ToString();
                    route.Bairro = worksheet.Cells[row, 32].Value.ToString();
                    route.Pacote = worksheet.Cells[row, 33].Value.ToString();
                    route.Cod = worksheet.Cells[row, 34].Value.ToString();
                    route.Telefone1 = worksheet.Cells[row, 35].Value.ToString();
                    route.Telefone2 = worksheet.Cells[row, 36].Value.ToString();
                    route.Obs = worksheet.Cells[row, 37].Value.ToString();
                    route.ObsTecnico = worksheet.Cells[row, 38].Value.ToString();
                    route.Equipamento = worksheet.Cells[row, 39].Value.ToString();
                    routes.Add(route);
                }
                
            }
            return routes;
        }

    }
}



