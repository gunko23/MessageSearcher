using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MessageSearchAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessagesController : ControllerBase
    {
        private Context _context;
        public MessagesController(Context context)
        {
            _context = context;

        }
        [HttpGet("all")]
        public IActionResult GetAll()
        {

            var response = _context.Messages.Where(x => x.UserId == "115804850478105112081").ToList();
            return Ok(response);
        }

        [HttpGet("contacts")]
        public IActionResult GetContacts()
        {

            var response = _context.Contacts.ToList();
            return Ok(response);
        }

        [HttpGet("contacts/{search}")]
        public IActionResult GetContacts(string search )
        {

            var response = _context.Messages.Where(x => x.Message.Contains(search)).OrderBy(x => x.Date).ToList();
            return Ok(response);
        }

        [HttpGet("date/{date1}/{date2}")]
        public IActionResult FilterByDate(string date1, string date2)
        {
            var array1 = date1.Split("-");
            var parsedDate1 = new DateTime(Int32.Parse(array1[0]), Int32.Parse(array1[1]), Int32.Parse(array1[2]));
            var array2 = date2.Split("-");
            var parsedDate2 = new DateTime(Int32.Parse(array2[0]), Int32.Parse(array2[1]), Int32.Parse(array2[2]));
            var response = _context.Messages.Where(x => x.Date >= parsedDate1).Where(x => x.Date <= parsedDate2).OrderBy(x => x.Date).ToList();
            return Ok(response);
        }

    }
}
