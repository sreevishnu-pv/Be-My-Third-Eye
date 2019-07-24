using ThirdEye.Services.Models;

namespace ThirdEye.Services.Services
{
    public static class ComputerVisionServices
    {
        public static readonly string[] ExcludedWords = { "person", "dance", "outdoor", "smile", "dancer", "clothing", "woman", "human face", "royal", "socie", "towers", "residences", "the", "of", "to", "new", "era", "lead", "future" };
        public static decimal Latitude { get; set; }
        public static decimal Longitude { get; set; }
        public static byte[] Image { get; set; }
        public static string FileName { get; set; }
        public static ImageSearchResponse ImageSearchResponse { get; set; }
    }
}
