using System;
using System.Linq;
using JetBrains.Annotations;
using UnityEngine;

namespace Game
{
    public class PlayerInventory : MonoBehaviour
    {
        private const int TOTAL_SLOTS = 6 * 3;

        [field: SerializeField]
        public int Money { get; private set; } = 250;

        [CanBeNull]
        public EquipmentSO this[int index] => index < 0 || index > _slots.Length ? null : _slots[index];

        public event Action<int, EquipmentSO> OnInserted;
        public event Action<int, EquipmentSO> OnRemoved;
        public event Action<int> OnMoneyChanged;

        private readonly EquipmentSO[] _slots = new EquipmentSO[TOTAL_SLOTS];

        public bool HasSpace()
        {
            return _slots.Any(s => s == null);
        }

        public bool TryAppend(EquipmentSO equip)
        {
            for (var i = 0; i < _slots.Length; i++)
            {
                if (_slots[i] == null)
                    return TryInsert(i, equip);
            }

            return false;
        }

        public bool TryInsert(int index, EquipmentSO equip)
        {
            if (equip == null || index >= _slots.Length || _slots[index] != null)
                return false;

            _slots[index] = equip;

            OnInserted?.Invoke(index, equip);
            return true;
        }

        public bool TryRemove(int index, out EquipmentSO equip)
        {
            if (index >= _slots.Length || _slots[index] == null)
            {
                equip = null;
                return false;
            }

            equip = _slots[index];
            _slots[index] = null;

            OnRemoved?.Invoke(index, equip);
            return true;
        }

        public bool TrySpend(int moneyAmount)
        {
            if (moneyAmount < 0 || moneyAmount > Money)
                return false;

            Money -= moneyAmount;
            OnMoneyChanged?.Invoke(Money);
            return true;
        }
    }
}