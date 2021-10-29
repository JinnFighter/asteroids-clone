using UnityEngine;

namespace UnityScripts.Presentation.Views
{
    public class PhysicsBodyView : MonoBehaviour, IPhysicsBodyView
    {
        public void UpdatePosition(float x, float y) =>
            transform.position = new Vector2(x, y);
    }
}
