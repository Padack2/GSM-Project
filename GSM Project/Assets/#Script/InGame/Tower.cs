using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    public SoundAnalyze sound;

    //information
    public Transform HPBar;
    float maxHP = 200;
    public float HP;

    //Basic values
    public float power;
    float time;
    float dbSum;
    float pitchSum;
    int count = 0;
    int powerLevel;

    //Use Item
    public enum BulletItem
    {
        BASIC_BULLET, POWER_BULLET, WEAKNESS_BULLET, SUPER_BULLET
    }
    public BulletItem bullet = BulletItem.BASIC_BULLET;
    public GameObject[] bulletObject;
    public Sprite[] bulletImg;

    // Start is called before the first frame update
    void Start()
    {
        switch (PlayerPrefs.GetInt("HP"))
        {
            case 1: maxHP += 10;
                break;
            case 2:
                maxHP += 20;
                break;
            case 3:
                maxHP += 35;
                break;
            case 4:
                maxHP += 50;
                break;
            case 5:
                maxHP += 70;
                break;
            case 6:
                maxHP += 90;
                break;
            case 7:
                maxHP += 120;
                break;
        }


        HP = maxHP;
        sound = GameObject.Find("Microphone").GetComponent<SoundAnalyze>();
        powerLevel = PlayerPrefs.GetInt("Power");
        powerLevel = powerLevel == 7 ? 8 : powerLevel;
    }

    // Update is called once per frame
    void Update()
    {
        if (!GameManager.Instance.GameOver)
        {
            if (HP >= maxHP) HP = maxHP;
            HPBar.localScale = new Vector2(HP / maxHP * 5, 1);
            if (HPBar.localScale.x <= 0)
            {
                GameManager.Instance.GameOver = true;
                HPBar.localScale = new Vector2(0, 0);
            }
            if (bullet != BulletItem.WEAKNESS_BULLET)
            {
                if (sound.isTalk)
                {
                    count++;
                    time += Time.deltaTime;
                    dbSum += sound.DbValue;
                    pitchSum += sound.PitchValue;
                }
                else
                {
                    if (time >= 0.05f)
                    {
                        power = dbSum / count + powerLevel * 0.5f;
                        Shoot();
                    }
                    dbSum = 0;
                    count = 0;
                    pitchSum = 0;
                    time = 0;
                }
            }
            else
            {
                time += Time.deltaTime;
                if (time >= 0.7f)
                {
                    Shoot();
                    time = 0;
                }
            }
        }
        
    }

    void Shoot()
    {
        float angle = PlayerPrefs.GetFloat("Pitch") - pitchSum / count;
        if (bullet == BulletItem.WEAKNESS_BULLET) angle = sound.PitchValue != 0? PlayerPrefs.GetFloat("Pitch") - sound.PitchValue : 0;

        if (angle >= 500) angle = 500;
        else if (angle <= -500) angle = -500;
        angle *= 0.1f;
        GameObject myBullet;

        if(!float.IsNaN(angle) || angle == 0)
        {
            Debug.Log((int)bullet + ", " + angle);
            myBullet = Instantiate(bulletObject[(int)bullet], transform.position, Quaternion.Euler(0, 0, angle), transform);
            
        }
        else
            myBullet = Instantiate(bulletObject[(int)bullet], transform.position, Quaternion.Euler(0, 0, 0), transform);

    }
}
