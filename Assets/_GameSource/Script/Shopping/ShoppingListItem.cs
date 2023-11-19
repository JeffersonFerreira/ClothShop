using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game
{
    public class ShoppingListItem : MonoBehaviour
    {
        [SerializeField] private Button _button;
        [SerializeField] private Image _icon;
        [SerializeField] private TMP_Text _descriptionText;
        [SerializeField] private TMP_Text _priceText;

        private ShoppingMenu _menu;
        private EquipmentSO _equip;

        private void Awake()
        {
            _button.onClick.AddListener(OnClick);
        }

        private void OnClick()
        {
            _menu.Purchase(_equip);
        }

        public void Draw(ShoppingMenu menu, EquipmentSO equip)
        {
            _equip = equip;
            _menu = menu;

            _icon.overrideSprite = equip.EquipmentSprite;
            _descriptionText.text = equip.Description;
            _priceText.text = equip.Price.ToString();
        }
    }
}