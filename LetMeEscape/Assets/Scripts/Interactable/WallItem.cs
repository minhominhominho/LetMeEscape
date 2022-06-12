using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallItem : Item
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && GameManager.Instance.IsGameRunning())
        {
            SoundManager.Instance.PlaySFX(SFXType.WallItem);
            WallCreater.Instance.GetWallItem();
            Destroy(this.gameObject);
        }
    }
}
