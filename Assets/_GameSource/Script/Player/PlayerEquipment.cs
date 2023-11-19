using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using UnityEngine;

namespace Game
{
    public class PlayerEquipment : MonoBehaviour
    {
        [SerializeField] private CategoryToRender[] _categoryToRendersList;

        [CanBeNull]
        public EquipmentSO this[EquipmentCategory category] => _equipmentMap.GetValueOrDefault(category);

        public event Action<EquipmentSO> OnEquip, OnDequip;

        private Dictionary<EquipmentCategory, SpriteRenderer> _catRenderMap;
        private readonly Dictionary<EquipmentCategory, EquipmentSO> _equipmentMap = new();

        private void Awake()
        {
            _catRenderMap = _categoryToRendersList.ToDictionary(
                map => map.Category,
                map => map.Renderer
            );
        }

        public bool TryEquip(EquipmentSO equipment)
        {
            if (!_equipmentMap.TryAdd(equipment.Category, equipment))
                return false;

            OnEquip?.Invoke(equipment);
            return true;
        }

        public bool TryDequip(EquipmentCategory category, out EquipmentSO equipment)
        {
            bool success = _equipmentMap.TryGetValue(category, out equipment);

            if (success)
            {
                _equipmentMap.Remove(category);
                OnDequip?.Invoke(equipment);
            }

            return success;
        }

        [Serializable]
        private class CategoryToRender
        {
            public EquipmentCategory Category;
            public SpriteRenderer Renderer;
        }
    }
}