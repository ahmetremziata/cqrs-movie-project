using System.Threading.Tasks;
using CSharpFunctionalExtensions;

namespace Logic.AppServices
{
    public interface ICommandHandler<TCommand> where TCommand : ICommand
    {
        Task<Result> Handle(TCommand command);
    }
}