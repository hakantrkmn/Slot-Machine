using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonController : MonoBehaviour
{
    public ButtonTypes buttonType;



    public void ButtonClicked()
    {
        switch (buttonType)
        {
            case ButtonTypes.SpinButton:
                EventManager.SpinButton();
                break;
        }
    }
}
