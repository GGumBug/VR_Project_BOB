using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class MenuUI : MonoBehaviour
{
    [Header("Main")]
    [SerializeField] RawImage videoimg;
    [SerializeField] Button SelectMusicBtn;
    [SerializeField] Image MusicPanel;
    [SerializeField] Camera MainCam;
    [SerializeField] Button MainMenuZoomBtn;

    [Header("MusicSelect")]

    // 곡 리스트 관련 버튼
    [SerializeField] Button[] MusicList;
    [SerializeField] Button ListUpBtn;
    [SerializeField] Button ListDownBtn;

    // 난이도 버튼
    [SerializeField] Button EasyBtn;
    [SerializeField] Button NormalBtn;
    [SerializeField] Button HardBtn;

    // 속도 버튼
    [SerializeField] Button x1Btn;
    [SerializeField] Button x2Btn;
    [SerializeField] Button x4Btn;

    // 게임시작버튼
    [SerializeField] Button StartBtn;
    [SerializeField] Button BackBtn;
    [SerializeField] Button RankingBtn;

    Vector3 dest;

    // Start is called before the first frame update
    void Start()
    {
        OnclickSetting();
    }

    // Update is called once per frame
    void Update()
    {
        MainCam = Camera.main;
    }
    void OnclickSetting()
    {
        MainMenuZoomBtn.onClick.AddListener(MainMenuOn);
        SelectMusicBtn.onClick.AddListener(SelectMusic);
        BackBtn.onClick.AddListener(SelecttoMain);
        StartBtn.onClick.AddListener(GameStartOn);
    }

    void MainMenuOn()
    {
        MainMenuZoomBtn.gameObject.SetActive(false);
        dest = new Vector3(-43, 7, 35);
        CameraMove(dest);
    }


    void SelectMusic()
    {
        dest = new Vector3(35, 164, 85);
        MusicPanel.gameObject.SetActive(true);
        SelectMusicBtn.gameObject.SetActive(false);
        CameraMove(dest);

    }
    void SelecttoMain()
    {
        dest = new Vector3(-43, 7, 35);
        MusicPanel.gameObject.SetActive(false);
        SelectMusicBtn.gameObject.SetActive(true);
        CameraMove(dest);
    }
    void ToStart()
    {
        dest = Vector3.zero;
        CameraMove(dest);
    }
    void GameStartOn()
    {
        dest = new Vector3(0, 259, 0);
        CameraMove(dest);
        Invoke("GameStart", 1);
    }
    void GameStart()
    {

        SceneManager.LoadScene("GameScene");
    }

    void CameraMove(Vector3 dest)
    {
        MainCam.transform.DOMoveX(dest.x, 0.7f).SetEase(Ease.InQuad);
        MainCam.transform.DOMoveY(dest.y, 0.5f).SetEase(Ease.InQuad);
        MainCam.transform.DOMoveZ(dest.z, 0.9f).SetEase(Ease.OutQuad);
    }
}
