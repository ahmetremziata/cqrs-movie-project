using System.Threading.Tasks;

namespace Logic.AppServices
{
    public interface IQueryHandler<TQuery, TResult> where TQuery : IQuery<TResult>
    {
        Task<TResult> Handle(TQuery command);
    }
}