using UnityEngine;
using System.Collections;

public class SecondTerrainAnimation : TerrainAnimation {

    [SerializeField] private GameObject FirstBlock;
    [SerializeField] private GameObject SecondBlock;
    [SerializeField] private float RefreshTime;

    private GameObject scaleObject;
    private float timerCounter;

    public override void AnimateEntry()
    {
        if (!gameObject.activeInHierarchy)
        {
            gameObject.SetActive(true);

            FirstBlock.transform.localScale = new Vector3(0f, 2f, 1f);
            SecondBlock.transform.localScale = new Vector3(0f, 2f, 1f);

            scaleObject = SecondBlock;
            LeanTween.scaleX(scaleObject, 3.8f, 0.3f).setEase(LeanTweenType.easeOutBack);
            CameraShake.Instance.DoShake();

            timerCounter = 0f;
        }
    }

    public override void AnimateExit()
    {
        LeanTween.scaleX(FirstBlock, 0f, 0.1f);
        LeanTween.scaleX(SecondBlock, 0f, 0.1f).setOnComplete(
            () =>
            {
                gameObject.SetActive(false);
            }
        );
    }

    private void Update()
    {
        timerCounter += Time.deltaTime;

        if (timerCounter > RefreshTime)
        {
            LeanTween.scaleX(scaleObject, 0f, 0.3f).setEase(LeanTweenType.easeOutBack);
            scaleObject = scaleObject == FirstBlock ? SecondBlock : FirstBlock;
            LeanTween.scaleX(scaleObject, 3.8f, 0.3f).setEase(LeanTweenType.easeOutBack);
            CameraShake.Instance.DoShake();

            timerCounter = 0f;
        }
    }
}
