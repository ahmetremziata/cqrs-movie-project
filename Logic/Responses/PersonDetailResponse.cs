using System.Collections.Generic;

namespace Logic.Responses
{
    public class PersonDetailResponse 
    {
        public int PersonId { get; set; }
        public string Name { get; set; }
        public string RealName { get; set; }
        public string Biography { get; set; }
        public string AvatarUrl { get; set; }
        public string BirthDate { get; set; }
        public string DeathDate { get; set; }
        public string BirthPlace { get; set; }
        public string DeathPlace { get; set; }
        
        public List<PersonMovieResponse> Movies { get; set; }
    }
}