using System;
using System.Collections.Generic;
using System.Text;
using Capstone.Models;

namespace Capstone.DAL
{
    public interface ISiteDAL
    {
        IList<Site> GetAvailableSites(int campgroundId, DateTime startDate, DateTime endDate);
    }
}
