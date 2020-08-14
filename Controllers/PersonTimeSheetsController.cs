using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EmpTimeSheet.Models;
using System.Net.Http;
using Microsoft.AspNetCore.Cors;
using System.Web.Http;
using System.Text.Json;

namespace EmpTimeSheet.Controllers
{
    [Microsoft.AspNetCore.Mvc.Route("api/PersonTimeSheet")]
    [ApiController]
    [EnableCors]
    public class PersonTimeSheetsController : ControllerBase
    {
        private readonly LawInOrderDBContext _context;

        public PersonTimeSheetsController(LawInOrderDBContext context)
        {
            _context = context;
        }
        [Microsoft.AspNetCore.Mvc.HttpGet]
        [Microsoft.AspNetCore.Mvc.Route("getPersonsList")]
        public ActionResult<List<Persons>> GetPersonsList()
        {
            List<Persons> list = new List<Persons>();
            var obj = (from p in _context.Persons
                       select new
                       {
                           p.Pid,
                           p.PersonName
                       }).ToList();
            foreach (var item in obj) {
                Persons p = new Persons();
                p.Pid=item.Pid;
                p.PersonName = item.PersonName;
                list.Add(p);
            }
            return list;
        }
        // GET: api/PersonTimeSheet/getEmpTimeSheet
        [Microsoft.AspNetCore.Mvc.HttpGet]
        [Microsoft.AspNetCore.Mvc.Route("getEmpTimeSheet")]
        public ActionResult<PersonTimeSheet> GetEmpTimeSheet()
        {
            List<PersonTimeSheet> list = new List<PersonTimeSheet>();
            var obj = (from pts in _context.PersonTimeSheet
                       join p in _context.Persons on pts.Pid equals p.Pid
                       select new
                       {
                           p.PersonName,
                           pts.Date,
                           pts.HoursWorked
                       }
                       ).ToList();
                return Ok(obj);
        }

        [Microsoft.AspNetCore.Mvc.HttpPost]
        [Microsoft.AspNetCore.Mvc.Route("savePersonTimeSheet")]
        public ActionResult<PersonTimeSheet> SavePersonTimeSheet([FromUri]TimeSaveModel personTimeSheet)
        {
            try
            {
                //var obj = JsonConvert.DeserializeObject(personTimeSheet);
                PersonTimeSheet P = new PersonTimeSheet();
                if (personTimeSheet != null)
                {
                    P.Pid = Convert.ToInt32(personTimeSheet.Pid.ToString());
                    P.Date = Convert.ToDateTime(personTimeSheet.Date.ToString());
                    P.HoursWorked = Convert.ToInt32(personTimeSheet.HoursWorked.ToString());
                }
                _context.PersonTimeSheet.Add(P);
                _context.SaveChanges();
            }
            catch (Exception ex) { }
            return Ok();
        }

   
    }
}
