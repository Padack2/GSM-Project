using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScenes : MonoBehaviour
{
    public GameObject Option;

    public void Change_InGame()
    {
        SoundManager.instance.PlayBtn();
        Loading.Instance.Move("InGame");
    }

    public void Change_Main()
    {
        Loading.Instance.Move("Main");
    }

    public void Change_PitchCheck()
    {
        Loading.Instance.Move("PitchCheck");
    }

    public void Open_Ontion()
    {
        SoundManager.instance.PlayMainBtn();
        Option.SetActive(true);
    }

    public void Close_Ontion()
    {
        SoundManager.instance.PlayMainBtn();
        Option.SetActive(false);
    }
}
