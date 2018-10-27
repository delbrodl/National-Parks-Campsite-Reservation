using System;
using System.Collections.Generic;
using System.Text;
using Capstone.DAL;
using Capstone.Models;

namespace Capstone
{
    public class ParkInfoCLI
    {
        public void Display(Park park)
        {
            while (true)
            {
                CLIHelper.PrintHeader();
                Console.WriteLine($"{park.Name} National Park");
                Console.WriteLine($@"Location:          {park.Location}");
                Console.WriteLine($@"Established:       {park.EstablishedDate.ToString("MM/dd/yyyy")}");
                Console.WriteLine($@"Area:              {park.Area.ToString("N0")} sq km");
                Console.WriteLine($@"Annual Visitors:   {park.Visitors.ToString("N0")}");
                Console.WriteLine();
                CLIHelper.WordWrap(park.Description);

                Console.WriteLine();
                Console.WriteLine("Select a Command");
                Console.WriteLine("1] >> View Campgrounds");
                Console.WriteLine("2] >> Search for Reservation");
                Console.WriteLine("Q] >> Return to Previous Screen");
                Console.Write("Enter selection: ");

                string input = string.Empty;
                while (input.Length == 0)
                {
                    input = Console.ReadLine();
                }

                input = input.Substring(0, 1).ToUpper();

                if (input == "1")
                {
                    Console.Clear();
                    CampgroundCLI campgroundMenu = new CampgroundCLI();
                    campgroundMenu.DisplayCampgrounds(park);
                }
                else if (input == "2")
                {
                    // Console.Clear();
                }
                else if (input == "Q" || input == "q")
                {
                    Console.WriteLine("Quitting");
                    break;
                }
                else
                {
                    Console.WriteLine("Please try again");
                }

                Console.Clear();
            }
        }
    }
}
