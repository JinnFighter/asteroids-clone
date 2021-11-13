using Logic.Events;
using Physics;
using UnityScripts.Factories;
using UnityScripts.Presentation.Views;

namespace UnityScripts.EventHandlers
{
    public class ShipUiTransformEventHandler : IEventHandler<BodyTransform>
    {
        private readonly ITransformPresenterFactory _transformPresenterFactory;
        private readonly ITransformBodyView _view;

        public ShipUiTransformEventHandler(ITransformPresenterFactory factory, ITransformBodyView view)
        {
            _transformPresenterFactory = factory;
            _view = view;
        }
        
        public void Handle(BodyTransform context) => _transformPresenterFactory.CreatePresenter(context, _view);
    }
}
