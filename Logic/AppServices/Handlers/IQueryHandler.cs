using System.Threading.Tasks;

namespace Logic.AppServices.Handlers
{
    public interface IQueryHandler<TQuery, TResult> where TQuery : IQuery<TResult>
    {
        Task<TResult> Handle(TQuery command);
    }
}