using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private int health = 100;

    public void TakeDamage(int damage)
    {
        health -= damage;

        FeedbackPopUp.CreateFeedback(transform.position, damage.ToString());

        if (health <= 0)
        {
            health = 0;
            Die();
        }

        UIManager.Instance.SetHealth(health);
        UIManager.Instance.UpdateUI();
        AudioManager.Instance.PlaySound(AudioManager.Instance.damageSfx);
        CameraShake.Instance.Shake(5f, 0.25f);
    }

    private void Die()
    {
        GameManager.Instance.ChangeState(GameState.Lost);
        AudioManager.Instance.PlaySound(AudioManager.Instance.failedSfx);
    }
}
