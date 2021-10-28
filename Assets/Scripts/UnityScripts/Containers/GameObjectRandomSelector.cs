using System.Collections.Generic;
using DataContainers;
using UnityEngine;

namespace UnityScripts.Containers
{
    public class GameObjectRandomSelector : RandomObjectSelector<GameObject>
    {
        public GameObjectRandomSelector(IEnumerable<GameObject> items) : base(items)
        {
        }
    }
}
