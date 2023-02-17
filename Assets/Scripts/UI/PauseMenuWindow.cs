using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Utils;

namespace UI
{
    public class PauseMenuWindow : MonoBehaviour
    {
        [SerializeField] private GameStatisticsController Statistics;
        [SerializeField] private TextMeshProUGUI StatisticsText;
        [SerializeField] private Button UnpauseButton;
        [SerializeField] private Button RestartButton;
        [SerializeField] private Button MainMenuButton;

        private void Awake()
        {
            if (UnpauseButton)
            {
                UnpauseButton.onClick.AddListener(() =>
                {
                    GameFlow.Unpause();
                    gameObject.SetActive(false);
                });
            }

            RestartButton.onClick.AddListener(() =>
            {
                GameFlow.Unpause();
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            });
            MainMenuButton.onClick.AddListener(() =>
            {
                GameFlow.Unpause();
                SceneManager.LoadScene("Scenes/MainMenu");
            });
        }

        private void OnDestroy()
        {
            if (UnpauseButton)
            {
                UnpauseButton.onClick.RemoveAllListeners();
            }

            RestartButton.onClick.RemoveAllListeners();
            MainMenuButton.onClick.RemoveAllListeners();
        }

        private void OnEnable()
        {
            StatisticsText.text = Statistics.ToString();
        }
    }
}
