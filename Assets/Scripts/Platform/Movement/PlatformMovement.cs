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

    [SerializeField] protected MinMaxValuesHolder horizontalBoundaries;
    [SerializeField] protected MinMaxValuesHolder verticalBoundaries;
    [SerializeField] protected float minDistToChange;
    [SerializeField] protected float rotationSpeed;
    [SerializeField] protected float movementSpeed;
    [HideInInspector] protected Vector2 direction = Vector2.right;

    #endregion

    public override void Init()
    {
        currentState = new PositionChooser(this);
    }

    public void Move()
    {
        transform.Translate(Direction * Time.deltaTime * movementSpeed);
    }

    public void RotateTowardTarget(Vector2 targetPos)
    {
        Vector2 targetDir = targetPos - Position2D;
        Vector2 platformDir = Direction ;
        float sign = Mathf.Sign(Vector3.Cross(platformDir, targetDir).z);
        float frameAngle = Vector2.Angle(targetDir, platformDir) * RotationSpeed;
        frameAngle = Mathf.Clamp(frameAngle, RotationSpeed, Mathf.Infinity) * sign;
        Rotation = frameAngle;
    }

    public Vector2 SelectPositionInZone()
    {
        float xRandomPos = Random.Range(HorizontalBoundaries.min, HorizontalBoundaries.max);
        float yRandomPos = Random.Range(VerticalBoundaries.min, VerticalBoundaries.max);

        return new Vector2(xRandomPos, yRandomPos);
    }
}