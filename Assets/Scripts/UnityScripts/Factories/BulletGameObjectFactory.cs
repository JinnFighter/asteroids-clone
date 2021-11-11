using Logic.Factories;
using Physics;
using UnityEngine;
using UnityScripts.Containers;
using Vector2 = Common.Vector2;

namespace UnityScripts.Factories
{
    public class BulletGameObjectFactory : BulletFactory
    {
        private readonly PrefabsContainer _prefabsContainer;
        private readonly BulletFactory _wrappedFactory;

        public BulletGameObjectFactory(PrefabsContainer container, BulletFactory wrappedFactory)
        {
            _prefabsContainer = container;
            _wrappedFactory = wrappedFactory;
        }

        public override BodyTransform CreateTransform(Vector2 position, float rotation, Vector2 direction) =>
            _wrappedFactory.CreateTransform(position, rotation, direction);

        public override PhysicsRigidBody CreateRigidBody(float mass, bool useGravity) =>
            _wrappedFactory.CreateRigidBody(mass, useGravity);

        public override PhysicsCollider CreateCollider(Vector2 position)
        {
            var gameObject = Object.Instantiate(_prefabsContainer.BulletPrefab, new UnityEngine.Vector2(position.X, position.Y),
                Quaternion.identity);
            
            var spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
            var rect = spriteRenderer.sprite.bounds;
            var size = rect.size;
            return new BoxPhysicsCollider(position, size.x, size.y);
        }
    }
}
