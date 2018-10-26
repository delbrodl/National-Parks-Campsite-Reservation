using System;
using System.Collections.Generic;
using System.Text;

namespace Capstone.Models
{
    public class Campground
    {
        public int CampgroundId { get; set; }

        public int ParkId { get; set; }

        public string Name { get; set; }

        public int OpenFromMM { get; set; } 

        public int OpenToMM { get; set; }

        public decimal DailyFee { get; set; }

        public string MonthNumberToName(int monthNumber)
        {
            Dictionary<int, string> monthNumberDic = new Dictionary<int, string>();
            monthNumberDic[1] = "January";
            monthNumberDic[2] = "February";
            monthNumberDic[3] = "March";
            monthNumberDic[4] = "April";
            monthNumberDic[5] = "May";
            monthNumberDic[6] = "June";
            monthNumberDic[7] = "July";
            monthNumberDic[8] = "August";
            monthNumberDic[9] = "September";
            monthNumberDic[10] = "October";
            monthNumberDic[11] = "November";
            monthNumberDic[12] = "December";

            if (monthNumber == this.OpenFromMM || monthNumber == this.OpenToMM)
            {
                return monthNumberDic[monthNumber];
            }
            else
            {
                return null;
            }
        }

    }
}
