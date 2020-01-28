using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using FinalProject.Models;
using System.Web.Http.Cors;

namespace FinalProject.Controllers
{

    public class UsersController : BaseController
    {
        MyLoggerLib.ILogger logger = MyLoggerLib.LoggerFactory.GetLogger(1);
        Response response = new Response();
        FinalprojectdbEntities dalobj = new FinalprojectdbEntities();
        

        UsersController()
        {
            dalobj.Configuration.ProxyCreationEnabled = false;
        }

        // GET: api/Users
        public Response Get()
        {
            List<T_Users> list = dalobj.T_Users.ToList();
            response.Data = list;
            response.Status = "success";
            response.Err = null;
            logger.Log("User List Displayed");
            return response;

        }

        // GET: api/Users/5
        public Response Get(int id)
        {
            T_Users userToFind = dalobj.T_Users.Find(id);
            response.Data = userToFind;
            response.Status = "success";
            response.Err = null;
            logger.Log("Specific User Displayed using UserId");
            return response;
        }

        // POST: api/Users
        public Response Post([FromBody]T_Users user)
        {
            if (user != null)
            {
                dalobj.T_Users.Add(user);
                dalobj.SaveChanges();
                logger.Log("User Added in Db");
                response.Status = "success";

                return response;
            }
            else
            {
                response.Status = "Failed";
                return response;
            }

        }

        // PUT: api/Users/5
        public Response Put(int id, [FromBody]T_Users user)
        {
            if (user != null)
            {

                T_Users userToBeUpdated = dalobj.T_Users.Find(id);

                if (user.Name != null)
                    userToBeUpdated.Name = user.Name;
                if (user.EmailId != null)
                    userToBeUpdated.EmailId = user.EmailId;
                if (user.Password != null)
                    userToBeUpdated.Password = user.Password;
                if (user.MobileNo != null)
                    userToBeUpdated.MobileNo = user.MobileNo;
                //userToBeUpdated.RoleId = user.RoleId;
                if (user.IsLocked == true)
                {
                    userToBeUpdated.IsLocked = user.IsLocked;
                }
                else { userToBeUpdated.IsLocked = user.IsLocked; }
                dalobj.SaveChanges();

                response.Status = "success";
                response.Err = null;
                logger.Log("User updated.");

                return response;
            }
            else
            {
                response.Status = "Failed";
                return response;
            }
        }

        // DELETE: api/Users/5
        public Response Delete(int id)
        {
            T_Users userToBeDeleted = dalobj.T_Users.Find(id);
            dalobj.T_Users.Remove(userToBeDeleted);
            dalobj.SaveChanges();
            logger.Log("User Deleted");
            response.Status = "success";

            return response;
        }
    }
}
