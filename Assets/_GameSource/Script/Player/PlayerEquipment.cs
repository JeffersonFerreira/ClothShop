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

        private Dictionary<EquipmentCategory, CategoryToRender> _catRenderMap;
        private Dictionary<EquipmentCategory, DefaultRenderData> _defaultRenderDataMap;
        private readonly Dictionary<EquipmentCategory, EquipmentSO> _equipmentMap = new();

        private void Awake()
        {
            _catRenderMap = _categoryToRendersList.ToDictionary(map => map.Category);

            _defaultRenderDataMap = _categoryToRendersList.ToDictionary(
                map => map.Category,
                map => DefaultRenderData.From(map)
            );
        }

        public bool TryEquip(EquipmentSO equipment)
        {
            if (!_equipmentMap.TryAdd(equipment.Category, equipment))
                return false;

            if (!_catRenderMap.TryGetValue(equipment.Category, out var renderMap))
                return false;

            if (renderMap.Renderer != null && equipment.EquipmentSprite != null)
                renderMap.Renderer.sprite = equipment.EquipmentSprite;

            if (renderMap.RendererSecondary != null && equipment.EquipmentSpriteSecondary != null)
                renderMap.RendererSecondary.sprite = equipment.EquipmentSpriteSecondary;

            OnEquip?.Invoke(equipment);
            return true;
        }

        public bool TryDequip(EquipmentCategory category, out EquipmentSO equipment)
        {
            if (!_equipmentMap.TryGetValue(category, out equipment))
                return false;

            if (!_catRenderMap.TryGetValue(category, out CategoryToRender catRenderers))
                return false;

            DefaultRenderData defaultRenderData = _defaultRenderDataMap[category];

            if (catRenderers.Renderer != null && defaultRenderData.PrimarySprite != null)
                catRenderers.Renderer.sprite = defaultRenderData.PrimarySprite;

            if (catRenderers.RendererSecondary != null && defaultRenderData.SecondarySprite != null)
                catRenderers.RendererSecondary.sprite = defaultRenderData.SecondarySprite;

            _equipmentMap.Remove(category);
            OnDequip?.Invoke(equipment);

            return true;
        }

        [Serializable]
        private class CategoryToRender
        {
            public EquipmentCategory Category;
            public SpriteRenderer Renderer;
            public SpriteRenderer RendererSecondary;
        }

        private class DefaultRenderData
        {
            public Sprite PrimarySprite;
            public Sprite SecondarySprite;

            public static DefaultRenderData From(CategoryToRender catRenderMap)
            {
                var data = new DefaultRenderData();

                if (catRenderMap.Renderer != null)
                    data.PrimarySprite = catRenderMap.Renderer.sprite;

                if (catRenderMap.RendererSecondary != null)
                    data.SecondarySprite = catRenderMap.RendererSecondary.sprite;

                return data;
            }
        }
    }
}