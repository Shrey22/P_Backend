using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using FinalProject.Models;

namespace FinalProject.Controllers
{
    public class SamplePaperController : BaseController
    {
        Response response = new Response();
        FinalprojectdbEntities dalobj = new FinalprojectdbEntities();
        MyLoggerLib.ILogger logger = MyLoggerLib.LoggerFactory.GetLogger(1);

        SamplePaperController()
        {
            dalobj.Configuration.ProxyCreationEnabled = false;
        }

        // GET: api/SamplePaper
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/SamplePaper/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/SamplePaper
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/SamplePaper/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/SamplePaper/5
        public void Delete(int id)
        {
        }
    }
}
