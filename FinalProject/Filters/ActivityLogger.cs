using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Controllers;
using FinalProject.Models;
using System.Web.Http.Filters;

namespace FinalProject.Models
{

    public class ActivityLogger : ActionFilterAttribute
    {
        
        MyLoggerLib.ILogger logger = MyLoggerLib.LoggerFactory.GetLogger(1);

        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            string msgformat = "/{0}/{1} method is called";
            string Controller = actionContext.ActionDescriptor.ControllerDescriptor.ControllerName;
            string method = actionContext.ActionDescriptor.ActionName;

            string msg = string.Format(msgformat, Controller, method);
            logger.Log(msg);
        }
        public override void OnActionExecuted(HttpActionExecutedContext actionExecutedContext)
        {
            logger.Log("method Executed");
        }

    }
}




    