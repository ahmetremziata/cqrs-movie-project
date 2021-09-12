using System;
using CSharpFunctionalExtensions;
using MediatR;

namespace Logic.AppServices.Commands
{
    public sealed class EditMovieInfoCommandWithMediatr : IRequest<Result>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string OriginalName { get; set; }
        public string Description { get; set; }
        public int ConstructionYear { get; set; }
        public int TotalMinute { get; set; }
        public string PosterUrl { get; set; }
        public DateTime? VisionEntryDate { get; set; }
        
        public EditMovieInfoCommandWithMediatr(int id, 
            string name, 
            string originalName, 
            string description, 
            int  constructionYear, 
            int totalMinute, 
            string posterUrl, 
            DateTime? visionEntryDate)
        {
            Id = id;
            Name = name;
            OriginalName = originalName;
            Description = description;
            ConstructionYear = constructionYear;
            TotalMinute = totalMinute;
            PosterUrl = posterUrl;
            VisionEntryDate = visionEntryDate;
        }
    }
}