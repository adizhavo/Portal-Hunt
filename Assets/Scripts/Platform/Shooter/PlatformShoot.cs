using UnityEngine;
using System.Collections;

public enum ShootPosition
{
    Left, 
    Right
}

public class PlatformShoot : FrameStateObject
{
    #region Public Fields
    public float MinDistanceOfTouch { get { return minDistanceOfTouch; } }

    public float MaxDragDistance { get { return maxDragDistance; } }

    public float ShootForceMultiplier { get { return shootForceMultiplier; } }

    public string DragGizmosCallCode { get { return dragGizmosCallCode; } }

    // Setted in the editor or throught code
    public ShootPosition ShooterPosition;
    #endregion

    #region Protected Fields
    [SerializeField] private BulletContainer bulletContainer;
    [SerializeField] protected float minDistanceOfTouch;
    [SerializeField] protected float maxDragDistance;
    [SerializeField] protected float shootForceMultiplier;
    [SerializeField] protected string dragGizmosCallCode = "DragGizmos";
    protected ShooterTrajectory shootTraject = new ShooterTrajectory();
    protected DragGizmo gizmo;
    #endregion

    public override void Init()
    {
        currentState = new FirstTouch(this);
    }

    public virtual void Shoot(DirectionVector dirVect)
    {
        if (Main.activeState.Equals(GameState.Stop)) return;

        Bullet bullet = bulletContainer.GetAvailableBullet();

        if (bullet != null)
        {
            DirectionVector multipliedDirVect = new DirectionVector(dirVect.direction * shootForceMultiplier);
            bullet.Shoot(multipliedDirVect, transform.position);
        }
        #if UNITY_EDITOR
        else
        {
            Debug.LogWarning("You tried to shoot not a bullet, check your call to the factory or maybe the pool size..");
        }
        #endif
    }

    public bool IsPlatformTouch(Vector2 touchPos)
    {
        int touchXPos = (int)Camera.main.WorldToScreenPoint(touchPos).x;

        if (touchXPos - Screen.width/2 > 0 && ShooterPosition.Equals(ShootPosition.Right)) return true;
        if (touchXPos - Screen.width/2 < 0 && ShooterPosition.Equals(ShootPosition.Left)) return true;

        return false;
    }

    public bool IsTouchOnGizmo(Vector2 firstTouch, DirectionVector shootDirection)
    {
        // Can be added collision layers only with the gizmo
        RaycastHit2D hit = Physics2D.Raycast(firstTouch + shootDirection.direction, Vector2.zero, Mathf.Epsilon);
        return hit.transform != null;
    }

    public bool IsDirectionValid(DirectionVector currentTouch)
    {
        return currentTouch.magnitudeOfDir > MinDistanceOfTouch && currentTouch.direction.y < - MinDistanceOfTouch;
    }

    public DirectionVector GetCalculatedShoot(Vector2 touchPos)
    {
        DirectionVector calcShootDir = MathCalc.GetTouchDistance(touchPos);
        MathCalc.ClampVectMagnitude(ref calcShootDir, MaxDragDistance);
        return calcShootDir;
    }

    public void ReleaseGizmo()
    {
        if(gizmo == null)
            return;

        gizmo.Release();
        gizmo = null;
    }

    public void DrawGizmo(Vector2 firstTouch, DirectionVector shootDirection, bool dragZoneActive = false)
    {
        if (gizmo == null)
            ChooseGizmo();

        bool isAllowed = IsTouchOnGizmo(firstTouch, shootDirection);

        gizmo.PositionObject(firstTouch, firstTouch + shootDirection.direction);
        gizmo.SetState(isAllowed);

        if (dragZoneActive)
        {
            gizmo.EnableDragZone();
            gizmo.SetDraggableZone(2, 2);
        }
    }

    public void DrawTrajecotry(DirectionVector shootDir)
    {
        bool isAllowedShot = IsDirectionValid(shootDir);
        ActivateTrajectory(shootDir, isAllowedShot);
    }

    public void DrawTrajecotry(Vector2 firstTouch, DirectionVector shootDir)
    {
        bool isAllowedShot = IsTouchOnGizmo(firstTouch, shootDir);
        ActivateTrajectory(shootDir, isAllowedShot);
    }

    public void ReleaseTrajectory()
    {
        shootTraject.Disable();
    }

    private void ActivateTrajectory(DirectionVector shootDir, bool isAllowedShot)
    {
        shootTraject.Enable();
        shootTraject.SetPointsState(isAllowedShot);
        shootTraject.Calculate(shootDir, Position2D, ShootForceMultiplier);
    }

    private void ChooseGizmo()
    {
        GameObject gizmoObject = ObjectFactory.Instance.CreateObjectCode(DragGizmosCallCode) as GameObject;
        gizmo = gizmoObject.GetComponent<DragGizmo>();
    }
}