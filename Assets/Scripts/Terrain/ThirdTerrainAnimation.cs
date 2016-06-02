using UnityEngine;
using System.Collections;

public class ThirdTerrainAnimation : TerrainAnimation
{
    [SerializeField] private GameObject Block;
    [SerializeField] private Rotate objectRotation;

    public override void AnimateEntry()
    {
        if (!gameObject.activeInHierarchy)
        {
            gameObject.SetActive(true);
            Block.transform.localScale = new Vector3(0f, 2f, 1f);

            LeanTween.scaleX(Block, 3.3f, 0.3f).setEase(LeanTweenType.easeOutBack).setOnComplete(
                ()=>
                {
                    objectRotation.enabled = true;
                }
            );
            CameraShake.Instance.DoShake();
        }
    }

    public override void AnimateExit()
    {
        objectRotation.enabled = false;
        LeanTween.scaleX(Block, 3.3f, 0.3f).setEase(LeanTweenType.easeInBack).setOnComplete(
            () =>
            {
                gameObject.SetActive(false);
            }
        );
    }
}
