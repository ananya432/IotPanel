using MQTTnet;
using MQTTnet.Server;

namespace IotPanel.NewFolder
{
    public class MQTTHub :  IHostedService, IDisposable

    {

        private readonly MqttServer _server;

        private bool disposedValue;

        public MQTTHub()

        {

            var mqttFactory = new MqttFactory();

            var mqttServerOptions = mqttFactory.CreateServerOptionsBuilder().WithDefaultEndpoint().Build();

            _server = mqttFactory.CreateMqttServer(mqttServerOptions); 

        }

        public async Task StartAsync(CancellationToken cancellationToken)

        {

            _server.ClientConnectedAsync += _server_ClientConnectedAsync;

            await _server.StartAsync();

            await Task.CompletedTask;

        }

        private Task _server_ClientConnectedAsync(ClientConnectedEventArgs arg)
        {

            return Task.CompletedTask;

        }



        public async Task PublishMessageFromHub(string topic, string payload, string senderClientId)

        {

            MqttApplicationMessage message = new MqttApplicationMessageBuilder().WithTopic(topic).WithPayload(payload).Build();

            await _server.InjectApplicationMessage(

                new InjectedMqttApplicationMessage(message)

                {
                    SenderClientId = senderClientId
                });

        }



        public async Task StopAsync(CancellationToken cancellationToken)

        {

            await _server.StopAsync();

            await Task.CompletedTask;

        }



        protected virtual void Dispose(bool disposing)

        {

            if (!disposedValue)

            {

                if (disposing)

                {

                    _server.Dispose();

                    // TODO: dispose managed state (managed objects)

                }



                // TODO: free unmanaged resources (unmanaged objects) and override finalizer

                // TODO: set large fields to null

                disposedValue = true;

            }

        }



        // // TODO: override finalizer only if 'Dispose(bool disposing)' has code to free unmanaged resources

        // ~MQTTHub()

        // {

        //     // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method

        //     Dispose(disposing: false);

        // }



        public void Dispose()

        {

            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method

            Dispose(disposing: true);

            GC.SuppressFinalize(this);

        }

    }
}
