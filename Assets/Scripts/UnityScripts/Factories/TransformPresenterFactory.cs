using Physics;
using UnityScripts.Presentation.Models;
using UnityScripts.Presentation.Presenters;
using UnityScripts.Presentation.Views;

namespace UnityScripts.Factories
{
    public class TransformPresenterFactory : ITransformPresenterFactory
    {
        public TransformBodyPresenter CreatePresenter(BodyTransform transform, ITransformBodyView view)
        {
            var physicsBodyModel = new TransformBodyModel(transform.Position.X, transform.Position.Y, transform.Rotation);
            transform.PositionChangedEvent += physicsBodyModel.UpdatePosition;
            transform.RotationChangedEvent += physicsBodyModel.UpdateRotation;
            transform.DestroyEvent += physicsBodyModel.Destroy;
            return new TransformBodyPresenter(physicsBodyModel, view);
        }
    }
}
