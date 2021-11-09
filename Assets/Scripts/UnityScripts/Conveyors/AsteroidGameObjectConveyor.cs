using DataContainers;
using Ecs;
using Helpers;
using Logic.Components.Gameplay;
using Logic.Components.Physics;
using Logic.Conveyors;
using Physics;
using UnityEngine;
using UnityScripts.Containers;
using UnityScripts.Presentation.Models;
using UnityScripts.Presentation.Presenters;
using UnityScripts.Presentation.Views;

namespace UnityScripts.Conveyors
{
    public class AsteroidGameObjectConveyor : AsteroidCreatorConveyor
    {
        private readonly IObjectSelector<GameObject>[] _objectSelectors;
        private readonly CollisionLayersContainer _collisionLayersContainer;

        public AsteroidGameObjectConveyor(PrefabsContainer prefabsContainer, CollisionLayersContainer collisionLayersContainer, IRandomizer randomizer)
        {
            _objectSelectors = new IObjectSelector<GameObject>[]
            {
                new GameObjectRandomSelector(prefabsContainer.SmallAsteroidsPrefabs, randomizer),
                new GameObjectRandomSelector(prefabsContainer.MediumAsteroidsPrefabs, randomizer),
                new GameObjectSingleSelector(prefabsContainer.BigAsteroidPrefab)
            };

            _collisionLayersContainer = collisionLayersContainer;
        }
        
        protected override void UpdateItemInternal(EcsEntity item, CreateAsteroidEvent param)
        {
            if (item.HasComponent<PhysicsBody>())
            {
                ref var physicsBody = ref item.GetComponent<PhysicsBody>();
                var transform = physicsBody.Transform;
                
                var asteroidGameObject = Object.Instantiate(_objectSelectors[param.Stage - 1].GetObject());
                
                var spriteRenderer = asteroidGameObject.GetComponent<SpriteRenderer>();
                var rect = spriteRenderer.sprite.rect;
                var collider = new BoxPhysicsCollider(transform.Position, rect.width, rect.height);
                physicsBody.Collider = collider;
                transform.PositionChangedEvent += collider.UpdatePosition;
                collider.CollisionLayers.Add(_collisionLayersContainer.GetData("asteroids"));
                
                var physicsBodyModel = new PhysicsBodyModel(transform.Position.X, transform.Position.Y);
                transform.PositionChangedEvent += physicsBodyModel.UpdatePosition;
                transform.RotationChangedEvent += physicsBodyModel.UpdateRotation;
                transform.DestroyEvent += physicsBodyModel.Destroy;
                var presenter = new PhysicsBodyPresenter(physicsBodyModel, asteroidGameObject.GetComponent<PhysicsBodyView>());
            }
        }
    }
}
