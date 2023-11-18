using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    [CreateAssetMenu(menuName = "Game/Database", fileName = "GameDatabase")]
    public class GameDatabase : ScriptableObject
    {
        [SerializeField] private EquipmentSO[] _equipment;

        public IReadOnlyList<EquipmentSO> Equipment => _equipment;

        public void SetItems(EquipmentSO[] equipment)
        {
            _equipment = equipment;
        }
    }
}