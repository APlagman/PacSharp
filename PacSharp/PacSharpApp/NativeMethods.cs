using System;
using System.Runtime.InteropServices;

namespace PacSharpApp
{
    /// <summary>
    /// See: https://blogs.msdn.microsoft.com/shawnhar/2010/12/06/when-winforms-met-game-loop/
    /// </summary>
    static class NativeMethods
    {
        [StructLayout(LayoutKind.Sequential)]
        public struct Message
        {
            internal IntPtr hWnd;
            internal uint Msg;
            internal IntPtr wParam;
            internal IntPtr lParam;
            internal uint Time;
            internal System.Drawing.Point Point;
        }

        [DllImport("User32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool PeekMessage(out Message message, IntPtr hWnd, uint filterMin, uint filterMax, uint flags);
    }
}
