using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using Random = UnityEngine.Random;

public class GameController : MonoBehaviour
{
    public List<ColumnController> columns;
    public int[,] gameMatrix = new int[3,5];


    private void OnEnable()
    {
        EventManager.GetGameCanvasHeight += GetGameCanvasHeight;
        EventManager.SpinButton += SpinButton;
    }

    private float GetGameCanvasHeight()
    {
        return GetComponent<RectTransform>().rect.height;
    }

    private void OnDisable()
    {
        EventManager.GetGameCanvasHeight -= GetGameCanvasHeight;
        EventManager.SpinButton -= SpinButton;
    }

    private void SpinButton()
    {
        Sequence spin = DOTween.Sequence();
        for (int i = 0; i < 2; i++)
        {
            var temp = columns[i];
            spin.AppendCallback(()=> { temp.stopColumn = true; });
            spin.AppendInterval(.5f);
            


        }
        spin.AppendCallback(()=> { EventManager.SpeedUpColumns(); });
        spin.AppendInterval(2f);
        for (int i = 2; i < columns.Count; i++)
        {
            var temp = columns[i];
            spin.AppendCallback(()=> { temp.stopColumn = true; });
            spin.AppendInterval(.2f);

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

    public void RandomMatrix()
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
        for (int i = 0; i < columns.Count; i++)
        {
            for (int j = 0; j < gameMatrix.GetLength(0); j++)
            {
                columns[i].columnNumbers.Add(gameMatrix[j, i]);
            }
            
        }

        
    }


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            
        }
        else if (Input.GetKeyDown(KeyCode.A))
        {
            var temp = columns[2];
           
            temp.stopColumn = true;           
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            var temp = columns[3];
           
            temp.stopColumn = true;           
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            var temp = columns[4];
           
            temp.stopColumn = true;           
        }
    }
}
