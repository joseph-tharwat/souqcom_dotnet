using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace souqcomApp.Models
{
    public class CustomExceptionFilter: ExceptionFilterAttribute
    {
        public override void OnException(ExceptionContext context)
        {
            Console.WriteLine("handling the exception");
            //Console.WriteLine(context.Exception);
            context.ExceptionHandled = true;
            context.Result = new RedirectToActionResult("Login","Account",null);
        }
    }
}
