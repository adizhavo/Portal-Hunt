using UnityEngine;
using System.Collections;

public class AnchorToUI : MonoBehaviour {

    [SerializeField] private Transform AnchoredTransform;

    private void Awake()
    {
        transform.position = AnchoredTransform.position;
    }

    #if UNITY_EDITOR

    private void LateUpdate()
    {
        Awake();
    }

    #endif
}
