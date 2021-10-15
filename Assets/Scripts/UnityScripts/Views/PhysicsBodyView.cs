using UnityEngine;
using UnityScripts.Views;
using Vector2 = Physics.Vector2;

public class PhysicsBodyView : MonoBehaviour, IPhysicsBodyView
{
    public void UpdatePosition(Vector2 position) =>
        transform.position = new UnityEngine.Vector2(position.X, position.Y);
}
