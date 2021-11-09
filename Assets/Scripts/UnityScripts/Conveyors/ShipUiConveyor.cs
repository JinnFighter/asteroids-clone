using Ecs;
using Logic.Components.Physics;
using Logic.Conveyors;
using UnityEngine;
using UnityScripts.Presentation.Models;
using UnityScripts.Presentation.Presenters;
using UnityScripts.Presentation.Views;

namespace UnityScripts.Conveyors
{
    public class ShipUiConveyor : EntityConveyor
    {
        private readonly GameObject _shipUiGameObject;

        public ShipUiConveyor(GameObject shipUiGameObject)
        {
            _shipUiGameObject = shipUiGameObject;
        }
        
        protected override void UpdateItemInternal(EcsEntity item)
        {
            if (item.HasComponent<PhysicsBody>())
            {
                var physicsBody = item.GetComponent<PhysicsBody>();
                var transform = physicsBody.Transform;
                
                var transformBodyModel = new TransformBodyModel(transform.Position.X, transform.Position.Y);
                transform.PositionChangedEvent += transformBodyModel.UpdatePosition;
                transform.RotationChangedEvent += transformBodyModel.UpdateRotation;
                transform.DestroyEvent += transformBodyModel.Destroy;
                var presenter = new TransformBodyPresenter(transformBodyModel, 
                    _shipUiGameObject.GetComponent<UiTransformBodyView>());
            }
        }
    }
}
