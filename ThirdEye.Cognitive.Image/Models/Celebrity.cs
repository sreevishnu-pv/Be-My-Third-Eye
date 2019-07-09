namespace ThirdEye.Cognitive.Image.Models
{
    public class Celebrity
    {
        public string Name { get; set; }
        public double Confidence { get; set; }
        public FaceRectangle FaceRectangle { get; set; }
    }
}
