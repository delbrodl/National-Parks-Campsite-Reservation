using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Capstone
{
    public class CLIHelper
    {
        public static string GetCleanSelectionInput(string input)
        {
            input = "";

            while (input.Length == 0)
            {
                input = Console.ReadLine();
            }
            input = input.Substring(0, 1).ToUpper();
            return input;
        }

        public static string GetCleanNameInput(string input)
        {
            input = "";

            while (input.Length == 0)
            {
                input = Console.ReadLine();
            }
            return input;
        }

        public static string GetCleanSelectionInput(string input, string escapeSq)
        {
            int intResult;
            input = "";

            while (input.Length == 0 || ((!int.TryParse(input, out intResult)) && input != escapeSq))
            {
                input = Console.ReadLine();
                if (!int.TryParse(input, out intResult))
                {
                    input.Substring(0, 1).ToUpper();
                }
            }
            return input;
        }

        public static void WordWrap(string paragraph)
        {
            paragraph = new Regex(@" {2,}").Replace(paragraph.Trim(), @" ");
            var left = Console.CursorLeft; var top = Console.CursorTop; var lines = new List<string>();
            for (var i = 0; paragraph.Length > 0; i++)
            {
                lines.Add(paragraph.Substring(0, Math.Min(Console.WindowWidth, paragraph.Length)));
                var length = lines[i].LastIndexOf(" ", StringComparison.Ordinal);
                if (length > 0) lines[i] = lines[i].Remove(length);
                paragraph = paragraph.Substring(Math.Min(lines[i].Length + 1, paragraph.Length));
                Console.SetCursorPosition(left, top + i); Console.WriteLine(lines[i]);
            }
        }

        public static void PrintHeader()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("National Park Campsite Reservation");
            Console.ResetColor();
        }

        public static string GetCleanDateInput(string input)
        {
            DateTime dateValue = DateTime.MinValue;

            do
            {
                input = "";
                while (input.Length == 0)
                {
                    input = Console.ReadLine();
                }
            } while (!DateTime.TryParse(input, out dateValue));
            return input;
        }
    }
}
