using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{
    public static UIController _ui;
    public static bool paused;
    public static GameObject ierarhy;
    Text text;
    GameObject menu;
    GameObject globalCaption;
    Slider slider;
    void Awake()
    {
        paused = false;
        _ui = this;
        text = GameObject.Find("CurrentEvent").GetComponent<Text>();
        menu = GameObject.Find("Menu");
        slider = GameObject.Find("Slider").GetComponent<Slider>();
        globalCaption = GameObject.Find("Sense");
        ierarhy = GameObject.Find("Ierarhy");
        menu.SetActive(false);
        globalCaption.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void TextRelease(string s)
	{
        if (text.text == s)
            text.text = "";
	}
    public void ChangeGlobalCaption(string s)
	{
        globalCaption.transform.GetChild(0).gameObject.GetComponent<Text>().text = s;
	}
	public void TextChange(string s)
	{
        text.text = s;
	}
    public void OpenTome(int num)
	{
        PlayerPrefs.SetInt("tome", num);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void SwitchGlobalCaption()
    {
        if (!menu.activeSelf) {
            if (globalCaption.activeSelf) {
                paused = false;
                Time.timeScale = 1;
                slider.enabled = true;
                globalCaption.SetActive(false);
            } else {
                paused = true;
                slider.enabled = false;
                Time.timeScale = 0;
                globalCaption.SetActive(true);
            }
        }
    }
    public void SwitchMenu()
	{
        if (!globalCaption.activeSelf) {
            if (menu.activeSelf) {
                paused = false;
                slider.enabled = true;
                Time.timeScale = 1;
                menu.SetActive(false);
            } else {
                paused = true;
                Time.timeScale = 0;
                slider.enabled = false;
                menu.SetActive(true);
            }
        }
	}
}
