using Physics;

namespace Logic.Factories
{
    public interface IPhysicsObjectFactory
    {
        CustomRigidbody2D CreateObject();
    }
}
