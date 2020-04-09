using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Whatever_Namespace
{
    class Memory
    {
        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern bool ReadProcessMemory(IntPtr pHandle, IntPtr Address, byte[] Buffer, int Size, IntPtr NumberofBytesRead);

        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern bool WriteProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress, byte[] lpBuffer, Int32 nSize, out IntPtr lpNumberOfBytesWritten);



        public Process process { get; set; }
        public IntPtr BaseAdr { get; set; }

        public Memory(Process proc)
        {
            this.process = proc;
            this.BaseAdr = this.process.MainModule.BaseAddress;
        }
        public int ReadInt32(IntPtr memory_adr)
        {
            byte[] buffer = new byte[sizeof(int)];
            IntPtr NumberOfBytesRead = IntPtr.Zero;

            ReadProcessMemory(process.Handle, memory_adr, buffer, 4, NumberOfBytesRead);
            return BitConverter.ToInt32(buffer, 0);
        }

        public string ReadString(IntPtr memory_adr, int length)
        {
            string final_string = "";

            byte[] buffer = new byte[length];
            IntPtr NumberOfBytesRead = IntPtr.Zero;

            ReadProcessMemory(process.Handle, memory_adr, buffer, length, NumberOfBytesRead);

            final_string = ASCIIEncoding.ASCII.GetString(buffer);

            return final_string;
        }

        public void WriteInt32(IntPtr memory_adr, int value)
        {
            byte[] buffer = new byte[sizeof(int)];
            buffer = BitConverter.GetBytes(value);
            IntPtr AmountWritten = IntPtr.Zero;

            WriteProcessMemory(process.Handle, memory_adr, buffer, sizeof(int), out AmountWritten);
        }

        public void WriteDouble(IntPtr memory_adr, double value)
        {
            byte[] buffer = new byte[sizeof(double)];
            buffer = BitConverter.GetBytes(value);
            IntPtr AmountWritten = IntPtr.Zero;

            WriteProcessMemory(process.Handle, memory_adr, buffer, sizeof(double), out AmountWritten);
        }

        public void WriteFloat(IntPtr memory_adr, float value)
        {
            byte[] buffer = new byte[sizeof(float)];
            buffer = BitConverter.GetBytes(value);
            IntPtr AmountWritten = IntPtr.Zero;

            WriteProcessMemory(process.Handle, memory_adr, buffer, sizeof(float), out AmountWritten);
        }

        public void WriteString(IntPtr memory_adr, string value)
        {
            byte[] buffer = new byte[value.Length+1];
            buffer = ASCIIEncoding.ASCII.GetBytes(value);
            IntPtr AmountWritten = IntPtr.Zero;

            WriteProcessMemory(process.Handle, memory_adr, buffer, value.Length + 1, out AmountWritten);
        }


    }
}
