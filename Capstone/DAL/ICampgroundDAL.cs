﻿using System;
using System.Collections.Generic;
using System.Text;
using Capstone.Models;

namespace Capstone.DAL
{
    public interface ICampgroundDAL
    {
        IList<Campground> GetAllCampgrounds(int parkId);
    }
}
