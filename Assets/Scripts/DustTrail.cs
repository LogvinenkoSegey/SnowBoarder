using UnityEngine;

public class DustTrail : MonoBehaviour
{
    [SerializeField] ParticleSystem DustParticles;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("LevelShape"))
            DustParticles.Play();
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("LevelShape"))
            DustParticles.Stop();
    }
}
