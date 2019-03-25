using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace BackEnd.Movie.DataWriteRepository.Models
{
    /// <summary>
    /// This model represent the data model is returning from third party API response.
    /// </summary>
    public class UpdatedMovieModel
    {
        [JsonProperty("ID")]
        public string MovieReferenceId { get; set; }

        [JsonProperty("Title")]
        public string MovieTitle { get; set; }

        [JsonProperty("Year")]        
        public string ReleasedYear { get; set; }

        [JsonProperty("Price")]          
        public string Price { get; set; }

        [JsonProperty("Released")]
        public string ReleadedDate { get; set; }
    }
}
