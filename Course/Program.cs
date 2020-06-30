
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;

namespace Course
{
    class Program
    {
        static void Main(string[] args)
        {
            //FileInfo_File();
            //StreamReader();
            //StreamWriter();
            //BlocoUsing();
            //DirectoryInfo_Directory();
            //UsandoPath();

            string sourceFolderPath = @"C:\TEMP\myfolder";
            string sourceFilePath = sourceFolderPath + @"\products.csv";
            string targetFolderPath = sourceFolderPath + @"\out";
            string targetFilePath = targetFolderPath + @"\summary.csv";

            try
            {
                string[] lines = File.ReadAllLines(sourceFilePath);
                DirectoryInfo directory = Directory.CreateDirectory(targetFolderPath);

                using (StreamWriter sw = File.CreateText(targetFilePath))
                {
                    foreach (string line in lines)
                    {
                        string[] data = line.Split(',');
                        string name = data[0];
                        double price = double.Parse(data[1], CultureInfo.InvariantCulture);
                        int quantity = int.Parse(data[2]);

                        sw.WriteLine(name + ", " + (price * quantity).ToString("C"));
                    }
                }
            }
            catch (IOException e)
            {
                Console.WriteLine("An error occurred");
                Console.WriteLine(e.Message);
            }

            Console.ReadLine();
        }

        private static void UsandoPath()
        {
            string path = @"c:\temp\myfolder\file1.txt";

            Console.WriteLine("DirectorySeparatorChar: " + Path.DirectorySeparatorChar);
            Console.WriteLine("PathSeparator: " + Path.PathSeparator);
            Console.WriteLine("GetDirectoryName: " + Path.GetDirectoryName(path));
            Console.WriteLine("GetFileName: " + Path.GetFileName(path));
            Console.WriteLine("GetExtension: " + Path.GetExtension(path));
            Console.WriteLine("GetFileNameWithoutExtension: " + Path.GetFileNameWithoutExtension(path));
            Console.WriteLine("GetFullPath: " + Path.GetFullPath(path));
            Console.WriteLine("GetTempPath: " + Path.GetTempPath());
        }

        private static void DirectoryInfo_Directory()
        {
            string desktop = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            string path = @"C:\TEMP\myfolder";

            try
            {
                Directory.CreateDirectory(path + @"\newfolder");

                IEnumerable<string> folders = Directory.EnumerateDirectories(path, "*.*", SearchOption.AllDirectories);
                Console.WriteLine("Folders: ");
                foreach (string folder in folders)
                    Console.WriteLine(folder);

                IEnumerable<string> files = Directory.EnumerateFiles(path, "*.*", SearchOption.AllDirectories);
                Console.WriteLine("Files: ");
                foreach (string file in files)
                    Console.WriteLine(file);

            }
            catch (IOException e)
            {
                Console.WriteLine("An error occurred");
                Console.WriteLine(e.Message);
            }
        }

        private static void StreamWriter()
        {
            string sourcePath = @"C:\TEMP\file1.txt";
            string targetPath = @"C:\TEMP\file2.txt";

            try
            {
                string[] lines = File.ReadAllLines(sourcePath);

                using (StreamWriter sw = File.AppendText(targetPath))
                {
                    foreach (string line in lines)
                        sw.WriteLine(line.ToUpper());
                }
            }
            catch (IOException e)
            {
                Console.WriteLine("An error occurred");
                Console.WriteLine(e.Message);
            }
        }

        private static void BlocoUsing()
        {
            string sourcePath = @"C:\TEMP\file1.txt";

            try
            {
                //using (StreamReader sr = File.OpenText(path)) - Para substituir os dois using

                using (FileStream fs = new FileStream(sourcePath, FileMode.Open))
                using (StreamReader sr = new StreamReader(fs))
                {
                    string line = sr.ReadLine();
                    Console.WriteLine(line);
                }
            }
            catch (IOException e)
            {
                Console.WriteLine("An error occurred");
                Console.WriteLine(e.Message);
            }
        }

        private static void StreamReader()
        {
            string sourcePath = @"C:\TEMP\file1.txt";
            FileStream fs = null;
            StreamReader sr = null;

            try
            {
                //sr = File.OpenText(path) - Implicitamente já instancia um FileStream e um StreamReader a partir de um caminho

                fs = new FileStream(sourcePath, FileMode.Open);
                sr = new StreamReader(fs);

                while (!sr.EndOfStream)
                {
                    string line = sr.ReadLine();
                    Console.WriteLine(line);
                }
            }
            catch (IOException e)
            {
                Console.WriteLine("An error ocurred");
                Console.WriteLine(e.Message);
            }
            finally
            {
                if (sr != null) sr.Close();
                if (fs != null) fs.Close();
            }
        }

        private static void FileInfo_File()
        {
            string sourcePath = @"C:\TEMP\file1.txt";
            string targetPath = @"C:\TEMP\file2.txt";

            try
            {
                FileInfo fileInfo = new FileInfo(sourcePath);
                fileInfo.CopyTo(targetPath);

                string[] lines = File.ReadAllLines(sourcePath);
                foreach (string line in lines)
                    Console.WriteLine(line);

            }
            catch (IOException e)
            {
                Console.WriteLine("An error ocurred");
                Console.WriteLine(e.Message);
            }
        }
    }
}
