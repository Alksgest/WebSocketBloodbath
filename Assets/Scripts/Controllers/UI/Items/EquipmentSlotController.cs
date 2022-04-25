using UnityEngine;

namespace Controllers.UI.Items
{
    public class EquipmentSlotController : MonoBehaviour
    {
        [SerializeField] public EquipmentItemController currentItem;

        public bool SlotFilled => currentItem != null;

        public void AddItem(EquipmentItemController controller)
        {
            if (SlotFilled) return;

            currentItem = controller;
            controller.transform.SetParent(transform);
            controller.currentSlot = this;
            AdjustContent();
        }

        public void Clear()
        {
            currentItem = null;
        }

        public void AdjustContent()
        {
            var rectTransform = GetComponent<RectTransform>();
            var rect = rectTransform.rect;
            // rectTransform.anchorMax
            var itemHeight = rect.height - 10;

            var r = currentItem.GetComponent<RectTransform>();
            r.sizeDelta = new Vector2(itemHeight, itemHeight);
            r.localScale = new Vector3(1f, 1f, 1f);
            r.anchoredPosition = new Vector2(0f, 0f);
            r.pivot = new Vector2(0.5f, 0.5f);
        }
    }
}