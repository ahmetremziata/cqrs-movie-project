using System;
using System.Collections.Generic;

namespace Logic.Responses
{
    public class MovieDetailResponse   
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string OriginalName { get; set; }
        public string Description { get; set; }
        public int ConstructionYear { get; set; }
        public int TotalMinute { get; set; }
        public string PosterUrl { get; set; }
        public DateTime? VisionEntryDate { get; set; }
        public bool IsActive { get; set; }
        public int TotalActorCount { get; set; }
        public List<MovieTypeResponse> Types { get; set; }
        public List<MovieCountryResponse> Countries { get; set; }
        public List<MovieActorResponse> Actors { get; set; }
    }

    public class MovieTypeResponse
    {
        public int TypeId { get; set; }
        public string Name { get; set; }
    }
    
    public class MovieCountryResponse
    {
        public int CountryId { get; set; }
        public string Name { get; set; }
    }
    
    public class MovieActorResponse
    {
        public int ActorId { get; set; }
        public string Name { get; set; }
        public string CharacterName { get; set; }
    }
}