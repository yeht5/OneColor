using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UserGUI : MonoBehaviour {
    private IUserAction action;
    public Text colortext;//显示所选颜色

	void Start () {
        action = Director.getInstance().currentSceneControl as IUserAction;
	}
    void OnGUI()
    {
        if (GUI.Button(new Rect(40, Screen.height - 60, 80, 40), "Red"))
        {
            action.selectColor(Color.red);
            colortext.text = "Color : Red";
        }
        if (GUI.Button(new Rect(140, Screen.height - 60, 80, 40), "Green"))
        {
            action.selectColor(Color.green);
            colortext.text = "Color : Green";
        }
        if (GUI.Button(new Rect(240, Screen.height - 60, 80, 40), "Blue"))
        {
            action.selectColor(Color.blue);
            colortext.text = "Color : Blue";
        }
        if (GUI.Button(new Rect(340, Screen.height - 60, 80, 40), "Yellow"))
        {
            action.selectColor(Color.yellow);
            colortext.text = "Color : Yellow";
        }
    }
}
