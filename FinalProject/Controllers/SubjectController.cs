using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using FinalProject.Models;

namespace FinalProject.Controllers
{
    public class SubjectController : BaseController
    {
        Response response = new Response();
        FinalprojectdbEntities dalobj = new FinalprojectdbEntities();
        MyLoggerLib.ILogger logger = MyLoggerLib.LoggerFactory.GetLogger(1);

        SubjectController()
        {
            dalobj.Configuration.ProxyCreationEnabled = false;
           // dalobj.Configuration.ProxyCreationEnabled = true;
        }

        // GET: api/Subject
        public Response Get()
        {
            try
            {
                List<T_Subject> list = dalobj.T_Subject.ToList();
                response.Data = list;
                response.Status = "success";
                response.Err = null;
                logger.Log("Subject list displayed");
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

        // GET: api/Subject/5
        public Response Get(int id)
        {
            try
            { 
                 T_Subject subjToBeFind = dalobj.T_Subject.Find(id);
                if (subjToBeFind != null)
                {
                    response.Data = subjToBeFind;
                    response.Status = "success";
                    response.Err = null;
                    logger.Log("Specific Subject displayed using Id");
                    return response;
                }
                else
                {
                    response.Data = null;
                    response.Status = "Failed";
                    response.Err = null;
                    logger.Log("Specific Subject not found using Id");
                    return response;
                }

            }
            catch(Exception cause)
            {
                response.Data = cause.Message;
                response.Status = "Failed";
                response.Err = cause;
                logger.Log("Exception occured returned Error msg");
                return response;
            }

        }

       

        // POST: api/Subject
        public Response Post([FromBody]T_Subject subj)
        {
            try
            {
                if (subj != null)                    // afterwards check valid subject or not so if/else loop ...
                {
                    dalobj.T_Subject.Add(subj);
                    dalobj.SaveChanges();
                    response.Data = null;
                    response.Status = "success";
                    response.Err = null;
                    logger.Log("Subject added in db");
                    return response;
                }
                else
                {
                    response.Data = null;
                    response.Status = "Invalid Subject";
                    response.Err = null;
                    logger.Log("Subject insertion failed");
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

        // PUT: api/Subject/5
        public Response Put(int id, [FromBody]T_Subject subj)
        {
            try
            {
                T_Subject subjToBeFind = dalobj.T_Subject.Find(id);
                if (subjToBeFind != null)
                {
                    subjToBeFind.SubName = subj.SubName;
                    dalobj.SaveChanges();

                    response.Data = subjToBeFind;
                    response.Status = "success";
                    response.Err = null;
                    logger.Log("Specific Subject updated using Id");
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

        // DELETE: api/Subject/5
        public Response Delete(int id)
        {
            try
            {
                T_Subject subjToBedeleted = dalobj.T_Subject.Find(id);
                if (subjToBedeleted != null)
                {
                    dalobj.T_Subject.Remove(subjToBedeleted);
                    dalobj.SaveChanges();

                    response.Data = null;
                    response.Status = "success";
                    response.Err = null;
                    logger.Log("Specific Subject deleted using Id");
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
