using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace TimeDeal
{
    class PlayMusic
    {
        //指示该属性化方法由非托管动态链接库 (DLL) 作为静态入口点公开。
        public static string Pcommand;
        [System.Runtime.InteropServices.DllImport("winmm.dll")]
        private static extern long mciSendString(string strCommand, StringBuilder strReturn, int iReturnLength, IntPtr hwndCallback);
        [DllImport("kernel32.dll", CharSet = CharSet.Auto)]
        public static extern int GetShortPathName(string path, StringBuilder shortPath, int shortPathLength);

        static public void Playsound(string s)
        {
            mciSendString("close all", null, 0, IntPtr.Zero);



            StringBuilder shortPath = new StringBuilder(80);
            GetShortPathName(s, shortPath, 80);

            Pcommand = "open \"" + shortPath.ToString() + "\" type mpegvideo alias MediaFile";
            mciSendString(Pcommand, null, 0, IntPtr.Zero);
            Pcommand = "set MediaFile time format milliseconds";
            mciSendString(Pcommand, null, 0, IntPtr.Zero);
            Pcommand = "play MediaFile";
            mciSendString(Pcommand, null, 0, IntPtr.Zero);
            //Pcommand = "close MediaFile";
            //mciSendString(Pcommand,null ,0,IntPtr.Zero);
        }
        static public void Stopsound()
        {
            Pcommand = "close MediaFile";
            mciSendString(Pcommand, null, 0, IntPtr.Zero);
        }

        static public void Pausesound()
        {
            Pcommand = "pause MediaFile";
            mciSendString(Pcommand, null, 0, IntPtr.Zero);
        }

        static public void Resumesound()
        {
            Pcommand = "resume MediaFile";
            mciSendString(Pcommand, null, 0, IntPtr.Zero);
        }

    /*    static public void MediaPosition()
        {
            char sPosition[256];
            long lLength;
            string str="abc";
            string durLength = durLength.PadLeft(128, Convert.ToChar(" "));
            Pcommand = "status MediaFile length";
        }
        */
    }
}
