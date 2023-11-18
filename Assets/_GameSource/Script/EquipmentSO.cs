using UnityEngine;
using UnityEngine.Serialization;

namespace Game
{
    [CreateAssetMenu(menuName = "Game/Equipment", fileName = "Equipment_New")]
    public class EquipmentSO : ScriptableObject
    {
        public string Id;

        [Space]
        [FormerlySerializedAs("Icon")]
        public Sprite EquipmentSprite;
        public int Price;
        public string Description;
    }
}