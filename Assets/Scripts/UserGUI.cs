using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UserGUI : MonoBehaviour {
    private IUserAction action;
	public Color color_1 = new Color(0.1176f, 0.1843f, 0.1215f);
	public Color color_2 = new Color(0.8627f, 0.2039f, 0.2823f);
	public Color color_3 = new Color(0.8509f, 0.6039f, 0.3019f);
	public Color color_4 = new Color(0.8196f, 0.6941f, 0.5333f);
    public Text colortext;//显示所选颜色

	void Start () {
        action = Director.getInstance().currentSceneControl as IUserAction;
	}
    void OnGUI()
    {
        if (GUI.Button(new Rect(40, Screen.height - 60, 80, 40), "Red"))
        {
            action.selectColor(color_1);
            colortext.text = "Color : Red";
        }
        if (GUI.Button(new Rect(140, Screen.height - 60, 80, 40), "Green"))
        {
			action.selectColor(color_2);
            colortext.text = "Color : Green";
        }
        if (GUI.Button(new Rect(240, Screen.height - 60, 80, 40), "Blue"))
        {
			action.selectColor(color_3);
            colortext.text = "Color : Blue";
        }
        if (GUI.Button(new Rect(340, Screen.height - 60, 80, 40), "Yellow"))
        {
			action.selectColor(color_4);
            colortext.text = "Color : Yellow";
		}
		if (GUI.Button(new Rect(440, Screen.height - 60, 80, 40), "Reset"))
		{
			colortext.text = "Reset";
		}
    }
}
