namespace UnityScripts.Presentation.Views
{
    public interface ITransformBodyView
    {
        void UpdatePosition(float x, float y);
        void UpdateRotation(float rotationAngle);
        void Destroy();
    }
}
