using System;
using System.Collections.Generic;
using System.Text;

namespace Capstone
{
    class ReservationMenuCLI
    {
        public void Display()
        {
            while (true)
            {
                PrintHeader();

                Console.WriteLine();
                Console.WriteLine("Main Menu");
                Console.WriteLine("1] >> View Campgrounds");
                Console.WriteLine("2] >> Book Reservation");
                Console.WriteLine("Q] >> Return to View of Parks");

                Console.Write("What option do you want to select? ");
                string input = Console.ReadLine();
                Console.WriteLine();

                if (input == "1")
                {

                    //foreach (Item item in vm.Items)
                    //{
                    //    Console.WriteLine($"{item.SlotID}: {item.Name} has {item.Quantity} left");
                    //}
                }
                else if (input == "2")
                {
                    //PurchaseMenuCLI submenu = new PurchaseMenuCLI(vm);
                    //Console.WriteLine();
                    //submenu.Display();
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

                Console.ReadLine();
                Console.Clear();
            }
        }

        private void PrintHeader()
        {
            Console.WriteLine("National Park Campsite Reservation");
        }
    }
}
