using Physics;
using UnityScripts.Presentation.Models;
using UnityScripts.Presentation.Presenters;
using UnityScripts.Presentation.Views;

namespace UnityScripts.Factories
{
    public class UiTransformPresenterFactory : ITransformPresenterFactory
    {
        public TransformBodyPresenter CreatePresenter(TransformBody transform, ITransformBodyView view)
        {
            var transformBodyModel = new TransformBodyModel(transform.Position.X, transform.Position.Y, transform.Rotation);
            transform.PositionChangedEvent += transformBodyModel.UpdatePosition;
            transform.RotationChangedEvent += transformBodyModel.UpdateRotation;
            transform.DestroyEvent += transformBodyModel.Destroy;
            return new TransformBodyPresenter(transformBodyModel, view);
        }
    }
}
