using System.Collections.Generic;

namespace Physics
{
    public class PhysicsWorld
    {
        private readonly List<CustomRigidbody2D> _rigidbodies;
        private readonly Vector2 _gravity;

        public PhysicsWorld()
        {
            _rigidbodies = new List<CustomRigidbody2D>();
            _gravity = new Vector2(0, -9.81f);
        }

        public void AddRigidbody(CustomRigidbody2D customRigidbody2D) => _rigidbodies.Add(customRigidbody2D);

        public void RemoveRigidbody(CustomRigidbody2D customRigidbody2D)
        {
            if (_rigidbodies.Contains(customRigidbody2D))
                _rigidbodies.Remove(customRigidbody2D);
        }

        public void Step(float dt)
        {
            foreach (var rigidbody in _rigidbodies)
            {
                var nextForce = (rigidbody.UseGravity ? _gravity : Vector2.Zero) * rigidbody.Mass;
                rigidbody.Force += nextForce;

                rigidbody.Velocity += rigidbody.Force / rigidbody.Mass * dt;
                rigidbody.Position += rigidbody.Velocity * dt;
                
                rigidbody.Force = Vector2.Zero;
            }
        }
    }
}
