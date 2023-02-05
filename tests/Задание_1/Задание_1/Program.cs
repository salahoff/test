using System;
using System.IO;
using System.Text;
using HtmlAgilityPack;
using System.Linq;

namespace Задание_1
{
    class Program
    {
        static void Main(string[] args)
        {
            System.Net.WebClient webClient = new System.Net.WebClient();
            Console.WriteLine("Введите адрес сайта:");
            string adds = Console.ReadLine();
            webClient.Headers.Add("User-Agent: Other");
            string HTML = webClient.DownloadString(adds);
            File.WriteAllText("html.txt", HTML, Encoding.UTF8);

            HtmlDocument document = new HtmlDocument();
            document.LoadHtml(HTML);
            var nodes = document.DocumentNode
                .Descendants("img")
                .Select(node =>  node.GetAttributeValue("src", "").Trim());

                int count = 0;         
                foreach (var htmlNode in nodes)
                {
                    if (count != 5)
                    {
                        count++;
                        Console.WriteLine("\nКартинка №" + count + " успешно загружена.");
                        Console.WriteLine(htmlNode);
                        File.AppendAllText("link.txt", htmlNode.ToString() + Environment.NewLine, Encoding.UTF8);
          
                        webClient.Dispose();
                        Uri uri = new Uri(htmlNode);
                        webClient.DownloadFile(uri, "images\\picture_" + count + ".jpg");
                        webClient.Dispose();                                   
                    } 
                }

            Console.ReadKey();
        }
    }
}
