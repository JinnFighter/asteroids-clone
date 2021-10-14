using System.Collections.Generic;

namespace Physics
{
    public class PhysicsWorld
    {
        private readonly List<Rigidbody2D> _rigidbodies;
        private readonly Vector2 _gravity;

        public PhysicsWorld()
        {
            _rigidbodies = new List<Rigidbody2D>();
            _gravity = new Vector2(0, -9.81f);
        }

        public void AddRigidbody(Rigidbody2D rigidbody2D) => _rigidbodies.Add(rigidbody2D);

        public void RemoveRigidbody(Rigidbody2D rigidbody2D)
        {
            if (_rigidbodies.Contains(rigidbody2D))
                _rigidbodies.Remove(rigidbody2D);
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
