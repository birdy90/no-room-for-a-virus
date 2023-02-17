using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace UI
{
    public class MainGameMenu : MonoBehaviour
    {
        [SerializeField] private Button NewGameButton;
        [SerializeField] private Button ExitButton;

        private void Awake()
        {
            NewGameButton.onClick.AddListener(() =>
            {
                SceneManager.LoadScene(Constants.GameSceneName);
            });
            ExitButton.onClick.AddListener(() =>
            {
                Application.Quit();
            });
        }

        private void OnDestroy()
        {
            NewGameButton.onClick.RemoveAllListeners();
        }
    }
}
