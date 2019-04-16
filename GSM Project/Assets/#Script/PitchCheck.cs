using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PitchCheck : MonoBehaviour
{
    int count = 0;
    float pitchSum = 0;
    float time = 0;
    bool isStart;
    bool isDone = true;
    public SoundAnalyze sound;

    public Text text;
    public AudioSource beep;
    // Update is called once per frame
    void Update()
    {
        if (!isDone)
        {
            if (sound.isCheck) isStart = true;
            if (isStart)
            {
                time += Time.deltaTime;
                count++;
                pitchSum += sound.PitchValue;
                if (time >= 3)
                {
                    PlayerPrefs.SetFloat("Pitch", pitchSum / count);
                    Debug.Log(pitchSum / count);
                    beep.Play();
                    text.text = "완료되었습니다.";
                    gameObject.GetComponent<Animator>().SetBool("IsDone", true);
                    isDone = true;
                }
                if (!sound.isCheck)
                {
                    isStart = false;
                    text.text = "소리를 좀 더 크게 해주세요.";
                    time = 0;
                    pitchSum = 0;
                    count = 0;
                }
                else
                {
                    text.text = "음역대를 체크하는 중입니다...";
                }
            }
            else
            {
                time = 0;
                pitchSum = 0;
                count = 0;
            }
        }
        
    }

    public void CheckStart()
    {
        SoundManager.instance.PlayPitchBtn();

        isDone = false;
        gameObject.GetComponent<Animator>().SetBool("IsDone", false);
    }

    public void Exit()
    {
        SceneManager.LoadScene("Main");
    }
}
