using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ObjectScript : MonoBehaviour
{
    GameObject tower;
    Vector2 dir;
    public float speed;
    // Start is called before the first frame update
    public void Start()
    {
        tower = GameObject.Find("Tower");
        dir = transform.position - tower.transform.position;
        dir = dir.normalized;
    }

    // Update is called once per frame
    public void Update()
    {
        transform.Translate(dir * -speed * Time.deltaTime);
    }
}
