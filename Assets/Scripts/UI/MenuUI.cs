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
    [SerializeField] Button ExitBtn;
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
    [SerializeField] TMP_Text txtBestScore;

    [SerializeField] GameObject MenuObj;
    [SerializeField] GameObject SelectObj;
    [SerializeField] GameObject OptionObj;
    public List<Sheet> sheetList = new List<Sheet>();

    Vector3 dest;
    Vector3 rot;

    // Start is called before the first frame update
    void Start()
    {
        xrOrigin = GameObject.FindGameObjectWithTag("XROrigin");
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
        dest = new Vector3(-3.1f, 25.2f, 36.36f);
        rot = new Vector3(0, 86.986f, 0);
        CameraMove(dest);
        CameraRotate(rot);
        MenuObj.gameObject.SetActive(false);
        SelectObj.gameObject.SetActive(false);
        OptionObj.gameObject.SetActive(true);

    }
    void MainBack()
    {
        dest = new Vector3(-12, 8.1f, 52.4f);
        rot = new Vector3(0, 0.255f, 0);
        CameraMove(dest);
        CameraRotate(rot);
        MenuObj.gameObject.SetActive(true);
        SelectObj.gameObject.SetActive(false);
        OptionObj.gameObject.SetActive(false);
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
    // Start XRorigin p(-22.7, 8.1, 49.7)/ R y :46.535
    void MainMenuOn()
    {
        MainMenuZoomBtn.gameObject.SetActive(false);
        dest = new Vector3(-12, 8.1f, 52.4f);
        rot = new Vector3(0, 0.255f, 0);
        CameraMove(dest);
        CameraRotate(rot);
        MenuObj.gameObject.SetActive(true);
        SelectObj.gameObject.SetActive(false);
        OptionObj.gameObject.SetActive(false);
    }

    void SelectMusic()
    {
        dest = new Vector3(-7.2f, 17.9f, 42.7f);
        rot = new Vector3(0, 271.021f, 0);
        CameraMove(dest);
        CameraRotate(rot);
        MenuObj.gameObject.SetActive(false);
        SelectObj.gameObject.SetActive(true);
        OptionObj.gameObject.SetActive(false);
    }
    void SelecttoMain()
    {
        dest = new Vector3(-12, 8.1f, 52.4f);
        rot = new Vector3(0, 0.255f, 0);
        CameraMove(dest);
        CameraRotate(rot);
        MenuObj.gameObject.SetActive(true);
        SelectObj.gameObject.SetActive(false);
        OptionObj.gameObject.SetActive(false);
    }
    void GameStartOn()
    {
        dest = new Vector3(-3.1f, 27f, 15.1f);
        rot = new Vector3(40, 362.259f, 0);
        CameraMove(dest);
        CameraRotate(rot);
        MenuObj.gameObject.SetActive(false);
        SelectObj.gameObject.SetActive(false);
        OptionObj.gameObject.SetActive(false);

        Invoke("GameStart", 2);
        StartCoroutine(StartMove());
        

    }
    IEnumerator StartMove()
    {
        yield return new WaitForSeconds(1);
        dest = new Vector3(-0.45f, 0.2f, -18.85f);
        rot = new Vector3(0,0, 0);
        CameraMove(dest);
        CameraRotate(rot);
    }

    void GameStart()
    {
        ScenesManager.GetInstance().ChangeScene(Scenes.GameScene);
    }

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
