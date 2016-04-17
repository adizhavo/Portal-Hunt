#if UNITY_EDITOR
using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class MoveZoneDebugger : MonoBehaviour
{

    [SerializeField] private PlatformMovement movementControl;

    private void Update()
    {
        Rect debugRect = movementControl.MoveZone;

        Vector3 BottomLeft = new Vector3(debugRect.x, debugRect.y + debugRect.height, 0f);
        Vector3 BottomRight = new Vector3(debugRect.x + debugRect.width, debugRect.y + debugRect.height, 0f);
        Vector3 UpperLeft = new Vector3(debugRect.x, debugRect.y, 0f);
        Vector3 UpperRight = new Vector3(debugRect.x + debugRect.width, debugRect.y, 0f);

        Debug.DrawLine(BottomLeft, BottomRight);
        Debug.DrawLine(BottomLeft, UpperLeft);
        Debug.DrawLine(BottomRight, UpperRight);
        Debug.DrawLine(UpperLeft, UpperRight);
    }
}
#endif
