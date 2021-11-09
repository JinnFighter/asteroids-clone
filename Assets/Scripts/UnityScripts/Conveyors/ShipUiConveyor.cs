using Ecs;
using Logic.Components.Physics;
using Logic.Conveyors;
using UnityScripts.Presentation.Models;
using UnityScripts.Presentation.Presenters;
using UnityScripts.Presentation.Views;

namespace UnityScripts.Conveyors
{
    public class ShipUiConveyor : EntityConveyor
    {
        private readonly UiTransformBodyView _uiTransformBodyView;

        public ShipUiConveyor(UiTransformBodyView uiTransformBodyView)
        {
            _uiTransformBodyView = uiTransformBodyView;
        }
        
        protected override void UpdateItemInternal(EcsEntity item)
        {
            if (item.HasComponent<PhysicsBody>())
            {
                var physicsBody = item.GetComponent<PhysicsBody>();
                var transform = physicsBody.Transform;
                
                var physicsBodyModel = new TransformBodyModel(transform.Position.X, transform.Position.Y);
                transform.PositionChangedEvent += physicsBodyModel.UpdatePosition;
                transform.RotationChangedEvent += physicsBodyModel.UpdateRotation;
                transform.DestroyEvent += physicsBodyModel.Destroy;
                var presenter = new TransformBodyPresenter(physicsBodyModel, _uiTransformBodyView);
            }
        }
    }
}
