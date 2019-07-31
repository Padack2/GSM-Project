using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicBullet : Bullet
{

    private void Start()
    {
        power = GameObject.Find("Tower").GetComponent<Tower>().power;
        if (power >= 10) power = 10;
        transform.localScale = new Vector3(power * 0.028f, power * 0.028f);
        TrailRenderer a = GetComponentInChildren<TrailRenderer>();
        a.startWidth = power < 5 ? 0.5f : power < 12 ? 1 : 2;
        base.Start();

    }

}
