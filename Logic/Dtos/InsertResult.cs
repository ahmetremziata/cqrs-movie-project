using CSharpFunctionalExtensions;
using Logic.Responses;

namespace Logic.Dtos
{
    public class InsertResult
    {
        public Result Result { get; set; }
        public InsertResponse InsertResponse { get; set; }
    }
}