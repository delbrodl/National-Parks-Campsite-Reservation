using Capstone.DAL;
using Capstone.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace Capstone.Tests
{
    [TestClass]
    public class ReservationDALTests : NPReservationDBDALTest
    {
        [TestMethod]
        public void BookReservation_Test()
        {
            ReservationSQLDAL dal = new ReservationSQLDAL(_connectionString);
            int initialCount = GetRowCount("reservation");
            Reservation res = new Reservation()
            {
                SiteId = SiteId,
                Name = "Jason",
                FromDate = Convert.ToDateTime("09/30/2018"),
                ToDate = Convert.ToDateTime("10/05/2018"),
                CreateDate = Convert.ToDateTime("09/01/2018")
            };

            dal.BookReservation(res);

            int finalCount = GetRowCount("reservation");

            Assert.AreEqual(initialCount + 1, finalCount);
        }
    }
}
