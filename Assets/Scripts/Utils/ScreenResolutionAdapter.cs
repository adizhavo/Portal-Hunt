using UnityEngine;
using System.Collections;

public class ScreenResolutionAdapter : MonoBehaviour {

    [SerializeField] private Transform rightAnchor;
    [SerializeField] private Transform leftAnchor;

    // Change the value if you want to change the ration on the map
    private readonly float calibratedCameraValue = 5f;
    private readonly float calibratedHorizontalValue = 2 * 8.908629f;

	private void Start () 
    {
        float ratio = calibratedCameraValue / calibratedHorizontalValue;
        float newSize = 2 * calibratedCameraValue - Mathf.Abs(rightAnchor.position.x - leftAnchor.position.x) * ratio;
        Camera.main.orthographicSize = newSize;
	}
}
