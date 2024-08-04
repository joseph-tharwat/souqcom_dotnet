using Microsoft.AspNetCore.Mvc.Filters;

namespace souqcomApp.Models
{
    public class executionTimeFilter: ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            Console.Write("test filter 1"); 
            base.OnActionExecuting(context);
        }
        public override void OnActionExecuted(ActionExecutedContext context)
        {
            Console.Write("test filte 2");
            base.OnActionExecuted(context);
        }
    }
}
