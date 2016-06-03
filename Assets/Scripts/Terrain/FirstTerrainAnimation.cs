using UnityEngine;
using System.Collections;

public class FirstTerrainAnimation : TerrainAnimation {

    [SerializeField] private GameObject MainPivot;
    [SerializeField] private GameObject StartCube;
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

            MainPivot.transform.localScale = new Vector3(0f, 1f, 1f);
            StartCube.transform.localScale = Vector3.zero;
            Wing1.transform.localScale = new Vector3(0f, 1f, 1f);
            Wing2.transform.localScale = new Vector3(0f, 1f, 1f);

            Collider1.transform.localScale = Vector3.zero;
            Collider2.transform.localScale = Vector3.zero;

            CameraShake.Instance.DoShake(ShakeType.Medium);
            LeanTween.scale(MainPivot, Vector3.one, introTime).setEase(LeanTweenType.easeOutBack).setOnComplete(
                () =>
                {
                    LeanTween.scale(StartCube, Vector3.one, introTime).setEase(LeanTweenType.easeOutBack).setOnComplete(
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
                        LeanTween.scale(StartCube, Vector3.zero, 0.1f).setOnComplete(
                            () => 
                            {
                                CameraShake.Instance.DoShake(ShakeType.Medium);
                                LeanTween.scale(MainPivot, new Vector3(0f, 1f, 1f), 0.1f).setEase(LeanTweenType.easeOutBack).setOnComplete(
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