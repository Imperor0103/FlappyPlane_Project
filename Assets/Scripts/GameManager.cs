using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// ���ӿ���, �����, ����
public class GameManager : MonoBehaviour
{
    // singleton ��ü�� ����� ���� ���
    static GameManager gameManager; // 1.�ڽ��� �����ϴ� static ����
    public static GameManager Instance { get { return gameManager; } }  // 2.�� ������ �ܺη� ������ �� �ִ� ������Ƽ 1��

    private int currentScore = 0;

    private void Awake()
    {
        gameManager = this; // 3.������ ��ü�� �������ִ� �۾�
    }
    // ���� ����
    public void GameOver()
    {
        Debug.Log("Game Over");
    }
    // �����
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void AddScore(int score)
    {
        currentScore += score;
        Debug.Log("Score: " + currentScore);
    }
}
