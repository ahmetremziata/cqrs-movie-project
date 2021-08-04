namespace Logic.Requests
{
    public class InsertPersonInfoRequest
    {
        public string Name { get; set; }
        public string RealName { get; set; }
        public string Biography { get; set; }
        public string AvatarUrl { get; set; }
        public string BirthDate { get; set; }
        public string BirthPlace { get; set; }
        public string DeathDate { get; set; }
        public string DeathPlace { get; set; }
    }
}