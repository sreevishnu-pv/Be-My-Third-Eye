using System.Collections.Generic;
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
        public static BingWebSearchResult BingWebSearchResult { get; set; }
        public static Dictionary<string, List<string>> ParagraphsFromInternet { get; set; } = new Dictionary<string, List<string>>();
        public static string Story { get; set; }
        public static string Location { get; set; }
    }
}
