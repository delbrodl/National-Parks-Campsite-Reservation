using Capstone.DAL;
using System;
using System.Collections.Generic;
using System.Text;
using Capstone.Models;

namespace Capstone
{
    public class CampgroundCLI
    {
        const string DatabaseConnection = @"Data Source=.\sqlexpress;Initial Catalog=NPCampsite;Integrated Security=True";

        public void DisplayCampgrounds(Park park)
        {
            ICampgroundDAL campDAL = new CampgroundSQLDAL(DatabaseConnection);
            IList<Campground> campgrounds = campDAL.GetAllCampgrounds(park.ParkId);

            while (true)
            {
                CLIHelper.PrintHeader();
                DisplayList(park, campgrounds);

                Console.WriteLine();

                Console.WriteLine("Select a Command");
                Console.WriteLine("1] >> Search for Availale Reservation");
                Console.WriteLine("Q] >> Return to Previous Screen");
                Console.Write("Enter input: ");

                string input = "";
                input = CLIHelper.GetCleanSelectionInput(input);


                if (input == "1")
                {
                    Console.Clear();
                    RunReservationInput(park, campgrounds);
                    Console.Clear();
                }
                else if (input == "Q")
                {
                    Console.Clear();
                    break;
                }
                Console.Clear();
            }


        }

        private void RunReservationInput(Park park, IList<Campground> campgrounds)
        {
            Campground campgroundToPass = new Campground();
            IList<Site> sites = new List<Site>();
            ISiteDAL siteDal = new SiteSQLDAL(DatabaseConnection);
            DateTime fromDate = new DateTime();
            DateTime toDate = new DateTime();

            while (true)
            {
                DisplayList(park, campgrounds);
                Console.Write("Which campground (enter Q to cancel): ");
                string input = "";
                input = CLIHelper.GetCleanSelectionInput(input);

                foreach (Campground campground in campgrounds)
                {
                    if (input == campground.CampgroundId.ToString())
                    {
                        campgroundToPass = campground;
                        break;
                    }
                }
                if (input == "Q")
                {
                    break;
                }

                Console.Write("Enter arrival date (MM/DD/YYYY): ");
                fromDate = DateTime.Parse(CLIHelper.GetCleanDateInput(input));

                Console.Write("Enter departure date (MM/DD/YYYY): ");
                toDate = DateTime.Parse(CLIHelper.GetCleanDateInput(input));

                if (toDate > fromDate)
                {
                    sites = siteDal.GetAvailableSites(campgroundToPass.CampgroundId, fromDate, toDate);

                    decimal cost = campgroundToPass.DailyFee * (int)(toDate - fromDate).TotalDays;

                    Reservation reservation = new Reservation();
                    reservation.ToDate = toDate;
                    reservation.FromDate = fromDate;
                    reservation.CreateDate = DateTime.Now;

                    RunResultsMatchingSearchCriteria(sites, cost, reservation);
                }
                else
                {
                    Console.WriteLine("Departure date must be after your arrival date.");
                }

                Console.Clear();
            }
        }

        private void RunResultsMatchingSearchCriteria(IList<Site> sites, decimal cost, Reservation reservation)
        {
            IReservationDAL resDAL = new ReservationSQLDAL(DatabaseConnection);
            while (true)
            {
                string input = "";
                bool askName = false;

                Console.WriteLine("Results Mathcing Your Search Criteria");
                DisplayList(sites, cost);

                Console.WriteLine();
                Console.WriteLine("Which site should be reserved (enter Q to cancel || Enter site Number): ");
                input = CLIHelper.GetCleanSelectionInput(input, "Q");
                foreach (Site site in sites)
                {
                    if (site.SiteNumber.ToString() == input)
                    {
                        reservation.SiteId = site.SiteId;
                        askName = true;
                    }
                }
                if (input == "Q")
                {
                    break;
                }

                if (askName)
                {
                    Console.Write("Enter the name of the reservation: ");
                    input = CLIHelper.GetCleanNameInput(input);

                    reservation.Name = input;

                    int confirmationId = resDAL.BookReservation(reservation);

                    Console.WriteLine($"The reservation has been made and the confirmation id is {confirmationId}");
                    Console.WriteLine("Press any key to continue...");
                    Console.ReadKey();
                    Console.Clear();
                    break;
                }

            }
        }

        private static void DisplayList(Park park, IList<Campground> campgrounds)
        {
            Console.WriteLine($"{park.Name} National Park Campgrounds");

            Console.WriteLine($@"{"".PadRight(6, ' ')}{"Name".PadRight(35, ' ')}{"Open".PadRight(20, ' ')}{"Close".PadRight(20, ' ')}{"Daily Fee"}");
            foreach (Campground campground in campgrounds)
            {
                Console.WriteLine($@"#{campground.CampgroundId.ToString().PadRight(5, ' ')}{campground.Name.ToString().PadRight(35, ' ')}{campground.MonthNumberToName(campground.OpenFromMM).ToString().PadRight(20, ' ')}{campground.MonthNumberToName(campground.OpenToMM).ToString().PadRight(20, ' ')}{campground.DailyFee.ToString("C2")}");
            }
        }

        private static void DisplayList(IList<Site> sites,  decimal cost)
        {
            Dictionary<bool, string> accessibleYesNo = new Dictionary<bool, string>();
            accessibleYesNo[true] = "Yes";
            accessibleYesNo[false] = "No";
            Console.WriteLine($@"{"Site No.".PadRight(10, ' ')}{"Max Occup.".PadRight(20, ' ')}{"Accessible?".PadRight(30, ' ')}{"Max Rv Length".PadRight(20, ' ')}{"Utility".PadRight(20, ' ')}{"Cost"}");

            foreach(Site site in sites)
            {
                Console.Write($@"{site.SiteNumber.ToString().PadRight(10, ' ')}{site.MaxOccupancy.ToString().PadRight(20, ' ')}{accessibleYesNo[site.Accessible].ToString().PadRight(30, ' ')}");

                string notApplicable = "N/A";
                if (site.MaxRVLength == 0)
                {
                    Console.Write($@"{notApplicable.PadRight(20, ' ')}");
                }
                else
                {
                    Console.Write($@"{site.MaxRVLength.ToString().PadRight(20, ' ')}");
                }
                if (site.Utilities == false)
                {
                    Console.Write($@"{notApplicable.PadRight(20, ' ')}");
                }
                else
                {
                    Console.Write($@"{accessibleYesNo[site.Utilities].ToString().PadRight(20, ' ')}");
                }

                Console.WriteLine($@"{cost.ToString("C2")}");

            }
        }
    }
}
