using UnityEngine;
using System.Collections;

public class BulletContainer : MonoBehaviour 
{
    public string BulletCallCode { get { return bulletCallCode; } }

    [SerializeField] protected int ContainerSize;
    [SerializeField] protected string bulletCallCode = "Bullet";

    private Bullet[] Bullets;

    public Bullet GetAvailableBullet()
    {
        for (int i = 0; i < ContainerSize; i++)
        {
            if (Bullets[i].IsReleased())
            {
                Bullets[i].Setup();
                return Bullets[i];
            }
        }

        return null;
    }

    private void Start()
    {
        FillContainer();
    }

    private void FillContainer()
    {
        Bullets = new Bullet[ContainerSize];

        for (int i = 0; i < Bullets.Length; i++)
        {
            Bullets[i] = ObjectFactory.Instance.CreateObjectCode(bulletCallCode).GetComponent<Bullet>();
        }
    }

    private void LateUpdate()
    {
        PositionBulletsToPlatform();
    }

    private void PositionBulletsToPlatform()
    {
        float yOffset = - 0.5f;
        float xOffset = 0.3f;
        int counter = 0;

        for (int i = 0; i < Bullets.Length; i ++)
        {
            if (Bullets[i].IsReleased())
            {
                Bullets[i].transform.position = transform.position + new Vector3(counter * xOffset, yOffset, 0f);
                counter ++;
            }
        }
    }
}
