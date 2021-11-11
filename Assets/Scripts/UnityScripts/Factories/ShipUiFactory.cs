using Ecs;
using Logic.Factories;
using Physics;
using UnityEngine;
using UnityScripts.Presentation.Models;
using UnityScripts.Presentation.Presenters;
using UnityScripts.Presentation.Views;
using Vector2 = Common.Vector2;

namespace UnityScripts.Factories
{
    public class ShipUiFactory : ShipFactory
    {
        private readonly ShipFactory _wrappedFactory;
        private readonly ITransformPresenterFactory _transformPresenterFactory;
        private readonly GameObject _gameObject;

        public ShipUiFactory(ShipFactory wrappedFactory, GameObject gameObject, ITransformPresenterFactory transformPresenterFactory)
        {
            _wrappedFactory = wrappedFactory;
            _gameObject = gameObject;
            _transformPresenterFactory = transformPresenterFactory;
        }

        public override void AddEntity(EcsEntity entity) => _wrappedFactory.AddEntity(entity);

        public override BodyTransform CreateTransform(Vector2 position, float rotation, Vector2 direction)
        {
            var transform = _wrappedFactory.CreateTransform(position, rotation, direction);
            
            var transformBodyPresenter = _transformPresenterFactory.CreatePresenter(transform,
                _gameObject.GetComponent<UiTransformBodyView>());
            
            return transform;
        }

        public override PhysicsRigidBody CreateRigidBody(float mass, bool useGravity)
        {
            var rigidBody = _wrappedFactory.CreateRigidBody(mass, useGravity);

            var physicsBodyModel = new PhysicsRigidBodyModel(rigidBody.Velocity);
            rigidBody.VelocityChangedEvent += physicsBodyModel.UpdateVelocity;
            var physicsBodyPresenter = new PhysicsRigidBodyPresenter(physicsBodyModel,
                _gameObject.GetComponent<UiPhysicsRigidBodyView>());

            return rigidBody;
        }

        public override PhysicsCollider CreateCollider(Vector2 position) => _wrappedFactory.CreateCollider(position);
    }
}
