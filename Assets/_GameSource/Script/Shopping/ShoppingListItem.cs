using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game
{
    public class ShoppingListItem : MonoBehaviour
    {
        [SerializeField] private Image _icon;
        [SerializeField] private TMP_Text _descriptionText;
        [SerializeField] private TMP_Text _priceText;

        private void Awake()
        {
            GetComponent<Button>().onClick.AddListener(OnClick);
        }

        private void OnClick()
        {
            // TODO: Add buy action
        }

        public void Draw(EquipmentSO equip)
        {
            _icon.overrideSprite = equip.EquipmentSprite;
            _descriptionText.text = equip.Description;
            _priceText.text = equip.Price.ToString();
        }
    }
}