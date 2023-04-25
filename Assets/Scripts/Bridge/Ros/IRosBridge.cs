﻿using Unity.Robotics.ROSTCPConnector.MessageGeneration;

namespace Assets.Scripts.Bridge.Ros
{
    public interface IRosBridge : IBridge
    {
        public IPublisher<T> CreatePublisher<T>(string topic) where T : Message;

        public ISubscriber<T> CreateSubscriber<T>(string topic) where T : Message;
    }
}
