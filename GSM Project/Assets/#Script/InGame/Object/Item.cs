using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : ObjectScript
{
    public Sprite[] itemImg;
    public bool isPop;
    float time = 1;
    enum ItemCode{
        ITEM_BITA = 0, ITEM_COFFEE, ITEM_HAMBURGER, ITEM_MONEY, ITEM_REDBULL, ITEM_SHIELD
    }
    int index;

    private void Start()
    {
        index = Random.Range(0, itemImg.Length);
        gameObject.GetComponent<SpriteRenderer>().sprite = itemImg[index];
        base.Start();
    }

    private void Update()
    {
        if (isPop)
        {
            time -= Time.deltaTime;
            if (time < 0) Destroy(gameObject);
        }
        base.Update();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Bullet"))
        {
            isPop = true;
            SoundManager.instance.PlayBubble();
            GetComponent<Animator>().enabled = true;
        }else if (collision.CompareTag("Tower"))
        {
            isPop = true;
            SoundManager.instance.PlayBubble();
            GetComponent<Animator>().enabled = true;
            switch ((ItemCode)index)
            {
                case ItemCode.ITEM_BITA:
                    ItemController.Instance.ChangeHP(20);
                    break;
                case ItemCode.ITEM_COFFEE:
                    ItemController.Instance.ChangeHP(10);
                    break;
                case ItemCode.ITEM_HAMBURGER:
                    ItemController.Instance.ChangeBullet(2);
                    break;
                case ItemCode.ITEM_MONEY:
                    ItemController.Instance.ChangeBullet(1);
                    break;
                case ItemCode.ITEM_REDBULL:
                    ItemController.Instance.ChangeBullet(0);
                    break;
                case ItemCode.ITEM_SHIELD:
                    ItemController.Instance.Shield();
                    break;
            }
        }
        
    }

}
