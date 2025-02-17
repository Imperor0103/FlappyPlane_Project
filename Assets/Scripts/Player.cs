using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
using UnityEditor;
using UnityEngine;

public class Player : MonoBehaviour
{
    Animator animator;
    Rigidbody2D _rigidbody;

    public float flapForce = 6f;    // 점프력
    public float fowrardSpeed = 3f; // 정면이동
    public bool isDead = false;
    float deathCooldown = 0f;   // 충돌하고 일정 시간 후에 죽는다

    bool isFlap = false;    // 점프를 뛰었는지 안뛰었는지

    public bool godMode = false;    // 테스트 모드

    GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameManager.Instance; // Awake에서 호출하면 꼬일 수 있으니 Start에서 호출

        animator = GetComponentInChildren<Animator>();    // 자식인 Model이 가지고 있다
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
                // 게임 재시작
                if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
                {
                    gameManager.RestartGame();
                }

            }
            else
            {
                // deathCooldown이 남아있다면 두 프레임 사이의 시간만큼 뺀다
                deathCooldown -= Time.deltaTime;    // Time.deltaTime: 이전 프레임의 Update와 다음 프레임의 Update 사이의 시간 간격
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
    // 물리 처리
    private void FixedUpdate()
    {
        if (isDead) return;

        Vector3 velocity = _rigidbody.velocity; // rigidbody가 물리적으로 받고 있는 힘

        velocity.x = fowrardSpeed;

        if (isFlap)
        {
            velocity.y += flapForce;
            // Vector3는 구조체라서 값이 velocity에 복사되었을 뿐, _rigidbody.velocity의 값이 바뀌는 것은 아니다
            isFlap = false;
        }
        _rigidbody.velocity = velocity;
        float angle = Mathf.Clamp((_rigidbody.velocity.y) * 10f, -90, 90);
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (godMode) return;        // 테스트 모드
        if (isDead) return;

        isDead = true;
        deathCooldown = 1f; // 1초 후 재시작 가능
        animator.SetInteger("IsDie", 1);

        gameManager.GameOver();
    }
}
