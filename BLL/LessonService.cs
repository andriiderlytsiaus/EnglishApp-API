using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using YoutubeTranscriptApi;

namespace BLL
{
    public  class LessonService
    {
        public static List<TranscriptItem> GetTranscript(string youTubeVideoId)
        {
            using (var youTubeTranscriptApi = new YouTubeTranscriptApi())
            {
                var transcriptItems = youTubeTranscriptApi
                    .GetTranscript(youTubeVideoId).ToList();
                foreach (var transcriptItem in transcriptItems)
                {
                    Console.WriteLine(transcriptItem.Text);
                }   
                return transcriptItems;
            }
        }
        public static string GetYouTubeVideoId(string youtubeLink)
        {
            Regex regex = new Regex(@"(?:youtube\.com\/(?:[^\/\n\s]+\/\S+\/|(?:v|e(?:mbed)?)\/|\S*?[?&]v=)|youtu\.be\/)([a-zA-Z0-9_-]{11})", RegexOptions.IgnoreCase);
            Match match = regex.Match(youtubeLink);
            if (match.Success)
            {
                return match.Groups[1].Value;
            }
            return null;
        }
    }
}
