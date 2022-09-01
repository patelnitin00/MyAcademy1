using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class VideoDAO
    {
        public int AddVideo(Video video)
        {
            using(POSTDATAEntities db = new POSTDATAEntities())
            {
                try
                {
                    db.Videos.Add(video);
                    db.SaveChanges();
                    return video.ID;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
           
        }

        public List<VideoDTO> GetVideoList()
        {
            using (POSTDATAEntities db = new POSTDATAEntities())
            {
                List<VideoDTO> list = new List<VideoDTO>();

                List<Video> videoList = db.Videos.Where(x => x.isDeleted == false)
                    .OrderByDescending(x => x.AddDate).ToList();

                foreach (var video in videoList)
                {
                    VideoDTO videoDTO = new VideoDTO();
                    videoDTO.ID = video.ID;
                    videoDTO.Title = video.Title;
                    videoDTO.VideoPath = video.VideoPath;
                    videoDTO.OriginalVideoPath = video.OriginalVideoPath;
                    videoDTO.AddDate = video.AddDate;

                    list.Add(videoDTO);
                }

                return list;
            }
            
        }

   

        public VideoDTO GetVideoWithID(int ID)
        {
            using(POSTDATAEntities db = new POSTDATAEntities())
            {
                VideoDTO videoDTO = new VideoDTO();

                Video video = db.Videos.First(x => x.ID == ID);

                videoDTO.ID = video.ID;
                videoDTO.OriginalVideoPath = video.OriginalVideoPath;
                videoDTO.Title = video.Title;

                return videoDTO;
            }
           
        }

        

        public void UpdateVideo(VideoDTO model)
        {
            using(POSTDATAEntities db = new POSTDATAEntities())
            {
                try
                {
                    Video video = db.Videos.First(x => x.ID == model.ID);

                    video.Title = model.Title;
                    video.VideoPath = model.VideoPath;
                    video.OriginalVideoPath = model.OriginalVideoPath;
                    // video.AddDate = model.AddDate;
                    video.LastUpdateDate = DateTime.Now;

                    video.LastUpdateUserID = UserStatic.UserID;

                    db.SaveChanges();

                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            
        }

        public void DeleteVideo(int ID)
        {
            using (POSTDATAEntities db = new POSTDATAEntities())
            {
                try
                {
                    Video video = db.Videos.First(x => x.ID == ID);
                    video.isDeleted = true;
                    video.DeletedDate = DateTime.Now;
                    video.LastUpdateDate = DateTime.Now;
                    video.LastUpdateUserID = UserStatic.UserID;

                    db.SaveChanges();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
           
        }

    }
}
