using UnityEngine;
using System.Collections;

public class Rotate : MonoBehaviour 
{
    [SerializeField] private float RotationSpeed;
    [HideInInspector] public bool rotate = false;

    private int rotationDirection = 1;

    public void SetRotationDirection(bool clockwise)
    {
        rotationDirection = clockwise ? 1 : -1;
    }

    public bool IsRotatingClockwise()
    {
        return rotationDirection == 1;
    }

    private void OnDisable()
    {
        transform.localEulerAngles = Vector3.zero;
    }

	private void Update () 
    {
        if (!rotate) return;

        transform.Rotate( rotationDirection * Vector3.forward * RotationSpeed * Time.deltaTime );
	}
}
