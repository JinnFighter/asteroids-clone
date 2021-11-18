using System;
using System.Collections.Generic;
using Logic.Factories;

namespace Logic.Containers
{
    public class ColliderFactoryContainer
    {
        private readonly Dictionary<Type, IColliderFactory> _colliderFactories;

        public ColliderFactoryContainer()
        {
            _colliderFactories = new Dictionary<Type, IColliderFactory>();
        }

        public void AddColliderFactory<T>(IColliderFactory colliderFactory) where T : struct =>
            _colliderFactories[typeof(T)] = colliderFactory;

        public IColliderFactory GetFactory<T>() where T : struct => _colliderFactories[typeof(T)];
    }
}
