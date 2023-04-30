using Assets.Scripts.Settings;
using Unity.Robotics.ROSTCPConnector;
using Unity.Robotics.ROSTCPConnector.MessageGeneration;

namespace Assets.Scripts.Bridge.Ros
{
    public class RosBridge : IRosBridge
    {
        public ISettings Settings { get; }

        public string Name => "ROS";

        private readonly ROSConnection _connection;

        public RosBridge(RosSettings settings)
        {
            Settings = settings;

            _connection = ROSConnection.GetOrCreateInstance();

            _connection.RosIPAddress = settings.IpAddress;
            _connection.RosPort = settings.Port;

            settings.IpAddressChanged += Settings_IpAddressChanged;
            settings.PortChanged += Settings_PortChanged;
        }

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

        private void Settings_IpAddressChanged(object sender, string ipAddress)
        {
            _connection.RosIPAddress = ipAddress;
        }

        private void Settings_PortChanged(object sender, int port)
        {
            _connection.RosPort = port;
        }
    }
}
