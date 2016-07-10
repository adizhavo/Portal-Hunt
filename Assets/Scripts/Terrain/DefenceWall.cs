using UnityEngine;
using System.Collections;

public class DefenceWall : TerrainChanger
{
    [SerializeField] private StopObject TriggerStopObject;
    [SerializeField] private GameObject RotateWall;

    public override void AnimateEntry(Collision2D coll)
    {
        if (terrainState.Equals(ObjectState.Open)) LeanTween.pause(gameObject);

        gameObject.SetActive(true);
        RotateWall.transform.localEulerAngles = new Vector3(0f, 0f, 90f);
        SetToInitialRotation(); 
    }

    private void SetToInitialRotation()
    {
        CameraShake.Instance.DoShake(ShakeType.Large);
        LeanTween.rotateZ(RotateWall, 0f, 0.3f).setEase(LeanTweenType.easeOutBack).setOnComplete(() => 
        {
            SetOpenState();
            LeanTween.rotateZ(RotateWall, 35f, TriggerStopObject.ObjectStopTime);
        });
    }

    public override void AnimateExit()
    {
        LeanTween.pause(RotateWall);
        CameraShake.Instance.DoShake(ShakeType.Large); 
        LeanTween.rotateZ(RotateWall, 90f, 0.3f).setEase(LeanTweenType.easeInBack).setOnComplete(
            () =>
            {
                SetCloseState();
                gameObject.SetActive(false);
            }
        );
    }

    public override void AnotherCollision(TerrainStopObject StopObject, Collision2D coll)
    {
        Bullet collidedBullet = coll.transform.GetComponent<Bullet>();

        if (!collidedBullet.Type.Equals(DefenderType))
        {
            collidedBullet.StopForSec( -collidedBullet.CooldownTime );
            StopObject.ResetWait();
        }
        else
        {
            LeanTween.pause(RotateWall);
            SetToInitialRotation();
        }
    }
}
