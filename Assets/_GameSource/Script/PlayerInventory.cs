using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class PlayerInventory : MonoBehaviour
    {
        public IReadOnlyCollection<EquipmentSO> Equipments => _equipments;

        // Hashset to avoid duplicates
        private readonly HashSet<EquipmentSO> _equipments = new();

        public void AddItem(EquipmentSO equip)
        {
            _equipments.Add(equip);
        }
    }
}