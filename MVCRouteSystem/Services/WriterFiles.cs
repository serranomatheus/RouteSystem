using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Microsoft.AspNetCore.Hosting;
using MVCRouteSystem.Models;

namespace MVCRouteSystem.Services
{
    public class WriterFiles
    {

        public static string WriterFileXlsx(string routeService, string routeCity, List<string> routeTeams, List<string> selectColumn, List<List<string>> routeFile, string rootPath, int columnService, List<string> headerList)
        {
            string pathFile = rootPath + "//file//routes.docx";
            List<int> selectColumnInt = new List<int>();
            int columnAdress;

            for (int i = 0; i < routeFile.Count; i++)
            {
                routeFile.Remove(routeFile.Find(route => route[columnService].ToLower().Replace("ç", "c").Replace("ã", "a")
                                                != routeService.ToLower().Replace("ç", "c").Replace("ã", "a")));
            }

            foreach (var item in selectColumn)
            {
                selectColumnInt.Add(int.Parse(item));
            }


            columnAdress = headerList.FindIndex(col => col.ToLower().Replace("ç", "c") == "endereco");

            foreach (int address in selectColumnInt)
            {
                if (address == columnAdress)
                {
                    selectColumnInt.Add(headerList.FindIndex(coluna => coluna.ToLower().Replace("ú", "u") == "numero"));
                    selectColumnInt.Add(headerList.FindIndex(coluna => coluna.ToLower() == "bairro"));
                    selectColumnInt.Add(headerList.FindIndex(coluna => coluna.ToLower() == "complemento"));
                    selectColumnInt.Add(headerList.FindIndex(coluna => coluna.ToLower().Replace(" ", "") == "cep"));
                    selectColumnInt.Sort();
                    break;
                }
            }

            var divisionTeams = routeFile.Count / routeTeams.Count;
            var restDivisionTeams = routeFile.Count % routeTeams.Count;
            var indexGeneral = 0;

            using (FileStream fileStream = new(pathFile, FileMode.Create))
            {
                using (StreamWriter sw = new(fileStream, Encoding.UTF8))
                {

                    sw.WriteLine($"{routeService} - {DateTime.Now.ToString("dd/MM/yyyy")}\n{routeCity}\n\n");

                    for (int team = 0; team < routeTeams.Count; team++)
                    {
                        sw.WriteLine("Equipe: " + routeTeams[team]);
                        sw.WriteLine("Rotas:");
                        sw.WriteLine("\n");
                        for (int i = 0; i < divisionTeams; i++)
                        {
                            if (i == 0 && restDivisionTeams > 0)
                                divisionTeams++;

                            if (i == 0)
                                restDivisionTeams--;

                            foreach (int index in selectColumnInt)
                                sw.WriteLine($"{headerList[index]}: {routeFile[i + indexGeneral][index]}");

                            if ((i + 1) >= divisionTeams)
                                indexGeneral += 1 + i;

                            sw.WriteLine("\n");

                        }

                        if (restDivisionTeams >= 0)
                            divisionTeams--;

                        sw.WriteLine("--------------------------------------------------------------");
                    }
                }
            }
                return pathFile;            
        }
    }
}


