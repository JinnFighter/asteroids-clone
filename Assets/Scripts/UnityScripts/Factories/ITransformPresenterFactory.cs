using Physics;
using UnityScripts.Presentation.Presenters;
using UnityScripts.Presentation.Views;

namespace UnityScripts.Factories
{
    public interface ITransformPresenterFactory
    {
        TransformBodyPresenter CreatePresenter(BodyTransform transform, ITransformBodyView view);
    }
}
