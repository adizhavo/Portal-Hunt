using UnityEngine;
using System.Collections;

public class DragGizmos : MonoBehaviour 
{
    [SerializeField] private Transform FirstPos;
    [SerializeField] private Transform FingetPos;
    [SerializeField] private Transform Line;
    [SerializeField] private Transform DraggableZone;

    [SerializeField] private Color DisableColor;
    [SerializeField] private Color EnabledColor;

    private SpriteRenderer[] gizmosSprites;

    private void Awake()
    {
        gizmosSprites = GetComponentsInChildren<SpriteRenderer>();
    }

    public void PositionObject(Vector2 startPos, Vector2 fingerPos)
    {
        FirstPos.position = startPos;
        FingetPos.position = fingerPos;

        Line.position = (startPos + fingerPos) / 2;
        Line.localScale = new Vector3(Vector2.Distance(startPos, fingerPos), 1f, 1f);

        Quaternion rotation = Quaternion.LookRotation(fingerPos - startPos, transform.TransformDirection(Vector3.up));
        Line.rotation = new Quaternion(0, 0, rotation.z, rotation.w);
    }

    public void SetDraggableZone(float xSize, float ySize)
    {
        DraggableZone.localScale = new Vector3(xSize, ySize, 1f);
    }

    public void SetState(bool enable)
    {
        for (int sp = 0; sp < gizmosSprites.Length; sp ++)
            gizmosSprites[sp].color = enable ? EnabledColor : DisableColor;
    }

    public void Enable()
    {
        DraggableZone.gameObject.SetActive(true);
    }

    public void Release()
    {
        DraggableZone.gameObject.SetActive(false);
        gameObject.SetActive(false);
    }
}
