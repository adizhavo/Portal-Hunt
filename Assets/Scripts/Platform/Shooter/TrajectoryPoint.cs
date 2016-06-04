using UnityEngine;
using System.Collections;

public class TrajectoryPoint : MonoBehaviour {

    [SerializeField] private SpriteRenderer graphic;
    [SerializeField] private Color DisableColor;
    [SerializeField] private Color EnabledColor;

    public void SetState(bool active)
    {
        graphic.color = active ? EnabledColor : DisableColor;
    }
}
