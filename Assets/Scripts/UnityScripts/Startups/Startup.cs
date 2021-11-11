using Helpers;
using Logic;
using Logic.Conveyors;
using Logic.Factories;
using Logic.Services;
using Physics;
using UnityEngine;
using UnityScripts.Containers;
using UnityScripts.Conveyors;
using UnityScripts.EventEmitters;
using UnityScripts.Factories;
using UnityScripts.Services;

namespace UnityScripts.Startups
{
    public class Startup : MonoBehaviour
    {
        private RuntimeCore _runtimeCore;
        
        private PrefabsContainer _prefabsContainer;

        public GameObject ShipUiView;

        // Start is called before the first frame update
        void Start()
        {
            _prefabsContainer = GetComponent<PrefabsContainer>();
            _runtimeCore = new RuntimeCore();
            _runtimeCore.Setup();

            var randomizer = _runtimeCore.GetService<IRandomizer>();
            var collisionLayersContainer = _runtimeCore.GetService<CollisionLayersContainer>();
            var playerEntitiesContainer = new PlayerEntitiesDataContainer();
            var inputEventEmitter = new InputEventEmitter(playerEntitiesContainer, 
                _runtimeCore.GetService<InputCommandQueue>());
            var shipConveyor = _runtimeCore.GetService<ShipConveyor>().GetLast();
            shipConveyor
                .AddNextConveyor(new ShipGameObjectConveyor(_prefabsContainer, playerEntitiesContainer, 
                inputEventEmitter, collisionLayersContainer))
                .AddNextConveyor(new ShipUiConveyor(ShipUiView));

            var asteroidConveyor = _runtimeCore.GetService<AsteroidConveyor>().GetLast();
            asteroidConveyor.AddNextConveyor(new AsteroidGameObjectConveyor(_prefabsContainer, collisionLayersContainer, 
                randomizer));

            var bulletConveyor = _runtimeCore.GetService<BulletConveyor>().GetLast();
            bulletConveyor.AddNextConveyor(new BulletGameObjectConveyor(_prefabsContainer, collisionLayersContainer));

            var bulletFactory = _runtimeCore.GetService<BulletFactory>();
            _runtimeCore.AddService<BulletFactory>(new BulletGameObjectFactory(_prefabsContainer, bulletFactory));

            _runtimeCore.AddService<IDeltaTimeCounter>(new UnityDeltaTimeCounter());
            
            _runtimeCore.Init();
        }

        // Update is called once per frame
        void Update()
        {
            _runtimeCore.Run();
        }

        private void OnDestroy()
        {
            _runtimeCore.Destroy();
        }
    }
}
