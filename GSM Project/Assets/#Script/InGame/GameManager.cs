using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    static GameManager instance;
    public static GameManager Instance
    {
        get
        {
            return instance;
        }
    }

    private void Awake()
    {
        instance = this;
    }

    [Header("Text")]
    public Text scoreText;
    public Text waveText;
    public Text ByteText;
    public GameObject waveWaring;
    [Space()]
    [Header("Play")]
    public SpriteRenderer player;
    public Sprite[] playerImg;
    public GameObject obj;
    public float WaveTime;
    public GameObject[] falseObejct;
    public GameObject GameOverImg;
    public GameObject result;
    public Text scoreResult;
    public Text byteResult;
    public GameObject best;
    public GameObject[] explosion;
    public GameObject pause;

    public int _wave = 0;
    public int _byte = 0;
    public int _score = 0;
    public float tempTime = 4;

    public bool bChangeWave = false;
    public bool GameOver= false;

    int explosionIndex;
    // Start is called before the first frame update
    void Start()
    {
        int upPoint = PlayerPrefs.GetInt("HP") + PlayerPrefs.GetInt("Power") + PlayerPrefs.GetInt("Assistant");

        player.sprite = playerImg[upPoint / 3];
    }

    // Update is called once per frame
    void Update()
    {
        if (!GameOver)
        {
            UpdateUI();
            if (bChangeWave)
            {
                _wave++;
                waveWaring.SetActive(false);
                waveWaring.SetActive(true);
                waveWaring.GetComponent<Text>().text = "WAVE " + _wave.ToString("D2");
                bChangeWave = false;
                tempTime = WaveTime + 10;
            }
            else
            {
                tempTime -= Time.deltaTime;
                if (tempTime <= 0) bChangeWave = true;
                else if (tempTime <= 7)
                {
                    obj.SetActive(false);
                }
                else if (tempTime <= WaveTime + 7)
                {
                    obj.SetActive(true);
                }
            }
        }
        else if(_wave == 0)
        {
            tempTime -= Time.deltaTime;
            if (tempTime <= 0)
            {
                bChangeWave = true;
                GameOver = false;
            }
        }
        else
        {
            if (!GameOverImg.activeSelf) {
                GameOverImg.SetActive(true);
                for(int i = 0; i<falseObejct.Length; i++)
                {
                    falseObejct[i].SetActive(false);
                }
                tempTime = 3.5f;
            }else if (!result.activeSelf)
            {
                tempTime -= Time.deltaTime;
                if (tempTime <= 0)
                {
                    Result();
                }
            }
        }
        

    }

    void Result()
    {
        result.SetActive(true);
        byteResult.text = "byte    " + _byte.ToString("D5");
        scoreResult.text = "score   " + _score.ToString("D5");
        PlayerPrefs.SetInt("Byte", PlayerPrefs.GetInt("Byte") + _byte);
        PlayerPrefs.SetInt("Project", PlayerPrefs.GetInt("Project") + 1);

        if (PlayerPrefs.GetInt("Wave") <= _wave)
            PlayerPrefs.SetInt("Wave", _wave);

        //1-3위 순위 정보 저장
        if (PlayerPrefs.GetInt("First") <= _score)
        {
            PlayerPrefs.SetInt("Third", PlayerPrefs.GetInt("Second"));
            PlayerPrefs.SetInt("Second", PlayerPrefs.GetInt("First"));

            PlayerPrefs.SetInt("First", _score);
            best.SetActive(true);
        }
        else if (PlayerPrefs.GetInt("Second") <= _score)
        {
            PlayerPrefs.SetInt("Third", PlayerPrefs.GetInt("Second"));
            PlayerPrefs.SetInt("Second", _score);
        }
        else if (PlayerPrefs.GetInt("Third") <= _score)
        {
            PlayerPrefs.SetInt("Third", _score);
        }
    }

    void UpdateUI()
    {
        scoreText.text = "SCORE " + _score.ToString("D5");
        waveText.text = "wave " + _wave.ToString("D2");
        ByteText.text = "BYTE " + _byte.ToString("D3");
    }

    public void goMain()
    {
        Loading.Instance.Move("Main");
    }

    public void Resume()
    {
        pause.SetActive(false);
        Time.timeScale = 1;
    }

    public void Pause()
    {
        pause.SetActive(true);
        Time.timeScale = 0;
    }

    public void Explosion(Vector2 pos, float power)
    {
        SoundManager.instance.PlayExplosion();

        explosion[explosionIndex].transform.localPosition = pos;
        explosion[explosionIndex].transform.localScale = new Vector2((power>=10? 10:power) * 0.5992f, (power >= 10? 10 : power) * 0.5992f);
        explosion[explosionIndex].SetActive(false);
        explosion[explosionIndex].SetActive(true);

        explosionIndex = explosionIndex + 1 >= 10 ? 0 : explosionIndex + 1;
    }

}
