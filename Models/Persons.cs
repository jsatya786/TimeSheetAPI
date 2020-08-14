using System;
using System.Collections.Generic;

namespace EmpTimeSheet.Models
{
    public partial class Persons
    {
        public Persons()
        {
            PersonTimeSheet = new HashSet<PersonTimeSheet>();
        }

        public int Pid { get; set; }
        public string PersonName { get; set; }

        public virtual ICollection<PersonTimeSheet> PersonTimeSheet { get; set; }
    }
}
