using UnityEngine;
using System.Collections;

public class AnchorToUI : MonoBehaviour {

    [SerializeField] private Transform AnchoredTransform;

    private void Awake()
    {
        transform.GetComponent<Rigidbody2D>().position = AnchoredTransform.position;
    }

    #if UNITY_EDITOR

    private void LateUpdate()
    {
        Awake();
    }

    #endif
}
