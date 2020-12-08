using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ImagesEF.Data
{
    public class ImageRepository
    {
        private string _connectionString;

        public ImageRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public IEnumerable<Image> GetImages()
        {
            using(var context = new ImageContext(_connectionString))
            {
                return context.Images.OrderByDescending(i => i.Date).ToList();
            }
        }

        public Image GetImageById(int id)
        {
            using(var context = new ImageContext(_connectionString))
            {
                return context.Images.FirstOrDefault(i => i.Id == id);
            }
        }

        public void AddImage(Image image)
        {
            using (var context = new ImageContext(_connectionString))
            {
                context.Images.Add(image);
                context.SaveChanges();
            }
        }

        public void UpdateLikeCount(int id)
        {
            using(var context = new ImageContext(_connectionString))
            {
                context.Database.ExecuteSqlCommand(
                    "UPDATE Images SET LikeCount = LikeCount + 1 WHERE Id = @id",
                    new SqlParameter("@id", id));
            }
        }

        public int GetLikeCount(int id)
        {
            using (var context = new ImageContext(_connectionString))
            {

                var image = context.Images.FirstOrDefault(i => i.Id == id);
                if (image != null)
                {
                    return image.LikeCount;
                }
                return -1;
            }
        }

    }
}
