using UnityEngine;
using System.Collections;

public class DefensiveTerrain : TerrainAnimation
{
    [SerializeField] private GameObject FirstBlock;
    [SerializeField] private GameObject SecondBlock;

    public override void AnimateEntry()
    {
        if (!gameObject.activeInHierarchy)
        {
            gameObject.SetActive(true);
            FirstBlock.transform.localScale = new Vector3(0f, 1f, 1f);
            SecondBlock.transform.localScale = new Vector3(0f, 1f, 1f);

            CameraShake.Instance.DoShake(ShakeType.Large);
            LeanTween.scaleX(FirstBlock, 1f, 0.3f).setEase(LeanTweenType.easeOutBack).setOnComplete(
                ()=>
                {
                    CameraShake.Instance.DoShake(ShakeType.Medium);
                    LeanTween.scaleX(SecondBlock, 1f, 0.3f).setEase(LeanTweenType.easeOutBack);
                }
            );
        }
    }

    public override void AnimateExit()
    {
        CameraShake.Instance.DoShake(ShakeType.Medium);
        LeanTween.scaleX(SecondBlock, 0, 0.3f).setEase(LeanTweenType.easeOutBack).setOnComplete(
            () =>
            {
                CameraShake.Instance.DoShake(ShakeType.Large);
                LeanTween.scaleX(FirstBlock, 0f, 0.3f).setEase(LeanTweenType.easeOutBack).setOnComplete(
                    ()=>
                    {
                        gameObject.SetActive(false);
                    }
                );
            }
        );
    }
}
