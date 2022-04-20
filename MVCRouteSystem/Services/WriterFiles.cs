using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.AspNetCore.Hosting;
using MVCRouteSystem.Models;

namespace MVCRouteSystem.Services
{
    public class WriterFiles
    {

        public static void WriterFileXlsx(string routeService, string routeCity, List<string> routeTeams, List<string> selectColumn, List<List<string>> routeFile, string rootPath, int columnService, List<string> headerList)
        {
            string pathFile = rootPath + "//file//routes.docx";

            for (int i = 0; i < routeFile.Count; i++)
            {
                routeFile.Remove(routeFile.Find(route => route[columnService].ToLower().Replace("ç", "c").Replace("ã", "a")
                                                != routeService.ToLower().Replace("ç", "c").Replace("ã", "a")));
            }

            var divisionTeams = routeFile.Count / routeTeams.Count;
            var restDivisionTeams = routeFile.Count % routeTeams.Count;
            var indexGeneral = 0;

            using (StreamWriter sw = new(pathFile))
            {
                sw.WriteLine($"{routeService} - {DateTime.Now.ToString("dd/MM/yyyy")}\n{routeCity}\n\n");

                for (int team = 0; team < routeTeams.Count; team++)
                {
                    sw.WriteLine("Equipe: " + routeTeams[team]);
                    sw.WriteLine("Rotas:");

                    for (int i = 0; i < divisionTeams; i++)
                    {
                        if (i == 0 && restDivisionTeams > 0)
                            divisionTeams++;

                        if (i == 0)
                            restDivisionTeams--;

                        foreach (var index in selectColumn)
                            sw.WriteLine($"{headerList[int.Parse(index)]}: {routeFile[i + indexGeneral][int.Parse(index)]}");

                        if ((i + 1) >= divisionTeams)
                            indexGeneral += 1 + i;

                        sw.WriteLine("\n");

                    }

                    if(restDivisionTeams >= 0)
                        divisionTeams--;

                    sw.WriteLine("--------------------------------------------------------------");
                }
            }
        }
    }
}


