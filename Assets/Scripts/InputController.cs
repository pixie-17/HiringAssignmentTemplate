using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class InputController
{

    public static float GetHorizontalAxis()
    {
#if UNITY_ANDROID || UNITY_IOS
        return GetHorizontalViaTouch();
#elif UNITY_EDITOR || UNITY_STANDALONE
        return GetHorizontalViaKeyBoard();
#endif
    }

    private static float GetHorizontalViaTouch()
    {
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved)
        {
            Vector2 touchDeltaPosition = Input.GetTouch(0).deltaPosition;
            return touchDeltaPosition.x;
        }
        return 0f;
    }

    private static float GetHorizontalViaKeyBoard()
    {
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow))
        {
            return Input.GetAxis("Horizontal");
        }
        return 0f;
    }
}
