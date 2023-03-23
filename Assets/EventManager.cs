using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class EventManager
{
    public static Action SpinButton;
    public static Action SpeedUpColumns;
    public static Action<SlotController,ColumnController> RefreshSlot;
    public static Func<float> GetGameCanvasHeight;

}
