
using BLL;
using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace UI.Areas.Admin.Controllers
{
    public class CommentController : BaseController
    {
        //controller for approve commnets
        // GET: Admin/Comment
        PostBLL postBLL = new PostBLL();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult UnapprovedComment()
        {
            List<CommentDTO> commentList = new List<CommentDTO>();
            commentList = postBLL.GetCommentsList();
            return View(commentList);
        }

        public ActionResult AllComments()
        {
            List<CommentDTO> commentsList = postBLL.GetAllComments();
            return View(commentsList);
        }

        public ActionResult ApproveComment(int ID)
        {
           postBLL.ApproveComment(ID);
            //after approve we will direct to same page so
            return RedirectToAction("UnapprovedComment", "Comment");
        }

        public ActionResult ApproveComment2(int ID)
        {
            postBLL.ApproveComment(ID);
            //after approve we will direct to same page so
            return RedirectToAction("AllComments", "Comment");
        }

        public JsonResult DeleteComment(int ID)
        {
            postBLL.DeleteComment(ID);

            return Json("");
        }

    }
}