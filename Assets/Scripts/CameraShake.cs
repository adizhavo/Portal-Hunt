using UnityEngine;
using System.Collections;

public class CameraShake : MonoBehaviour
{
    [SerializeField] private float ShakeIntensity;    
    [SerializeField] private float ShakeDecay;

    private float currentIntensity;
    private bool Shaking = false; 

    private Vector3 OriginalPos;
    private Quaternion OriginalRot;


    private static CameraShake instance;
    public static CameraShake Instance
    {
        get { return instance; }
    }

    private void Awake()
    {
        instance = this;
    }

    private void Update () 
    {
        if (!Shaking) return;

        if(currentIntensity > 0)
        {
            transform.position = OriginalPos + Random.insideUnitSphere * ShakeIntensity;
            transform.rotation = new Quaternion(OriginalRot.x + Random.Range(-currentIntensity, currentIntensity) * 0.2f,
                OriginalRot.y + Random.Range(-currentIntensity, currentIntensity) * 0.2f,
                OriginalRot.z + Random.Range(-currentIntensity, currentIntensity) * 0.2f,
                OriginalRot.w + Random.Range(-currentIntensity, currentIntensity) * 0.2f);

            currentIntensity -= ShakeDecay;
        }
        else
            Shaking = false;  
    } 

    public void DoShake()
    {
        OriginalPos = transform.position;
        OriginalRot = transform.rotation;
        currentIntensity = ShakeIntensity;
        Shaking = true;
    }  
}