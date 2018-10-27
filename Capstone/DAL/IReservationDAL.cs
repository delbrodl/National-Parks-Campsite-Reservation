using System;
using System.Collections.Generic;
using System.Text;
using Capstone.Models;

namespace Capstone.DAL
{
    public interface IReservationDAL
    {
        // IList<Reservation> GetReservations(int siteId);
        int BookReservation(Reservation reservation);
    }
}
