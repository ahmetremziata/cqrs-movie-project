using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using Logic.Data.DataContexts;
using Logic.Data.Entities;
using Logic.Data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Logic.AppServices
{
    public sealed class DeleteMovieCommandHandler : ICommandHandler<DeleteMovieCommand>
    {
        private readonly MovieDataContext _dataContext;

        public DeleteMovieCommandHandler(MovieDataContext dataContext)
        {
            _dataContext = dataContext;
        }
        
        public async Task<Result> Handle(DeleteMovieCommand command)
        {
            return Result.Success();
        }
    }
}