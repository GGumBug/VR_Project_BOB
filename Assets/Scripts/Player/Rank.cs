using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rank
{
    public string SongName { get; private set; }
    public int Score { get; private set; }
    public int Combo { get; private set; }


    public Rank(string SongName, int Score, int Combo)
    {
        this.SongName = SongName;
        this.Score = Score;
        this.Combo = Combo;

    }

    public void LoadData()
    {
        PlayerPrefs.GetString("SongName", SheetManager.GetInstance().
        sheets[SheetManager.GetInstance().title[SheetManager.GetInstance().curMusic]].title);
        PlayerPrefs.GetInt("Score", GameManager.GetInstance().player.score);
        PlayerPrefs.GetInt("Combo", GameManager.GetInstance().player.combo);
    }

    public void SaveData()
    {
        PlayerPrefs.SetString("CurrentSongName", SheetManager.GetInstance().
        sheets[SheetManager.GetInstance().title[SheetManager.GetInstance().curMusic]].title);
        PlayerPrefs.SetInt("CurrentPlayerScore", GameManager.GetInstance().player.score);
        PlayerPrefs.SetInt("Combo", GameManager.GetInstance().player.combo);
    }
    int[] ranks;
    int[] scores;
    int[] combos;

    private float[] bestScore = new float[5];
    private string[] bestName = new string[5];

    void ScoreSet(float currentScore, string currentName)
    {
        PlayerPrefs.SetString("CurrentSongName", currentName);
        PlayerPrefs.SetFloat("CurrentPlayerScore", currentScore);

        float tmpScore = 0f;
        string tmpName = "";

        for (int i = 0; i < 5; i++)
        {
            // 저장된 최고점수와 이름 불러오기
            bestScore[i] = PlayerPrefs.GetFloat(i + "BestScore");
            bestName[i] = PlayerPrefs.GetString(i + "BestName");

            // 현재 점수 랭킹에 오르게 하는 과정
            while (bestScore[i] < currentScore)
            {
                // 자리바꾸기
                tmpScore = bestScore[i];
                tmpName = bestName[i];
                bestScore[i] = currentScore;
                bestName[i] = currentName;

                // 랭킹에 저장
                PlayerPrefs.SetFloat(i + "BestScore", currentScore);
                PlayerPrefs.SetString(i.ToString() + "BestName", currentName);

                // 다음 반복을 위한 준비
                currentScore = tmpScore;
                currentName = tmpName;
            }
        }
        // 랭킹에 맞춰 점수와 이름 저장
        for (int i = 0; i < 5; i++)
        {
            PlayerPrefs.SetFloat(i + "BestScore", bestScore[i]);
            PlayerPrefs.SetString(i.ToString() + "BestName", bestName[i]);
        }
    }


    private void Start()
    {
        RankSort(ranks);
        RankSort(scores);
        RankSort(combos);
    }

    void RankSort(int[] arr)
    {
        for (int i = 0; i < arr.Length; i++)
        {
            int max = i; // 최대값 지역변수를 만들어서 i라고 해준다.

            for (int j = i + 1; j < arr.Length; j++)
            {
                //최대값 비교
                if (arr[max] < arr[j]) // i와 다음 인덱스인 j를 비교하여 j가 더 크다면 최대값 지역변수를 j로 바꿔준다.
                    max = j;
            }
            Swap(ref arr[i], ref arr[max]); // Swap이라는 함수를 이용해 인덱스값을 교체해준다.
            /*
                        for (int k = 0; k < ranks.Length; k++) //
                        {
                            Debug.Log(ranks[k]);
                        }*/
        }
        for (int i = 0; i < arr.Length; i++)
        {
            Debug.Log(arr[i]); // 최종 정렬값확인.
        }
    }

    static void Swap(ref int a, ref int b) // 변수를 이용하여 값을 바꿔주는 함수
    {
        int temp = a;
        a = b;
        b = temp;
    }

}
