using UnityEngine;

namespace Game
{
    [CreateAssetMenu(menuName = "Game/Equipment", fileName = "Equipment_New")]
    public class EquipmentSO : ScriptableObject
    {
        public string Id;

        [Space]
        public Sprite Icon;
        public int Price;
        public string Description;
    }
}