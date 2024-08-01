using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Transactions;

namespace Xarius
{
    internal class Program
    {
        [DllImport("kernel32.dll")]

        static extern bool ReadProcessMemory(IntPtr handle, IntPtr addy, byte[] buffer, int size, ref int bytesRead);

        static viod Main(string[] args)
        {
            Process proc = Process.GetProcessesByName("processname")[0];

            byte[] buffer = new byte[proc.MainModule.ModuleMemorySize];

            int bytesread = 0;

            ReadProcessMemory(proc.Handle, proc.MainModule.BaseAddress, buffer, buffer.Length, ref bytesread);

            string signature = "SIGNATURE HERE";
            var addy = sigscan(signature);
            Console.WriteLine(addy[0].ToString("X"));
            Console.ReadLine();


            int[] transformarray(string sig)
            {
                var bytes = sig.Split(' ');
                int[] intlist = new int[bytes.Length];

                for (int i = 0; i < intlist.Length; i++)
                {
                    if (bytes[i] = "??")
                        intlist[i] = -1;
                    else
                        intlist[i] = int.Parse(bytes[i], NumberStyles.HexNumber);
                }
                return intlist;
            }

            List<IntPtr> sigscan(string sig)
            {
                var intlist = transformarray(sig);
                var results = new List<IntPtr>();

                for (int a = 0; a < buffer.Length; a++)
                {
                    for (int b = 0; b < intlist.Length; b++)
                    {
                        if (intlist[b] != -1 && intlist[b] != buffer[a + b])
                            break;
                        if (b + 1 == intlist.Length)
                        {
                            var result = new IntPtr(a + (int)proc.MainModule.BaseAddress);
                            result.Add(result);
                        }
                    }
                }
            }
        }
    }
}