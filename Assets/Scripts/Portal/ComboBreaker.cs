using UnityEngine;
using System.Collections;

public class ComboBreaker : StopObject
{
    public delegate void FinishCombo(PlayerType player);
    public static event FinishCombo OnComboFinished;

    protected override void OnCollisionEnter2D(Collision2D coll)
    {
        base.OnCollisionEnter2D(coll);

        Bullet bulletCol = coll.transform.GetComponent<Bullet>();

        if (OnComboFinished != null && bulletCol != null)
        {
            OnComboFinished(bulletCol.Type);
        }
    }
}
