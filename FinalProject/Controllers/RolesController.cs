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
    
    public class RolesController : BaseController
    {
        MyLoggerLib.ILogger logger = MyLoggerLib.LoggerFactory.GetLogger(1);
        FinalprojectdbEntities dalobj = new FinalprojectdbEntities();
        Response response = new Response();

        RolesController()
        {
            dalobj.Configuration.ProxyCreationEnabled = false;
        }

        // GET: api/Roles
        public Response Get()
        {
            List<T_Roles> ro = dalobj.T_Roles.ToList();
            response.Data = ro;
            response.Status = "Success";
            response.Err = null;
            logger.Log("Diaplayed Roles");
            return response;
        }

        // GET: api/Roles/5
        public Response Get(int id)
        {
            T_Roles roleToFind = dalobj.T_Roles.Find(id);
            response.Data = roleToFind;
            response.Status = "success";
            response.Err = null;
            logger.Log(" Displayed Specify Role using roleid");
            return response;
        }

        // POST: api/Roles
        public Response Post([FromBody]T_Roles role)
        {
            //dalobj.T_Roles.Add(role);
            //dalobj.SaveChanges();
            try
            {
                dalobj.proc_AddRole(role.RoleName);
                response.Err = null;
                response.Status = "success";
                response.Data = null;
                logger.Log("Role Added in db");

                return response;

            }
            catch(Exception cause)
            {
                response.Err = cause;
                response.Status = "Failed. cause : "+cause.Message;
                response.Data = null;
                logger.Log("Exception occured while inserting Role db");
                return response;
            }
           
            
        }

        // PUT: api/Roles/5
        public Response Put(int id, [FromBody]T_Roles role)
        {
            //T_Roles roleToBeUpdated =  dalobj.T_Roles.Find(id);
            // roleToBeUpdated.RoleName = role.RoleName;
            // dalobj.SaveChanges();
            try
            {
                dalobj.proc_ModifyRole(id, role.RoleName);
                logger.Log("Role updated.");
                response.Status = "success";
                return response;
            }
            catch (Exception cause)
            {
                response.Err = cause;
                response.Status = "Failed. cause : " + cause.Message;
                response.Data = null;
                logger.Log("Exception occured while updating Role db");
                return response;
            }

        }

        // DELETE: api/Roles/5
        public Response Delete(int id)
        {
            //T_Roles roleToBeDeleted = dalobj.T_Roles.Find(id);
            //dalobj.T_Roles.Remove(roleToBeDeleted);
            //dalobj.SaveChanges();
            try
            {
                dalobj.proc_RemoveRole(id);
                logger.Log("Role deleted.");
                response.Status = "success";
                return response;
            }
            catch (Exception cause)
            {
                response.Err = cause;
                response.Status = "Failed. cause : " + cause.Message;
                response.Data = null;
                logger.Log("Exception occured while deleting Role db");
                return response;
            }
        }
    }
}
