using EmployeeDataAccess;
using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Web.Http;
using System.Web.Http.Cors;

namespace EmployeeService.Controllers
{
    public class EmployeeController : ApiController
    {
        [HttpGet]
        [RequireHttps]
        [BasicAuthentication]
        public HttpResponseMessage LoadAllEmployeeList(string gender = "All")
        {
            string username = Thread.CurrentPrincipal.Identity.Name;

            using (EmployeeDBEntities entities = new EmployeeDBEntities())
            {
                //switch(gender.ToLower())
                switch (username.ToLower())
                {
                    case "all":
                        return Request.CreateResponse(HttpStatusCode.OK, entities.Employees.ToList());
                    case "male":
                        return Request.CreateResponse(HttpStatusCode.OK, entities.Employees.Where(e => e.Gender.Equals("male", StringComparison.OrdinalIgnoreCase)).ToList());
                    case "female":
                        return Request.CreateResponse(HttpStatusCode.OK, entities.Employees.Where(e => e.Gender.Equals("female", StringComparison.OrdinalIgnoreCase)).ToList());
                    default:
                        return Request.CreateErrorResponse(HttpStatusCode.BadRequest, $"Value not found");
                        //return Request.CreateErrorResponse(HttpStatusCode.BadRequest, $"Value of gender must be all, male, or female. ({gender}) is invalid.");
                };
            }
        }

        [HttpGet]
        public HttpResponseMessage LoadEmployeeByID(int id)
        {
            using (EmployeeDBEntities entities = new EmployeeDBEntities())
            {
                var entity = entities.Employees.FirstOrDefault(e => e.ID == id);

                if (entity != null)
                {
                    // if found, response message with status code 200 OK
                    return Request.CreateResponse(HttpStatusCode.OK, entity);
                }
                else
                {
                    // if not found, response message with status code 404 not found
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, $"Employee with ID:{id} Not Found");
                }
            }
        }

        [EnableCors("*", "*", "*")]
        public HttpResponseMessage Post([FromBody] Employee emp)
        {
            try
            {
                using (EmployeeDBEntities entities = new EmployeeDBEntities())
                {
                    entities.Employees.Add(emp);
                    entities.SaveChanges();

                    // Add the new response message with status code 201 created
                    var message = Request.CreateResponse(HttpStatusCode.Created, emp);
                    // Add the URI loaclion
                    message.Headers.Location = new Uri(Request.RequestUri + emp.ID.ToString());
                    return message;
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }

        }

        [DisableCors]
        public HttpResponseMessage Delete(int id)
        {
            try
            {
                using (EmployeeDBEntities entities = new EmployeeDBEntities())
                {
                    var entity = entities.Employees.FirstOrDefault(e => e.ID == id);
                    if (entity != null)
                    {
                        entities.Employees.Remove(entity);
                        entities.SaveChanges();
                        return Request.CreateResponse(HttpStatusCode.OK);
                    }
                    else
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, $"Employee With ID: {id} Not Found to delete");
                    }
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        public HttpResponseMessage Put([FromBody] int id, [FromUri] Employee emp)
        {
            using (EmployeeDBEntities entities = new EmployeeDBEntities())
            {
                try
                {
                    var entity = entities.Employees.FirstOrDefault(e => e.ID == id);
                    if (entity == null)
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, $"Employee With ID: {id} Not Found to update");
                    }
                    else
                    {
                        entity.FirstName = emp.FirstName;
                        entity.LastName = emp.LastName;
                        entity.Gender = emp.Gender;
                        entity.Salary = emp.Salary;
                        entities.SaveChanges();
                        return Request.CreateResponse(HttpStatusCode.OK, entity);
                    }
                }
                catch (Exception ex)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
                }
            }

        }

    }
}
