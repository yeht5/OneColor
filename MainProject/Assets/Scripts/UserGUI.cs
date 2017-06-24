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
    public Texture buttonTexture_Red;
    public Texture buttonTexture_Green;
    public Texture buttonTexture_White;
    public Texture buttonTexture_Yellow;

	void Start () {
        action = Director.getInstance().currentSceneControl as IUserAction;
	}
    void OnGUI()
    {
        if (GUI.Button(new Rect(Screen.width - 180, 120, 40, 40), buttonTexture_Green))
        {
            action.selectColor(color_1);
            colortext.text = "Color : Green";
        }
        if (GUI.Button(new Rect(Screen.width - 180, 200, 40, 40), buttonTexture_Red))
        {
            action.selectColor(color_2);
            colortext.text = "Color : Red";
        }
        if (GUI.Button(new Rect(Screen.width - 180, 280, 40, 40), buttonTexture_Yellow))
        {
            action.selectColor(color_3);
            colortext.text = "Color : Yellow";
        }
        if (GUI.Button(new Rect(Screen.width - 180, 360, 40, 40), buttonTexture_White))
        {
            action.selectColor(color_4);
            colortext.text = "Color : White";
        }
        if (GUI.Button(new Rect(Screen.width - 200, Screen.height - 60, 60, 40), "Quit"))
        {
            Application.Quit();
        }
    }
}
