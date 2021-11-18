namespace Logic.Config
{
    public class AsteroidConfig
    {
        public float DefaultMass { get; }

        public float MinRespawnTime { get; }
        public float MaxRespawnTime { get; }

        public AsteroidConfig(float mass)
        {
            DefaultMass = mass;
        }
    }
}
