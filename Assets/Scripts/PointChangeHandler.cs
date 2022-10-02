using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PointChangeHandler
{
    public delegate void PointChangeEvent();
    public static PointChangeEvent OnPointChange;

    public static void PointChange()
    {
        if(OnPointChange != null)
        {
            OnPointChange();
        }

    }

}
