using System.Collections.Generic;

namespace ThirdEye.Cognitive.Image.Models
{
    public class Description
    {
        public List<string> Tags { get; set; }
        public List<Caption> Captions { get; set; }
    }
}
