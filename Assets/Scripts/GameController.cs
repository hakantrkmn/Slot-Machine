using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using Random = UnityEngine.Random;

public class GameController : MonoBehaviour
{
    public List<ColumnController> columns;
    public int[,] gameMatrix = new int[3, 5];


    private void OnEnable()
    {
        EventManager.GetGameCanvasHeight += GetGameCanvasHeight;
        EventManager.StopSlot += StopSlot;
        EventManager.SpinButton += SpinButton;

    }

    private void SpinButton()
    {
        Sequence spin = DOTween.Sequence();
        spin.AppendInterval(2);
        spin.AppendCallback((() => { EventManager.StopSlot(); }));
    }

    private float GetGameCanvasHeight()//oyun panelinin yüksekliğini döndürüyorum
    {
        return GetComponent<RectTransform>().rect.height;
    }

    private void OnDisable()
    {
        EventManager.GetGameCanvasHeight -= GetGameCanvasHeight;
        EventManager.StopSlot -= StopSlot;
        EventManager.SpinButton -= SpinButton;
    }

    private void StopSlot()
    { // spin durdurulduysa önce ilk 2 column u durduruyorum. diğerlerinin hızını arttırıyorum. biraz bekleyip
        // diğer columnları durduruyorum. aralarına .3f gibi bir bekleme süresi koydum
        
        Sequence stopSlot = DOTween.Sequence();

        for (int i = 0; i < 2; i++)
        {
            var temp = columns[i];
            stopSlot.AppendCallback(() => { temp.stopColumn = true; });
            stopSlot.AppendInterval(.5f);
        }

        stopSlot.AppendCallback(() => { EventManager.SpeedUpColumns(); });
        stopSlot.AppendInterval(1f);
        for (int i = 2; i < columns.Count; i++)
        {
            var temp = columns[i];
            stopSlot.AppendCallback(() => { temp.stopColumn = true; });
            stopSlot.AppendInterval(.3f);
        }
    }

    private void OnValidate()
    {
        columns.Clear();

        foreach (var column in GetComponentsInChildren<ColumnController>())
        {
            columns.Add(column);
        }

        RandomMatrix();
    }

    // oyun başında random matrix oluşturma
    void RandomMatrix()
    {
        for (int i = 0; i < gameMatrix.GetLength(0); i++)
        {
            for (int j = 0; j < gameMatrix.GetLength(1); j++)
            {
                gameMatrix[i, j] = Random.Range(0, 10);
            }
        }
    }

    private void Start()
    {
        SetColumnsNumber();
    }

    
    //columnlara spin sonunda durması gereken numaraları ekliyorum
    void SetColumnsNumber()
    {
        for (int i = 0; i < columns.Count; i++)
        {
            for (int j = 0; j < gameMatrix.GetLength(0); j++)
            {
                columns[i].columnNumbers.Add(gameMatrix[j, i]);
            }
        }
    }


   
}