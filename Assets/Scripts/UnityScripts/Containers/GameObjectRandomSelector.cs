using System.Collections.Generic;
using DataContainers;
using Helpers;
using UnityEngine;

namespace UnityScripts.Containers
{
    public class GameObjectRandomSelector : RandomObjectSelector<GameObject>
    {
        public GameObjectRandomSelector(IEnumerable<GameObject> items, IRandomizer randomizer) : base(items, randomizer)
        {
        }
    }
}
