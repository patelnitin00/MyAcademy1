using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace UI.Areas.Admin.Models.Attributes
{
    public class LoginControl : FilterAttribute, IActionFilter
    {
        //FilterAttribute is an abstract class
        //IActionFilter is an itterface
        //With help of these we manage/control our cookies with help of them

        public void OnActionExecuted(ActionExecutedContext filterContext)
        {
            //we are not going to use this method - so we can leave this empty
        }

        public void OnActionExecuting(ActionExecutingContext filterContext)
        {
            //now we want to control on excuting so we will use this

            //for checking cookies we can write
            /*if (!HttpContext.Current.User.Identity.IsAuthenticated)
            {
                //this means we didn't login yet - so we will direct to login page
                filterContext.HttpContext.Response.Redirect("/Admin/Login/Index");
            }*/


            //the above method is enough but sometime you can get an error for userID so you can use below method
            //I will check user is legitimate or not - definitive solution
            if (UserStatic.UserID == 0)
            {
                filterContext.HttpContext.Response.Redirect("/Admin/Login/Index");
            }

            //now add basecoontroller - add this class as an attribute to baseController
            //and extends all controller with baseController except LoginController


        }
    }
}