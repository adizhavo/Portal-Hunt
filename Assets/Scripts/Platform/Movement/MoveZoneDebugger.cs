#if UNITY_EDITOR
using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class MoveZoneDebugger : MonoBehaviour
{

    [SerializeField] private PlatformMovement movementControl;

    private void Update()
    {
        movementControl.SetWorldBoundaries();

        MinMaxValuesHolder horizontal = movementControl.HorizontalBoundaries;
        MinMaxValuesHolder vertical = movementControl.VerticalBoundaries;

        Vector3 BottomLeft = new Vector2(horizontal.min, vertical.min);
        Vector3 BottomRight = new Vector2(horizontal.max, vertical.min);
        Vector3 UpperLeft = new Vector2(horizontal.min, vertical.max);
        Vector3 UpperRight = new Vector2(horizontal.max, vertical.max);

        Debug.DrawLine(BottomLeft, BottomRight);
        Debug.DrawLine(BottomLeft, UpperLeft);
        Debug.DrawLine(BottomRight, UpperRight);
        Debug.DrawLine(UpperLeft, UpperRight);
    }
}
#endif
