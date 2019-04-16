using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomingBullet : MonoBehaviour
{
    public GameObject[] EnemySet;
    public GameObject Target;

    public float bulletSpeed;

    private Vector2 deltaMove;
    public float time;
    protected float RotateSpeed = 5f; // 회전 스피드
    protected float Tracking = 0.01f; // 트래킹 간격      
    public Vector2 dir;

    //미사일 기능.
    public int bulletCount = 1;  //공격가능횟수 카운트.

    void Start()
    {
        StartCoroutine(Attack());
    }

    public IEnumerator Attack()
    {
        do
        {
            RotateSpeed = bulletSpeed * 0.1f;
            EnemySet = GameObject.FindGameObjectsWithTag("Enemy");


            //if (Target == null)
            Target = GameObject.FindGameObjectWithTag("GameManager");

            if (EnemySet != null)
            {
                if (Target.tag == "GameManager")
                {

                    for (int i = 0; i < EnemySet.Length; i++)
                    {
                        float dist = Vector2.Distance(gameObject.transform.position, EnemySet[i].transform.position);

                        if (dist <= Vector2.Distance(gameObject.transform.position, Target.transform.position))
                        {
                            Target = EnemySet[i];
                        }
                    }
                }

                //거리계산.
                float distance = Vector2.Distance(gameObject.transform.position, Target.transform.position);

                //이동.
                if (Target.tag != "GameManager")
                {
                    Vector3 temp = gameObject.transform.position;
                    if (distance > 0.1f)
                    {

                        //Walk();
                        transform.Translate(Vector2.up * bulletSpeed * Time.deltaTime);
                        //transform.Translate(dir.normalized * bulletSpeed * Time.deltaTime, Space.World);
                        temp = gameObject.transform.position;
                    }
                    else
                    {
                        if (Target.tag == "Enemy")
                        {
                            transform.position += new Vector3(dir.x, dir.y, 0) * 4;  //통과하는척하기.
                            bulletCount -= 1;  //카운트 뺴기.

                            //터뜨리기.
                            if(Target.GetComponent<Enemy>() != null)
                            Target.GetComponent<Enemy>().HP -= 10;
                        }
                    }
                }
                else
                {//타겟이 없으면
                    Destroy(gameObject);
                }

                //회전.
                if (Time.time - time >= Tracking) // 트래킹할 시간이 됐는지 체크한다.
                {
                    dir = Target.transform.position - transform.position; // 유도탄과 타겟 사이의 벡터값 구하기
                    float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg; // 2D 각도값 구하기
                    Quaternion tarRot = Quaternion.AngleAxis(angle - 90f, Vector3.forward); // 얻어진 2D 좌표계 각도를 Quaternion으로 변환한다.
                    transform.rotation = Quaternion.Slerp(transform.rotation, tarRot, RotateSpeed); // 목표 각도로 서서히 이동시킨다.
                    //transform.rotation = tarRot;

                    if (transform.rotation == tarRot) time = Time.time; // 목표각도까지 회전했으면 타이머를 리셋한다.
                }

                yield return new WaitForSeconds(0.01f);
            }
        } while (bulletCount > 0);

        Die();
    }

    public void Die()
    {
        GameManager.Instance.Explosion(gameObject.transform.position, 5);
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
            Die();
    }
}
