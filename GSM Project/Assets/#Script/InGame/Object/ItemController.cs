using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemController : MonoBehaviour
{
    private static ItemController instance;
    public static ItemController Instance
    {
        get
        {
            return instance;
        }
    }

    public GameObject shield;
    Tower tower;
    float time;
    int index;
    bool isItem;

    void Awake()
    {
        instance = this;
        tower = GameObject.Find("Tower").GetComponent<Tower>();
    }

    void Update()
    {
        if (isItem)
        {
            time -= Time.deltaTime;
            if (time <= 0)
            {
                tower.bullet = Tower.BulletItem.BASIC_BULLET;
                isItem = false;
            }
        }
    }

    public void ChangeBullet(int a)
    {
        switch (a)
        {
            case 0:
                isItem = true;
                tower.bullet = Tower.BulletItem.SUPER_BULLET;
                time = 1; break;
            case 1:
                isItem = true;
                tower.bullet = Tower.BulletItem.POWER_BULLET;
                time = 5; break;
            case 2:
                isItem = true;
                tower.bullet = Tower.BulletItem.WEAKNESS_BULLET;
                time = 5; break;
        }
    }

    public void ChangeHP(int a)
    {

        tower.HP += a;
    }

    public void Shield()
    {
        shield.SetActive(false);
        shield.SetActive(true);
    }
}
