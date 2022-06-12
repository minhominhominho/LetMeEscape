using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float fasterAmountThanPlayer;
    public float respawnTime;
    private float enemySpeed;
    private SpriteRenderer sprite;
    private CircleCollider2D col2d;

    private void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        col2d = GetComponent<CircleCollider2D>();
        sprite.enabled = false;
        col2d.isTrigger = true;
        sprite.color = new Color(sprite.color.r, sprite.color.g, sprite.color.b, 0);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && GameManager.Instance.IsGameRunning())
        {
            SoundManager.Instance.PlaySFX(SFXType.EnemySpawning);
            sprite.enabled = true;
            StartCoroutine(turnOnSpriteRender());
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.collider.CompareTag("Wall") && GameManager.Instance.IsGameRunning())
        {
            SoundManager.Instance.PlaySFX(SFXType.EnenmyDeath);
            Destroy(gameObject);
        }
        else if (other.collider.CompareTag("Player") && GameManager.Instance.IsGameRunning())
        {
            SoundManager.Instance.PlaySFX(SFXType.PlayerDeath);
            GameManager.Instance.CallGameOver();
        }
    }

    private IEnumerator turnOnSpriteRender()
    {
        Vector2 direction = (Vector2)PlayerController.Instance.transform.position - (Vector2)transform.position;
        float angle = Mathf.Atan2(direction.x, direction.y) * Mathf.Rad2Deg;
        Quaternion targetRotation = Quaternion.Euler(0, 0, angle + 180f);
        Quaternion startRotation = transform.rotation;
        float time = 0;

        Color c = sprite.color;

        while (sprite.color.a < 1)
        {
            transform.rotation = Quaternion.Slerp(startRotation, targetRotation, time);
            time += 0.01f;

            c = sprite.color + new Color(0, 0, 0, 0.01f);
            sprite.color = c;

            yield return new WaitForSeconds(0.01f * respawnTime);
        }

        col2d.isTrigger = false;
        enemySpeed = PlayerController.Instance.PlayerCurrentSpeed + fasterAmountThanPlayer;
    }

    private void Update()
    {
        if (!col2d.isTrigger)
        {
            transform.position += -transform.up * Time.deltaTime * enemySpeed;
        }
    }
}
