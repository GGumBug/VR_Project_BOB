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

    public string title = "Test";

    float speed = 1.0f;
    public float Speed
    {
        get
        {
            return speed;
        }
        set
        {
            speed = Mathf.Clamp(value, 1.0f, 5.0f);
        }
    }

    public Dictionary<string, Sheet> sheets = new Dictionary<string, Sheet>();

    private void Awake()
    {
        Init(title);
    }

    public void Init(string musicRoute)
    {
        sourceFile = Resources.Load<TextAsset>($"Sheet/{musicRoute}/{musicRoute}");
    }

    public TextAsset GetSourceFile()
    {
        return sourceFile;
    }

    public void AddSheet(string key, Sheet sheet)
    {
        sheets.Add(key, sheet);
    }
}
