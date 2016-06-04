using UnityEngine;
using System.Collections.Generic;

public class BulletContainer : MonoBehaviour 
{
    public string BulletCallCode { get { return bulletCallCode; } }

    [SerializeField] protected int ContainerSize;
    [SerializeField] protected string bulletCallCode = "Bullet";

    private Bullet[] Bullets;

    public Bullet GetAvailableBullet()
    {
        for (int i = 0; i < ContainerSize; i++)
            if (Bullets[i].IsReleased())
                return Bullets[i];

        return null;
    }

    public float GetBulletCooldown()
    {
        List<float> bulletCooldown = new List<float>();
        List<float> minCooldown = new List<float>();

        for (int i = 0; i < ContainerSize; i++)
        {
            bulletCooldown.Add(Bullets[i].CooldownTime);
            minCooldown.Add(Bullets[i].PreviousCooldown);
        }

        float calculatedPercentage = MathCalc.MinOfFloat(bulletCooldown) / MathCalc.MinOfFloat(minCooldown);

        return GetAvailableBullet() != null ? 1 : calculatedPercentage ;
    }

    public void Init(PlayerType type)
    {
        FillContainer(type);
    }

    private void FillContainer(PlayerType type)
    {
        Bullets = new Bullet[ContainerSize];

        for (int i = 0; i < Bullets.Length; i++)
        {
            Bullets[i] = ObjectFactory.Instance.CreateObjectCode(bulletCallCode).GetComponent<Bullet>();
            Bullets[i].Type = type;
        }
    }

    private void LateUpdate()
    {
        PositionBulletsToPlatform();
    }

    private void PositionBulletsToPlatform()
    {
        float xOffset = 0.3f;
        float HorizontalDistance = -0.15f;
        int counter = 0;

        for (int i = 0; i < Bullets.Length; i ++)
        {
            if (Bullets[i].IsReleased())
            {
                Bullets[i].RigidBody.position = transform.position + new Vector3(HorizontalDistance + counter * xOffset, 0f, 0f);
                counter ++;
            }
        }
    }
}
