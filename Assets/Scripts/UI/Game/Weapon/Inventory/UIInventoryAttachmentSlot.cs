using System;
using Attachment_system;
using UI.Game.Inventory.Model;
using UnityEngine.EventSystems;

namespace UI.Game.Inventory
{
    public class UIInventoryAttachmentSlot: UISlot
    {
        public AttachmentType type;
        public AttachmentSystem attachmentSystem;

        public InventoryItemInfo ItemInfo { get; private set; }

        public override void OnDrop(PointerEventData eventData)
        {
            DraggableItem item = eventData.pointerDrag.GetComponent<DraggableItem>();
            item.OnEndDrag(null);

            if(ItemInfo is not  null) 
                return;
            
            if (item.UIInventorySlot is UIInventorySlot uiInventorySlot)
            {
                if (uiInventorySlot.InventorySlot.ItemInfo.type != InventoryItemType.Attachment)
                    return;
                if (CheckAttachmentsDoNotMatch(uiInventorySlot.InventorySlot.ItemInfo.secondaryType))
                    return;
                
                ItemInfo = uiInventorySlot.InventorySlot.ItemInfo;
                SetModule(ItemInfo);
                Inventory.RemoveFromSlot(uiInventorySlot.InventorySlot);
                return;
            }

            if (item.UIInventorySlot is UIInventoryAttachmentSlot uiAttachmentSlot)
            {
                ItemInfo = uiAttachmentSlot.ItemInfo;
                uiAttachmentSlot.Clear();
                SetModule(ItemInfo);
            }
        }

        public override void Drop()
        {
            if (Inventory.TryToAdd(ItemInfo, 1, out int restAmount))
            {
                draggableItem.OnPointerExit(null);
                Clear();
            }
        }

        public void SetModule(InventoryItemInfo info)
        {
            ItemInfo = info;
            draggableItem.SetNewInfo(info, 1);
            attachmentSystem.SetModule(info.secondaryType); 
        }

        public override void Clear()
        {
            if(ItemInfo is null)
                return;
            
            attachmentSystem.RemoveModule(ItemInfo.secondaryType);
            ItemInfo = null;
            draggableItem.Hide();
        }

        private bool CheckAttachmentsDoNotMatch(SecondaryInventoryItemType secondaryType)
        {
            return type switch
            {
                AttachmentType.BarrelModule => !CheckBarrelModuleType(secondaryType),
                AttachmentType.MagazineModule => !CheckMagazineModuleType(secondaryType),
                _ => throw new Exception("Type not founded")
            };
        }

        private bool CheckBarrelModuleType(SecondaryInventoryItemType type)
        {
            return type switch
            {
                SecondaryInventoryItemType.None => false,
                SecondaryInventoryItemType.Silencer => true,
                SecondaryInventoryItemType.Compensator => true,
                SecondaryInventoryItemType.ExtendedMagazine => false,
                _ => throw new Exception("Type not founded")
            };
        }
        
        private bool CheckMagazineModuleType(SecondaryInventoryItemType type)
        {
            return type switch
            {
                SecondaryInventoryItemType.None => false,
                SecondaryInventoryItemType.Silencer => false,
                SecondaryInventoryItemType.Compensator => false,
                SecondaryInventoryItemType.ExtendedMagazine => true,
                _ => throw new Exception("Type not founded")
            };
        }
    }
}