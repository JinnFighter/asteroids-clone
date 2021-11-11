using Logic.Factories;
using Physics;
using UnityEngine;
using UnityScripts.Containers;
using UnityScripts.Presentation.Models;
using UnityScripts.Presentation.Presenters;
using UnityScripts.Presentation.Views;
using Vector2 = Common.Vector2;

namespace UnityScripts.Factories
{
    public class ShipGameObjectFactory : ShipFactory
    {
        private readonly ShipFactory _wrappedFactory;
        private readonly PrefabsContainer _prefabsContainer;
        private GameObject _gameObject;

        public ShipGameObjectFactory(ShipFactory wrappedFactory, PrefabsContainer prefabsContainer)
        {
            _wrappedFactory = wrappedFactory;
            _prefabsContainer = prefabsContainer;
        }

        public override BodyTransform CreateTransform(Vector2 position, float rotation, Vector2 direction)
        {
            var transform = _wrappedFactory.CreateTransform(position, rotation, direction);

            _gameObject = Object.Instantiate(_prefabsContainer.ShipPrefab, new UnityEngine.Vector2(position.X, position.Y),
                Quaternion.identity);
            
            var physicsBodyModel = new TransformBodyModel(transform.Position.X, transform.Position.Y);
            transform.PositionChangedEvent += physicsBodyModel.UpdatePosition;
            transform.RotationChangedEvent += physicsBodyModel.UpdateRotation;
            transform.DestroyEvent += physicsBodyModel.Destroy;
            var presenter = new TransformBodyPresenter(physicsBodyModel, _gameObject.GetComponent<TransformBodyView>());

            return transform;
        }

        public override PhysicsRigidBody CreateRigidBody(float mass, bool useGravity) =>
            _wrappedFactory.CreateRigidBody(mass, useGravity);

        public override PhysicsCollider CreateCollider(Vector2 position)
        {
            var spriteRenderer = _gameObject.GetComponent<SpriteRenderer>();
            var rect = spriteRenderer.sprite.bounds;
            var size = rect.size;
            var collider = new BoxPhysicsCollider(position, size.x, size.y);
            
            return collider;
        }
    }
}
