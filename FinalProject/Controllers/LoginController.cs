using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using FinalProject.Models;
using System.Web.Http.Cors;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Web;
using System.Diagnostics;
using System.Net.Http.Headers;

namespace FinalProject.Controllers
{

    public class LoginController : BaseController
    {
        MyLoggerLib.ILogger logger = MyLoggerLib.LoggerFactory.GetLogger(1);

        FinalprojectdbEntities dalobj = new FinalprojectdbEntities();
        Response response = new Response();

        LoginController()
        {       
            dalobj.Configuration.ProxyCreationEnabled = false;
        }

        // POST: api/Login
        public Response Post([FromBody] T_Users data)
        {
            List<T_Users> user = dalobj.T_Users.ToList();

            if ((data.EmailId != null && data.Password != null))
            {

                
                    var validUser = (from u in user
                                     where u.EmailId == data.EmailId && u.Password == data.Password
                                     select u).SingleOrDefault();
                if (validUser != null)
                {
                    response.Status = "success";
                    response.Err = null;
                    response.Data = validUser;
                    logger.Log("Login Successfull.");
                }
                else
                {
                    response.Status = "failed! Invalid Credentials";
                    response.Err = null;
                    response.Data = null;
                    logger.Log("Login Failed!");
                }

                    return response;
                
            }
            else
            {
                response.Status = "Plz enter Credentials";
                response.Err = null;
                return response;
            }
           
        }

        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("api/Login/Register")]
        public Response Post1([FromBody] T_Users data)
        {

            if (data != null)
            {
                var added = dalobj.T_Users.Add(data);

                    try
                    {
                        dalobj.SaveChanges();
                        response.Data = added;
                        response.Status = "success";
                        response.Err = null;
                        logger.Log("Register Successfull.");
                        var res = SendEmail(added.EmailId, "Registration Successful", "Dear "+ added.Name+ " your registration is successfully done on ONLINE EXAMINATION PORTAL. Please Login now on link http://Shiksha.kdac.org/Login");
                    return response;
                       
                }
                    catch(Exception e)
                    {
                        response.Status = "Exception Occured! Check credentials...";
                    //    response.Err = null;
                          response.Err = e;
                          response.Data = null;
                          logger.Log("Exception occured!");
                          return response;

                     }
            }
            else
            {
                response.Data = null;
                response.Status = "Empty Fileds";
                response.Err = null;
                return response;
            }

        }


        //generate OTP and send to user 
        private string GetOTP(string otpType = "1", int len = 5)
        {
            //otptype 1 = alpha numeric
            string alphabets = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            string small_alphabets = "abcdefghijklmnopqrstuvwxyz";
            string numbers = "1234567890";

            string characters = numbers;
            if (otpType == "1")
            {
                characters += alphabets + small_alphabets + numbers;
            }
            int length = 5;
            string otp = string.Empty;
            for (int i = 0; i < length; i++)
            {
                string character = string.Empty;
                do
                {
                    int index = new Random().Next(0, characters.Length);
                    character = characters.ToCharArray()[index].ToString();
                } while (otp.IndexOf(character) != -1);
                otp += character;
            }
            return otp;
        }

        //check the otp entered by user exist in db
        [HttpPost]
        [Route("api/Login/OTP")]
        public Response OTP([FromBody]dynamic otpDetails)
        {
            string email = otpDetails.EmailId.ToString();


            var userlist = dalobj.T_Users.ToList();
            var User = (from u in userlist
                        where u.EmailId == email
                        select u).SingleOrDefault();
            string o = otpDetails.OTP.ToString();

            var otpd = dalobj.T_OTP_Details.ToList();
            var vOTP = (from v in otpd
                        where v.UserId == User.UserId && v.OTP == o
                        select v).SingleOrDefault();

            if (vOTP != null && vOTP.ValidTill > DateTime.Now)
            {
                Response RC = new Response();
                RC.Status = "success";
                RC.Err = null;
                RC.Data = vOTP;
                return RC;
            }
            else
            {
                Response RC = new Response();
                RC.Status = "fail";
                RC.Err = null;
                RC.Data = false;
                return RC;
            }
        }


        //sent otp when emailid exist
        [HttpPost]
        [Route("api/Login/IsExist")]
        public Response IsExist([FromBody]T_Users value)
        {


            var userlist = dalobj.T_Users.ToList();
            var User = (from u in userlist
                        where u.EmailId == value.EmailId
                        select u).SingleOrDefault();
            if (User != null)
            {
                Response RC = new Response();
                string mails = GetOTP();

                T_OTP_Details otp = new T_OTP_Details();
                otp.UserId = User.UserId;
                otp.ValidTill = (DateTime.Now).AddMinutes(5);
                otp.GeneratedOn = DateTime.Now;
                otp.OTP = mails;
                dalobj.T_OTP_Details.Add(otp);
                dalobj.SaveChanges();
                var res = SendEmail(User.EmailId,"Generated OTP","Your One Time Password is "+mails+". Valid till "+otp.ValidTill+".");

                RC.Status = "success";
                RC.Err = null;
                RC.Data = mails;
                return RC;
            }
            else
            {
                Response RC = new Response();
                RC.Status = "fail";
                RC.Err = null;
                RC.Data = false;
                return RC;
            }

        }


        //if otp validation succeded then let user update his password
        [HttpPut]
        [Route("api/Login/UpdatePassword")]
        public Response updatepassword([FromBody]T_Users value)
        {

            var userlist = dalobj.T_Users.ToList();
            var User = (from u in userlist
                        where u.EmailId == value.EmailId
                        select u).SingleOrDefault();

            if (User != null)
            {
                User.Password = value.Password;
                dalobj.SaveChanges();
                Response rc = new Response();
                rc.Status = "success";
                rc.Err = null;
                rc.Data = User;
                return rc;
            }
            else
            {
                Response rc = new Response();
                rc.Status = "fail";
                rc.Err = null;
                rc.Data = null;
                return rc;
            }
        }


        [HttpPost]
        [Route("api/Login/SendMail")]
        public string SendEmail(string toAddress,string subject, string body)
        {

            string result = "Message Sent Successfully..!!";
            string senderID = "kdaconlineexaminationportal@gmail.com";// use sender’s email id here..
            const string senderPassword = "admin@onlineportal"; // sender password here…
            try
            {
                SmtpClient smtp = new SmtpClient
                {
                    Host = "smtp.gmail.com", // smtp server address here…
                    Port = 587,
                    EnableSsl = true,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    Credentials = new System.Net.NetworkCredential(senderID, senderPassword),
                    Timeout = 30000,
                };
                MailMessage message = new MailMessage(senderID, toAddress, subject, body);
                smtp.Send(message);
            }
            catch (Exception ex)
            {
                //result = "Error sending email.!!!";
                result = ex.ToString();
               
            }
            return result;

        }

    }

   
}












