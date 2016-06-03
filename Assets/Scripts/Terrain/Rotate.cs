using UnityEngine;
using System.Collections;

public class Rotate : MonoBehaviour 
{
    [SerializeField] private float RotationSpeed;
    [HideInInspector] public bool rotate = false;

    private void OnDisable()
    {
        transform.localEulerAngles = Vector3.zero;
    }

	private void Update () 
    {
        if (!rotate) return;

        transform.Rotate( Vector3.forward * RotationSpeed * Time.deltaTime );
	}
}
