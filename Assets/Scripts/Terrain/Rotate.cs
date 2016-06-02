using UnityEngine;
using System.Collections;

public class Rotate : MonoBehaviour 
{
    [SerializeField] private float RotationSpeed;

	private void Update () 
    {
        transform.Rotate( Vector3.forward * RotationSpeed * Time.deltaTime );
	}
}
