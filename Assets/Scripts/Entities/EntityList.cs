using UnityEngine;

namespace Entities
{
    [CreateAssetMenu(fileName = "EntityList", menuName = "ScriptableObjects/EntityList", order = 1)]
    public class EntityList : ScriptableObject
    {
        [SerializeField] private SpawnerEntry[] entities;

        public SpawnerEntry[] GetEntities()
        {
            return entities;
        }
    }
}
