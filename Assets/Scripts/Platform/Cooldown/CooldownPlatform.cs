using UnityEngine;
using System.Collections;

public class CooldownPlatform : MonoBehaviour {

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
        float percentage = container.GetBulletCooldown();
        // invert value if in cooldown, since the cooldown starts from maxValue to 0
        pivotScale.x = (percentage > 0f && percentage < 1f) ? 1f - percentage : percentage;
        scalePivot.localScale = pivotScale;
    }

    private void SetSpriteColor()
    {
        if (pivotScale.x <= 0f)
            sprite.color = HoldColor;
        else
            if (pivotScale.x >= 1f)
                sprite.color = ReadyColor;
            else
                sprite.color = RechargingColor;
    }
}
