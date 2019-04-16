using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainManager : MonoBehaviour
{

    [Header("Rank")]
    public Text first;
    public Text second;
    public Text third;
    public Text project;
    public Text wave;

    [Space()]
    [Header("Enhance")]
    public Sprite point;
    public GameObject[] upgrade;
    public Button[] upBtn;
    public Text[] price;
    public Sprite[] playerImg;
    public Image player;

    [Space()]
    [Header("Base")]
    public Text _byte;
    public Animator animator;
    public Image RnEBtn;
    public Sprite[] btnImg;


    // Start is called before the first frame update
    void Start()
    {        
        first.text = PlayerPrefs.GetInt("First").ToString();
        second.text = PlayerPrefs.GetInt("Second").ToString();
        third.text = PlayerPrefs.GetInt("Third").ToString();
        project.text = "터진 프로젝트 수 : " + PlayerPrefs.GetInt("Project").ToString();
        wave.text = "최대 웨이브 수 : " + PlayerPrefs.GetInt("Wave");

        Setting();
    }

    void Setting()
    {
        for(int i = 0; i <3; i++)
        {
            Image[] point = upgrade[i].GetComponentsInChildren<Image>();
            for(int j = 0; j< (i==0?PlayerPrefs.GetInt("HP"):i==1?PlayerPrefs.GetInt("Power"):PlayerPrefs.GetInt("Assistant")); j++)
            {
                point[j].sprite = this.point;
            }
            int level = i == 0 ? PlayerPrefs.GetInt("HP") : i == 1 ? PlayerPrefs.GetInt("Power") : PlayerPrefs.GetInt("Assistant");
            int price = 0;

            switch (level)
            {
                case 0: this.price[i].text = "200"; price = 200; break;
                case 1: this.price[i].text = "500"; price = 500; break;
                case 2: this.price[i].text = "1000"; price = 1000; break;
                case 3: this.price[i].text = "1500"; price = 1500; break;
                case 4: this.price[i].text = "2000"; price = 2000; break;
                case 5: this.price[i].text = "3500"; price = 3500; break;
                case 6: this.price[i].text = "5000"; price = 5000; break;
                case 7: this.price[i].text = "X"; price = int.MaxValue; break;
            }

            if (PlayerPrefs.GetInt("Byte") <= price)
            {
                upBtn[i].interactable = false;
            }
            int temp = i;
            Button button = upBtn[temp];
            button.onClick.RemoveAllListeners();
            button.onClick.AddListener(() => Upgrade(price, temp == 0 ? "HP" : temp == 1 ? "Power" : "Assistant"));
        }
        _byte.text = PlayerPrefs.GetInt("Byte").ToString();
        int upPoint = PlayerPrefs.GetInt("HP") + PlayerPrefs.GetInt("Power") + PlayerPrefs.GetInt("Assistant");

        player.sprite = playerImg[upPoint / 3];
    }

    void Upgrade(int price, string kind)
    {
        SoundManager.instance.PlayMainBtn();
        PlayerPrefs.SetInt(kind, PlayerPrefs.GetInt(kind) + 1);
        PlayerPrefs.SetInt("Byte", PlayerPrefs.GetInt("Byte") - price);
        Setting();
    }

    public void RnEBtnFunction()
    {
        SoundManager.instance.PlayMainBtn();

        if(RnEBtn.sprite == btnImg[0])
        {
            RnEBtn.sprite = btnImg[1];
            animator.SetTrigger("Enhance");
        }
        else
        {
            RnEBtn.sprite = btnImg[0];
            animator.SetTrigger("Rank");
        }
    }

}
