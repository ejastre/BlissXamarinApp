using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace BlissXamarinApp.Models
{
    public class Question
    {
        public int Id { get; set; }

        [JsonProperty("question")]
        public string QuestionName { get; set; }

        public string Image_Url { get; set; }
        public string Thumb_Url { get; set; }
        public DateTime Published_At { get; set; }
        public List<QuestionChoice> Choices { get; set; }
    }
}
