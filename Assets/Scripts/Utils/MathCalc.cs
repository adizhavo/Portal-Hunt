using UnityEngine;
using System.Collections;

public static class MathCalc
{
    public static DirectionVector GetTouchDistance(Transform fromPos)
    {
        Vector2 wordTouchPos = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition);
        float touchDistance = Vector2.Distance(fromPos.position, wordTouchPos);
        return new DirectionVector(wordTouchPos, touchDistance);
    }

    public static DirectionVector GetTouchDistance(Vector3 fromPos)
    {
        Vector2 wordTouchPos = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition);
        float touchDistance = Vector2.Distance(fromPos, wordTouchPos);
        return new DirectionVector(wordTouchPos, touchDistance);
    }

    public static void ClampVectMagnitude(ref DirectionVector dirVect, float maxMagnitude)
    {
        if (dirVect.magnitudeOfDir > maxMagnitude)
        {
            dirVect.direction = Vector3.ClampMagnitude(dirVect.direction, maxMagnitude);
        }
    }

    public static Vector2 GetDeltaBetweenDirVector(DirectionVector fromDir, DirectionVector toDir)
    {
        return toDir.direction - fromDir.direction;
    }
}

public struct DirectionVector
{
    public Vector2 direction;
    public float magnitudeOfDir;

    public DirectionVector(Vector2 worldTouchPos, float touchDistance)
    {
        this.direction = worldTouchPos;
        this.magnitudeOfDir = touchDistance;
    }

    public DirectionVector(Vector2 worldTouchPos)
    {
        this.direction = worldTouchPos;
        this.magnitudeOfDir = direction.magnitude;
    }
}
