using System;
using System.Web;
using System.Web.Http;

namespace EmployeeService.Controllers
{
    public class SessionController : ApiController
    {
        // GET api/session
        //public IEnumerable<string> Get()
        public string Get()
        {

            var session = HttpContext.Current.Session;
            if (session != null)
            {
                if (session["Time"] == null)
                    session["Time"] = DateTime.Now;
                return "Session Time: " + session["Time"];
            }
            return "Session is not availabe";
            //return new string[] { "value1", "value2" };
        }

        // GET api/session/5
        public string Get(int id)
        {
            var session = HttpContext.Current.Session;
            if (session != null)
            {
                if (session["Time"] == null)
                    session["Time"] = DateTime.Now;
                return "Session Time: " + session["Time"] + id;
            }
            return "Session is not availabe" + id;
        }

        // POST api/values
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }
    }
}
