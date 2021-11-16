using UnityScripts.Presentation.Models;
using UnityScripts.Presentation.Views;

namespace UnityScripts.Presentation.Presenters
{
    public class TransformBodyPresenter
    {
        private readonly TransformBodyModel _transformBodyModel;
        private readonly ITransformBodyView _transformBodyView;

        public TransformBodyPresenter(TransformBodyModel transformBodyModel, ITransformBodyView transformBodyView)
        {
            _transformBodyModel = transformBodyModel;
            _transformBodyModel.PositionChangedEvent += UpdatePosition;
            _transformBodyModel.RotationChangedEvent += UpdateRotation;
            _transformBodyModel.DestroyEvent += Destroy;

            _transformBodyView = transformBodyView;
            UpdatePosition(transformBodyModel.X, transformBodyModel.Y);
            UpdateRotation(transformBodyModel.Rotation);
        }

        private void UpdatePosition(float x, float y) =>
            _transformBodyView.UpdatePosition(x, y);

        private void UpdateRotation(float rotationAngle) => _transformBodyView.UpdateRotation(rotationAngle);

        private void Destroy()
        {
            _transformBodyModel.PositionChangedEvent -= UpdatePosition;
            _transformBodyModel.RotationChangedEvent -= UpdateRotation;
            _transformBodyModel.DestroyEvent -= Destroy;
            
            _transformBodyView.Destroy();
        }
    }
}
