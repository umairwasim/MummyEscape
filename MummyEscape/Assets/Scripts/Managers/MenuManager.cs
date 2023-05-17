using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip buttonClickSfx;
    [SerializeField] private Image fadeInImage;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        fadeInImage.gameObject.SetActive(false);
    }

    public void StartGame()
    {
        audioSource.PlayOneShot(buttonClickSfx);
        fadeInImage.gameObject.SetActive(true);
        fadeInImage.DOFade(1f, 2f)
             .OnComplete(() =>
         {
             SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
         });
    }
}
