using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishLine : MonoBehaviour
{
    [SerializeField] float FinishDelay = 2f;
    [SerializeField] int NextLevel;
    [SerializeField] ParticleSystem FinishEffect;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            FinishEffect.Play();
            GetComponent<AudioSource>().Play();
            Invoke(nameof(LoadNextLevel), FinishDelay);
        }

    }

    private void LoadNextLevel() => 
        SceneManager.LoadScene(NextLevel - 1);
}
