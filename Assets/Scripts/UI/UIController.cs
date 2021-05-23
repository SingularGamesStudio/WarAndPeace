using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public static UIController _ui;
    Text text;
    // Start is called before the first frame update
    void Start()
    {
        _ui = this;
        text = GameObject.Find("Text").GetComponent<Text>();
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
	public void TextChange(string s)
	{
        text.text = s;
	}
}
