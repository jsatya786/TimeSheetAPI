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
using EmpTimeSheet.IRepository;

namespace EmpTimeSheet.Controllers
{
    [Microsoft.AspNetCore.Mvc.Route("api/PersonTimeSheet")]
    [ApiController]
    [EnableCors]
    public class PersonTimeSheetsController : ControllerBase
    {
        private readonly LawInOrderDBContext _context;
        private ITimeSheet iTimeSheet;
        public PersonTimeSheetsController(LawInOrderDBContext context,
            ITimeSheet _iTimeSheet)
        {
            this.iTimeSheet = _iTimeSheet;
            _context = context;
        }
        [Microsoft.AspNetCore.Mvc.HttpGet]
        [Microsoft.AspNetCore.Mvc.Route("getPersonsList")]
        public ActionResult<List<Persons>> GetPersonsList()
        {
            List<Persons> list = new List<Persons>();
            list = iTimeSheet.GetPersonsList();            
            return list;
        }
        // GET: api/PersonTimeSheet/getEmpTimeSheet
        [Microsoft.AspNetCore.Mvc.HttpGet]
        [Microsoft.AspNetCore.Mvc.Route("getEmpTimeSheet")]
        public ActionResult<List<TimeSheetList>> GetEmpTimeSheet()
        {
            List<TimeSheetList> list = new List<TimeSheetList>();
            list = iTimeSheet.GetEmpTimeSheet();
            return Ok(list);
        }

        [Microsoft.AspNetCore.Mvc.HttpPost]
        [Microsoft.AspNetCore.Mvc.Route("savePersonTimeSheet")]
        public void SavePersonTimeSheet([FromUri]TimeSaveModel personTimeSheet)
        {
            try
            {
                iTimeSheet.SavePersonTimeSheet(personTimeSheet);
            }
            catch (Exception ex) { }
        }   
    }
}
