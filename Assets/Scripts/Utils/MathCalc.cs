using UnityEngine;
using System.Collections;

public static class MathCalc
{
    public static DirectionVector GetTouchDistance(Transform fromPos)
    {
        return GetTouchDistance(fromPos.position);
    }

    public static DirectionVector GetTouchDistance(Vector2 fromPos)
    {
        Vector2 wordTouchPos = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition);
        float touchDistance = Vector2.Distance(fromPos, wordTouchPos);
        return new DirectionVector(wordTouchPos - fromPos, touchDistance);
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

    public static float CalculateVectorAngle(Vector2 v1, Vector2 v2)
    {
        Vector3 diffVect = (v1 - v2).normalized;
        float AngleRad = Mathf.Atan2(diffVect.y, diffVect.x);
        return (180 / Mathf.PI) * AngleRad;
    }

    public static Vector3 GetWorldPos(Vector2 screenPercentagePos)
    {
        return Camera.main.ViewportToWorldPoint(new Vector3(screenPercentagePos.x / 100f, screenPercentagePos.y / 100f));
    }
}

[System.Serializable]
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

[System.Serializable]
public struct MinMaxValuesHolder
{
    public float min;
    public float max;

    public MinMaxValuesHolder(float min, float max)
    {
        this.min = min;
        this.max = max;
    }
}
