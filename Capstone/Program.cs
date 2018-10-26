using Capstone;
using System;

namespace capstone
{
    class Program
    {
        const string DatabaseConnection = @"Data Source=.\sqlexpress;Initial Catalog=EmployeeDB;Integrated Security=True";

        static void Main(string[] args)
        {
            ReservationMenuCLI mainMenu = new ReservationMenuCLI();
            mainMenu.Display();
        }
    }
}
