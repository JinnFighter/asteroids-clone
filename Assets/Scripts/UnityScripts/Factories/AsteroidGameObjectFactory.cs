using DataContainers;
using Helpers;
using Logic.Factories;
using Physics;
using UnityEngine;
using UnityScripts.Containers;
using UnityScripts.Presentation.Views;
using Vector2 = Common.Vector2;

namespace UnityScripts.Factories
{
    public class AsteroidGameObjectFactory : AsteroidFactory
    {
        private readonly AsteroidFactory _wrappedFactory;
        private readonly IObjectSelector<GameObject>[] _objectSelectors;
        private readonly ITransformPresenterFactory _transformPresenterFactory;
        private GameObject _gameObject;

        public AsteroidGameObjectFactory(AsteroidFactory wrappedFactory, PrefabsContainer prefabsContainer, IRandomizer randomizer, ITransformPresenterFactory transformPresenterFactory)
        {
            _wrappedFactory = wrappedFactory;
            _objectSelectors = new IObjectSelector<GameObject>[]
            {
                new GameObjectRandomSelector(prefabsContainer.SmallAsteroidsPrefabs, randomizer),
                new GameObjectRandomSelector(prefabsContainer.MediumAsteroidsPrefabs, randomizer),
                new GameObjectSingleSelector(prefabsContainer.BigAsteroidPrefab)
            };
            _transformPresenterFactory = transformPresenterFactory;
        }
        
        public override void SetStage(int stage)
        {
            Stage = stage;
            _wrappedFactory.SetStage(stage);
        }

        public override BodyTransform CreateTransform(Vector2 position, float rotation, Vector2 direction)
        {
            var transform = _wrappedFactory.CreateTransform(position, rotation, direction);
            _gameObject = Object.Instantiate(_objectSelectors[Stage - 1].GetObject(), 
                new UnityEngine.Vector2(position.X, position.Y), Quaternion.identity);
            
            var presenter = _transformPresenterFactory.CreatePresenter(transform, _gameObject.GetComponent<TransformBodyView>());
            
            return transform;
        }

        public override PhysicsRigidBody CreateRigidBody(float mass, bool useGravity) =>
            _wrappedFactory.CreateRigidBody(mass, useGravity);

        public override PhysicsCollider CreateCollider(Vector2 position)
        {
            var spriteRenderer = _gameObject.GetComponent<SpriteRenderer>();
            var rect = spriteRenderer.sprite.bounds;
            var size = rect.size;
            return new BoxPhysicsCollider(position, size.x, size.y);
        }
    }
}
