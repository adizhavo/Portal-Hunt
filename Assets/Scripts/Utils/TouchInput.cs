﻿using UnityEngine;

public static class TouchInput 
{
    private static bool Multitouch = false;

    public static bool TouchDown()
    {
        if (!Multitouch)
            return Input.GetMouseButtonDown(0);
        else
        {
            // TODO : Add multitouch support
            return false;
        }

        return false;
    }

    public static bool Touch()
    {
        if (!Multitouch)
            return Input.GetMouseButton(0);
        else
        {
            // TODO : Add multitouch support
            return false;
        }

        return false;
    }

    public static bool TouchUp()
    {
        if (!Multitouch)
            return Input.GetMouseButtonUp(0);
        else
        {
            // TODO : Add multitouch support
            return false;
        }

        return false;
    }

    public static Vector2 TouchPos()
    {
        if (!Multitouch)
            return (Vector2)Input.mousePosition;
        else
        {
            // TODO : Add multitouch support
            return Vector2.zero;
        }

        return Vector2.zero;
    }
}
