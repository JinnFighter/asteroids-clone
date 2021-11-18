using System.Collections.Generic;
using DataContainers;

namespace Physics
{
    public class CollisionsContainer : DictionaryDataContainer<PhysicsCollider, List<CollisionInfo>>
    {
    }
}
