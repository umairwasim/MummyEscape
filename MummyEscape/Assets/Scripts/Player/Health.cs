using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private int health = 100;

    public void TakeDamage(int damage)
    {
        health -= damage;

        FeedbackPopUp.CreateFeedback(transform.position, damage.ToString());
        AudioManager.Instance.PlaySound(AudioManager.Instance.damageSfx);
        CameraShake.Instance.Shake(5f, 0.25f);

        if (health <= 0)
        {
            health = 0;
            Die();
        }

        UIManager.Instance.SetHealth(health);
        UIManager.Instance.UpdateUI();
    }

    private void Die()
    {
        GameManager.Instance.ChangeState(GameState.Lost);
        AudioManager.Instance.PlaySound(AudioManager.Instance.failedSfx);
    }
}
