using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance;
    private Rigidbody2D rigid2D;
    private float playerCurrentSpeed;
    public float PlayerCurrentSpeed
    {
        get
        {
            return playerCurrentSpeed;
        }
    }
    private float speedToZeroSec;
    private Vector2 playerDirection;


    void Start()
    {
        if (Instance == null) { Instance = this; }
        else { Destroy(gameObject); }

        rigid2D = GetComponent<Rigidbody2D>();
        playerDirection = -transform.up;
        playerCurrentSpeed = GameManager.Instance.playerStartSpeed;
        speedToZeroSec = GameManager.Instance.speedToZeroSec;
        rigid2D.velocity = playerDirection * playerCurrentSpeed;
    }

    void Update()
    {
        if (GameManager.Instance.IsGameRunning())
        {
            if (playerCurrentSpeed <= 0)
            {
                rigid2D.velocity = Vector2.zero;
                GameManager.Instance.CallGameOver();
            }
            else
            {
                playerCurrentSpeed -= GameManager.Instance.playerStartSpeed / speedToZeroSec * Time.deltaTime;
                rigid2D.velocity = playerDirection * playerCurrentSpeed;
            }

            float angle = Mathf.Atan2(playerDirection.y, playerDirection.x) * Mathf.Rad2Deg;
            Quaternion rotation = Quaternion.AngleAxis(angle + 90f, Vector3.forward);
            transform.rotation = rotation;
        }
        else
        {
            rigid2D.velocity = Vector2.zero;
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (GameManager.Instance.IsGameRunning())
        {
            SoundManager.Instance.PlaySFX(SFXType.Reflection);
            Vector2 normalVec = other.contacts[0].normal;
            playerDirection = Vector3.Reflect(playerDirection, normalVec);

            float angle = Mathf.Atan2(playerDirection.y, playerDirection.x) * Mathf.Rad2Deg;
            Quaternion rotation = Quaternion.AngleAxis(angle + 90f, Vector3.forward);
            transform.rotation = rotation;

            playerDirection.Normalize();
            rigid2D.velocity = playerDirection * playerCurrentSpeed;
        }
    }

    public void AddPlayerSpeed(int second)
    {
        playerCurrentSpeed += second;
    }

    public void AddPlayerSpeedToZeroSec(int second)
    {
        speedToZeroSec += second;
    }
}
