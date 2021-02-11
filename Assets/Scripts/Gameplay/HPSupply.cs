using UnityEngine;
using System.Collections;

namespace Assets.Scripts
{
    public class HPSupply : Supply
    {
        public float recoverVal = 50f;

        public override void OnSupply()
        {
            base.OnSupply();
            Player.Instance.Recover(recoverVal);
        }
    }
}