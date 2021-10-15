using Logic;
using Logic.Conveyors;
using UnityEngine;
using UnityScripts.Conveyors;
using UnityScripts.Views;

namespace UnityScripts.Startups
{
    public class Startup : MonoBehaviour
    {
        private RuntimeCore _runtimeCore;

        [SerializeField] private GameObject _shipView;

        // Start is called before the first frame update
        void Start()
        {
            _runtimeCore = new RuntimeCore();
            _runtimeCore.Setup();
            var shipConveyor = _runtimeCore.GetService<ShipConveyor>();
            shipConveyor.AddNextConveyor(new ShipGameObjectConveyor(_shipView.GetComponent<PhysicsBodyView>()));
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
