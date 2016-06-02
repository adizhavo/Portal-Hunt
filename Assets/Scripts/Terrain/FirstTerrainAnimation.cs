using UnityEngine;
using System.Collections;

public class FirstTerrainAnimation : TerrainAnimation {

    [SerializeField] private GameObject StartCube;
    [SerializeField] private GameObject Wing1;
    [SerializeField] private GameObject Wing2;
    [SerializeField] private GameObject Collider1;
    [SerializeField] private GameObject Collider2;

    [SerializeField] private Rotate frameRotation;
	
    public override void AnimateEntry()
    {
        if(!gameObject.activeSelf)
        {
            gameObject.SetActive(true);

            StartCube.transform.localScale = Vector3.zero;
            Wing1.transform.localScale = Vector3.zero;
            Wing2.transform.localScale = Vector3.zero;
            Collider1.transform.localScale = Vector3.zero;
            Collider2.transform.localScale = Vector3.zero;

            LeanTween.scale(StartCube, Vector3.one * 2, 0.5f).setEase(LeanTweenType.easeInOutBounce).setOnComplete(
                () => 
                {
                    CameraShake.Instance.DoShake();
                    LeanTween.scale(Wing1, Vector3.one * 2, 0.3f).setEase(LeanTweenType.easeOutBack);
                    LeanTween.scale(Wing2, Vector3.one * 2, 0.3f).setEase(LeanTweenType.easeOutBack).setOnComplete(
                        () => 
                        {
                            LeanTween.scale(Collider1, Vector3.one * 3.5f, 0.3f).setEase(LeanTweenType.easeOutBack);
                            LeanTween.scale(Collider2, Vector3.one * 3.5f, 0.3f).setEase(LeanTweenType.easeOutBack).setOnComplete(
                                () =>
                                {
                                    frameRotation.enabled = true;
                                }
                            );
                        }
                    );
                }
            );
        }
    }

    public override void AnimateExit()
    {
        frameRotation.enabled = false;

        LeanTween.scale(Collider1, Vector3.zero, 0.1f);
        LeanTween.scale(Collider2, Vector3.zero, 0.1f).setOnComplete(
            () =>
            {
                LeanTween.scale(Wing1, Vector3.zero, 0.1f);
                LeanTween.scale(Wing2, Vector3.zero, 0.1f).setOnComplete(
                    () => 
                    {
                        LeanTween.scale(StartCube, Vector3.zero, 0.2f).setOnComplete(
                            () => 
                            {
                                gameObject.SetActive(false);
                            }
                        );
                    }
                );
            }
        );
    }
}