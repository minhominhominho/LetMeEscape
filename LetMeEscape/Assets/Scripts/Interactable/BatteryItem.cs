using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatteryItem : Item
{
    public int AddingSeconds;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && GameManager.Instance.IsGameRunning())
        {
            SoundManager.Instance.PlaySFX(SFXType.BoostItem);
            PlayerController.Instance.AddPlayerSpeedToZeroSec(AddingSeconds);
            Destroy(this.gameObject);
        }
    }
}
