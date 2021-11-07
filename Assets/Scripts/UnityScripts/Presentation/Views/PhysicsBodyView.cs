using UnityEngine;

namespace UnityScripts.Presentation.Views
{
    public class PhysicsBodyView : MonoBehaviour, IPhysicsBodyView
    {
        public void UpdatePosition(float x, float y) =>
            transform.position = new Vector2(x, y);

        public void UpdateRotation(float rotationAngle) =>
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, rotationAngle));

        public void Destroy() => Object.Destroy(gameObject);
    }
}
