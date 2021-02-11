using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoSupply : Supply
{
    public int ammoSupply = 100;

    public override void OnSupply()
    {
        base.OnSupply();
        Player.Instance.ammo += ammoSupply;
        Player.Instance.gunStatus = 1;
    }
}
