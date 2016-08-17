using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using Microsoft.Win32;

namespace ResharperCrack
{
    class Program
    {
        static void WriteInfo()
        {
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Assembly assembly = Assembly.GetExecutingAssembly();
            String[] resourceStrings = assembly.GetManifestResourceNames();
            Stream embeddedCtfStream = null;
            foreach (String name in resourceStrings)
            {
                if (name.Contains("info.txt"))
                {
                     embeddedCtfStream = assembly.GetManifestResourceStream(name);
                    break;
                }
            }

            if (embeddedCtfStream != null)
            {
                StreamReader sr=new StreamReader(embeddedCtfStream,Encoding.Default);
                string tmp = "";
                while (!string.IsNullOrEmpty(tmp= sr.ReadLine()))
                {
                    Console.WriteLine(tmp);
                }
            }
           Console.ForegroundColor=ConsoleColor.White;
            
            
        }

        static void Main(string[] args)
        {
            try
            {
                WriteInfo();

                RegistryKey key = Registry.CurrentUser;
                var subkey =
                    key.OpenSubKey(
                        @"Software\Microsoft\Windows\CurrentVersion\Ext\Settings", true);
                if (subkey != null)
                {
                    subkey.DeleteSubKeyTree("{9656c84c-e0b4-4454-996d-977eabdf9e86}", false);
                    subkey.CreateSubKey("{9656c84c-e0b4-4454-996d-977eabdf9e86}");
                    var tmpk = subkey.OpenSubKey("{9656c84c-e0b4-4454-996d-977eabdf9e86}", true);
                    tmpk.SetValue("{294AEEB3-D72E-426D-8AF8-2BBA18633156}", "{ReSharper,ReSharperCpp,dotCover,dotMemory,dotTrace}");
                    Console.WriteLine("破解成功");
                }



            }
            catch (Exception e)
            {
                Console.WriteLine("破解失败");
            }
            Console.ReadKey();
        }
    }
}
