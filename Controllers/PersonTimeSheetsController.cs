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

namespace EmpTimeSheet.Controllers
{
    [Route("api/PersonTimeSheet")]
    [ApiController]
    [EnableCors]
    public class PersonTimeSheetsController : ControllerBase
    {
        private readonly LawInOrderDBContext _context;

        public PersonTimeSheetsController(LawInOrderDBContext context)
        {
            _context = context;
        }
        [HttpGet]
        [Route("getPersonsList")]
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
        [HttpGet]
        [Route("getEmpTimeSheet")]
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

        [HttpPost]
        [Route("savePersonTimeSheet")]
        public async Task<ActionResult<PersonTimeSheet>> SavePersonTimeSheet(PersonTimeSheet personTimeSheet)
        {
            //_context.personTimeSheet.Add(personTimeSheet);
            await _context.SaveChangesAsync();

            return Ok();
        }

   
    }
}
