using System.Collections.Generic;

namespace ThirdEye.Cognitive.Image.Models
{
    public class ImageTagResponse
    {
        public List<Category> Categories { get; set; }
        public Adult Adult { get; set; }
        public Color Color { get; set; }
        public ImageType ImageType { get; set; }
        public List<Tag> Tags { get; set; }
        public Description Description { get; set; }
        public List<Face> Faces { get; set; }
        public string RequestId { get; set; }
        public Metadata Metadata { get; set; }
    }
}
