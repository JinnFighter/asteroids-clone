using Logic;
using Logic.Conveyors;
using Logic.Services;
using Physics;
using UnityEngine;
using UnityScripts.Containers;
using UnityScripts.Conveyors;
using UnityScripts.EventEmitters;
using UnityScripts.Services;

namespace UnityScripts.Startups
{
    public class Startup : MonoBehaviour
    {
        private RuntimeCore _runtimeCore;
        
        private PrefabsContainer _prefabsContainer;

        // Start is called before the first frame update
        void Start()
        {
            _prefabsContainer = GetComponent<PrefabsContainer>();
            _runtimeCore = new RuntimeCore();
            _runtimeCore.Setup();

            var collisionLayersContainer = _runtimeCore.GetService<CollisionLayersContainer>();
            var playerEntitiesContainer = new PlayerEntitiesDataContainer();
            var inputEventEmitter = new InputEventEmitter(playerEntitiesContainer, 
                _runtimeCore.GetService<InputCommandQueue>());
            var shipConveyor = _runtimeCore.GetService<ShipConveyor>().GetLast();
            shipConveyor.AddNextConveyor(new ShipGameObjectConveyor(_prefabsContainer, playerEntitiesContainer, 
                inputEventEmitter, collisionLayersContainer));

            var asteroidConveyor = _runtimeCore.GetService<AsteroidConveyor>().GetLast();
            asteroidConveyor.AddNextConveyor(new AsteroidGameObjectConveyor(_prefabsContainer));

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
