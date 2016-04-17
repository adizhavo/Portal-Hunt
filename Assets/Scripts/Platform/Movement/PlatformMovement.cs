using UnityEngine;
using System.Collections;

public class PlatformMovement : FrameStateObject
{
    #region Public Fields

    public MinMaxValuesHolder HorizontalBoundaries { get { return horizontalBoundaries; } }

    public MinMaxValuesHolder VerticalBoundaries { get { return verticalBoundaries; } }

    public float Rotation{ set { direction = Quaternion.AngleAxis(value, Vector3.forward) * direction; } }

    public Vector2 Direction { get { return direction; } }

    public float MinDistToChange { get { return minDistToChange; } }

    public float RotationSpeed { get { return rotationSpeed; } }

    #endregion

    #region Private Fields

    [HideInInspector] protected MinMaxValuesHolder horizontalBoundaries;
    [HideInInspector] protected MinMaxValuesHolder verticalBoundaries;
    [SerializeField] protected ScreenPercentage percentagePos;
    [SerializeField] protected float minDistToChange;
    [SerializeField] protected float rotationSpeed;
    [SerializeField] protected float movementSpeed;
    [HideInInspector] protected Vector2 direction = Vector2.right;

    #endregion

    protected override void InitializeStates()
    {
        currentState = new PositionChooser(this);
        SetWorldBoundaries();
    }

    public void SetWorldBoundaries()
    {
        Vector3 BottomLeft = MathCalc.GetWorldPos(
            new Vector2(percentagePos.LeftMarginPercentage, percentagePos.BottomMarginPercentage));
        
        Vector3 UpperRight = MathCalc.GetWorldPos
            (new Vector2(percentagePos.RightMarginPercentage, percentagePos.UpperMarginPercentage));

        horizontalBoundaries.min = BottomLeft.x;
        horizontalBoundaries.max = UpperRight.x;
        verticalBoundaries.min = BottomLeft.y;
        verticalBoundaries.max = UpperRight.y;
    }

    public void Move()
    {
        transform.Translate(Direction * Time.deltaTime * movementSpeed);
    }
}

[System.Serializable]
public struct ScreenPercentage
{
    [RangeAttribute(0f, 100f)] public float LeftMarginPercentage;
    [RangeAttribute(0f, 100f)] public float RightMarginPercentage;
    [RangeAttribute(0f, 100f)] public float UpperMarginPercentage;
    [RangeAttribute(0f, 100f)] public float BottomMarginPercentage;

    public ScreenPercentage(float LeftMarginPercentage, float RightMarginPercentage, float UpperMarginPercentage, float BottomMarginPercentage)
    {
        this.LeftMarginPercentage = LeftMarginPercentage;
        this.RightMarginPercentage = RightMarginPercentage;
        this.UpperMarginPercentage = UpperMarginPercentage;
        this.BottomMarginPercentage = BottomMarginPercentage;
    }
}