using Ecs;
using Ecs.Interfaces;
using Logic.Components.Gameplay;
using Logic.Events;
using UnityScripts.EventHandlers;
using UnityScripts.Factories;
using UnityScripts.Presentation.Views;

namespace UnityScripts.Startups.InitSystems
{
    public class InitShipTransformUiHandlersSystem : IEcsInitSystem
    {
        private readonly TransformHandlerKeeper _transformHandlerKeeper;
        private readonly ITransformPresenterFactory _transformPresenterFactory;
        private readonly ITransformBodyView _transformBodyView;

        public InitShipTransformUiHandlersSystem(TransformHandlerKeeper transformHandlerKeeper,
            ITransformPresenterFactory transformPresenterFactory,
            ITransformBodyView view)
        {
            _transformHandlerKeeper = transformHandlerKeeper;
            _transformPresenterFactory = transformPresenterFactory;
            _transformBodyView = view;
        }
        
        public void Init(EcsWorld world) => _transformHandlerKeeper.AddHandler<Ship>
        (new ShipUiTransformEventHandler(_transformPresenterFactory, _transformBodyView));
    }
}
