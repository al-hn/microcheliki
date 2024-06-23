using System.Collections;
using System.Collections.Generic;
// using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventoryItem : MonoBehaviour , IBeginDragHandler , IDragHandler , IEndDragHandler
{
   [HideInInspector] public Item itm;
   [Header("UI")]
   public UnityEngine.UI.Image image;

   public void InistialiseItem(Item newItem){
      itm = newItem;
      image.sprite = newItem.image;
   }
   [HideInInspector] public Transform parentAfterDrag;
   public void OnBeginDrag(PointerEventData eventData){
      image.raycastTarget = false;
      parentAfterDrag = transform.parent;
      transform.SetParent(transform.root);
   }
   public void OnDrag(PointerEventData eventData){
      transform.position = Input.mousePosition;
   }
   public void OnEndDrag(PointerEventData eventData){
      image.raycastTarget = true;
      transform.SetParent(parentAfterDrag);

   }
}