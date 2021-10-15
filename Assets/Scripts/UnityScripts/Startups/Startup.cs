using Logic;
using UnityEngine;

namespace UnityScripts.Startups
{
    public class Startup : MonoBehaviour
    {
        private RuntimeCore _runtimeCore;
        
        // Start is called before the first frame update
        void Start()
        {
            _runtimeCore = new RuntimeCore();
            _runtimeCore.Setup();
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
