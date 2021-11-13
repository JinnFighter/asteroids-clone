using Logic.Events;
using Logic.Factories;
using Physics;
using UnityEngine;
using Vector2 = Common.Vector2;

namespace UnityScripts.Factories
{
    public class SpriteSizeColliderFactory : IColliderFactory, IEventHandler<GameObject>
    {
        private Vector3 _size;

        public PhysicsCollider CreateCollider(Vector2 position) => new BoxPhysicsCollider(position, _size.x, _size.y);

        public void Handle(GameObject context)
        {
            var spriteRenderer = context.GetComponent<SpriteRenderer>();
            var rect = spriteRenderer.sprite.bounds;
            _size = rect.size;
        }
    }
}
