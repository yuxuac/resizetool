using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace resizetool
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                string folder = System.AppDomain.CurrentDomain.BaseDirectory;
                string outputFolder = Path.Combine(folder, "output") + @"\";

                if (!Directory.Exists(outputFolder))
                    Directory.CreateDirectory(outputFolder);

                string[] files = Directory.GetFiles(folder, ConfigurationManager.AppSettings["fileExtension"], SearchOption.AllDirectories);

                ImageHandler tool = new ImageHandler();

                var maxWidth = int.Parse(ConfigurationManager.AppSettings["maxWidth"]);
                var maxHeight = int.Parse(ConfigurationManager.AppSettings["maxHeight"]);
                var quality = int.Parse(ConfigurationManager.AppSettings["quality"]);

                foreach (var file in files)
                {
                    Console.WriteLine("Processing " + file);
                    string outputFile = file.Replace(folder, outputFolder);

                    FileInfo fi = new FileInfo(outputFile);
                    string subFolder = fi.Directory.FullName;
                    if (!Directory.Exists(subFolder))
                        Directory.CreateDirectory(subFolder);

                    tool.Save(Image.FromFile(file), maxWidth, maxHeight, quality, outputFile);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + ex.StackTrace);
                Console.WriteLine("Enter to exit.");
                Console.ReadLine();
            }
        }
    }
}
