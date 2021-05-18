using System.Threading.Tasks;

namespace Logic.Business.Service.Kafka.Interfaces
{
    public interface IProducerService
    {
        Task<bool> Produce(string topicName, object message);
    }
}