using BLL;
using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace UI.Controllers
{
    public class HomeController : Controller
    {
        LayoutBLL layoutBLL = new LayoutBLL();
        GeneralBLL generalBLL = new GeneralBLL();
        PostBLL postBLL = new PostBLL();
        ContactBLL contactBLL = new ContactBLL();
        // GET: Home
        public ActionResult Index()
        {
            HomeLayoutDTO layouDTO = new HomeLayoutDTO();
            //we will not send layoutDTo to view we will send another DTO

            //now write method to fill dto
            layouDTO = layoutBLL.GetLayoutData();
            //in the above method we will set all the elements 1 by 1

            //now send this layout using ViewData
            ViewData["LayoutDTO"] = layouDTO;

            //define instance from general DTO
            GeneralDTO generalDTO = new GeneralDTO();
            generalDTO = generalBLL.GetAllPosts();

            return View(generalDTO);
        }

        //now add pages for front pages

        public ActionResult CategoryPostList(string CategoryName)
        {
            HomeLayoutDTO layouDTO = new HomeLayoutDTO();
            layouDTO = layoutBLL.GetLayoutData();
            ViewData["LayoutDTO"] = layouDTO;

            //above was for header and footer
            //below is for main content on category page
            GeneralDTO dto = new GeneralDTO();
            dto = generalBLL.GetCategoryPostList(CategoryName);

            return View(dto);
        }


        public ActionResult PostDetail(int ID)
        {
            HomeLayoutDTO layouDTO = new HomeLayoutDTO();
            layouDTO = layoutBLL.GetLayoutData();
            ViewData["LayoutDTO"] = layouDTO;

            //for post page main content except header and footer 
            GeneralDTO dto = new GeneralDTO();
            dto = generalBLL.GetPostDetailPageItemsWithID(ID);

            return View(dto);
        }


        [HttpPost]
        public ActionResult PostDetail(GeneralDTO model)
        {
           //omment is part of post so we will use postBLL and postDAO

            if(model.Name!=null && model.Email!=null && model.Message != null)
            {
                if (postBLL.AddComment(model))
                {
                    //now we will use toaster
                    ViewData["CommentState"] = "success";
                    ModelState.Clear();
                }
                else
                {
                    ViewData["CommentState"] = "error";
                }
            }
            else
            {
                ViewData["CommentState"] = "error";
            }

            HomeLayoutDTO layouDTO = new HomeLayoutDTO();
            layouDTO = layoutBLL.GetLayoutData();
            ViewData["LayoutDTO"] = layouDTO;

            
            model = generalBLL.GetPostDetailPageItemsWithID(model.PostID);

            return View(model);
        }

        //route using MVC route
        [Route("contactus")]
        public ActionResult ContactUS()
        {
            HomeLayoutDTO layouDTO = new HomeLayoutDTO();
            layouDTO = layoutBLL.GetLayoutData();
            ViewData["LayoutDTO"] = layouDTO;

            GeneralDTO dto = new GeneralDTO();
            dto = generalBLL.GetContactPageItems();
            return View(dto);
        }

        //route using MVC route
        [Route("contactus")]
        [HttpPost]
        public ActionResult ContactUs(GeneralDTO model)
        {
            if(model.Name!=null && model.Subject!=null && model.Email!=null && model.Message!=null)
            {
                if (contactBLL.AddContact(model))
                {
                    ViewData["CommentState"] = "success";
                    ModelState.Clear();
                }
                else
                {
                    ViewData["CommentState"] = "error";
                }
            }
            else
            {
                ViewData["CommentState"] = "error";
            }


            HomeLayoutDTO layouDTO = new HomeLayoutDTO();
            layouDTO = layoutBLL.GetLayoutData();
            ViewData["LayoutDTO"] = layouDTO;

            GeneralDTO dto = new GeneralDTO();
            dto = generalBLL.GetContactPageItems();
            return View(dto);
        }

        //route is give to access this action method from any page
        [Route("search")]
        [HttpPost]
        public ActionResult Search(GeneralDTO model)
        {
            HomeLayoutDTO layouDTO = new HomeLayoutDTO();
            layouDTO = layoutBLL.GetLayoutData();
            ViewData["LayoutDTO"] = layouDTO;

            GeneralDTO dto = new GeneralDTO();
            dto = generalBLL.GetSearchPosts(model.SearchText);

            return View(dto);
        }





    }
}