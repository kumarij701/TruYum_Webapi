using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http.Results;

//using System.Web.Http.Results;


namespace TruYumWebAPI
{
    public class CustomExceptionFilter : ActionFilterAttribute, IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            //HttpStatusCode status = HttpStatusCode.InternalServerError;
                                   

            //write to file
         //context.Result = new ExceptionResult(context.Exception, true);

        }
    }
}
