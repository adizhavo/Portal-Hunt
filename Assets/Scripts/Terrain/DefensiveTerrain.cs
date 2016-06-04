using UnityEngine;
using System.Collections;

public class DefensiveTerrain : TerrainChanger
{
    [SerializeField] private PlayerType DefenderType;
    [SerializeField] private GameObject FirstBlock;
    [SerializeField] private GameObject SecondBlock;

    public override void AnimateEntry(Collision2D coll)
    {
        gameObject.SetActive(true);
        FirstBlock.transform.localScale = new Vector3(0f, 1f, 1f);
        SecondBlock.transform.localScale = new Vector3(0f, 1f, 1f);

        CameraShake.Instance.DoShake(ShakeType.Large);
        LeanTween.scaleX(FirstBlock, 1f, 0.3f).setEase(LeanTweenType.easeOutBack).setOnComplete(
            () =>
            {
                CameraShake.Instance.DoShake(ShakeType.Medium);
                LeanTween.scaleX(SecondBlock, 1f, 0.3f).setEase(LeanTweenType.easeOutBack);
            }
        );
    }

    public override void AnimateExit()
    {
        CameraShake.Instance.DoShake(ShakeType.Medium);
        LeanTween.scaleX(SecondBlock, 0, 0.3f).setEase(LeanTweenType.easeOutBack).setOnComplete(
            () =>
            {
                CameraShake.Instance.DoShake(ShakeType.Large);
                LeanTween.scaleX(FirstBlock, 0f, 0.3f).setEase(LeanTweenType.easeOutBack).setOnComplete(
                    () =>
                    {
                        gameObject.SetActive(false);
                    }
                );
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
    }
}
