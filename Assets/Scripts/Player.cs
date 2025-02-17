using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
using UnityEditor;
using UnityEngine;

public class Player : MonoBehaviour
{
    Animator animator;
    Rigidbody2D _rigidbody;

    public float flapForce = 6f;    // ������
    public float fowrardSpeed = 3f; // �����̵�
    public bool isDead = false;
    float deathCooldown = 0f;   // �浹�ϰ� ���� �ð� �Ŀ� �״´�

    bool isFlap = false;    // ������ �پ����� �ȶپ�����

    public bool godMode = false;    // �׽�Ʈ ���

    GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameManager.Instance; // Awake���� ȣ���ϸ� ���� �� ������ Start���� ȣ��

        animator = GetComponentInChildren<Animator>();    // �ڽ��� Model�� ������ �ִ�
        _rigidbody = GetComponent<Rigidbody2D>();
        if (animator == null)
        {
            Debug.LogError("Not Founded animator");
        }
        if (_rigidbody == null)
        {
            Debug.LogError("Not Founded Rigidbody");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isDead)
        {
            if (deathCooldown <= 0)
            {
                // ���� �����
                if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
                {
                    gameManager.RestartGame();
                }

            }
            else
            {
                // deathCooldown�� �����ִٸ� �� ������ ������ �ð���ŭ ����
                deathCooldown -= Time.deltaTime;    // Time.deltaTime: ���� �������� Update�� ���� �������� Update ������ �ð� ����
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
            {
                isFlap = true;
            }
        }
    }
    // ���� ó��
    private void FixedUpdate()
    {
        if (isDead) return;

        Vector3 velocity = _rigidbody.velocity; // rigidbody�� ���������� �ް� �ִ� ��

        velocity.x = fowrardSpeed;

        if (isFlap)
        {
            velocity.y += flapForce;
            // Vector3�� ����ü�� ���� velocity�� ����Ǿ��� ��, _rigidbody.velocity�� ���� �ٲ�� ���� �ƴϴ�
            isFlap = false;
        }
        _rigidbody.velocity = velocity;
        float angle = Mathf.Clamp((_rigidbody.velocity.y) * 10f, -90, 90);
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (godMode) return;        // �׽�Ʈ ���
        if (isDead) return;

        isDead = true;
        deathCooldown = 1f; // 1�� �� ����� ����
        animator.SetInteger("IsDie", 1);

        gameManager.GameOver();
    }
}
