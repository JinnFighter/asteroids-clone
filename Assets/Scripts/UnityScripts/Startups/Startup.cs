using Logic;
using Logic.Conveyors;
using Logic.EventAttachers;
using Logic.Services;
using UnityEngine;
using UnityEngine.InputSystem;
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

        public PlayerInput PlayerInput;

        // Start is called before the first frame update
        void Start()
        {
            _prefabsContainer = GetComponent<PrefabsContainer>();
            _runtimeCore = new RuntimeCore();
            _runtimeCore.Setup();
            
            var shipConveyor = _runtimeCore.GetService<ShipConveyor>();
            shipConveyor.AddNextConveyor(new ShipGameObjectConveyor(_prefabsContainer));
            var inputEventEmitter = new InputEventEmitter(_runtimeCore.GetService<IEventAttacher>(), PlayerInput);
            _runtimeCore.AddService<IDeltaTimeCounter, UnityDeltaTimeCounter>(new UnityDeltaTimeCounter());
            
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
