using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class GeneralDAO
    {
        public List<PostDTO> GetSliderPost()
        {
            using(POSTDATAEntities db = new POSTDATAEntities())
            {
                List<PostDTO> list = new List<PostDTO>();

                var sliderList = (from p in db.Posts.Where(x => x.isDeleted == false && x.Slider == true)
                                  join c in db.Categories on p.CategoryID equals c.ID
                                  select new
                                  {
                                      postID = p.ID,
                                      title = p.Title,
                                      categoryName = c.CategoryName,
                                      seolink = p.SeoLink,
                                      viewcount = p.ViewCount,
                                      Adddate = p.AddDate
                                  }).OrderBy(x => x.Adddate).Take(8).ToList();



                foreach (var item in sliderList)
                {
                    PostDTO dto = new PostDTO();
                    dto.ID = item.postID;
                    dto.Title = item.title;
                    dto.CategoryName = item.categoryName;
                    dto.ViewCount = item.viewcount;
                    dto.SeoLink = item.seolink;

                    //now we will grab images - but we will grab only 1st image to show in slider
                    PostImage image = db.PostImages.First(x => x.PostID == item.postID && x.isDeleted == false);
                    dto.ImagePath = image.ImagePath;

                    //now for comment Count
                    dto.CommentCount = db.Comments.Where(x => x.PostID == item.postID && x.isDeleted == false && x.isApproved == true).Count();

                    dto.AddDate = item.Adddate;
                    list.Add(dto);
                }

                return list;
            }
          

        }

       

        public List<PostDTO> GetBreakingPosts()
        {
            using(POSTDATAEntities db = new POSTDATAEntities())
            {
                List<PostDTO> list = new List<PostDTO>();

                var breakingList = (from p in db.Posts.Where(x => x.isDeleted == false && x.Slider == false)
                                    join c in db.Categories on p.CategoryID equals c.ID
                                    select new
                                    {
                                        postID = p.ID,
                                        title = p.Title,
                                        categoryName = c.CategoryName,
                                        seolink = p.SeoLink,
                                        viewcount = p.ViewCount,
                                        Adddate = p.AddDate
                                    }).OrderBy(x => x.Adddate).Take(5).ToList();



                foreach (var item in breakingList)
                {
                    PostDTO dto = new PostDTO();
                    dto.ID = item.postID;
                    dto.Title = item.title;
                    dto.CategoryName = item.categoryName;
                    dto.ViewCount = item.viewcount;
                    dto.SeoLink = item.seolink;

                    //now we will grab images - but we will grab only 1st image to show in slider
                    PostImage image = db.PostImages.First(x => x.PostID == item.postID && x.isDeleted == false);
                    dto.ImagePath = image.ImagePath;

                    //now for comment Count
                    dto.CommentCount = db.Comments.Where(x => x.PostID == item.postID && x.isDeleted == false && x.isApproved == true).Count();

                    dto.AddDate = item.Adddate;
                    list.Add(dto);
                }

                return list;
            }

           
        }

        

        public List<PostDTO> GetPopularPosts()
        {
            using(POSTDATAEntities db = new POSTDATAEntities())
            {
                List<PostDTO> list = new List<PostDTO>();

                var popularList = (from p in db.Posts.Where(x => x.isDeleted == false && x.Area2 == true)
                                   join c in db.Categories on p.CategoryID equals c.ID
                                   select new
                                   {
                                       postID = p.ID,
                                       title = p.Title,
                                       categoryName = c.CategoryName,
                                       seolink = p.SeoLink,
                                       viewcount = p.ViewCount,
                                       Adddate = p.AddDate
                                   }).OrderBy(x => x.Adddate).Take(5).ToList();



                foreach (var item in popularList)
                {
                    PostDTO dto = new PostDTO();
                    dto.ID = item.postID;
                    dto.Title = item.title;
                    dto.CategoryName = item.categoryName;
                    dto.ViewCount = item.viewcount;
                    dto.SeoLink = item.seolink;

                    //now we will grab images - but we will grab only 1st image to show in slider
                    PostImage image = db.PostImages.First(x => x.PostID == item.postID && x.isDeleted == false);
                    dto.ImagePath = image.ImagePath;

                    //now for comment Count
                    dto.CommentCount = db.Comments.Where(x => x.PostID == item.postID && x.isDeleted == false && x.isApproved == true).Count();

                    dto.AddDate = item.Adddate;
                    list.Add(dto);
                }

                return list;
            }
        
        }


        public List<PostDTO> GetMostViewedPosts()
        {
            using(POSTDATAEntities db = new POSTDATAEntities())
            {
                List<PostDTO> list = new List<PostDTO>();

                var popularList = (from p in db.Posts.Where(x => x.isDeleted == false)
                                   join c in db.Categories on p.CategoryID equals c.ID
                                   select new
                                   {
                                       postID = p.ID,
                                       title = p.Title,
                                       categoryName = c.CategoryName,
                                       seolink = p.SeoLink,
                                       viewcount = p.ViewCount,
                                       Adddate = p.AddDate
                                   }).OrderByDescending(x => x.viewcount).Take(5).ToList();



                foreach (var item in popularList)
                {
                    PostDTO dto = new PostDTO();
                    dto.ID = item.postID;
                    dto.Title = item.title;
                    dto.CategoryName = item.categoryName;
                    dto.ViewCount = item.viewcount;
                    dto.SeoLink = item.seolink;

                    //now we will grab images - but we will grab only 1st image to show in slider
                    PostImage image = db.PostImages.First(x => x.PostID == item.postID && x.isDeleted == false);
                    dto.ImagePath = image.ImagePath;

                    //now for comment Count
                    dto.CommentCount = db.Comments.Where(x => x.PostID == item.postID && x.isDeleted == false && x.isApproved == true).Count();

                    dto.AddDate = item.Adddate;
                    list.Add(dto);
                }

                return list;
            }
        
        }



        public List<VideoDTO> GetVideos()
        {
            using(POSTDATAEntities db = new POSTDATAEntities())
            {
                List<VideoDTO> dtoList = new List<VideoDTO>();

                List<Video> videoList = db.Videos.Where(x => x.isDeleted == false)
                                        .OrderByDescending(x => x.AddDate).Take(3).ToList();

                foreach (var item in videoList)
                {
                    VideoDTO dto = new VideoDTO();
                    dto.ID = item.ID;
                    dto.Title = item.Title;
                    dto.VideoPath = item.VideoPath;
                    dto.OriginalVideoPath = item.OriginalVideoPath;
                    dto.AddDate = item.AddDate;
                    dtoList.Add(dto);
                }
                return dtoList;
            }
           
        }


        public PostDTO GetPostDetail(int ID)
        {
            using(POSTDATAEntities db = new POSTDATAEntities())
            {
                //now here we have to increment view count for each select
                Post post = db.Posts.First(x => x.ID == ID);
                post.ViewCount++;
                db.SaveChanges();

                //now we have to return postdto
                PostDTO dto = new PostDTO();
                dto.ID = post.ID;
                dto.Title = post.Title;
                dto.ShortContent = post.ShortContent;
                dto.PostContent = post.PostContent;
                dto.Language = post.LanguageName;
                dto.SeoLink = post.SeoLink;
                dto.CategoryID = post.CategoryID;

                //now we need to get ID
                dto.CategoryName = (db.Categories.First(x => x.ID == dto.CategoryID)).CategoryName;

                //now we need to get all post Images
                List<PostImage> images = db.PostImages.Where(x => x.PostID == ID && x.isDeleted == false).ToList();
                List<PostImageDTO> imageList = new List<PostImageDTO>();
                foreach (var item in images)
                {
                    PostImageDTO img = new PostImageDTO();
                    img.ID = item.ID;
                    img.ImagePath = item.ImagePath;
                    imageList.Add(img);
                }

                dto.PostImages = imageList;

                //for comment
                dto.CommentCount = db.Comments.Where(x => x.isDeleted == false && x.PostID == ID && x.isApproved == true).Count();

                //we also need commentDTo for holiding post comment
                //firt we will define comment dto
                List<Comment> comments = db.Comments.Where(x => x.isDeleted == false && x.PostID == ID && x.isApproved == true).ToList();
                List<CommentDTO> commentDtoList = new List<CommentDTO>();
                foreach (var item in comments)
                {
                    CommentDTO cdto = new CommentDTO();
                    cdto.ID = item.ID;
                    cdto.AddDate = item.AddDate;
                    cdto.CommentContent = item.CommentContent;
                    cdto.Name = item.NameSurname;
                    cdto.Email = item.Email;
                    commentDtoList.Add(cdto);
                }
                dto.CommentList = commentDtoList;

                //now for tags
                List<PostTag> tags = db.PostTags.Where(x => x.isDeleted == false && x.PostID == ID).ToList();
                List<TagDTO> tagsDtoList = new List<TagDTO>();
                foreach (var item in tags)
                {
                    TagDTO tag = new TagDTO();
                    tag.ID = item.ID;
                    tag.TagContent = item.TagContent;
                    tagsDtoList.Add(tag);

                }
                dto.TagList = tagsDtoList;

                return dto;
            }
           
        }

        public List<VideoDTO> GetAllVideos()
        {
            using(POSTDATAEntities db = new POSTDATAEntities())
            {
                List<VideoDTO> dtoList = new List<VideoDTO>();

                List<Video> videoList = db.Videos.Where(x => x.isDeleted == false)
                                        .OrderByDescending(x => x.AddDate).ToList();

                foreach (var item in videoList)
                {
                    VideoDTO dto = new VideoDTO();
                    dto.ID = item.ID;
                    dto.Title = item.Title;
                    dto.VideoPath = item.VideoPath;
                    dto.OriginalVideoPath = item.OriginalVideoPath;
                    dto.AddDate = item.AddDate;
                    dtoList.Add(dto);
                }
                return dtoList;
            }
        
        }



        public List<PostDTO> GetCategoryPostList(int categoryID)
        {
            using (POSTDATAEntities db = new POSTDATAEntities())
            {
                List<PostDTO> list = new List<PostDTO>();

                var popularList = (from p in db.Posts.Where(x => x.isDeleted == false && x.CategoryID == categoryID)
                                   join c in db.Categories on p.CategoryID equals c.ID
                                   select new
                                   {
                                       postID = p.ID,
                                       title = p.Title,
                                       categoryName = c.CategoryName,
                                       seolink = p.SeoLink,
                                       viewcount = p.ViewCount,
                                       Adddate = p.AddDate
                                   }).OrderBy(x => x.Adddate).ToList();



                foreach (var item in popularList)
                {
                    PostDTO dto = new PostDTO();
                    dto.ID = item.postID;
                    dto.Title = item.title;
                    dto.CategoryName = item.categoryName;
                    dto.ViewCount = item.viewcount;
                    dto.SeoLink = item.seolink;

                    //now we will grab images - but we will grab only 1st image to show in slider
                    PostImage image = db.PostImages.First(x => x.PostID == item.postID && x.isDeleted == false);
                    dto.ImagePath = image.ImagePath;

                    //now for comment Count
                    dto.CommentCount = db.Comments.Where(x => x.PostID == item.postID && x.isDeleted == false && x.isApproved == true).Count();

                    dto.AddDate = item.Adddate;
                    list.Add(dto);
                }

                return list;
            }
            
        }



        public List<PostDTO> GetSearchPosts(string searchText)
        {
            using(POSTDATAEntities db = new POSTDATAEntities())
            {
                List<PostDTO> list = new List<PostDTO>();

                var popularList = (from p in db.Posts.Where(x => x.isDeleted == false && (x.Title.Contains(searchText) || x.PostContent.Contains(searchText)))
                                   join c in db.Categories on p.CategoryID equals c.ID
                                   select new
                                   {
                                       postID = p.ID,
                                       title = p.Title,
                                       categoryName = c.CategoryName,
                                       seolink = p.SeoLink,
                                       viewcount = p.ViewCount,
                                       Adddate = p.AddDate
                                   }).OrderBy(x => x.Adddate).ToList();



                foreach (var item in popularList)
                {
                    PostDTO dto = new PostDTO();
                    dto.ID = item.postID;
                    dto.Title = item.title;
                    dto.CategoryName = item.categoryName;
                    dto.ViewCount = item.viewcount;
                    dto.SeoLink = item.seolink;

                    //now we will grab images - but we will grab only 1st image to show in slider
                    PostImage image = db.PostImages.First(x => x.PostID == item.postID && x.isDeleted == false);
                    dto.ImagePath = image.ImagePath;

                    //now for comment Count
                    dto.CommentCount = db.Comments.Where(x => x.PostID == item.postID && x.isDeleted == false && x.isApproved == true).Count();

                    dto.AddDate = item.Adddate;
                    list.Add(dto);
                }

                return list;
            }
          
        }


    }
}
