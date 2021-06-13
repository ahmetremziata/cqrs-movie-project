namespace Logic.Responses
{
    public class MovieResponse   
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string OriginalName { get; set; }
        public int ConstructionYear { get; set; }
        public int TotalMinute { get; set; }
        public bool IsActive { get; set; }
    }
}