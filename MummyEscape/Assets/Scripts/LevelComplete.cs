using UnityEngine;

public class LevelComplete : MonoBehaviour
{
    private const string PLAYER = "Player";
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(PLAYER))
        {
            GameManager.Instance.ChangeState(GameState.Won);
            AudioManager.Instance.PlaySound(AudioManager.Instance.damageSfx);
        }
    }
}
