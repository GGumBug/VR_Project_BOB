using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class PlayerUI : MonoBehaviour
{
    [SerializeField] TMP_Text score;
    [SerializeField] TMP_Text combo;
    [SerializeField] Slider hp;
    [SerializeField] Slider combogauge;

    public void SetPlayerInfo()
    {
        score.text = $"SCORE : {GameManager.GetInstance().player.score.ToString()}";
        combo.text = $"COMBO : {GameManager.GetInstance().player.combo.ToString()}";
        hp.maxValue = GameManager.GetInstance().player.maxHp;
        hp.value = GameManager.GetInstance().player.hp;
        combogauge.value = GameManager.GetInstance().player.combo;
    }
}
