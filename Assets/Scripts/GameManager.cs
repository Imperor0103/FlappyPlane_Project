using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// 게임오버, 재시작, 점수
public class GameManager : MonoBehaviour
{
    // singleton 객체를 만들기 위한 요소
    static GameManager gameManager; // 1.자신을 참조하는 static 변수
    public static GameManager Instance { get { return gameManager; } }  // 2.그 변수를 외부로 가져갈 수 있는 프로퍼티 1개

    private int currentScore = 0;

    // UI매니저
    UIManager uiManager;
    public UIManager UIManager { get { return uiManager; } }    // UIManager 를 외부로 가져갈 수 있는 프로퍼티

    private void Awake()
    {
        gameManager = this; // 3.최초의 객체를 생성해주는 작업

        // UIManager는 싱글턴 객체가 아니므로 FindObjectsOfType으로 찾는다
        uiManager = FindObjectOfType<UIManager>();
    }
    private void Start()
    {
        uiManager.UpdateScore(0);   // 시작할때는 0점
    }

    // 게임 오버
    public void GameOver()
    {
        Debug.Log("Game Over");
        uiManager.SetRestart(); // Restart 텍스트 띄운다
    }
    // 재시작
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void AddScore(int score)
    {
        currentScore += score;
        Debug.Log("Score: " + currentScore);
        uiManager.UpdateScore(currentScore);    // 점수 추가
    }
}
