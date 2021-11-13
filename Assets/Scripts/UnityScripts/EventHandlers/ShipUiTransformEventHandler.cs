using Logic.Events;
using Physics;
using UnityEngine;
using UnityScripts.Factories;
using UnityScripts.Presentation.Views;

namespace UnityScripts.EventHandlers
{
    public class ShipUiTransformEventHandler : IEventHandler<BodyTransform>
    {
        private readonly ITransformPresenterFactory _transformPresenterFactory;
        private readonly GameObject _gameObject;

        public ShipUiTransformEventHandler(ITransformPresenterFactory factory, GameObject gameObject)
        {
            _transformPresenterFactory = factory;
            _gameObject = gameObject;
        }
        
        public void OnCreateEvent(BodyTransform context)
        {
            var presenter =
                _transformPresenterFactory.CreatePresenter(context, _gameObject.GetComponent<UiTransformBodyView>());
        }
    }
}
