using UnityEngine;
using UnityEngine.UI;
using Vector2 = Common.Vector2;

namespace UnityScripts.Presentation.Views
{
    public class UiPhysicsRigidBodyView : MonoBehaviour, IPhysicsRigidBodyView
    {
        [SerializeField] private Text _velocityText;
        // Start is called before the first frame update
        void Start()
        {
            UpdateVelocity(Vector2.Zero);
        }

        public void UpdateVelocity(Vector2 velocity) => _velocityText.text = $"Speed : { velocity.Length }";

        public void Destroy() => _velocityText.gameObject.SetActive(false);
    }
}
