using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

namespace Game
{
    public class PlayerEquipment : MonoBehaviour
    {
        [SerializeField] private CategoryToRender[] _categoryToRendersList;

        public event Action<EquipmentSO> OnEquip;
        public event Action<EquipmentSO> OnDequip;

        // Outsiders should be allowed to read which equipment I'm wearing, but can't modify directly
        [CanBeNull]
        public EquipmentSO this[EquipmentCategory category] => _activeEquipmentMap.GetValueOrDefault(category);

        private readonly Dictionary<EquipmentCategory, EquipmentSO> _activeEquipmentMap = new();
        private readonly Dictionary<EquipmentCategory, CategoryToRender> _categoryRenderMap = new();
        private readonly Dictionary<EquipmentCategory, DefaultRenderData> _defaultRenderMap = new();

        private void Awake()
        {
            foreach (var renderMap in _categoryToRendersList)
            {
                _categoryRenderMap.Add(renderMap.Category, renderMap);
                _defaultRenderMap.Add(renderMap.Category, DefaultRenderData.From(renderMap));
            }
        }

        public bool TryEquip(EquipmentSO equipment)
        {
            // Item of equal type is already equipped?
            if (!_activeEquipmentMap.TryAdd(equipment.Category, equipment))
                return false;

            // Apply equipment sprites to their renderers
            CategoryToRender renderMap = _categoryRenderMap[equipment.Category];

            if (renderMap.Renderer != null && equipment.EquipmentSprite != null)
                renderMap.Renderer.sprite = equipment.EquipmentSprite;

            if (renderMap.RendererSecondary != null && equipment.EquipmentSpriteSecondary != null)
                renderMap.RendererSecondary.sprite = equipment.EquipmentSpriteSecondary;

            OnEquip?.Invoke(equipment);
            return true;
        }

        public bool TryDequip(EquipmentCategory category, out EquipmentSO equipment)
        {
            // I'm wearing this equipment?
            if (!_activeEquipmentMap.Remove(category, out equipment))
                return false;

            // Restore the original sprites for this equipment type
            CategoryToRender renderMap = _categoryRenderMap[category];
            DefaultRenderData defaultRenderData = _defaultRenderMap[category];

            if (renderMap.Renderer != null && defaultRenderData.PrimarySprite != null)
                renderMap.Renderer.sprite = defaultRenderData.PrimarySprite;

            if (renderMap.RendererSecondary != null && defaultRenderData.SecondarySprite != null)
                renderMap.RendererSecondary.sprite = defaultRenderData.SecondarySprite;

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