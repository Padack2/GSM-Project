using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Assistant : MonoBehaviour
{
    public GameObject firePosition;
    public Sprite[] GunImg;
    public SpriteRenderer gun;
    public GameObject bullet;

    float makeTime = 7;
    float time = 0;
    // Start is called before the first frame update
    void Start()
    {
        int level = PlayerPrefs.GetInt("Assistant");
        if (level == 0) Destroy(gameObject);
        else gun.sprite = GunImg[level - 1];
        makeTime -= (level - 1) * 0.3f;
    }

    // Update is called once per frame
    void Update()
    {
        if (time <= 0)
        {
            time = makeTime;
            Instantiate(bullet, firePosition.transform.position, Quaternion.identity, gameObject.transform);
        }
        else
        {
            time -= Time.deltaTime;
        }
    }
}
