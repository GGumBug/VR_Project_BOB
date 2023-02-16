using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class MenuUI : MonoBehaviour
{
    [Header("Main")]
    [SerializeField] Button SelectMusicBtn;
    [SerializeField] GameObject xrOrigin;
    [SerializeField] Button MainMenuZoomBtn;
    [SerializeField] Button OptionBtn;
    [SerializeField] Button MainExitBtn;

    [Header("MusicSelect")]

    // 곡 리스트 관련 버튼
    [SerializeField] Button ListUpBtn;
    [SerializeField] Button ListDownBtn;

    // 게임시작버튼
    [SerializeField] Button StartBtn;
    [SerializeField] Button BackBtn;

    [Header("Option")]
    [SerializeField] GameObject SoundPanel;
    [SerializeField] GameObject OptionPanel;
    [SerializeField] Button OptionExitBtn;
    [SerializeField] Button SoundBtn;
    [SerializeField] Button SoundBackBtn;
    [SerializeField] Button MainBackBtn;
    [SerializeField] Slider Bgm;
    [SerializeField] Slider Sfx;
    [SerializeField] GameObject Cube2;

    // 음악선택관련 버튼
    [SerializeField] TMP_Text txtSongName;
    [SerializeField] TMP_Text txtSongArtist;
    [SerializeField] TMP_Text txtBPM;
    [SerializeField] Image ImgDisk;
    [SerializeField] TMP_Text txtNoteCount;
    [SerializeField] Button RankingBtn;

    [SerializeField] GameObject MenuObj;
    [SerializeField] GameObject SelectObj;
    [SerializeField] GameObject OptionObj;

    [Header("Ranking")]
    [SerializeField] GameObject RankingPanel;
    [SerializeField] Button RankBackBtn;
    [SerializeField] Button RankUpBtn;
    [SerializeField] Button RankDownBtn;
    [SerializeField] TMP_Text txtRankSongName;


    public List<Sheet> sheetList = new List<Sheet>();

    Vector3 dest;
    Vector3 rot;

    // Start is called before the first frame update
    void Start()
    {
        SetSheetList(SheetManager.GetInstance().curMusic);
        xrOrigin = GameObject.FindGameObjectWithTag("XROrigin");
        xrOrigin.transform.position = new Vector3(-22.7f, 8.1f, 49.7f);
        xrOrigin.transform.localEulerAngles = new Vector3(0, 46.535f, 0);
        OnclickSetting();
        RankingPanel.SetActive(false);
        

    }
    // Start XRorigin p(-22.7, 8.1, 49.7)/ R y :46.535
    void OnclickSetting()
    {
        MainMenuZoomBtn.onClick.AddListener(MainMenuOn);
        SelectMusicBtn.onClick.AddListener(SelectMusic);
        BackBtn.onClick.AddListener(SelecttoMain);
        StartBtn.onClick.AddListener(GameStartOn);
        OptionBtn.onClick.AddListener(OptionOn);
        MainExitBtn.onClick.AddListener(Exit);
        OptionExitBtn.onClick.AddListener(RemoveRank);
        SoundBtn.onClick.AddListener(SoundOn);
        SoundBackBtn.onClick.AddListener(SoundBack);
        MainBackBtn.onClick.AddListener(MainBack);
        ListUpBtn.onClick.AddListener(NextSheet);
        ListDownBtn.onClick.AddListener(PriorSheet);
        RankingBtn.onClick.AddListener(RankOn);
        RankBackBtn.onClick.AddListener(ExitRank);
        RankUpBtn.onClick.AddListener(NextSheet);
        RankDownBtn.onClick.AddListener(PriorSheet);
    }
    // 옵션과 메인메뉴 연결 버튼////
    void OptionOn()
    {
        dest = new Vector3(1.8f, 30, 23.7f);
        rot = new Vector3(0, 86.986f, 0);
        CameraMove(dest);
        CameraRotate(rot);
        MenuObj.gameObject.SetActive(false);
        SelectObj.gameObject.SetActive(false);
        OptionObj.gameObject.SetActive(true);

    }
    void MainBack()
    {
        dest = new Vector3(-12, 11, 52.4f);
        rot = new Vector3(0, 0.255f, 0);
        CameraMove(dest);
        CameraRotate(rot);
        MenuObj.gameObject.SetActive(true);
        SelectObj.gameObject.SetActive(false);
        OptionObj.gameObject.SetActive(false);
    }

    /// <summary>
    /// 옵션 사운드 패널 온오프버튼///
    /// </summary>
    void SoundOn()
    {
        SoundPanel.gameObject.SetActive(true);
        OptionPanel.gameObject.SetActive(false);
    }

    void SoundBack()
    {
        SoundPanel.gameObject.SetActive(false);
        OptionPanel.gameObject.SetActive(true);
    }

    // 사운드패널 볼륨 조절 함수
    void SetVolume()
    {
        /*audioManager.BgmPlayer.volume = Bgm.value;
        audioManager.SfxPlayer.volume = SFX.value;*/
    }
    // 종료버튼
    void Exit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit(); // 어플리케이션 종료
#endif
    }
    // 메인메뉴로 갈 수 있게 눌러주는 버튼
    void MainMenuOn()
    {
        Debug.Log("MainMenuOn");
        MainMenuZoomBtn.gameObject.SetActive(false);
        dest = new Vector3(-12, 11, 52.4f);
        rot = new Vector3(0, 0.255f, 0);
        CameraMove(dest);
        CameraRotate(rot);
        MenuObj.gameObject.SetActive(true);
        SelectObj.gameObject.SetActive(false);
        OptionObj.gameObject.SetActive(false);
        RankingPanel.gameObject.SetActive(false);
    }
    // ///////////메인 메뉴 패널과 음악 선택창 연결 /////////
    void SelectMusic()
    {
        dest = new Vector3(-10.2f, 20.3f, 39.59f);
        rot = new Vector3(0, -55.8f, 0);
        CameraMove(dest);
        CameraRotate(rot);
        MenuObj.gameObject.SetActive(false);
        SelectObj.gameObject.SetActive(true);
        OptionObj.gameObject.SetActive(false);
        RankingPanel.gameObject.SetActive(false);

    }
    void SelecttoMain()
    {
        dest = new Vector3(-12, 11, 52.4f);
        rot = new Vector3(0, 357.45f, 0);
        CameraMove(dest);
        CameraRotate(rot);
        MenuObj.gameObject.SetActive(true);
        SelectObj.gameObject.SetActive(false);
        OptionObj.gameObject.SetActive(false);
        RankingPanel.gameObject.SetActive(false);
    }
    /// <summary>
    /// 게임시작 함수
    /// </summary>
    void GameStartOn()
    {
        dest = new Vector3(-3.1f, 27f, 15.1f);
        rot = new Vector3(40, 362.259f, 0);
        CameraMove(dest);
        CameraRotate(rot);
        MenuObj.gameObject.SetActive(false);
        SelectObj.gameObject.SetActive(false);
        OptionObj.gameObject.SetActive(false);
        RankingPanel.gameObject.SetActive(false);

        Invoke("GameStart", 2);
        StartCoroutine(StartMove());


    }
    IEnumerator StartMove()
    {
        yield return new WaitForSeconds(1);
        dest = new Vector3(-0.45f, 0.2f, -18.85f);
        rot = new Vector3(0, 0, 0);
        CameraMove(dest);
        CameraRotate(rot);
    }

    void GameStart()
    {
        ScenesManager.GetInstance().ChangeScene(Scenes.GameScene);
    }
    /// <summary>
    /// 카메라 움직이는 함수
    /// </summary>
    /// <param name="dest"></param>
    void CameraMove(Vector3 dest)
    {
        xrOrigin.transform.DOMoveX(dest.x, 0.7f).SetEase(Ease.InOutQuad);
        xrOrigin.transform.DOMoveY(dest.y, 1f).SetEase(Ease.InOutQuad);
        xrOrigin.transform.DOMoveZ(dest.z, 0.5f).SetEase(Ease.InOutQuad);
    }
    void CameraRotate(Vector3 rot)
    {
        xrOrigin.transform.DORotate(rot, 1f, RotateMode.FastBeyond360);
    }

    /////////////// 음악선택 창////////////////////
    void SetSheetList(int curMusic)
    {
        string title = SheetManager.GetInstance().title[curMusic];
        txtSongName.text = SheetManager.GetInstance().sheets[title].title;
        txtRankSongName.text = SheetManager.GetInstance().sheets[title].title;
        txtSongArtist.text = SheetManager.GetInstance().sheets[title].artist;
        txtBPM.text = "BPM :" + SheetManager.GetInstance().sheets[title].bpm.ToString();
        ImgDisk.sprite = SheetManager.GetInstance().sheets[title].img;
        txtNoteCount.text = "Note :" + SheetManager.GetInstance().sheets[title].notes.Count.ToString();
        /*txtBestScore.text = sheetList[curMusic].*/
    }

    void NextSheet()
    {

        if (++SheetManager.GetInstance().curMusic > SheetManager.GetInstance().sheets.Count - 1)
            SheetManager.GetInstance().curMusic = 0;
        SetSheetList(SheetManager.GetInstance().curMusic);
        RankSystem.GetInstance().ChangeRankTab();

    }

    void PriorSheet()
    {
        if (--SheetManager.GetInstance().curMusic < 0)
            SheetManager.GetInstance().curMusic = SheetManager.GetInstance().sheets.Count - 1;
        SetSheetList(SheetManager.GetInstance().curMusic);
        RankSystem.GetInstance().ChangeRankTab();
    }

    /////////////////////랭크시스템////////////////////////////////////////
    
    void RankOn()
    {
        dest = new Vector3(-13.6f, 29.2f, 40.3f);
        rot = new Vector3(0, 87, 0);
        CameraMove(dest);
        CameraRotate(rot);
        RankingPanel.SetActive(true);
        MenuObj.gameObject.SetActive(false);
        SelectObj.gameObject.SetActive(false);
        OptionObj.gameObject.SetActive(false);
    }
    void ExitRank()
    {
        dest = new Vector3(-10.2f, 20.3f, 39.59f);
        rot = new Vector3(0, -55.8f, 0);
        CameraMove(dest);
        CameraRotate(rot);
        RankingPanel.SetActive(false);
        MenuObj.gameObject.SetActive(false);
        SelectObj.gameObject.SetActive(true);
        OptionObj.gameObject.SetActive(false);
    }

    // 개발용 랭킹 지우기 버튼(옵션 - 나가기)
    void RemoveRank()
    {
        PlayerPrefs.DeleteAll();
    }

}
