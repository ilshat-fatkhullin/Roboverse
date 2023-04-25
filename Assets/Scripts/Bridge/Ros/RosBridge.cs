using Assets.Scripts.Settings;
using Unity.Robotics.ROSTCPConnector;
using Unity.Robotics.ROSTCPConnector.MessageGeneration;

namespace Assets.Scripts.Bridge.Ros
{
    public class RosBridge : IRosBridge
    {
        public ISettings Settings { get; }

        private readonly ROSConnection _connection;

        public RosBridge(RosSettings settings)
        {
            Settings = settings;

            _connection = ROSConnection.GetOrCreateInstance();
            _connection.RosIPAddress = settings.IpAddress;
            _connection.RosPort = settings.Port;
        }

        public string Name => "ROS";

        public IPublisher<T> CreatePublisher<T>(string topic) where T : Message
        {
            return new RosPublisher<T>(topic, _connection);
        }

        public ISubscriber<T> CreateSubscriber<T>(string topic) where T : Message
        {
            return new RosSubscriber<T>(topic, _connection);
        }

        public void Dispose()
        {
            _connection.Disconnect();
        }
    }
}
