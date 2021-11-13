using Logic.Events;
using Physics;
using UnityEngine;
using UnityScripts.Factories;
using UnityScripts.Presentation.Views;

namespace UnityScripts.EventHandlers
{
    public class TransformPresenterEventHandler : IEventHandler<BodyTransform>, IEventHandler<GameObject>
    {
        private readonly ITransformPresenterFactory _transformPresenterFactory;
        private ITransformBodyView _transformBodyView;
        
        public void OnCreateEvent(BodyTransform context) => _transformPresenterFactory.CreatePresenter(context, _transformBodyView);

        public void OnDestroyEvent(BodyTransform context)
        {
        }

        public void OnCreateEvent(GameObject context) => _transformBodyView = context.GetComponent<TransformBodyView>();

        public void OnDestroyEvent(GameObject context)
        {
        }
    }
}
