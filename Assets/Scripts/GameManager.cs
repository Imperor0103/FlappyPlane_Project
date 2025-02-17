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

    // UI�Ŵ���
    UIManager uiManager;
    public UIManager UIManager { get { return uiManager; } }    // UIManager �� �ܺη� ������ �� �ִ� ������Ƽ

    private void Awake()
    {
        gameManager = this; // 3.������ ��ü�� �������ִ� �۾�

        // UIManager�� �̱��� ��ü�� �ƴϹǷ� FindObjectsOfType���� ã�´�
        uiManager = FindObjectOfType<UIManager>();
    }
    private void Start()
    {
        uiManager.UpdateScore(0);   // �����Ҷ��� 0��
    }

    // ���� ����
    public void GameOver()
    {
        Debug.Log("Game Over");
        uiManager.SetRestart(); // Restart �ؽ�Ʈ ����
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
        uiManager.UpdateScore(currentScore);    // ���� �߰�
    }
}
