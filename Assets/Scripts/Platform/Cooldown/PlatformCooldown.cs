using UnityEngine;
using System.Collections;

public class PlatformCooldown : MonoBehaviour
{

    public BulletContainer container;

    [SerializeField] private Transform scalePivot;
    [SerializeField] private SpriteRenderer sprite;
    [SerializeField] private Color HoldColor;
    [SerializeField] private Color RechargingColor;
    [SerializeField] private Color ReadyColor;

    Vector3 pivotScale = Vector3.one;

    private void LateUpdate()
    {
        AnimateCooldown();
        SetSpriteColor();
    }

    void AnimateCooldown()
    {
        pivotScale.x = container.GetBulletCooldown();
        scalePivot.localScale = pivotScale;
    }

    private void SetSpriteColor()
    {
        if (pivotScale.x >= 1f)
            sprite.color = ReadyColor;
        else
            sprite.color = RechargingColor;
    }
}
