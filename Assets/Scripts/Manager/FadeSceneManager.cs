using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;
using TMPro;



public class FadeSceneManager : MonoBehaviour
{
    public static FadeSceneManager Instance
    {
        get
        {
            return instance;
        }
    }
    private static FadeSceneManager instance;



}
