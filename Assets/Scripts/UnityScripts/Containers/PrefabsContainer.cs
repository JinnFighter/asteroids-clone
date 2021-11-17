using UnityEngine;

namespace UnityScripts.Containers
{
    [CreateAssetMenu]
    public class PrefabsContainer : ScriptableObject
    {
        public GameObject ShipPrefab;

        public GameObject BigAsteroidPrefab;
        public GameObject[] MediumAsteroidsPrefabs;
        public GameObject[] SmallAsteroidsPrefabs;

        public GameObject BulletPrefab;
        public GameObject LaserPrefab;
        public GameObject SaucerPrefab;
    }
}
