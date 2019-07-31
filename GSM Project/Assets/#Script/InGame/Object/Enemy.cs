using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : ObjectScript
{
    public Transform HPBar;
    public float maxHP;
    public float HP;
    int power;
    int index = 0;
    public bool bDie = false;
    float tempTime = 1.5f;

    public Sprite[] Monster;
    public Sprite[] MonsterDead;
    public GameObject Red;
    public GameObject blue;
    public Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = Monster[index];

        base.Start();
    }

    // Update is called once per frame
    void Update()
    {
        if (!bDie)
        {
            HPBar.localScale = new Vector2(HP / maxHP, 1);
            if (HPBar.localScale.x <= 0)
            {
                HPBar.localScale = new Vector2(0, 0);
                bDie = true;
            }
        }
        else if(tempTime == 1.5f)
        {   
            transform.GetChild(0).GetComponent<BoxCollider2D>().enabled = false;
            gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = MonsterDead[index];
            Red.SetActive(false);
            blue.SetActive(false);
            animator.SetBool("bDie", true);
            tempTime -= Time.deltaTime;
        }
        else
        {
            tempTime -= Time.deltaTime;
            if (tempTime <= 0) Destroy(gameObject);
        }
        base.Update();
    }

    public void EnemySetting(int index)
    {
        this.index = index;
        switch (index)
        {                                   //WAVE 1~20 ENEMY
            case 0:
                maxHP = 70;
                power = 15;
                speed = 0.6f;               //제일 기본적. 속도 중간 약함
                break;
            case 1:
                maxHP = 50;
                power = 30;                //속도와 파워는 있지만 체력이 약함
                speed = 0.6f;
                break;
            case 2:
                maxHP = 40;
                power = 25;                //속도는 있지만 파워와 체력이 약함
                speed = 0.7f;
                break;
            case 3:
                maxHP = 120;
                power = 50;                 //속도는 느리지만 체력과 파워가 높음
                speed = 0.4f;
                break;

            //WAVE 5~20 ENEMY
            case 4:
                maxHP = 10;
                power = 30;                 //속도는 매우 빠르지만 체력이 약함
                speed = 4;
                break;
            case 5:
                maxHP = 40;
                power = 10;                 //속도는 매우매우 빠르고 체력도 쎔. 거의 막기 힘듦
                speed = 7;
                break;
            case 6:
                maxHP = 100;
                power = 20;                 // 속도도 있는데 체력도 있음. 파워는 약함
                speed = 1f;
                break;
            case 7:
                maxHP = 130;
                power = 50;                 //느리지만 힘도쎄고 체력도 있음
                speed = 0.4f;
                break;

            //WAVVE 10~20 ENEMY
            case 8:
                maxHP = 110;
                power = 50;                 //표준적으로 쎔
                speed = 0.8f;
                break;
            case 9:
                maxHP = 130;
                power = 150;                //데드락. 보스 중 한 명 
                speed = 0.3f;
                Red.SetActive(true);
                break;
            case 10:
                maxHP = 230;
                power = 150;                //블루스크린. 보스 중 한 명
                speed = 0.25f;
                blue.SetActive(true);
                break;
        }
        power += 10;
        speed += 0.007f * GameManager.Instance._wave + 0.3f;
        HP = maxHP;
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Tower"))
        {
            collision.GetComponent<Tower>().HP -= power;
            bDie = true;
        }else if (collision.CompareTag("Shield"))
        {
            bDie = true;
        }
    }
}
