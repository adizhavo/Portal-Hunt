using UnityEngine;
using System.Collections;

public class PlatformAIShoot : PlatformShoot {

    [SerializeField] private Vector2 AimTime;
    [SerializeField] protected PlatformMovement pltMover;
    private PlatformPositionReader posReader;

    private float currentAimTime;
    private float timeCounter;

    public override void Init()
    {
        posReader = new PlatformPositionReader(pltMover.HorizontalBoundaries, pltMover.VerticalBoundaries);
        ChooseAimTime();
    }

    private void ChooseAimTime()
    {
        currentAimTime = Random.Range(AimTime.x, AimTime.y);
        timeCounter = 0f;
    }

    private void Shoot()
    {
        if (Main.activeState.Equals(GameState.Stop)) return;

        PlatformPos currentPos = posReader.GetPosition(Position2D);
        int currentMapState = MapStateAccessor.Instance.GetActiveState();
        DirectionVector shootVector = AIDatabase.Instance.GetShot(currentPos, currentMapState);

        if (shootVector.magnitudeOfDir > Mathf.Epsilon) base.Shoot(shootVector);
    }

    private void Update()
    {
        if (timeCounter < currentAimTime)
            timeCounter += Time.deltaTime;
        else
        {
            Shoot();
            ChooseAimTime();
        }
    }
}