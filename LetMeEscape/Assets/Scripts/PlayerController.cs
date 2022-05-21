using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rigid2D;
    public float playerSpeed;
    private Vector2 playerMovement;

    void Start()
    {
        rigid2D = GetComponent<Rigidbody2D>();
        playerMovement = -transform.up * playerSpeed;
        rigid2D.velocity = playerMovement;
    }

    void Update()
    {
        rigid2D.velocity = playerMovement;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        playerMovement = Vector3.Reflect(playerMovement, other.contacts[0].normal);
        rigid2D.velocity = playerMovement;
    }
}
