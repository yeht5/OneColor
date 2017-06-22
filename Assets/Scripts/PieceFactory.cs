using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PieceFactory : MonoBehaviour
{
    private static List<GameObject> pieceList = new List<GameObject>();
    private int size = 0; //一行纸片的数量

	void Start () {
	}

	void Update () {
	}

    public void LoadSrc(int s, List<int> color)
    {
        size = s;
        float start = (float)size / -2.0f;

        for (int i = 0; i < size; i++)
        {
            for (int j = 0; j < size; j++)
            {
                Vector3 pos = new Vector3(start + 1.0f * j, start + 1.0f * i, 0);
                pieceList.Add(Instantiate(Resources.Load("Prefabs/Piece"), pos, Quaternion.identity) as GameObject);
                switch (color[i*size+j])
                {
                    case 0:
                        pieceList[pieceList.Count - 1].GetComponent<Renderer>().material.color = Color.red;
                        break;
                    case 1:
                        pieceList[pieceList.Count - 1].GetComponent<Renderer>().material.color = Color.green;
                        break;
                    case 2:
                        pieceList[pieceList.Count - 1].GetComponent<Renderer>().material.color = Color.blue;
                        break;
                    case 3:
                        pieceList[pieceList.Count - 1].GetComponent<Renderer>().material.color = Color.yellow;
                        break;
                }
            }
        }
    }

    //改变与选中片颜色相同且互相连接的片的颜色
    public int SetPieceColor(GameObject slt_piece, Color slt_color)
    {
        Color old_color = slt_piece.GetComponent<Renderer>().material.color;
        if (old_color == slt_color) return 0;
        int piece_id = 0;
        for (int i = 0; i < pieceList.Count; i++)
        {
            if (slt_piece.GetInstanceID() == pieceList[i].GetInstanceID())
            {
                piece_id = i;
            }
        }
        pieceList[piece_id].GetComponent<Renderer>().material.color = slt_color;
        GetSameColorPiece(piece_id, old_color, slt_color);
        return 1;
    }

    //改变一个片周围颜色相同的片的颜色
    void GetSameColorPiece(int id, Color old_color, Color new_color)
    {
        List<int> id_list = new List<int>();
        if ((id % size) > 0)
        {
            if (pieceList[id - 1].GetComponent<Renderer>().material.color == old_color)
                id_list.Add(id - 1);
        }
        if ((id % size + 1) < size)
        {
            if (pieceList[id + 1].GetComponent<Renderer>().material.color == old_color)
                id_list.Add(id + 1);
        }
        if ((id / size) > 0)
        {
            if (pieceList[id - size].GetComponent<Renderer>().material.color == old_color)
                id_list.Add(id - size);
        }
        if ((id / size + 1) < size)
        {
            if (pieceList[id + size].GetComponent<Renderer>().material.color == old_color)
                id_list.Add(id + size);
        }
        if (id_list.Count > 0)
        {
            for (int i = 0; i < id_list.Count; i++)
            {
                pieceList[id_list[i]].GetComponent<Renderer>().material.color = new_color;
            }
            for (int i = 0; i < id_list.Count; i++)
            {
                GetSameColorPiece(id_list[i], old_color, new_color);
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
        }
        return true;
    }
}