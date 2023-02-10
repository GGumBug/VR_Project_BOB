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
    [SerializeField] Camera MainCam;
    [SerializeField] Button MainMenuZoomBtn;
    [SerializeField] Button OptionBtn;

    [Header("MusicSelect")]

    // 곡 리스트 관련 버튼
    [SerializeField] Button ListUpBtn;
    [SerializeField] Button ListDownBtn;

    // 게임시작버튼
    [SerializeField] Button StartBtn;
    [SerializeField] Button BackBtn;

    [Header("옵션")]
    [SerializeField] GameObject SoundPanel;
    [SerializeField] GameObject OptionPanel;
    [SerializeField] Button ExitBtn;
    [SerializeField] Button SoundBtn;
    [SerializeField] Button SoundBackBtn;
    [SerializeField] Button MainBackBtn;
    [SerializeField] Slider Bgm;
    [SerializeField] Slider Sfx;

    // 음악선택관련 버튼
    [SerializeField] TMP_Text txtSongName;
    [SerializeField] TMP_Text txtSongArtist;
    [SerializeField] TMP_Text txtBPM;
    [SerializeField] Image ImgDisk;
    [SerializeField] TMP_Text txtNoteCount;
    [SerializeField] TMP_Text txtBestScore;

    public List<Sheet> sheetList = new List<Sheet>();

    Vector3 dest;
    Vector3 rot;

    // Start is called before the first frame update
    void Start()
    {
        MainCam = Camera.main;
        OnclickSetting();
        SetSheetList(SheetManager.GetInstance().curMusic);
    }

    void OnclickSetting()
    {
        MainMenuZoomBtn.onClick.AddListener(MainMenuOn);
        SelectMusicBtn.onClick.AddListener(SelectMusic);
        BackBtn.onClick.AddListener(SelecttoMain);
        StartBtn.onClick.AddListener(GameStartOn);
        OptionBtn.onClick.AddListener(OptionOn);
        ExitBtn.onClick.AddListener(Exit);
        SoundBtn.onClick.AddListener(SoundOn);
        SoundBackBtn.onClick.AddListener(SoundBack);
        MainBackBtn.onClick.AddListener(MainBack);
        ListUpBtn.onClick.AddListener(NextSheet);
        ListDownBtn.onClick.AddListener(PriorSheet);
    }

    void OptionOn()
    {
        dest = new Vector3(-716, -2095, 705);
        rot = new Vector3(0, 360, 0);
        CameraMove(dest);
        CameraRotate(rot);

    }
    void MainBack()
    {
        dest = new Vector3(-19.7f, 8.4f, 50.5f);
        rot = new Vector3(0, -80, 0);
        CameraMove(dest);
        CameraRotate(rot);
    }

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

    void SetVolume()
    {
        /*audioManager.BgmPlayer.volume = Bgm.value;
        audioManager.SfxPlayer.volume = SFX.value;*/
    }

    void Exit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit(); // 어플리케이션 종료
#endif
    }

    void MainMenuOn()
    {
        MainMenuZoomBtn.gameObject.SetActive(false);
        dest = new Vector3(-19.7f, 8.4f, 50.5f);
        rot = new Vector3(0, -80, 0);
        CameraMove(dest);
        CameraRotate(rot);
    }

    void SelectMusic()
    {
        dest = new Vector3(41, -1176, 516);
        rot = new Vector3(0, 450, 0);
        CameraMove(dest);
        CameraRotate(rot);

    }
    void SelecttoMain()
    {
        dest = new Vector3(-19.7f, 8.4f, 50.5f);
        rot = new Vector3(0, -80, 0);
        CameraMove(dest);
        CameraRotate(rot);
    }
    void ToStart()
    {
        dest = Vector3.zero;
        CameraMove(dest);
    }
    void GameStartOn()
    {
        dest = new Vector3(0, -800, 0);
        rot = Vector3.zero;
        CameraMove(dest);
        CameraRotate(rot);

        Invoke("GameStart", 2);
        StartCoroutine(StartMove());
        

    }
    IEnumerator StartMove()
    {
        yield return new WaitForSeconds(1);
        dest = new Vector3(0, -3110, 0);
        CameraMove(dest);
    }

    void GameStart()
    {
        
        SceneManager.LoadScene("GameScene");
    }

    void CameraMove(Vector3 dest)
    {
        MainCam.transform.DOMoveX(dest.x, 0.7f).SetEase(Ease.InOutQuad);
        MainCam.transform.DOMoveY(dest.y, 1f).SetEase(Ease.InOutQuad);
        MainCam.transform.DOMoveZ(dest.z, 0.5f).SetEase(Ease.InOutQuad);
    }
    void CameraRotate(Vector3 rot)
    {
        MainCam.transform.DORotate(rot, 1f, RotateMode.FastBeyond360);  
    }

    void SetSheetList(int curMusic)
    {
        string title = SheetManager.GetInstance().title[curMusic];
        txtSongName.text = SheetManager.GetInstance().sheets[title].title;
        txtSongArtist.text = SheetManager.GetInstance().sheets[title].artist;
        txtBPM.text = SheetManager.GetInstance().sheets[title].bpm.ToString();
        ImgDisk.sprite = SheetManager.GetInstance().sheets[title].img;
        txtNoteCount.text = SheetManager.GetInstance().sheets[title].notes.Count.ToString();
        /*txtBestScore.text = sheetList[curMusic].*/
    }

    void NextSheet()
    {
        
        if (++SheetManager.GetInstance().curMusic > SheetManager.GetInstance().sheets.Count - 1)
            SheetManager.GetInstance().curMusic = 0;
        SetSheetList(SheetManager.GetInstance().curMusic);

    }

    void PriorSheet()
    {
        if (--SheetManager.GetInstance().curMusic < 0)
            SheetManager.GetInstance().curMusic = SheetManager.GetInstance().sheets.Count- 1;
        SetSheetList(SheetManager.GetInstance().curMusic);
    }

    

}
