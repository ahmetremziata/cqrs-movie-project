using System.Threading.Tasks;
using Confluent.Kafka;
using Logic.Business.Service.Kafka.Interfaces;
using Newtonsoft.Json;

namespace Logic.Business.Service.Kafka
{
    public class ProducerService : IProducerService
    {
        private readonly ProducerConfig _config;

        public ProducerService(ProducerConfig config)
        {
            _config = config;
        }

        public async Task<bool> Produce(string topicName, object message)
        {
            using (var producer = new ProducerBuilder<Null, string>(_config).Build())
            {
                var result = await producer.ProduceAsync(topicName, new Message<Null, string> { Value = JsonConvert.SerializeObject(message)});

                return result.Status != PersistenceStatus.NotPersisted;
            }        
        }
    }
}