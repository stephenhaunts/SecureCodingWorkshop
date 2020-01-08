using System;
using System.Runtime.InteropServices;
using System.Security;

namespace SecureCodingWorkshop.String
{
    static class Program
    {

        // https://docs.microsoft.com/en-us/dotnet/api/system.security.securestring?view=netcore-3.1
        static void Main()
        {
            // Instantiate the secure string.
            SecureString securePwd = new SecureString();
            ConsoleKeyInfo key;

            Console.Write("Enter password: ");
            do
            {
                key = Console.ReadKey(true);

                // Ignore any key out of range.
                if (((int)key.Key) >= 65 && ((int)key.Key <= 90))
                {
                    // Append the character to the password.
                    securePwd.AppendChar(key.KeyChar);
                    Console.Write("*");
                }
                // Exit if Enter key is pressed.
            } while (key.Key != ConsoleKey.Enter);
            Console.WriteLine();

            try
            {
                //Process.Start("Notepad.exe", "MyUser", securePwd, "MYDOMAIN");
            }
            //catch (Win32Exception e)
            //{
            //    Console.WriteLine(e.Message);
            //}
            finally
            {
                securePwd.Dispose();
            }

        }

    }
}
