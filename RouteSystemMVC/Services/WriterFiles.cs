using System.Collections.Generic;
using System.IO;
using RouteSystemMVC.Models;

namespace RouteSystemMVC.Services
{
    public class WriterFiles
    {
        public static void WriterFile(List<Route> routes)
        {
            using (StreamWriter sw = new StreamWriter(@"C:\Users\matheus\source\repos\RouteSystem\Routes.docx"))
            {
                for (int i = 0; i < routes.Count; i++)
                {
                    sw.WriteLine("Os: " + routes[i].Os +
                                 " Base: " + routes[i].Base +
                                 " Serviço: " + routes[i].Servico +
                                 $"\nEndereco: {routes[i].Endereco},{routes[i].Numero}-{routes[i].Bairro}\nComplemento: {routes[i].Complemento}\n");

                }
            }
        }
    }
}
