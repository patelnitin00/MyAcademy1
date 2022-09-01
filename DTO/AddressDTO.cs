using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class AddressDTO
    {
        public int ID { get; set; }
        [Required (ErrorMessage ="Please fill the Address Area")]
        public string AddressContent { get; set; }
        [Required(ErrorMessage = "Please fill the Email Area")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Please fill the Phone Area")]
        public string Phone { get; set; }
        public string Phone2 { get; set; }
        public string Fax { get; set; }
        [Required(ErrorMessage = "Please fill the Map Area")]
        public string LargeMapPath { get; set; }
        [Required(ErrorMessage = "Please fill the Map Area")]
        public string SmallMapPath { get; set; }
    }
}
