using Logic.Events;
using Physics;
using UnityEngine;
using UnityScripts.Containers;
using UnityScripts.Factories;
using UnityScripts.Presentation.Views;

namespace UnityScripts.EventHandlers
{
    public class ShipEventHandler : IEventHandler<BodyTransform>
    {
        private readonly PrefabsContainer _prefabsContainer;
        private readonly ITransformPresenterFactory _transformPresenterFactory;
        private GameObject _gameObject;

        public ShipEventHandler(ITransformPresenterFactory transformPresenterFactory, PrefabsContainer prefabsContainer)
        {
            _transformPresenterFactory = transformPresenterFactory;
            _prefabsContainer = prefabsContainer;
        }
        
        public void OnCreateEvent(BodyTransform context)
        {
            var position = context.Position;
            _gameObject = Object.Instantiate(_prefabsContainer.ShipPrefab, new Vector2(position.X, position.Y),
                Quaternion.identity);
            
            var presenter = _transformPresenterFactory.CreatePresenter(context, _gameObject.GetComponent<TransformBodyView>());
        }

        public void OnDestroyEvent(BodyTransform context)
        {
        }
    }
}
