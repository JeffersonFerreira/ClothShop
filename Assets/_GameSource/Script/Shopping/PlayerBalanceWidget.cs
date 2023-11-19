using TMPro;
using UnityEngine;

namespace Game
{
    public class PlayerBalanceWidget : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _balanceText;

        private PlayerInventory _playerInventory;

        private void Awake()
        {
            _playerInventory = FindObjectOfType<PlayerInventory>();

            _playerInventory.OnMoneyChanged += Redraw;
        }

        private void Start()
        {
            Redraw(_playerInventory.Money);
        }

        private void Redraw(int balance)
        {
            _balanceText.text = balance.ToString();
        }
    }
}