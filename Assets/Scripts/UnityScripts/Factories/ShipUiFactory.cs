using Ecs;
using Logic.Factories;
using Physics;
using Vector2 = Common.Vector2;

namespace UnityScripts.Factories
{
    public class ShipUiFactory : ShipFactory
    {
        private readonly ShipFactory _wrappedFactory;

        public ShipUiFactory(ShipFactory wrappedFactory)
        {
            _wrappedFactory = wrappedFactory;
        }

        public override void AddEntity(EcsEntity entity) => _wrappedFactory.AddEntity(entity);

        public override BodyTransform CreateTransform(Vector2 position, float rotation, Vector2 direction) 
            => _wrappedFactory.CreateTransform(position, rotation, direction);

        public override PhysicsCollider CreateCollider(Vector2 position) => _wrappedFactory.CreateCollider(position);
    }
}
