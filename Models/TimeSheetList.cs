using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmpTimeSheet.Models
{
    public class TimeSheetList
    {
        public string PersonName { get; set; }
        public DateTime Date { get; set; }
        public int HoursWorked { get; set; }
    }
}
