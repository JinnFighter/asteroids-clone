using UnityEngine;

namespace UnityScripts.Views
{
    public class PhysicsBodyView : MonoBehaviour, IPhysicsBodyView
    {
        public void UpdatePosition(float x, float y) =>
            transform.position = new Vector2(x, y);
    }
}
