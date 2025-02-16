﻿using System.ComponentModel.DataAnnotations;

namespace Cinema.Payloads.Requests.Request_Movie
{
    public class Request_CreateMovie
    {
        [Range(0,100)]
        public int MovieDuration { get; set; }
        
        public DateTime EndTime { get; set; }
        public DateTime PremiereDate { get; set; }
        public string Description { get; set; }
        public string Director { get; set; }
        public IFormFile Image { get; set; }
        public IFormFile HeroImage { get; set; }
        public string Language { get; set; }
        public int MovieTypeId { get; set; }
        public string Name { get; set; }
        public int? RateId { get; set; }
        public string Trailer { get; set; }

    }
}
