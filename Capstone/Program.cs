using Capstone;
using System;

namespace Capstone
{
    public class Program
    {
        public const string DatabaseConnection = @"Data Source=.\sqlexpress;Initial Catalog=NPCampsite;Integrated Security=True";

        private static void Main(string[] args)
        {
            ViewParksMenuCLI mainMenu = new ViewParksMenuCLI();
            mainMenu.DisplayAllParks();
        }
    }
}
