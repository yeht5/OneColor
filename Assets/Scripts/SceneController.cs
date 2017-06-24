using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SimpleJson;

public class SceneController : MonoBehaviour, ISceneControl, IUserAction
{
	private static SceneController _instance;

	public bool is_slt_color = false; //是否选择颜色
	public Color slt_color; //选择的颜色
	public int used_step = 0; //已用的次数
	public int max_step = 0; //已用的次数

	public Text maintext; //中间显示
	public Text steptext; //显示次数
	public Texture buttonTexture_Reset;

	void Awake()
	{
		Director director = Director.getInstance();
		director.currentSceneControl = this;
		director.currentSceneControl.LoadResources();
	}

	void Start () {
	}

	void Update () {
		if (Input.GetButtonDown("Fire1"))
		{
			Vector3 pos = Input.mousePosition;
			selectPiece(pos);
		}
		steptext.text = "Step : " + used_step.ToString() + "/" + max_step.ToString();
	}

	public void LoadResources()
	{
		FileManager fileManager = Singleton<FileManager>.Instance;
		string file = "level1.json";
		fileManager.loadLevelJson(file);
	}

	void OnGUI() {
		if (GUI.Button(new Rect(Screen.width - 200, Screen.height - 60, 70, 40), "Reset"))
		{
			Debug.Log ("reset");
			PieceFactory pf = Singleton<PieceFactory>.Instance;
			pf.reset();
			used_step = 0;
		}
	}
	//选择改变颜色的片
	public void selectPiece(Vector3 pos)
	{
		Ray ray = Camera.main.ScreenPointToRay(pos);

		RaycastHit hit;
		if (Physics.Raycast(ray, out hit) && hit.collider.gameObject.tag == "Piece" && is_slt_color)
		{
			PieceFactory pf = Singleton<PieceFactory>.Instance;
			used_step += pf.SetPieceColor(hit.collider.gameObject, slt_color);
			if (pf.isOneColor())
			{
				if (used_step > max_step)
					maintext.text = "OK!";
				else
					maintext.text = "Perfect!";
			}
		}
	}

	//选择颜色
	public void selectColor(Color color)
	{
		if (slt_color == null || slt_color != color)
		{
			is_slt_color = true;
			slt_color = color;
		}
	}

	//从json读取游戏关卡
	public void stageLevel(string json)
	{
		LevelData data = LevelData.CreateFromJSON(json);

		int size = data.size;
		string color = data.color;
		max_step = data.step;
		string colorback = data.colorback;
		string colorleft = data.colorleft;
		string colorright = data.colorright;
		string colortop = data.colortop;
		string colorbottom = data.colorbottom;
		List<int> colorlist = new List<int>();
		List<int> color_left = new List<int>();
		List<int> color_right = new List<int>();
		List<int> color_top = new List<int>();
		List<int> color_bottom = new List<int>();
		List<int> color_back = new List<int>();
		for (int i = 0; i < color.Length; i++)
		{
			colorlist.Add(color[i] - 48);
			color_left.Add(colorleft[i] - 48);
			color_right.Add(colorright[i] - 48);
			color_top.Add(colortop[i] - 48);
			color_bottom.Add(colorbottom[i] - 48);
			color_back.Add(colorback[i] - 48);
			//Debug.Log(color[i] - 48);
		}
		PieceFactory pf = Singleton<PieceFactory>.Instance;
		pf.LoadSrc(size, colorlist, color_left, color_right, color_bottom, color_back, color_top);
	} 
}
