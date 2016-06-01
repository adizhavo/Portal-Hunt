using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class AnchorToUI : MonoBehaviour {

    [SerializeField] private Transform AnchoredTransform;

    private IEnumerator Start()
    {
        yield return new WaitForEndOfFrame();
        transform.position = AnchoredTransform.position;
    }

    #if UNITY_EDITOR

    private void LateUpdate()
    {
        transform.position = AnchoredTransform.position;
    }

    #endif
}
