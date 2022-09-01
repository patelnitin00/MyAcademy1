using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class HomeLayoutDTO
    {
        //1. Categories
        public List<CategoryDTO> Categories { get; set; } //1

        //2. links
        //we can define List foe social media and call from database
        //but we need to map name/link with icon on layout page
        //so for that we will add them seperatly
        public SocialMediaDTO Facebook { get; set; } //2
        public SocialMediaDTO Twitter { get; set; } //2
        public SocialMediaDTO Instagram { get; set; } //2
        public SocialMediaDTO Youtube { get; set; } //2
        public SocialMediaDTO LinkedIn { get; set; } //2
        public SocialMediaDTO GooglePlus { get; set; } //2

        //FavIcon ,Title
        public FavDTO FavDto { get; set; } //3

        //Meta Data
        public List<MetaDTO> MetaList { get; set; } //4

        //Address
        public AddressDTO Address { get; set; }//5

        //Hot News
        public List<PostDTO> HotNews { get; set; }//6





    }
}
