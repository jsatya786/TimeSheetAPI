using EmpTimeSheet.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmpTimeSheet.IRepository
{
    public interface ITimeSheet
    {
        public List<Persons> GetPersonsList();
        public List<TimeSheetList> GetEmpTimeSheet();
        public bool SavePersonTimeSheet(TimeSaveModel personTimeSheet);
    }
}
