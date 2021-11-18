using System.Linq;
using UnityEngine;

namespace UnityScripts.Presentation.Views
{
    public class LaserView : MonoBehaviour, ILaserView
    {
        [SerializeField] private GameObject[] _ammoObjects;

        // Start is called before the first frame update
        void Start()
        {
            foreach (var ammoObject in _ammoObjects)
                ammoObject.SetActive(false);
        }

        public void UpdateAmmoCount(int ammoCount)
        {
            var ammoToReplenish = _ammoObjects.Skip(_ammoObjects.Length - ammoCount).ToList();
            foreach (var ammo in ammoToReplenish)
                ammo.SetActive(true);

            foreach (var ammo in _ammoObjects.Except(ammoToReplenish))
                ammo.SetActive(false);
        }
    }
}
