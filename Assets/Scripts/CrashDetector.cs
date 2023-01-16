using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CrashDetector : MonoBehaviour
{
    [SerializeField] float CrashDelay = 2f;
    [SerializeField] int CurrentLevel;
    [SerializeField] ParticleSystem CrashEffect;
    [SerializeField] AudioClip CrashSFX;

    private bool isCrashed;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("LevelShape") && !isCrashed)
        {
            isCrashed = true;
            FindObjectOfType<PlayerController>().DisableControls();
            CrashEffect.Play();
            GetComponent<AudioSource>().PlayOneShot(CrashSFX);
            Invoke(nameof(ReloadScene), CrashDelay);
        }
    }

    private void ReloadScene() => 
        SceneManager.LoadScene(CurrentLevel - 1);
} 
  