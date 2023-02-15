using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class LongNoteUI : MonoBehaviour
{
    [SerializeField] TMP_Text lifeCount;

    public void MinusCount(int a)
    {
        lifeCount.text = a.ToString();
    }
}
