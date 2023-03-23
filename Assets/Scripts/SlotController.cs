using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class SlotController : MonoBehaviour
{
    public Slot slotStats;
    private RectTransform _rectTransform;
    private ColumnController _columnController;
    private void OnValidate()
    {
        SetSlot();
    }

    private void Awake()
    {
         slotStats.speed= EventManager.GetGameData().rotateSpeed;
    }

    private void OnEnable()
    {
        EventManager.SpinButton += SpinButton;
        EventManager.SpeedUpColumns += SpeedUpColumns;
    }

    private void SpinButton()
    {
        slotStats.canRotate = true;
    }

    private void OnDisable()
    {
        EventManager.SpinButton -= SpinButton;
        EventManager.SpeedUpColumns -= SpeedUpColumns;
    }

    private void SpeedUpColumns()
    {
        slotStats.speed *= 2;
    }

    private void Start()
    {
        _rectTransform = GetComponent<RectTransform>();
        _columnController = GetComponentInParent<ColumnController>();
    }

    private void Update()
    {
        if (slotStats.canRotate)
        {
            // slot oyun panelinin altına indiyse ekranın en üstüne taşıyıp yeni bir numara veriyoruz.eğer durdurma
            //aksiyonu çalıştıysa columndan gerekli numarayı atıp en üste taşıyoruz
            var screenHeight = EventManager.GetGameCanvasHeight();
            transform.position -= Vector3.up * Time.deltaTime * slotStats.speed;
            if (_rectTransform.anchoredPosition.y < -(screenHeight))
            {
                _rectTransform.anchoredPosition = new Vector2(_rectTransform.anchoredPosition.x,(screenHeight / 3));
               
                EventManager.RefreshSlot(this, GetComponentInParent<ColumnController>());
                
                if (_columnController.stopColumn)
                {
                    var lastNumber = _columnController.columnNumbers.Last();
                    SetSlot(lastNumber);
                    _columnController.columnNumbers.Remove(lastNumber);
                }
                else
                {
                    SetSlot();

                }

            }
        }
        
    }

    void SetSlot(int? numb = null )
    {
        if (numb==null)
        {
            slotStats.number = Random.Range(0, 10);
            slotStats.slotText.text = slotStats.number.ToString();
        }
        else
        {
            slotStats.number =(int)numb;
            slotStats.slotText.text = slotStats.number.ToString();
        }
       
    }
}