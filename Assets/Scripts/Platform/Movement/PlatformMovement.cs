using UnityEngine;
using System.Collections;

public class PlatformMovement : FrameStateObject
{
    #region Public Fields

    public Rect MoveZone { get { return moveZone; } }

    public float Rotation{ set { direction = Quaternion.AngleAxis(value, Vector3.forward) * direction; } }

    public Vector2 Direction { get { return direction; } }

    public float MinDistToChange { get { return minDistToChange; } }

    public float RotationSpeed { get { return rotationSpeed; } }

    #endregion

    #region Private Fields

    [SerializeField] protected Rect moveZone;
    [SerializeField] protected float minDistToChange;
    [SerializeField] protected float rotationSpeed;
    [SerializeField] protected float movementSpeed;
    [HideInInspector] protected Vector2 direction = Vector2.right;

    #endregion

    protected override void InitializeStates()
    {
        currentState = new PositionChooser(this);
    }

    public void Move()
    {
        transform.Translate(Direction * Time.deltaTime * movementSpeed);
    }
}
