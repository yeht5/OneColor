using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PieceFactory : MonoBehaviour
{
    private static List<GameObject> pieceList = new List<GameObject>();
	private static List<GameObject> pieceList_left = new List<GameObject>();
	private static List<GameObject> pieceList_right = new List<GameObject>();
	private static List<GameObject> pieceList_back = new List<GameObject>();
	private static List<GameObject> pieceList_top = new List<GameObject>();
	private static List<GameObject> pieceList_bottom = new List<GameObject>();
	private static List<int> colorList = new List<int> ();
	private static List<int> color_left = new List<int>();
	private static List<int> color_right = new List<int>();
	private static List<int> color_top = new List<int>();
	private static List<int> color_bottom = new List<int>();
	private static List<int> color_back = new List<int>();
	public Color color_1 = new Color(0.1176f, 0.1843f, 0.1215f);
	public Color color_2 = new Color(0.8627f, 0.2039f, 0.2823f);
	public Color color_3 = new Color(0.8509f, 0.6039f, 0.3019f);
	public Color color_4 = new Color(0.8196f, 0.6941f, 0.5333f);
	private int count = 0;
    private int size = 0; //一行纸片的数量
	private GameObject cube;
	Vector3 StartPosition;  
	Vector3 previousPosition;  
	Vector3 offset;  
	Vector3 finalOffset;  
	Vector3 eulerAngle;  

	bool isSlide;  
	float angle;  

	public float scale = 1;  

	RaycastHit hit;  

	/// <summary>
	/// Start this instance.
	/// </summary>
	void Start () {
	}

	void Update () {
		//实现cube的旋转
		if (cube != null) {
			if (Input.GetMouseButtonDown (0)) {  
				StartPosition = Input.mousePosition;  
				previousPosition = Input.mousePosition;  
			}  
			if (Input.GetMouseButton (0)) {  
				offset = Input.mousePosition - previousPosition;  
				previousPosition = Input.mousePosition;  
				cube.transform.Rotate (Vector3.Cross (offset, Vector3.forward).normalized, offset.magnitude, Space.World);
			}  
			if (Input.GetMouseButtonUp (0)) {  
				finalOffset = Input.mousePosition - StartPosition;  
				isSlide = true;  
				angle = finalOffset.magnitude; 
			}  
			if (isSlide) {  
				cube.transform.Rotate (Vector3.Cross (finalOffset, Vector3.forward).normalized, angle * 2 * Time.deltaTime, Space.World);
				if (angle > 0) {  
					angle -= 5;  
				} else {  
					angle = 0;  
				}  
			}
		}
	}
		
	//利用协程实现延时变色
	private IEnumerator delay(List<GameObject> new_list, int id, Color old_color, Color new_color)
	{
		yield return new WaitForSeconds(0.7f/(1.8f*(count+12)));
		GetSameColorPiece(new_list, id, old_color, new_color);
	}

	private void paint(List<GameObject> target_list, List<int> color, int i, int j) {
		switch (color[i*size+j])
		{
		case 0:
			target_list[i*size+j].GetComponent<Renderer>().material.color = color_1;
			break;
		case 1:
			target_list[i*size+j].GetComponent<Renderer>().material.color = color_2;
			break;
		case 2:
			target_list[i*size+j].GetComponent<Renderer>().material.color = color_3;
			break;
		case 3:
			target_list[i*size+j].GetComponent<Renderer>().material.color = color_4;
			break;
		}
	}

	public void reset() {
		Debug.Log (size);
		for (int i = 0; i < size; i++) {
			for (int j = 0; j < size; j++) {
				paint(pieceList, colorList, i, j);
				paint(pieceList_top, color_top, i, j);
				paint(pieceList_bottom, color_bottom, i, j);
				paint(pieceList_back, color_back, i, j);
				paint(pieceList_left, color_left, i, j);
				paint(pieceList_right, color_right, i, j);
			}
		}
	}
	public void LoadSrc(int s, List<int> color, List<int> left, List<int> right, List<int> top, List<int> bottom, List<int> back)
    {
		colorList = color;
		color_back = back;
		color_left = left;
		color_right = right;
		color_top = top;
		color_bottom = bottom;
        size = s;
        float start = (float)size / -2.0f;
		Vector3 Cube_pos = new Vector3(start + 1.0f * Mathf.Ceil(size/2), start + 1.0f * Mathf.Ceil(size/2), Mathf.Ceil(size/2));
		cube = Instantiate (Resources.Load ("Prefabs/Cube"), Cube_pos, Quaternion.identity) as GameObject;
		for (int i = 0; i < size; i++)
        {
            for (int j = 0; j < size; j++)
            {
                Vector3 pos = new Vector3(start + 1.0f * j, start + 1.0f * i, 0);
                pieceList.Add(Instantiate(Resources.Load("Prefabs/Piece"), pos, Quaternion.identity) as GameObject);
				paint (pieceList, color, i, j);
            }
        }
		//back
		for (int i = 0; i < size; i++)
		{
			for (int j = 0; j < size; j++)
			{
				Vector3 pos = new Vector3(start + 1.0f * j, start + 1.0f * i, 1.0f*s);
				pieceList_back.Add(Instantiate(Resources.Load("Prefabs/Piece"), pos, Quaternion.identity) as GameObject);
				paint (pieceList_back, color_back, i, j);
			}
		}

		//leftside
		for (int i = 0; i < size; i++)
		{
			for (int j = 0; j < size; j++)
			{
				Vector3 pos = new Vector3(start - 0.5f, start + 1.0f * i, start + 1.0f * j + 0.5f*(s+1));
				pieceList_left.Add(Instantiate(Resources.Load("Prefabs/Piece"), pos, Quaternion.Euler(0, 90, 0)) as GameObject);
				paint (pieceList_left, color_left, i, j);
			}
		}
		//rightside
		for (int i = 0; i < size; i++)
		{
			for (int j = 0; j < size; j++)
			{
				Vector3 pos = new Vector3(start - 0.5f + 1.0f*s, start + 1.0f * i, start + 1.0f * j + 0.5f*(s+1));
				pieceList_right.Add(Instantiate(Resources.Load("Prefabs/Piece"), pos, Quaternion.Euler(0, 90, 0)) as GameObject);
				paint (pieceList_right, color_right, i, j);
			}
		}
		//bottom
		for (int i = 0; i < size; i++)
		{
			for (int j = 0; j < size; j++)
			{
				Vector3 pos = new Vector3(start + 1.0f * j, start - 0.5f, 1.0f*i + 0.5f);
				pieceList_bottom.Add(Instantiate(Resources.Load("Prefabs/Piece"), pos, Quaternion.Euler(90, 0, 0)) as GameObject);
				paint (pieceList_bottom, color_bottom, i, j);
			}
		}
		//top
		for (int i = 0; i < size; i++)
		{
			for (int j = 0; j < size; j++)
			{
				Vector3 pos = new Vector3(start + 1.0f * j, start - 0.5f + 1.0f*s, 1.0f*i + 0.5f);
				pieceList_top.Add(Instantiate(Resources.Load("Prefabs/Piece"), pos, Quaternion.Euler(90, 0, 0)) as GameObject);
				paint (pieceList_top, color_top, i, j);
			}
		}
		//将cube设为piecelist的父对象实现旋转
		for (int i = 0; i < pieceList.Count; i++) {
			pieceList[i].transform.parent = cube.transform;
			pieceList_right[i].transform.parent = cube.transform;
			pieceList_left[i].transform.parent = cube.transform;
			pieceList_back[i].transform.parent = cube.transform;
			pieceList_bottom[i].transform.parent = cube.transform;
			pieceList_top[i].transform.parent = cube.transform;
		}
    }

	//判断GameObject所在列表并返回该列表
	public List<GameObject> GetList(GameObject obj) {
		for (int i = 0; i < pieceList.Count; i++)
		{
			if (obj.GetInstanceID() == pieceList[i].GetInstanceID())
			{
				Debug.Log("zhengmian");
				return pieceList;
			}
			if (obj.GetInstanceID() == pieceList_left[i].GetInstanceID())
			{
				Debug.Log("left");
				return pieceList_left;
			}
			if (obj.GetInstanceID() == pieceList_right[i].GetInstanceID())
			{
				Debug.Log("right");
				return pieceList_right;
			}
			if (obj.GetInstanceID() == pieceList_top[i].GetInstanceID())
			{
				Debug.Log("top");
				return pieceList_top;
			}
			if (obj.GetInstanceID() == pieceList_bottom[i].GetInstanceID())
			{
				Debug.Log("bottom");
				return pieceList_bottom;
			}
			if (obj.GetInstanceID() == pieceList_back[i].GetInstanceID())
			{
				Debug.Log("back");
				return pieceList_back;
			}
		}
		return pieceList;
	}
    //改变与选中片颜色相同且互相连接的片的颜色
    public int SetPieceColor(GameObject slt_piece, Color slt_color)
    {
		List<GameObject> new_list = GetList (slt_piece);
        Color old_color = slt_piece.GetComponent<Renderer>().material.color;
        if (old_color == slt_color) return 0;
        int piece_id = 0;
        for (int i = 0; i < pieceList.Count; i++)
        {
			if (slt_piece.GetInstanceID() == new_list[i].GetInstanceID())
            {
                piece_id = i;
            }
        }
		new_list[piece_id].GetComponent<Renderer>().material.color = slt_color;
		count = 0;
		GetSameColorPiece(new_list, piece_id, old_color, slt_color);
        return 1;
    }

    //改变一个片周围颜色相同的片的颜色
	void GetSameColorPiece(List<GameObject> new_list, int id, Color old_color, Color new_color)
    {
        List<int> id_list = new List<int>();
		count++;
        if ((id % size) > 0)
        {
			if (new_list[id - 1].GetComponent<Renderer>().material.color == old_color)
                id_list.Add(id - 1);
        }
        if ((id % size + 1) < size)
        {
			if (new_list[id + 1].GetComponent<Renderer>().material.color == old_color)
                id_list.Add(id + 1);
        }
        if ((id / size) > 0)
        {
			if (new_list[id - size].GetComponent<Renderer>().material.color == old_color)
                id_list.Add(id - size);
        }
        if ((id / size + 1) < size)
        {
			if (new_list[id + size].GetComponent<Renderer>().material.color == old_color)
                id_list.Add(id + size);
        }
        if (id_list.Count > 0)
        {
            for (int i = 0; i < id_list.Count; i++)
            {
				new_list[id_list[i]].GetComponent<Renderer>().material.color = new_color;
            }
            for (int i = 0; i < id_list.Count; i++)
            {
				StartCoroutine(delay(new_list ,id_list[i], old_color, new_color));
				Debug.Log(count);
            }
        }
        else
        {
            return;
        }
    }

    //判断颜色是否一致
    public bool isOneColor()
    {
        Color color = pieceList[0].GetComponent<Renderer>().material.color;
        for (int i = 0; i < pieceList.Count; i++)
        {
            if (pieceList[i].GetComponent<Renderer>().material.color != color)
            {
                return false;
            }
			if (pieceList_left[i].GetComponent<Renderer>().material.color != color)
			{
				return false;
			}
			if (pieceList_right[i].GetComponent<Renderer>().material.color != color)
			{
				return false;
			}
			if (pieceList_top[i].GetComponent<Renderer>().material.color != color)
			{
				return false;
			}
			if (pieceList_bottom[i].GetComponent<Renderer>().material.color != color)
			{
				return false;
			}
        }
        return true;
    }
}