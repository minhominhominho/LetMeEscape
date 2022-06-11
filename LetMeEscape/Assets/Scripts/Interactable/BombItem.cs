using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombItem : Item
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            SoundManager.Instance.PlaySFX(SFXType.PlayerDeath);
            GameManager.Instance.CallGameOver();
            Destroy(this);
        }
    }
}
