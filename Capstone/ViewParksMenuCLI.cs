using System;
using System.Collections.Generic;
using System.Text;
using Capstone.DAL;
using Capstone.Models;

namespace Capstone
{
    public class ViewParksMenuCLI
    {
        private const string DatabaseConnection = @"Data Source=.\sqlexpress;Initial Catalog=NPCampsite;Integrated Security=True";

        public void DisplayAllParks()
        {
            IParkDAL parkDAL = new ParkSQLDAL(DatabaseConnection);
            IList<Park> parks = parkDAL.GetAllAvailableParks();

            while (true)
            {
                CLIHelper.PrintHeader();

                Console.WriteLine();
                Console.WriteLine("Select a park for further details");
                for (int i = 0; i < parks.Count; i++)
                {
                    Console.WriteLine($"{i + 1}] >> {parks[i].Name} National Park");
                }

                Console.WriteLine("Q] >> Quit");

                Console.Write("Enter selection: ");
                string input = string.Empty;
                input = CLIHelper.GetCleanSelectionInput(input);

                for (int i = 0; i < parks.Count; i++)
                {
                    if (input == (i + 1).ToString())
                    {
                        Console.Clear();
                        ParkInfoCLI reserveMenu = new ParkInfoCLI();
                        reserveMenu.Display(parks[i]);
                     }
                }

                Console.Clear();

                if (input == "Q")
                {
                    break;
                }
            }
        }
    }
}
