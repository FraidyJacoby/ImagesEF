using System;

namespace ImagesEF.Data
{
    public class Image
    {
        public int Id { get; set; }
        public string Link { get; set; }
        public string Title { get; set; }
        public DateTime Date { get; set; }
        public int LikeCount { get; set; }
    }
}
