using Capstone;
using System;

namespace capstone
{
    class Program
    {
        public const string DatabaseConnection = @"Data Source=.\sqlexpress;Initial Catalog=NPCampsite;Integrated Security=True";

        static void Main(string[] args)
        {
            ViewParksMenuCLI mainMenu = new ViewParksMenuCLI();
            mainMenu.DisplayAllParks();
        }
    }
}
