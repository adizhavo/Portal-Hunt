using UnityEngine;
using System.Collections;

public class OffScreenUI : MonoBehaviour {

    [SerializeField] private Transform PivotUI;
    private float yPos;

    private void Start()
    {
        // Anchors only vetical position
        yPos = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width / 2, Screen.height, 0f)).y;
    }

    public void LateUpdate()
    {
        PositionUI();
        #if UNITY_EDITOR

            Start();

        #endif
    }

    private void PositionUI()
    {
        PivotUI.position = new Vector3(transform.position.x, yPos, transform.position.z);
        SetUIState(yPos);
    }

    private void SetUIState(float pivotY)
    {
        bool state = pivotY < transform.position.y ? true : false;
        PivotUI.gameObject.SetActive(state);
    }
}
