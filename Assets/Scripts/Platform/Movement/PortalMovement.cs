using UnityEngine;
using System.Collections;

public class PortalMovement : MonoBehaviour 
{    
    [SerializeField] private float movementAmplitude;
    [SerializeField] private float movementFrequency;
    [SerializeField] private float movementSpeed;

    private float startXPos;

    private void Start()
    {
        startXPos = transform.position.x;
    }

    private void Update()
    {
        float xPos = movementAmplitude * Mathf.Sin(Time.timeSinceLevelLoad * movementFrequency);
        transform.position = Vector3.Lerp(transform.position, new Vector3(startXPos + xPos, transform.position.y, transform.position.z), movementSpeed * Time.deltaTime);
    }
}