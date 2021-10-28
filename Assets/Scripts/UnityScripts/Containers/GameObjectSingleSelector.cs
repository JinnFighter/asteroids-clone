using DataContainers;
using UnityEngine;

namespace UnityScripts.Containers
{
    public class GameObjectSingleSelector : SingleObjectSelector<GameObject>
    {
        public GameObjectSingleSelector(GameObject obj) : base(obj)
        {
        }
    }
}
