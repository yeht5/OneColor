using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FileManager : MonoBehaviour {
    public string url;

    void Awake()
    {

    }

    public void loadLevelJson(string name)
    {
        url = "file://" + Application.dataPath + "/Data/" + name;
        StartCoroutine(LoadLevel());
    }

    IEnumerator LoadLevel()
    {
        if (url.Length > 0)
        {
            WWW www = new WWW(url);
            yield return www;
            if (!string.IsNullOrEmpty(www.error))
                Debug.Log(www.error);
            else
            {
                SceneController scene = Singleton<SceneController>.Instance;
                scene.stageLevel(www.text.ToString());  // 返回json字符串给scene  
            }
        }
    }
}

[SerializeField]
public class LevelData
{
    public int size = 5;
    public string color = "0000000000000000000000000";
    public int step = 0;

    public static LevelData CreateFromJSON(string json)
    {
        return JsonUtility.FromJson<LevelData>(json);
    }
}
