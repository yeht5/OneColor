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
        List<int> colorlist = new List<int>();
        for (int i = 0; i < color.Length; i++)
        {
            colorlist.Add(color[i] - 48);
            Debug.Log(color[i] - 48);
        }
        PieceFactory pf = Singleton<PieceFactory>.Instance;
        pf.LoadSrc(size, colorlist);
    } 
}
