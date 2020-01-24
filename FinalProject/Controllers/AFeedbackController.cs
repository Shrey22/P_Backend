using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using FinalProject.Models;

namespace FinalProject.Controllers
{
    public class AFeedbackController : BaseController
    {
        Response response = new Response();
        FinalprojectdbEntities dalobj = new FinalprojectdbEntities();
        MyLoggerLib.ILogger logger = MyLoggerLib.LoggerFactory.GetLogger(1);

        AFeedbackController()
        {
            dalobj.Configuration.ProxyCreationEnabled = false;
           // dalobj.Configuration.ProxyCreationEnabled = true;
        }

        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("api/Question/AFeedback/onlysubmiteduser")]
        // GET: api/AFeedback
        public Response Get1()
        {
            try
            {
                List<T_Feedback> feedbacks = dalobj.T_Feedback.ToList();
                List<T_Users> users = dalobj.T_Users.ToList();

                var output = (from fd in feedbacks
                              join u in users on fd.UserId_ equals u.UserId
                              select new { fd, u.Name });


                response.Data = output;
                response.Status = "success";
                response.Err = null;
                logger.Log("Feedback list displayed");
                return response;
            }
            catch (Exception cause)
            {
                response.Data = cause.Message;
                response.Status = "Failed";
                response.Err = cause;
                logger.Log("Exception occured returned Error msg");
                return response;
            }
        }


        // GET: api/AFeedback
        public Response Get()
        {
            try
            {
                List<T_Feedback> list = dalobj.T_Feedback.ToList();
                response.Data = list;
                response.Status = "success";
                response.Err = null;
                logger.Log("Feedback list displayed");
                return response;
            }
            catch (Exception cause)
            {
                response.Data = cause.Message;
                response.Status = "Failed";
                response.Err = cause;
                logger.Log("Exception occured returned Error msg");
                return response;
            }
        }

        // GET: api/AFeedback/5
        public Response Get(int id)
        {
            try
            {
                T_Feedback fdbkToFind = dalobj.T_Feedback.Find(id);
                if (fdbkToFind != null)
                {
                    response.Data = fdbkToFind;
                    response.Status = "success";
                    response.Err = null;
                    logger.Log("specific feedback displayed using id");
                    return response;
                }
                else
                {
                    response.Data = null;
                    response.Status = "failed";
                    response.Err = null;
                    logger.Log("specific feedback not found using id");
                    return response;
                }
                
            }
            catch (Exception cause)
            {
                response.Data = cause.Message;
                response.Status = "Failed";
                response.Err = cause;
                logger.Log("Exception occured returned Error msg");
                return response;
            }
        }

        // POST: api/AFeedback
        public Response Post([FromBody]T_Feedback fdbk)
        {
            try
            {
                if (fdbk != null)                    // afterwards check valid subject or not so if/else loop ...
                {
                    dalobj.T_Feedback.Add(fdbk);
                    dalobj.SaveChanges();
                    response.Data = null;
                    response.Status = "success";
                    response.Err = null;
                    logger.Log("feedback added in db");
                    return response;
                }
                else
                {
                    response.Data = null;
                    response.Status = "Empty fields";
                    response.Err = null;
                    logger.Log("feedback insertion failed due to empty fields");
                    return response;
                }
            }

            catch (Exception cause)
            {
                response.Data = cause.Message;
                response.Status = "Failed";
                response.Err = cause;
                logger.Log("Exception occured returned Error msg");
                return response;
            }
        }

        // PUT: api/AFeedback/5
        public Response Put(int id, [FromBody] T_Feedback fdbk)
        {
            try
            {
                T_Feedback fdbkToFind = dalobj.T_Feedback.Find(id);
                if (fdbkToFind != null)
                {
                    fdbkToFind.UserId_ = fdbk.UserId_;
                    fdbkToFind.Message = fdbk.Message;
                    dalobj.SaveChanges();

                    response.Data = fdbkToFind;
                    response.Status = "success";
                    response.Err = null;
                    logger.Log("Specific Feedback updated using Id");
                    return response;
                }
                else
                {
                    response.Data = null;
                    response.Status = "failed";
                    response.Err = null;
                    logger.Log("Something wentr wrong!");
                    return response;
                }

            }
            catch (Exception cause)
            {
                response.Data = cause.Message;
                response.Status = "Failed";
                response.Err = cause;
                logger.Log("Exception occured returned Error msg");
                return response;
            }
        }

        // DELETE: api/AFeedback/5
        public Response Delete(int id)
        {
            try
            {
                T_Feedback fdbkToFind = dalobj.T_Feedback.Find(id);

                if (fdbkToFind != null)
                {
                    dalobj.T_Feedback.Remove(fdbkToFind);
                    dalobj.SaveChanges();

                    response.Data = null;
                    response.Status = "success";
                    response.Err = null;
                    logger.Log("Specific Feedback deleted using Id");
                    return response;
                }
                else
                {
                    response.Data = null;
                    response.Status = "failed";
                    response.Err = null;
                    logger.Log("Something wentr wrong!");
                    return response;
                }

            }
            catch (Exception cause)
            {
                response.Data = cause.Message;
                response.Status = "Failed";
                response.Err = cause;
                logger.Log("Exception occured returned Error msg");
                return response;
            }
        }
    }
}
