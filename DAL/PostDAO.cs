using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class PostDAO 
    {
        //to improve speed we won't inherit from PostContext Class
        //we will define seperate connection for each method bu using function
       //WHY this?
       //using old technique  - connection is always open - for big project it will be a problem
       //by using this method - connection will close automatically

        public int AddPost(Post post)
        {
           
            try
            {
                using (POSTDATAEntities db = new POSTDATAEntities())
                {
                    db.Posts.Add(post);
                    db.SaveChanges();
                }
               
                return post.ID;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int AddImage(PostImage item)
        {
            try
            {
                using (POSTDATAEntities db = new POSTDATAEntities())
                {
                    db.PostImages.Add(item);
                    db.SaveChanges();
                }
                return item.ID;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public int AddTag(PostTag item)
        {
            try
            {
                using (POSTDATAEntities db = new POSTDATAEntities())
                {
                    db.PostTags.Add(item);
                    db.SaveChanges();
                }
                return item.ID;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public List<PostDTO> GetPostList()
        {
            List<PostDTO> dtoList = new List<PostDTO>();

            using (POSTDATAEntities db = new POSTDATAEntities())
            {
                var postList = (from p in db.Posts.Where(x => x.isDeleted == false)
                                join c in db.Categories on p.CategoryID equals c.ID
                                select new
                                {
                                    ID = p.ID,
                                    Title = p.Title,
                                    categoryname = c.CategoryName,
                                    AddDate = p.AddDate
                                }).OrderByDescending(x => x.AddDate).ToList();

                //need to add too list to convert list


                foreach (var item in postList)
                {
                    PostDTO dto = new PostDTO();
                    dto.Title = item.Title;
                    dto.ID = item.ID;
                    dto.CategoryName = item.categoryname;
                    dto.AddDate = item.AddDate;
                    dtoList.Add(dto);
                }
            }

            return dtoList;

        }

     

        public PostDTO GetPostWithID(int ID)
        {
            PostDTO dto = new PostDTO();

            using (POSTDATAEntities db = new POSTDATAEntities())
            {
                Post post = db.Posts.First(x => x.ID == ID);

                dto.ID = post.ID;
                dto.Title = post.Title;
                dto.ShortContent = post.ShortContent;
                dto.PostContent = post.PostContent;
                dto.Language = post.LanguageName;
                dto.Notification = post.Notification;
                dto.SeoLink = post.SeoLink;
                dto.Slider = post.Slider;
                dto.Area1 = post.Area1;
                dto.Area2 = post.Area2;
                dto.Area3 = post.Area3;
                dto.CategoryID = post.CategoryID;
            }

            return dto;
        }

        public List<PostImageDTO> getPostImagesWithPostID(int PostID)
        {

            //we have to return imageDTO list so we will define it
            List<PostImageDTO> dtoList = new List<PostImageDTO>();

            using (POSTDATAEntities db = new POSTDATAEntities())
            {
                List<PostImage> list = db.PostImages.Where(x => x.isDeleted == false
                            && x.PostID == PostID).ToList();



                foreach (var item in list)
                {
                    PostImageDTO dto = new PostImageDTO();
                    dto.ID = item.ID;
                    dto.ImagePath = item.ImagePath;
                    dtoList.Add(dto);
                }
            }
            return dtoList;

        }

        public List<PostTag> GetPostTagsWithPostID(int PostID)
        {
            List<PostTag> dtoList = new List<PostTag>();
            using (POSTDATAEntities db = new POSTDATAEntities())
            {
                dtoList = db.PostTags.Where(x => x.isDeleted == false && x.PostID == PostID).ToList();

            }
            return dtoList;
        }

        public void UpdatePost(PostDTO model)
        {
           
            try
            {
                using (POSTDATAEntities db = new POSTDATAEntities())
                {
                    Post Post = db.Posts.First(x => x.ID == model.ID);
                    Post.Title = model.Title;
                    Post.Area1 = model.Area1;
                    Post.Area2 = model.Area2;
                    Post.Area3 = model.Area3;
                    Post.CategoryID = model.CategoryID;
                    Post.LanguageName = model.Language;
                    Post.LastUpdateDate = DateTime.Now;
                    Post.LastUpdateUserID = UserStatic.UserID;
                    Post.Notification = model.Notification;
                    Post.PostContent = model.PostContent;
                    Post.SeoLink = model.SeoLink;
                    Post.ShortContent = model.ShortContent;
                    Post.Slider = model.Slider;

                    db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void DeleteTag(int PostID)
        {
            using (POSTDATAEntities db = new POSTDATAEntities())
            {
                List<PostTag> list = db.PostTags.Where(x => x.isDeleted == false && x.PostID == PostID).ToList();

                foreach (var item in list)
                {
                    item.isDeleted = true;
                    item.DeletedDate = DateTime.Now;
                    item.LastUpdateDate = DateTime.Now;
                    item.LastUpdateUserID = UserStatic.UserID;
                }

                db.SaveChanges();
            }

        }

        public List<PostImageDTO> DeletePost(int ID)
        {
            try
            {
                List<PostImageDTO> list = new List<PostImageDTO>();
                using (POSTDATAEntities db = new POSTDATAEntities())
                {

                    Post post = db.Posts.First(x => x.ID == ID);
                    post.isDeleted = true;
                    post.DeletedDate = DateTime.Now;
                    post.LastUpdateDate = DateTime.Now;
                    post.LastUpdateUserID = UserStatic.UserID;
                    db.SaveChanges();


                    List<PostImage> postImage = db.PostImages.Where(x => x.isDeleted == false && x.PostID == ID).ToList();

                   

                    foreach (var item in postImage)
                    {
                        PostImageDTO image = new PostImageDTO();
                        image.ImagePath = item.ImagePath;
                        item.isDeleted = true;
                        item.DeletedDate = DateTime.Now;
                        item.LastUpdateDate = DateTime.Now;
                        item.LastUpdateUserID = UserStatic.UserID;

                        list.Add(image);
                    }

                    db.SaveChanges();
                }

                return list;

            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

       

        public string DeletePostImage(int ID)
        {
            try
            {
                string imagePath = "";
                using (POSTDATAEntities db = new POSTDATAEntities())
                {
                    PostImage postImage = db.PostImages.First(x => x.ID == ID);
                     imagePath = postImage.ImagePath;
                    postImage.isDeleted = true;
                    postImage.DeletedDate = DateTime.Now;
                    postImage.LastUpdateDate = DateTime.Now;
                    postImage.LastUpdateUserID = UserStatic.UserID;

                    db.SaveChanges();
                }
                return imagePath;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        
        public int GetUnApprovedCommentCount()
        {
           int totalUnApprovedComment = 0;
            using (POSTDATAEntities db = new POSTDATAEntities())
            {

                totalUnApprovedComment = db.Comments.Where(x => x.isDeleted == false && x.isApproved == false).Count();
            }
            return totalUnApprovedComment;

        }

        public int GetUnreadMessageCount()
        {
            int totalUnreadMessages = 0;
            using (POSTDATAEntities db = new POSTDATAEntities())
            {
                totalUnreadMessages = db.Contacts.Where(x => x.isDeleted == false && x.isRead == false).Count();
            }
            return totalUnreadMessages;
        
        }





        //user side-------------------------------------------------------------------
        public List<PostDTO> GetHotNews()
        {
            //need to add too list to convert list
            List<PostDTO> dtoList = new List<PostDTO>();
            using (POSTDATAEntities db = new POSTDATAEntities())
            {

                //Area 1 - hot news
                var postList = (from p in db.Posts.Where(x => x.isDeleted == false && x.Area1 == true)
                                join c in db.Categories on p.CategoryID equals c.ID
                                select new
                                {
                                    ID = p.ID,
                                    Title = p.Title,
                                    categoryname = c.CategoryName,
                                    AddDate = p.AddDate,
                                    SeoLink = p.SeoLink
                                }).OrderByDescending(x => x.AddDate).Take(8).ToList();



                foreach (var item in postList)
                {
                    PostDTO dto = new PostDTO();
                    dto.Title = item.Title;
                    dto.ID = item.ID;
                    dto.CategoryName = item.categoryname;
                    dto.AddDate = item.AddDate;
                    dto.SeoLink = item.SeoLink;
                    dtoList.Add(dto);
                }
            }

            return dtoList;
        }


        public void AddComment(Comment comment)
        {
            try
            {
                using (POSTDATAEntities db = new POSTDATAEntities())
                {
                    db.Comments.Add(comment);
                    db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public List<CommentDTO> GetCommentsList()
        {
            //this is for unapproved comment list
            List<CommentDTO> dtoList = new List<CommentDTO>();

            using (POSTDATAEntities db = new POSTDATAEntities())
            {
                //we need post title for comment so join post table and comment table
                var list = (from c in db.Comments.Where(x => x.isDeleted == false && x.isApproved == false)
                            join p in db.Posts on c.PostID equals p.ID
                            select new
                            {
                                ID = c.ID,
                                PostTitle = p.Title,
                                Email = c.Email,
                                Content = c.CommentContent,
                                AddDate = c.AddDate
                            }).OrderByDescending(x => x.AddDate).ToList();

                foreach (var item in list)
                {
                    CommentDTO dto = new CommentDTO();
                    dto.ID = item.ID;
                    dto.PostTitle = item.PostTitle;
                    dto.Email = item.Email;
                    dto.CommentContent = item.Content;
                    dto.AddDate = item.AddDate;
                    dtoList.Add(dto);
                }
            }

            return dtoList;
        }


        public void ApproveComment(int ID)
        {
            using (POSTDATAEntities db = new POSTDATAEntities())
            {
                Comment comment = db.Comments.First(x => x.ID == ID);
                comment.isApproved = true;
                comment.ApproveUserID = UserStatic.UserID;
                comment.ApproveDate = DateTime.Now;
                comment.LastUpdateDate = DateTime.Now;
                comment.LastUpdateUserID = UserStatic.UserID;
                db.SaveChanges();
            }
        }

        public void DeleteComment(int ID)
        {
            using (POSTDATAEntities db = new POSTDATAEntities())
            {
                Comment comment = db.Comments.First(x => x.ID == ID);
                comment.isDeleted = true;
                comment.DeletedDate = DateTime.Now;
                comment.LastUpdateDate = DateTime.Now;
                comment.LastUpdateUserID = UserStatic.UserID;
                db.SaveChanges();
            }
        }

        public List<CommentDTO> GetAllComments()
        {
            List<CommentDTO> dtoList = new List<CommentDTO>();

            using (POSTDATAEntities db = new POSTDATAEntities())
            {
                //we need post title for comment so join post table and comment table
                var list = (from c in db.Comments.Where(x => x.isDeleted == false)
                            join p in db.Posts on c.PostID equals p.ID
                            select new
                            {
                                ID = c.ID,
                                PostTitle = p.Title,
                                Email = c.Email,
                                Content = c.CommentContent,
                                AddDate = c.AddDate,
                                isapproved = c.isApproved
                            }).OrderByDescending(x => x.AddDate).ToList();

                foreach (var item in list)
                {
                    CommentDTO dto = new CommentDTO();
                    dto.ID = item.ID;
                    dto.PostTitle = item.PostTitle;
                    dto.Email = item.Email;
                    dto.CommentContent = item.Content;
                    dto.AddDate = item.AddDate;
                    dto.isApproved = item.isapproved;
                    dtoList.Add(dto);
                }
            }

            return dtoList;
        }


        public CountDTO GetAllCounts()
        {
            CountDTO dto = new CountDTO();
            using (POSTDATAEntities db = new POSTDATAEntities())
            {
                dto.PostCount = db.Posts.Where(x => x.isDeleted == false).Count();
                dto.CommentCount = db.Comments.Where(x => x.isDeleted == false).Count();
                dto.MessageCount = db.Contacts.Where(x => x.isDeleted == false).Count();
                dto.ViewCount = db.Posts.Where(x => x.isDeleted == false).Sum(x => x.ViewCount);
            }
            return dto;
        }




    }
}
