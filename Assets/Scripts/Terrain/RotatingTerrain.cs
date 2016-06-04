using UnityEngine;
using System.Collections;

public class RotatingTerrain : TerrainAnimation {

    [SerializeField] private GameObject StartPivot;
    [SerializeField] private GameObject CenterPivot;
    [SerializeField] private GameObject Wing1;
    [SerializeField] private GameObject Wing2;
    [SerializeField] private GameObject Collider1;
    [SerializeField] private GameObject Collider2;

    [SerializeField] private Rotate frameRotation;

    private float introTime = 0.3f;
	
    public override void AnimateEntry()
    {
        if(!gameObject.activeSelf)
        {
            gameObject.SetActive(true);

            StartPivot.transform.localScale = new Vector3(0f, 1f, 1f);
            CenterPivot.transform.localScale = Vector3.zero;
            Wing1.transform.localScale = new Vector3(0f, 1f, 1f);
            Wing2.transform.localScale = new Vector3(0f, 1f, 1f);

            Collider1.transform.localScale = Vector3.zero;
            Collider2.transform.localScale = Vector3.zero;

            CameraShake.Instance.DoShake(ShakeType.Medium);
            LeanTween.scale(StartPivot, Vector3.one, introTime).setEase(LeanTweenType.easeOutBack).setOnComplete(
                () =>
                {
                    LeanTween.scale(CenterPivot, Vector3.one, introTime).setEase(LeanTweenType.easeOutBack).setOnComplete(
                        () => 
                        {
                            CameraShake.Instance.DoShake(ShakeType.Large);
                            LeanTween.scale(Wing1, Vector3.one, introTime).setEase(LeanTweenType.easeOutBack);
                            LeanTween.scale(Wing2, Vector3.one, introTime).setEase(LeanTweenType.easeOutBack).setOnComplete(
                                () => 
                                {
                                    CameraShake.Instance.DoShake(ShakeType.Large);
                                    LeanTween.scale(Collider1, Vector3.one, introTime).setEase(LeanTweenType.easeOutBack);
                                    LeanTween.scale(Collider2, Vector3.one, introTime).setEase(LeanTweenType.easeOutBack).setOnComplete(
                                        () =>
                                        {
                                            frameRotation.rotate = true;
                                        }
                                    );
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
        frameRotation.rotate = false;

        CameraShake.Instance.DoShake(ShakeType.Medium);
        LeanTween.scale(Collider1, Vector3.zero, 0.1f);
        LeanTween.scale(Collider2, Vector3.zero, 0.1f).setOnComplete(
            () =>
            {
                CameraShake.Instance.DoShake(ShakeType.Medium);
                LeanTween.scale(Wing1, new Vector3(0f, 1f, 1f), 0.1f);
                LeanTween.scale(Wing2, new Vector3(0f, 1f, 1f), 0.1f).setOnComplete(
                    () => 
                    {
                        LeanTween.scale(CenterPivot, Vector3.zero, 0.1f).setOnComplete(
                            () => 
                            {
                                CameraShake.Instance.DoShake(ShakeType.Medium);
                                LeanTween.scale(StartPivot, new Vector3(0f, 1f, 1f), 0.1f).setEase(LeanTweenType.easeOutBack).setOnComplete(
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
        );
    }
}