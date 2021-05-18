using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using Logic.AppServices.Commands;

namespace Logic.AppServices.Handlers
{
    public interface ICommandHandler<TCommand> where TCommand : ICommand
    {
        Task<Result> Handle(TCommand command);
    }
}