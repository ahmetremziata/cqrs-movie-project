using System;

namespace Logic.AppServices
{
    public sealed class EditMovieInfoCommand : ICommand
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string OriginalName { get; set; }
        public string Description { get; set; }
        public int ConstructionYear { get; set; }
        public int TotalMinute { get; set; }
        public string PosterUrl { get; set; }
        public DateTime? VisionEntryDate { get; set; }
    }
}