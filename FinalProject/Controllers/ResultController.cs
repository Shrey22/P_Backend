using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using FinalProject.Models;

namespace FinalProject.Controllers
{
    public class ResultController : BaseController
    {
        Response response = new Response();
        FinalprojectdbEntities dalobj = new FinalprojectdbEntities();
        MyLoggerLib.ILogger logger = MyLoggerLib.LoggerFactory.GetLogger(1);

        ResultController()
        {
            dalobj.Configuration.ProxyCreationEnabled = false;
        }

        // GET: api/Results
        public Response Get()
        {
            try
            {
                List<T_Result> resList = dalobj.T_Result.ToList();
                List<T_Subject> subList = dalobj.T_Subject.ToList();
                List<T_Users> uList = dalobj.T_Users.ToList();
                var list = (from res in resList
                            join u in uList on res.UserId equals u.UserId
                            join sub in subList on res.SubId equals sub.SubId
                            select new
                            {
                                u.Name,
                                u.EmailId,
                                sub.SubName,
                                u.IsLocked,
                                res.CntCorrectAns
                            }).ToList();

                response.Data = list;
                response.Status = "success";
                response.Err = null;
                logger.Log("Result list displayed");
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


        // GET: api/Result/5
        //[System.Web.Http.HttpGet]
        //[System.Web.Http.Route("api/Result/LoggedInUser")]
        public Response Get(int id)
        {
            try
            {

                //T_Result resultToBeFind = dalobj.T_Result.Find(id);

                List<T_Result> list = dalobj.T_Result.ToList();
                List<T_Subject> subj = dalobj.T_Subject.ToList();

                var loggeduserResult = (from res in list
                                        join s in subj on res.SubId equals s.SubId
                                        where res.UserId == id
                                        select new { res ,s.SubName });


                if (loggeduserResult != null)
                {
                    response.Data = loggeduserResult;
                    response.Status = "success";
                    response.Err = null;
                    logger.Log("current logged user result entire Result displayed");
                    return response;
                }
                else
                {
                    response.Data = null;
                    response.Status = "Not yet given any exam";
                    response.Err = null;
                    logger.Log("current logged user result not found");
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


        // GET: api/Result/5
        //public Response Get(int id)
        //{
        //    try
        //    {
        //        T_Result resultToBeFind = dalobj.T_Result.Find(id);
        //        if (resultToBeFind != null)
        //        {
        //            response.Data = resultToBeFind;
        //            response.Status = "success";
        //            response.Err = null;
        //            logger.Log("Specific Result displayed using Id");
        //            return response;
        //        }
        //        else
        //        {
        //            response.Data = null;
        //            response.Status = "Failed";
        //            response.Err = null;
        //            logger.Log("Specific Result not found using Id");
        //            return response;
        //        }

        //    }
        //    catch (Exception cause)
        //    {
        //        response.Data = cause.Message;
        //        response.Status = "Failed";
        //        response.Err = cause;
        //        logger.Log("Exception occured returned Error msg");
        //        return response;
        //    }
        //}



        // POST: api/Result
        public Response Post([FromBody] UserScore tally)
        {
            try
            {

                if (tally != null)                    // afterwards check valid subject or not so if/else loop ...
                {

                    T_Result list = new T_Result();



                    list.UserId = tally.UserId;
                    list.SubId = tally.SubId;
                    list.CntCorrectAns = tally.CntCorrectAns;

                    dalobj.T_Result.Add(list);
                    dalobj.SaveChanges();
                    response.Data = null;
                    response.Status = "success";
                    response.Err = null;
                    logger.Log("Result added in db");
                    return response;
                }
                else
                {
                    response.Data = null;
                    response.Status = "Failed";
                    response.Err = null;
                    logger.Log("Result insertion failed");
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



        //// POST: api/Result
        //public Response Post([FromBody]T_Result result)
        //{
        //    try
        //    {
        //        if (result != null)                    // afterwards check valid subject or not so if/else loop ...
        //        {
        //            dalobj.T_Result.Add(result);
        //            dalobj.SaveChanges();
        //            response.Data = null;
        //            response.Status = "success";
        //            response.Err = null;
        //            logger.Log("Result added in db");
        //            return response;
        //        }
        //        else
        //        {
        //            response.Data = null;
        //            response.Status = "Failed";
        //            response.Err = null;
        //            logger.Log("Result insertion failed");
        //            return response;
        //        }
        //    }

        //    catch (Exception cause)
        //    {
        //        response.Data = cause.Message;
        //        response.Status = "Failed";
        //        response.Err = cause;
        //        logger.Log("Exception occured returned Error msg");
        //        return response;
        //    }
        //}

        // PUT: api/Result/5
        public Response Put(int id, [FromBody]T_Result result)
        {
            try
            {
                T_Result resultToBeFind = dalobj.T_Result.Find(id);
                if (resultToBeFind != null)
                {
                    resultToBeFind.UserId = result.UserId;
                    resultToBeFind.SubId = result.SubId;
                    resultToBeFind.CntCorrectAns = result.CntCorrectAns;
                    dalobj.SaveChanges();

                    response.Data = resultToBeFind;
                    response.Status = "success";
                    response.Err = null;
                    logger.Log("Specific Result updated using Id");
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

        // DELETE: api/Result/5
        public Response Delete(int id)
        {
            try
            {
                T_Result resultToBedeleted = dalobj.T_Result.Find(id);
                if (resultToBedeleted != null)
                {
                    dalobj.T_Result.Remove(resultToBedeleted);
                    dalobj.SaveChanges();

                    response.Data = null;
                    response.Status = "success";
                    response.Err = null;
                    logger.Log("Specific REsult deleted using Id");
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
