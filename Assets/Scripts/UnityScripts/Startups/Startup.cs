using Ecs;
using UnityEngine;

namespace UnityScripts.Startups
{
    public class Startup : MonoBehaviour
    {
        private EcsWorld _world;
        private EcsSystems _systems;
        
        // Start is called before the first frame update
        void Start()
        {
            _world = new EcsWorld();
            _systems = new EcsSystems(_world);
        }

        // Update is called once per frame
        void Update()
        {
            _systems?.Run();
        }
    }
}
