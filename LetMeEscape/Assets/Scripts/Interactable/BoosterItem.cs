using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoosterItem : Item
{
    public int AddingSeconds;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            SoundManager.Instance.PlaySFX(SFXType.BoostItem);
            PlayerController.Instance.AddPlayerSpeed(AddingSeconds);
            Destroy(this.gameObject);
        }
    }
}
