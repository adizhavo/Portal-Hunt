using UnityEngine;
using System.Collections;

public enum Position
{
    Left, 
    Right, 
    Middle, 
    Up,
    Down
}

public class PlatformPositionReader
{
    private MinMaxValuesHolder horizontal;
    private MinMaxValuesHolder vertical;

    public PlatformPositionReader(MinMaxValuesHolder horizontal, MinMaxValuesHolder vertical)
    {
        this.horizontal = horizontal;
        this.vertical = vertical;
    }

    public PlatformPos GetPosition(Vector2 platformPos)
    {
        Position xPos = GetXPosition(platformPos.x);
        Position yPos = GetYPosition(platformPos.y);
        return new PlatformPos(xPos, yPos);
    }

    private Position GetXPosition(float currentXPos)
    {
        float zoneXSize = (horizontal.max - horizontal.min) / 3f;
        if (currentXPos > horizontal.min + 2 * zoneXSize) return Position.Right;
        else if (currentXPos > horizontal.min + zoneXSize) return Position.Middle;
        else return Position.Left;
    }

    private Position GetYPosition(float currentYPos)
    {
        float zoneYSize = (vertical.max - vertical.min) / 3f;
        if (currentYPos > vertical.min + 2 * zoneYSize) return Position.Up;
        else if (currentYPos > vertical.min + zoneYSize) return Position.Middle;
        else return Position.Down;
    }
}

public struct PlatformPos
{
    public Position xPos;
    public Position yPos;

    public PlatformPos(Position xPos, Position yPos)
    {
        this.xPos = xPos;
        this.yPos = yPos;
    }

    public override string ToString()
    {
        return string.Format("X: {0}, Y: {1}", xPos.ToString(), yPos.ToString());
    }
}