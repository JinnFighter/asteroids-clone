using Logic.Events;
using Logic.Factories;
using Physics;
using UnityEngine;
using Vector2 = Common.Vector2;

namespace UnityScripts.Factories
{
    public class BulletGameObjectFactory : BulletFactory, IEventHandler<GameObject>
    {
        private readonly BulletFactory _wrappedFactory;
        private Vector3 _size;

        public BulletGameObjectFactory(BulletFactory wrappedFactory)
        {
            _wrappedFactory = wrappedFactory;
        }

        public override BodyTransform CreateTransform(Vector2 position, float rotation, Vector2 direction) 
            => _wrappedFactory.CreateTransform(position, rotation, direction);

        public override PhysicsCollider CreateCollider(Vector2 position) 
            => new BoxPhysicsCollider(position, _size.x, _size.y);

        public void Handle(GameObject context)
        {
            var spriteRenderer = context.GetComponent<SpriteRenderer>();
            var rect = spriteRenderer.sprite.bounds;
            _size = rect.size;
        }
    }
}
