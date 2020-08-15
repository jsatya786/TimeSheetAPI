using EmpTimeSheet.IRepository;
using EmpTimeSheet.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmpTimeSheet.Repository
{
    public class TimeSheet : ITimeSheet
    {
        private readonly LawInOrderDBContext _context;
        public TimeSheet(LawInOrderDBContext context) {
            _context = context;
        }
        public List<TimeSheetList> GetEmpTimeSheet()
        {
            List<TimeSheetList> list = new List<TimeSheetList>();
            var obj = (from pts in _context.PersonTimeSheet
                       join p in _context.Persons on pts.Pid equals p.Pid
                       select new
                       {
                           p.PersonName,
                           pts.Date,
                           pts.HoursWorked
                       }
                       ).ToList();
            foreach (var item in obj)
            {
                TimeSheetList p = new TimeSheetList();
                p.PersonName = item.PersonName;
                p.Date = item.Date;
                p.HoursWorked = item.HoursWorked;
                list.Add(p);
            }
            return list;
        }

        public List<Persons> GetPersonsList()
        {
            List<Persons> list = new List<Persons>();
            try
            {
                var obj = (from p in _context.Persons
                           select new
                           {
                               p.Pid,
                               p.PersonName
                           }).ToList();
                foreach (var item in obj)
                {
                    Persons p = new Persons();
                    p.Pid = item.Pid;
                    p.PersonName = item.PersonName;
                    list.Add(p);
                }                
            }
            catch (Exception ex) { }
            return list;
        }

        public bool SavePersonTimeSheet(TimeSaveModel personTimeSheet)
        {
            bool flag=false;
            try
            {
                PersonTimeSheet P = new PersonTimeSheet();
                if (personTimeSheet != null)
                {
                    P.Pid = Convert.ToInt32(personTimeSheet.Pid.ToString());
                    P.Date = Convert.ToDateTime(personTimeSheet.Date.ToString());
                    P.HoursWorked = Convert.ToInt32(personTimeSheet.HoursWorked.ToString());
                }
                _context.PersonTimeSheet.Add(P);
                _context.SaveChanges();
                flag = true;
            }
            catch (Exception ex) { }
            return flag;
        }
    }
}
