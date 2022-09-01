using DAL;
using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class PostBLL
    {
        PostDAO dao = new PostDAO();
        public bool AddPost(PostDTO model)
        {
            Post post = new Post();
            post.Title = model.Title;
            post.PostContent = model.PostContent;
            post.ShortContent = model.ShortContent;
            post.Slider = model.Slider;
            post.Area1 = model.Area1;
            post.Area2 = model.Area2;
            post.Area3 = model.Area3;
            post.Notification = model.Notification;
            post.CategoryID = model.CategoryID;

            //using SeoLink from BLL
            //method to generate user friendly url in C#
            post.SeoLink = SeoLink.GenerateUrl(model.Title);

            post.LanguageName = model.Language;
            post.AddDate = DateTime.Now;
            post.AddUserID = UserStatic.UserID;
            post.LastUpdateUserID = UserStatic.UserID;
            post.LastUpdateDate = DateTime.Now;

            int ID = dao.AddPost(post);

            LogDAO.AddLog(General.ProcessType.PostAdd,
                General.TableName.Post, ID);

            SavePostImage(model.PostImages, ID);

            AddTag(model.TagText, ID);

            return true;
        }

       

        private void AddTag(string tagText, int PostID)
        {
            string[] tags;
            //what if user do not enter any tag - handle exception or provide required attribute
            tags = tagText.Split(',');

            List<PostTag> tagList = new List<PostTag>();

            foreach(var item in tags)
            {
                PostTag tag = new PostTag();
                tag.PostID = PostID;
                tag.TagContent = item;
                tag.AddDate = DateTime.Now;
                tag.LastUpdateDate= DateTime.Now;
                tag.LastUpdateUserID= UserStatic.UserID;
                tagList.Add(tag);
            }

            foreach(var item in tagList)
            {
                int tagID = dao.AddTag(item);
                LogDAO.AddLog(General.ProcessType.TagAdd,
                    General.TableName.Tag,tagID);
            }
        }

        

        void SavePostImage(List<PostImageDTO> list, int PostID)
        {
            //now we will save our post but befor that
            //we need to save our images and tags as well

            //save images
            List<PostImage> imageList = new List<PostImage>();
            foreach (var item in list)
            {
                PostImage image = new PostImage();
                image.PostID = PostID;
                image.ImagePath = item.ImagePath;
                image.AddDate = DateTime.Now;
                image.LastUpdateDate = DateTime.Now;
                image.LastUpdateUserID = UserStatic.UserID;
                imageList.Add(image);
            }

            //now call add image method
            foreach (var item in imageList)
            {
                int imageID = dao.AddImage(item);
                LogDAO.AddLog(General.ProcessType.ImageAdd,
                    General.TableName.Image, imageID);
            }
        }

       

        public List<PostDTO> GetPostList()
        {
            return dao.GetPostList();
        }

        public PostDTO GetPostWithID(int ID)
        {
            PostDTO dto = new PostDTO();
            dto = dao.GetPostWithID(ID);

            //now we need to get all images from the post
            dto.PostImages = dao.getPostImagesWithPostID(ID);

            //now we will get tags with postID
            List<PostTag> tagList = dao.GetPostTagsWithPostID(ID);

            //now we will convert tag list to string value (, seperated)
            string tagValue = "";
            foreach(var item in tagList)
            {
                tagValue += item.TagContent;
                tagValue += ",";
            }
            dto.TagText = tagValue;

            return dto; 
        }

        public bool UpdatePost(PostDTO model)
        {
            //we first need to adjust seoLink
            model.SeoLink = SeoLink.GenerateUrl(model.Title);
            dao.UpdatePost(model);

            LogDAO.AddLog(General.ProcessType.PostUpdate,
                General.TableName.Post, model.ID);

            //for images we can call add post images method

            if(model.PostImages != null)
            {
                SavePostImage(model.PostImages, model.ID);
            }

            //for tags we will first delet the old tags from database
            dao.DeleteTag(model.ID);

            //now we will add tags by calling add tags method
            AddTag(model.TagText, model.ID);
            return true;



        }

        public string DeletePostImage(int ID)
        {
            string imagePath = dao.DeletePostImage(ID);
            LogDAO.AddLog(General.ProcessType.ImageDelete, General.TableName.Image,ID);
            return imagePath;
        }

        public List<PostImageDTO> DeletePost(int ID)
        {
            List<PostImageDTO> list = dao.DeletePost(ID);
            LogDAO.AddLog(General.ProcessType.PostDelete, General.TableName.Post, ID);
            return list;
        }


        public bool AddComment(GeneralDTO model)
        {
            Comment comment = new Comment();
            comment.PostID = model.PostID;
            comment.NameSurname = model.Name;
            comment.Email = model.Email;
            comment.CommentContent = model.Message;
            comment.AddDate = DateTime.Now;

            dao.AddComment(comment);

            return true;

        }

        //comment
        public List<CommentDTO> GetCommentsList()
        {
            return dao.GetCommentsList();
        }


        public void ApproveComment(int ID)
        {
            dao.ApproveComment(ID);
            LogDAO.AddLog(General.ProcessType.CommentApprove, General.TableName.Comment, ID);
        }

        public void DeleteComment(int ID)
        {
            dao.DeleteComment(ID);
            LogDAO.AddLog(General.ProcessType.CommenDelete, General.TableName.Comment, ID);
        }

        public List<CommentDTO> GetAllComments()
        {
            return dao.GetAllComments();
        }

        public CountDTO GetCounts()
        {
            CountDTO dto = new CountDTO();
            dto.MessageCount = dao.GetUnreadMessageCount();
            dto.CommentCount = dao.GetUnApprovedCommentCount();

            return dto;
        }



        public CountDTO GetAllCounts()
        {
            return dao.GetAllCounts();
        }



    }
}
