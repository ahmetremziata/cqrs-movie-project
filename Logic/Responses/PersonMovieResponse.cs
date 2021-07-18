using Logic.Responses.Base;

namespace Logic.Responses
{
    public class PersonMovieResponse : BaseMovie
    {
        public string Role { get; set; }
        public string CharacterName { get; set; }
    }
}