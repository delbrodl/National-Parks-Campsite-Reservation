using Capstone.DAL;
using Capstone.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace Capstone.Tests
{
    [TestClass]
    public class ParkDALTests : NPReservationDBDALTest
    {
        [TestMethod]
        public void GetAllAvailableParks_Test()
        {
            // Arrange
            ParkSQLDAL dal = new ParkSQLDAL(_connectionString);

            // Act
            IList<Park> parks = dal.GetAllAvailableParks();

            // Assert 
            Assert.AreEqual(1, parks.Count);
        }
    }
}
