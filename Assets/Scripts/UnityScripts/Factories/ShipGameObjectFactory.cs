using Ecs;
using Logic.Factories;
using Logic.Input;
using Physics;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityScripts.Containers;
using UnityScripts.EventEmitters;
using UnityScripts.Presentation.Views;
using Vector2 = Common.Vector2;

namespace UnityScripts.Factories
{
    public class ShipGameObjectFactory : ShipFactory
    {
        private readonly ShipFactory _wrappedFactory;
        private GameObject _gameObject;

        public ShipGameObjectFactory(ShipFactory wrappedFactory)
        {
            _wrappedFactory = wrappedFactory;
        }

        public override BodyTransform CreateTransform(Vector2 position, float rotation, Vector2 direction) 
            => _wrappedFactory.CreateTransform(position, rotation, direction);

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
