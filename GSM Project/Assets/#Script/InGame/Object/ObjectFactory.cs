using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObjectFactory : MonoBehaviour
{

    int wave;
    float makeTime;
    float tempTime = 0;
    List<int> EnemyList;
    public Transform[] position;
    public GameObject enemyObject;
    public GameObject item;
    int k;
    // Start is called before the first frame update
    void OnEnable()
    {
        EnemyList = new List<int>();
        wave = GameManager.Instance._wave;
        int[] EnemyNumber = new int[11];
        switch (wave)
        {
            case 1:
                makeTime = 4;
                EnemyNumber[0] = 3;
                EnemyNumber[1] = 2;   //25sec
                break;
            case 2:
                makeTime = 3.8f;
                EnemyNumber[0] = 2;
                EnemyNumber[1] = 2;
                EnemyNumber[2] = 2;   //30sec
                break;
            case 3:
                makeTime = 3.8f;
                EnemyNumber[0] = 2;
                EnemyNumber[1] = 1;
                EnemyNumber[2] = 2;
                EnemyNumber[3] = 1;   //24sec
                break;
            case 4:
                makeTime = 3.6f;
                EnemyNumber[1] = 2;
                EnemyNumber[2] = 2;
                EnemyNumber[3] = 2;   //22.5sec
                break;
            case 5:
                makeTime = 3.5f;
                EnemyNumber[1] = 1;
                EnemyNumber[2] = 1;
                EnemyNumber[3] = 2;
                EnemyNumber[4] = 3;
                EnemyNumber[5] = 1;   //30.1sec
                break;
            case 6:
                makeTime = 3.5f;
                EnemyNumber[4] = 6;
                EnemyNumber[5] = 3;   //32.8sec
                break;
            case 7:
                makeTime = 3.5f;
                EnemyNumber[4] = 2;
                EnemyNumber[6] = 4;
                EnemyNumber[7] = 1;   //24sec
                break;
            case 8:
                makeTime = 3.5f;
                EnemyNumber[4] = 3;
                EnemyNumber[5] = 1;
                EnemyNumber[6] = 3;
                EnemyNumber[7] = 1;   //26.6sec
                break;
            case 9:
                makeTime = 3.5f;
                EnemyNumber[0] = 2;
                EnemyNumber[4] = 2;
                EnemyNumber[1] = 3;   
                EnemyNumber[7] = 3;   //33.3sec
                break;
            case 10:
                makeTime = 3.2f;
                EnemyNumber[2] = 3;
                EnemyNumber[6] = 4;   //24.5sec
                EnemyNumber[9] = 1;
                break;
            case 11:
                makeTime = 3.2f;
                EnemyNumber[6] = 5;
                EnemyNumber[8] = 7;   //36.3sec
                break;
            case 12:
                makeTime = 3.2f;
                EnemyNumber[4] = 6;
                EnemyNumber[5] = 3;
                EnemyNumber[7] = 3;   //36.3sec
                break;
            case 13:
                makeTime = 3.1f;
                EnemyNumber[10] = 1;
                EnemyNumber[7] = 2;
                EnemyNumber[8] = 5;
                EnemyNumber[0] = 3;   //32sec
                break;
            case 14:
                makeTime = 3.1f;
                EnemyNumber[9] = 1;
                EnemyNumber[7] = 3;
                EnemyNumber[4] = 2;
                EnemyNumber[8] = 5;
                EnemyNumber[0] = 1;   //34.1sec
                break;
            case 15:
                makeTime = 3f;
                EnemyNumber[9] = 1;
                EnemyNumber[3] = 3;
                EnemyNumber[7] = 3;
                EnemyNumber[8] = 4;
                EnemyNumber[0] = 1;    //33sec
                break;
            case 16:
                makeTime = 2.8f;
                EnemyNumber[9] = 1;
                EnemyNumber[5] = 2;
                EnemyNumber[6] = 5;
                EnemyNumber[8] = 4;   //37.7sec
                EnemyNumber[0] = 2;
                break;
            case 17:
                makeTime = 2.7f;
                EnemyNumber[9] = 1;
                EnemyNumber[3] = 3;
                EnemyNumber[7] = 3;
                EnemyNumber[8] = 4;   //28sec
                break;
            case 18:
                makeTime = 2.6f;
                EnemyNumber[9] = 1;
                EnemyNumber[4] = 5;
                EnemyNumber[7] = 5;   //27sec
                break;
            case 19:
                makeTime = 2.5f;
                EnemyNumber[10] = 1;
                EnemyNumber[9] = 1;
                EnemyNumber[8] = 5;
                EnemyNumber[2] = 5;   //30sec
                break;
            default:
                makeTime = 2.3f;
                EnemyNumber[10] = (int)(wave * 0.05f);
                EnemyNumber[9] = (int)(wave * 0.1f);
                EnemyNumber[4] = Random.Range(2, 4);
                EnemyNumber[5] = Random.Range(0, 2);
                EnemyNumber[6] = Random.Range(0, 5);
                EnemyNumber[7] = Random.Range(2, 4);
                EnemyNumber[8] = Random.Range(4, 5);
                break;
        }

        for(int i = 0; i<11; i++) {
            for(int j = 0; j<EnemyNumber[i]; j++)
                EnemyList.Add(i);
        }

        ShuffleList(EnemyList);

        k = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if(Random.Range(0, 1000) == 5) Instantiate(item, position[Random.Range(0, position.Length)].position, Quaternion.identity, GameObject.Find("EnemyGroup").transform);

        if (tempTime <= 0)
        {
            GameObject myEnemy = Instantiate(enemyObject, position[Random.Range(0, position.Length)].position, Quaternion.identity, GameObject.Find("EnemyGroup").transform);
            myEnemy.GetComponent<Enemy>().EnemySetting(EnemyList[k]);
            k++;
            tempTime = makeTime;
        }
        tempTime -= Time.deltaTime;
    }

    public void ShuffleList(List<int> list)
    {
        int random1;
        int random2;

        int tmp;

        for (int index = 0; index < list.Count; ++index)
        {
            random1 = UnityEngine.Random.Range(0, list.Count);
            random2 = UnityEngine.Random.Range(0, list.Count);

            tmp = list[random1];
            list[random1] = list[random2];
            list[random2] = tmp;
        }
    }

}
