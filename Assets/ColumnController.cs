using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using UnityEngine;

public class ColumnController : MonoBehaviour
{
    public List<SlotController> slots;
    public List<int> columnNumbers;
    public bool stopColumn;


    private void OnEnable()
    {
        EventManager.RefreshSlot += RefreshSlot;
    }

    private void OnDisable()
    {
        EventManager.RefreshSlot -= RefreshSlot;
    }

    private void RefreshSlot(SlotController slot, ColumnController column)
    {
        if (column==this)
        {
            slots.Remove(slot);
            slots.Insert(0,slot);
        }
    }

    private void Start()
    {
        SetSlot();
    }

    private void Update()
    {
        if (stopColumn)
        {
            if (columnNumbers.Count==0)
            {
                StopColumn();
                stopColumn = false;
            }
        }
        
    }

    public void SetSlot()
    {
        var height = EventManager.GetGameCanvasHeight();

        for (int i = 0; i < slots.Count; i++)
        {
            slots[i].GetComponent<RectTransform>().sizeDelta =
                new Vector2(slots[i].GetComponent<RectTransform>().sizeDelta.x, height / 3);
            slots[i].GetComponent<RectTransform>().anchoredPosition = new Vector2(0,height/3) -
                new Vector2(slots[i].GetComponent<RectTransform>().anchoredPosition.x, i * (height/3));
        }
    }


    public void StopColumn()
    {
        foreach (var slot in slots)
        {
            slot.slotStats.canRotate = false;
        }
        var screenHeight = EventManager.GetGameCanvasHeight();

        var gap = slots.Last().GetComponent<RectTransform>().anchoredPosition.y + screenHeight;
        foreach (var slot in slots)
        {
            slot.transform.DOLocalMoveY(slot.transform.localPosition.y-gap, .5f).SetEase(Ease.OutBack);
        }

        for (int i = 0; i < slots.Count-1; i++)
        {
            columnNumbers.Add(slots[i].slotStats.number);
        }

       
        
    }
}
