using Capstone.DAL;
using Capstone.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace Capstone.Tests
{
    [TestClass]
    public class SiteDALTests : NPReservationDBDALTest
    {
        [TestMethod]
        public void GetAllAvailableSites_Test()
        {
            SiteSQLDAL dal = new SiteSQLDAL(_connectionString);
            CampgroundSQLDAL dal2 = new CampgroundSQLDAL(_connectionString);

            IList<Site> sites = dal.GetAvailableSites(CampgroundId, Convert.ToDateTime("10/10/2018"), Convert.ToDateTime("10/15/2018"));

            Assert.AreEqual(1, sites.Count);
        }
    }
}
