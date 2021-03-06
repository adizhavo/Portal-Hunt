﻿using UnityEngine;
using System.Collections;

public class ShooterTrajectory
{
    private Vector2[] trajectoryPos;
    private Transform[] trajectoryTr;
    private TrajectoryPoint[] trajectoryPoints;

    private string factoryCallCode = "ShootTrajectories";
    private float SampleInterval = 5f;
    private int Samples = 7;

    public ShooterTrajectory()
    {
        trajectoryPos = new Vector2[Samples];
        trajectoryTr = new Transform[Samples];
        trajectoryPoints = new TrajectoryPoint[Samples];
    }

    private void ActivateTrajectories()
    {
        for (int tr = 0; tr < trajectoryTr.Length; tr++)
        {
            if (trajectoryTr[tr] == null)
            {
                trajectoryTr[tr] = ObjectFactory.Instance.CreateObjectCode(factoryCallCode).transform;
                trajectoryPoints[tr] = trajectoryTr[tr].GetComponent<TrajectoryPoint>();
            }
            else
                trajectoryTr[tr].gameObject.SetActive(true);
        }
    }

    public void Calculate(DirectionVector shootVect, Vector2 targetPos, float forceMultiplier)
    {
        DirectionVector shVect = new DirectionVector(shootVect.InvertedDirection() * forceMultiplier, shootVect.magnitudeOfDir);

        for (int index = 0; index < trajectoryPos.Length; index++)
        {
            Vector2 shootDir = shVect.direction * Time.fixedDeltaTime * SampleInterval;
            Vector2 calcDir = (index == 0) ? targetPos + shootDir : trajectoryPos[index - 1] + shootDir;
            trajectoryPos[index] = calcDir;
            trajectoryTr[index].position = (Vector3)calcDir;

            shVect.direction += (Physics2D.gravity * Time.fixedDeltaTime * SampleInterval);
        }
    }

    public void Enable()
    {
        ActivateTrajectories();
    }

    public void Disable()
    {
        for (int tr = 0; tr < trajectoryTr.Length; tr++)
            trajectoryTr[tr].gameObject.SetActive(false);
    }

    public void SetPointsState(bool state)
    {
        for (int tr = 0; tr < trajectoryTr.Length; tr++)
            trajectoryPoints[tr].SetState(state);
    }
}
