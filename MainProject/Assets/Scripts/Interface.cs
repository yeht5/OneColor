using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISceneControl
{
    void LoadResources();
}

public interface IUserAction
{
    void selectColor(Color color);
}