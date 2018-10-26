using Capstone.DAL;
using Capstone.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace Capstone.Tests
{
    [TestClass]
    public class CampgroundDALTests : NPReservationDBDALTest
    {
        [TestMethod]
        public void GetAllCampgrounds_Test()
        {
            // Arrange
            CampgroundSQLDAL dal = new CampgroundSQLDAL(_connectionString);
            ParkSQLDAL dal2 = new ParkSQLDAL(_connectionString);

            // Act
            IList<Campground> campground = dal.GetAllCampgrounds(ParkId);

            // Assert 
            Assert.AreEqual(1, campground.Count);
        }
    }
}
