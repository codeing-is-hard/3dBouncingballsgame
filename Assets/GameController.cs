using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;        //반드시적어야 텍스트메쉬프로UGUI오류안생김
using UnityEngine.SceneManagement;      //이것도적어줘야 오류안생김

public class GameController : MonoBehaviour
{
    [SerializeField] private PlatformSpawner platformSpawner;
    [SerializeField] private TextMeshProUGUI textTapToPlay;

    [SerializeField] private TextMeshProUGUI textScore;
    [SerializeField] private int score = 0;

    [SerializeField] private TextMeshProUGUI textHighScore;

    private void Awake()
    {
        //마지막 플레이에서 획득했던 점수 불러오기
        int score = PlayerPrefs.GetInt("LastScore");
        textScore.text=score.ToString();

        //기존에 등록된 최고 점수 불러오기
        int highScore = PlayerPrefs.GetInt("HighScore");
        textHighScore.text = $"High Score {highScore}";
    }

    public void GameStart()
    {
        //taptoplay 택스트를 보이지 않게 설정
        textTapToPlay.enabled= false;

        //최고 점수 택스트도 마찬가지로 안보이게 설정
        textHighScore.enabled= false;
        //게임 시작 전에는 마지막 획득 점수가 출력되기 때문에 게임 시작시 점수 택스트 갱신
        textScore.text = score.ToString();
    }

    public void IncreaseScore()     //점수증가 메소드
    {
        score++;
        textScore.text = score.ToString();

        //점수가 짝수일 때마다 모든 발판의 색상을 변경한다
        if (score % 2 == 0)
        {
            platformSpawner.SetPlatformColor();
        }
    }

    public void GameOver()      //게임오버시 점수저장및 현재씬 다시로드
    {
        //기존에 등록되어있는 최고점수를 불러오기
        int highScore = PlayerPrefs.GetInt("HighScore");

        //현재 점수가 최고점수 보다 높을때
        if (score > highScore)
        {
            //현재 점수를 최고점수로 갱신(저장)하기
            PlayerPrefs.SetInt("HighScore", score);
        }


        //마지막에 획득한 점수를 저장
        PlayerPrefs.SetInt("LastScore",score);

        //현재 씬을 다시 로드함
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);

    }

    private void OnApplicationQuit()
    {
        //프로그램을 종료할때 마지막점수를 0으로설정
        PlayerPrefs.SetInt("LastScore",0);
    }
}
