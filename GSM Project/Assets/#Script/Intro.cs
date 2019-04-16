using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;
using Spine;


public class Intro : MonoBehaviour
{
    SkeletonAnimation introAnim;
    string cur_animation;


    private void Start()
    {
        introAnim = GetComponent<SkeletonAnimation>();
        StartCoroutine(IntroAnimation());
        
    }

    IEnumerator IntroAnimation()
    {
        yield return new WaitForSeconds(2.9f);
        SetAnimation("loading", true);    }

    void SetAnimation(string name, bool loop)
    {
        if (name == cur_animation)
            return;
        else
        {
            introAnim.state.SetAnimation(0, name, loop);
            cur_animation = name;
        }
    }

    public void EnterGame()
    {
        SoundManager.instance.PlayBtn();

        if (PlayerPrefs.GetFloat("Pitch") == 0)
            Loading.Instance.Move("PitchCheck");
        else
            Loading.Instance.Move("Main");
    }
}
