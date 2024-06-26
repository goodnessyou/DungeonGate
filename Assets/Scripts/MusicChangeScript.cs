using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicChangeScript : MonoBehaviour
{
    public AudioManager audioManager;

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.CompareTag("PlayerTrigger"))
        {
            audioManager.Stop("LightAmbient");
            audioManager.Play("BossRoom");
            audioManager.Play("BossTheme");
        }
    }
    private void OnTriggerExit(Collider other) {
        if (other.gameObject.CompareTag("PlayerTrigger"))
        {
            audioManager.Stop("BossTheme");
            audioManager.Play("LightAmbient");
        }
    }
}
