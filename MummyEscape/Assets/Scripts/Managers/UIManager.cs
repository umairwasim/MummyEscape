using TMPro;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using DG.Tweening;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    public Transform feedbackPopUp;

    [Header("Gameplay UI")]
    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private TMP_Text healthText;
    [SerializeField] private int score = 0;
    [SerializeField] private int health = 100;

    [Header("Panels")]
    [SerializeField] private GameObject levelWonPanel;
    [SerializeField] private GameObject levelLostPanel;
    [SerializeField] private GameObject gameCompletedPanel;

    [SerializeField] private Image faderImage;

    private readonly float scaleDuration = 1f;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        faderImage.DOFade(0, 5f).OnComplete(() =>
        {
            faderImage.gameObject.SetActive(false);
        });
    }

    #region Helper Functions

    public int GetScore() => score;

    public int GetHealth() => health;

    public int SetHealth(int amount) => health = amount;

    public int SetScore(int amount) => score += amount;

    #endregion

    #region Update UI

    public void Init()
    {
        scoreText.text = "Score " + 0;
        healthText.text = "Health " + 100;
    }

    public void UpdateUI()
    {
        scoreText.text = "Score " + GetScore();
        healthText.text = "Health " + GetHealth();
    }

    #endregion

    #region Panels

    public void ShowLevelLostPanel()
    {
        levelLostPanel.SetActive(true);
        levelLostPanel.transform
            .DOScale(Vector3.one, scaleDuration)
            .SetEase(Ease.InQuad);
    }

    public void ShowLevelWonPanel()
    {
        levelWonPanel.SetActive(true);
        levelWonPanel.transform
            .DOScale(Vector3.one, scaleDuration)
            .SetEase(Ease.InBounce);
    }

    public void ShowGameCompletedPanel()
    {
        gameCompletedPanel.SetActive(true);
        gameCompletedPanel.transform
            .DOScale(Vector3.one, scaleDuration)
            .SetEase(Ease.InElastic);
    }

    #endregion
}
