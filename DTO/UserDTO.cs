using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.Design;
using System.ComponentModel.DataAnnotations;
using System.Web;

namespace DTO
{
    public class UserDTO
    {
        public int ID { get; set; }
        [Required(ErrorMessage = "Username required")]
        public string Username { get; set; }
        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }
        [Required(ErrorMessage = "Email is required")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }
        public string ImagePath { get; set; }
        public bool isAdmin { get; set; }
        [Display(Name ="User Image")]
        public  HttpPostedFileBase userImage { get; set; }


    }
}
