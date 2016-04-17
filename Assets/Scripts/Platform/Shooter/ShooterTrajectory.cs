using UnityEngine;
using System.Collections;

public class ShooterTrajectory
{
    private Vector2[] trajectoryPos;
    private Transform[] trajectoryTr;

    private string factoryCallCode = "ShootTrajectories";
    private float SampleInterval = 5f;
    private int Samples = 5;

    private static ShooterTrajectory instance;

    public static ShooterTrajectory GetShooterTrajectory()
    {
        if (instance == null)
        {
            instance = new ShooterTrajectory();
        }

        instance.ActivateTrajectories();
        return instance;
    }

    private ShooterTrajectory()
    {
        trajectoryPos = new Vector2[Samples];
        trajectoryTr = new Transform[Samples];
    }

    private void ActivateTrajectories()
    {
        for (int tr = 0; tr < trajectoryTr.Length; tr++)
        {
            if (trajectoryTr[tr] == null)
                trajectoryTr[tr] = ObjectFactory.Instance.CreateObjectCode(factoryCallCode).transform;
            else
                trajectoryTr[tr].gameObject.SetActive(true);
        }
    }

    public void Calculate(DirectionVector shootVect, Vector2 platformPos, float forceMultiplier)
    {
        DirectionVector shVect = new DirectionVector(-1 * shootVect.direction * forceMultiplier, shootVect.magnitudeOfDir);

        for (int index = 0; index < trajectoryPos.Length; index++)
        {
            Vector2 shootDir = shVect.direction * Time.fixedDeltaTime * SampleInterval;
            Vector2 calcDir = (index == 0) ? platformPos + shootDir : trajectoryPos[index - 1] + shootDir;
            trajectoryPos[index] = calcDir;
            trajectoryTr[index].position = (Vector3)calcDir;

            shVect.direction += (Physics2D.gravity * Time.fixedDeltaTime * SampleInterval);
        }
    }

    public void Disable()
    {
        for (int tr = 0; tr < trajectoryTr.Length; tr++)
        {
            trajectoryTr[tr].gameObject.SetActive(false);
        }
    }
}
