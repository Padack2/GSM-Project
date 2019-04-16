using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Bullet : MonoBehaviour
{
    public Sprite[] bulletImg;
    public float power;

    // Start is called before the first frame update
    public void Start()
    {
        GetComponent<SpriteRenderer>().sprite = bulletImg[Random.Range(0, bulletImg.Length)];

        SoundManager.instance.PlayBullet();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.up * Time.deltaTime * 20);

        if (transform.position.y > 10) Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Item"))
        {
            GameManager.Instance.Explosion(transform.position, power);
            Destroy(gameObject);
        }
        else if (collision.CompareTag("Enemy"))
        {
            if (collision.gameObject.GetComponentInParent<Enemy>().HP - power * 2 <= 0)
            {
                GameManager.Instance._byte += Random.Range(1, 3);
                GameManager.Instance._score += (int)collision.gameObject.GetComponentInParent<Enemy>().maxHP;
            }
            collision.gameObject.GetComponentInParent<Enemy>().HP -= (power * 2);
            GameManager.Instance.Explosion(transform.position, power);
            Destroy(gameObject);
        }
    }
}
