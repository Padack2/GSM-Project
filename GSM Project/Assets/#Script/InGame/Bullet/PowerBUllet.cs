using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerBUllet : Bullet
{
    // Start is called before the first frame update
    private void Start()
    {
        power = GameObject.Find("Tower").GetComponent<Tower>().power;
        power = 10;
        transform.localScale = new Vector3(0.5f, 0.5f);
        TrailRenderer a = GetComponentInChildren<TrailRenderer>();
        a.startWidth = 2;
        base.Start();
    }


}
