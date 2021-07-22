using System;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using Logic.AppServices;
using Logic.AppServices.Commands;
using Logic.AppServices.Commands.Handlers;
using Logic.AppServices.Queries;
using Logic.AppServices.Queries.Handlers;
using Logic.Dtos;

namespace Logic.Utils
{
    public sealed class Messages
    {
        private readonly IServiceProvider _provider;

        public Messages(IServiceProvider provider)
        {
            _provider = provider;
        }

        public async Task<Result> Dispatch(ICommand command)
        {
            Type type = typeof(ICommandHandler<>);
            Type[] typeArgs = {command.GetType()};
            Type handlerType = type.MakeGenericType(typeArgs);

            dynamic handler = _provider.GetService(handlerType);
            Result result = await handler.Handle((dynamic) command);
            return result;
        }
        
        public async Task<InsertResult> InsertDispatch(ICommand command)
        {
            Type type = typeof(IInsertCommandHandler<>);
            Type[] typeArgs = {command.GetType()};
            Type handlerType = type.MakeGenericType(typeArgs);

            dynamic handler = _provider.GetService(handlerType);
            InsertResult result = await handler.Handle((dynamic) command);
            return result;
        }

        public async Task<T> Dispatch<T>(IQuery<T> query)
        {
            Type type = typeof(IQueryHandler<,>);
            Type[] typeArgs = {query.GetType(), typeof(T)};
            Type handlerType = type.MakeGenericType(typeArgs);

            dynamic handler = _provider.GetService(handlerType);
            T result = await handler.Handle((dynamic) query);
            return result;
        }
    }
}