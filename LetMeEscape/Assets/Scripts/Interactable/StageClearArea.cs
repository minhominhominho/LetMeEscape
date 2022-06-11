using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageClearArea : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            SoundManager.Instance.PlaySFX(SFXType.GameClear);
            GameManager.Instance.CallGameClear();
        }
    }
}
