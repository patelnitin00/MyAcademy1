using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class GeneralDTO
    {
        public List<PostDTO> SliderPost { get; set; } //1
        public List<PostDTO> PopularPost { get; set; } //1
        public List<PostDTO > MostViewedPost { get; set; } //1
        public List <PostDTO> BreakingPost { get; set; } //1

        public List<VideoDTO> Videos { get; set; } //1
        public List<AdsDTO> AdsList { get; set; } //1
        public PostDTO PostDetail { get; set; } //1 - to get post detail names

        //for adding comment
        public string Name { get; set; }
        public string Email { get; set; }
        public string Message { get; set; }
        public int PostID { get; set; }


        //for categories
        public List<PostDTO> CateggoryPostList {   get; set; }
        //now property to hold name
        public string CategoryName { get; set; }


        //for contact page -address //here we have 1 address - if more you can use list property
        public AddressDTO Address { get; set; }
        public string Subject { get; set; }


        //for search operation
        public string SearchText { get; set; }

    }
}
