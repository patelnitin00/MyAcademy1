using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace UI
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            //using Attribute routing
            routes.MapMvcAttributeRoutes();

            //routing for just front pages not for admin
            //our custom routes

            //this means we do not have to run our project with view anymore
            //we can run our project with any browser page
            routes.MapRoute(
                name: "Home",
                url: "",
                defaults: new { controller = "Home", action = "Index"}
            );

            //route for category - for paramater we use brackets - {}
            routes.MapRoute(
                name:"Category",
                url:"{CategoryName}",
                defaults: new { controller = "Home", action = "CategoryPostList", CategoryName=UrlParameter.Optional}
            );

            //route for postDetails
            routes.MapRoute(
                name:"PostDetail",
                url:"{CategoryName}/{SeoLink}/{ID}",
                defaults: new { controller ="Home", action="PostDetail", ID=UrlParameter.Optional, CategoryName=UrlParameter.Optional, SeoLink = UrlParameter.Optional}
           );


            //defalut route 
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
