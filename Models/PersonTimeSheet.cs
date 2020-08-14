using System;
using System.Collections.Generic;

namespace EmpTimeSheet.Models
{
    public partial class PersonTimeSheet
    {
        public int Id { get; set; }
        public int Pid { get; set; }
        public DateTime Date { get; set; }
        public int HoursWorked { get; set; }

        public virtual Persons P { get; set; }
    }
}
