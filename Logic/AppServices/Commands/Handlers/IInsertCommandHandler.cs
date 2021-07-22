using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using Logic.Dtos;

namespace Logic.AppServices.Commands.Handlers
{
    public interface IInsertCommandHandler<TCommand> where TCommand : ICommand
    {
        Task<InsertResult> Handle(TCommand command);
    }
}