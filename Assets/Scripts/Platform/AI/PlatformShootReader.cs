using UnityEngine;
using System.Collections;

public class PlatformShootReader : PlatformShoot {

    [SerializeField] private PlatformMovement pltMover;
    private PlatformPositionReader posReader;

    public override void Init()
    {
        base.Init();
        posReader = new PlatformPositionReader(pltMover.HorizontalBoundaries, pltMover.VerticalBoundaries);
    }

	public override void Shoot(DirectionVector dirVect)
    {
        base.Shoot(dirVect);
        PlatformPos currentPos = posReader.GetPosition(Position2D);
        int currentMapState = MapStateAccessor.Instance.GetActiveState();
        AIDatabase.Instance.LearnShot(dirVect, currentPos, currentMapState);
    }
}