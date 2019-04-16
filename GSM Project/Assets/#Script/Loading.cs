using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Loading : MonoBehaviour
{
    public GameObject loading_img;

    private static Loading instance;
    public static Loading Instance
    {
        get
        {
            return instance;
        }
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    public void Move(string SceneName)
    {
        Time.timeScale = 1;
        StartCoroutine(LoadScene(SceneName));
    }

    IEnumerator LoadScene(string SceneName)
    {
        loading_img.SetActive(true);
        yield return new WaitForSeconds(1.0f);
        SceneManager.LoadScene(SceneName);
    }
}
