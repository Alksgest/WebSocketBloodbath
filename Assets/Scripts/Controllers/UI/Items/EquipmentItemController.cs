using System.Collections.Generic;
using Models.Items;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Controllers.UI.Items
{
    public class EquipmentItemController : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        [SerializeField] public ItemType itemType = ItemType.None;
        [SerializeField] public EquipmentSlotController currentSlot;
        
        private Canvas _canvas;
        private GraphicRaycaster _graphicRaycaster;

        public IItem Item { get; set; }

        public void Instantiate(IItem item)
        {
            Item = item;
            itemType = item.ItemType;
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            var currentTransform = transform;
            currentTransform.localPosition +=
                new Vector3(eventData.delta.x, eventData.delta.y, 0) / currentTransform.lossyScale.x;
            if (!_canvas)
            {
                _canvas = GetComponentInParent<Canvas>();
                _graphicRaycaster = _canvas.GetComponent<GraphicRaycaster>();
            }

            transform.SetParent(_canvas.transform, true);
            transform.SetAsLastSibling();
        }

        public void OnDrag(PointerEventData eventData)
        {
            var currentTransform = transform;
            currentTransform.localPosition +=
                new Vector3(eventData.delta.x, eventData.delta.y, 0) / currentTransform.lossyScale.x;
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            var results = new List<RaycastResult>();
            _graphicRaycaster.Raycast(eventData, results);
            foreach (var hit in results)
            {
                var slot = hit.gameObject.GetComponent<EquipmentSlotController>();

                if (slot == null) continue;
                
                if (slot.SlotFilled) continue;

                currentSlot.Clear();
                
                // slot.AddItem(this);
                currentSlot = slot;
                currentSlot.currentItem = this;

                break;
            }

            ResetPosition();
        }

        private void ResetPosition()
        {
            var thisTransform = transform;
            
            thisTransform.SetParent(currentSlot.transform);
            thisTransform.localPosition = Vector3.zero;
        }
    }
}