using System.Threading.Tasks;
using CSharpFunctionalExtensions;

namespace Logic.AppServices.Commands.Handlers
{
    public interface ICommandHandler<TCommand> where TCommand : ICommand
    {
        Task<Result> Handle(TCommand command);
    }
}