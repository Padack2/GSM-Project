using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaknessBullet : Bullet
{

    private void Start()
    {
        power = GameObject.Find("Tower").GetComponent<Tower>().power;
        power = 5;
        transform.localScale = new Vector3(power * 0.028f, power * 0.028f);
        TrailRenderer a = GetComponentInChildren<TrailRenderer>();
        a.startWidth = 0.5f;
        base.Start();
    }

    

}
