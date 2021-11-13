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

        public TransformPresenterEventHandler(ITransformPresenterFactory factory)
        {
            _transformPresenterFactory = factory;
        }
        
        public void Handle(BodyTransform context) => _transformPresenterFactory.CreatePresenter(context, _transformBodyView);

        public void Handle(GameObject context) => _transformBodyView = context.GetComponent<TransformBodyView>();
    }
}
