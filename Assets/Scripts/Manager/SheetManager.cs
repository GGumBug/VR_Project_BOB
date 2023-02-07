using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SheetManager : MonoBehaviour
{
    #region Singletone

    private static SheetManager instance = null;

    public static SheetManager GetInstance()
    {
        if (instance == null)
        {
            GameObject go = new GameObject("@SheetManager");
            instance = go.AddComponent<SheetManager>();

            DontDestroyOnLoad(go);
        }
        return instance;

    }
    #endregion

    TextAsset sourceFile;
    public string musicRoute = "Test";

    private void Awake()
    {
        Init(musicRoute);
    }

    public void Init(string musicRoute)
    {
        sourceFile = Resources.Load<TextAsset>($"Sheet/{musicRoute}/{musicRoute}");
    }

    public TextAsset GetSourceFile()
    {
        return sourceFile;
    }
}
