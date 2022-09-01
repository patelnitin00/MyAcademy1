using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace DTO
{
    public class PostDTO
    {
        public int ID { get; set; }
        [Required(ErrorMessage ="Please Fill the Title Area")]
        public string Title { get; set; }
        [Required(ErrorMessage = "Please Fill the Short Content Area")]
        public string ShortContent { get; set; }
        [Required(ErrorMessage = "Please Fill the Post Content Area")]
        public string PostContent { get; set; }
        [Required(ErrorMessage = "Please Fill the Title Area")]
        public int CategoryID { get; set; }
        public string CategoryName { get; set; }
        public List<SelectListItem> Categories  { get; set; }
        public List<PostImageDTO> PostImages { get; set; }
        [Display(Name ="Post Image")]
        public List<HttpPostedFileBase> PostImage { get; set; }
        public List<TagDTO> TagList { get; set; }
        public string TagText { get; set; }
        public int ViewCount { get; set; }
        public string SeoLink { get; set; }
        public bool Slider { get; set; }
        public bool Area1 { get; set; }
        public bool Area2 { get; set; }
        public bool Area3 { get; set; }
        public bool Notification { get; set; }
        public string Language { get; set; }

        public DateTime AddDate { get; set; }

        public bool isUpdate { get; set; }

        //extra fields added for user view pages for user side//

        //1. imagepath to hold image path for slider
        public string ImagePath { get; set; }
        //1. we also need comment count
        public int CommentCount { get; set; }


        //2. 
        public List<CommentDTO> CommentList { get; set; }
    }
}
