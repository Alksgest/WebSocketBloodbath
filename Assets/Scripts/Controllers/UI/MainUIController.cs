using Constants;
using Managers;
using Models;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

namespace Controllers.UI
{
    public class MainUIController : MonoBehaviour
    {
        public static MainUIController Instance;

        public bool IsUIBlocking => mainMenu.activeSelf || optionsMenu.activeSelf;
        
        [SerializeField] private GameObject mainMenu;
        [SerializeField] private GameObject optionsMenu;
        [SerializeField] private GameObject inventory;
        
        [SerializeField] private TMP_InputField nameInput;
        [SerializeField] private TMP_InputField urlInput;

        private UIInputActions _uiActions;
        
        private void Awake()
        {
            Instance = this;
            _uiActions = new UIInputActions();
            _uiActions.Player.Menu.started += OpenMenu;
            _uiActions.Player.Inventory.started += OpenInventory;
            DontDestroyOnLoad(gameObject);
        }

        private void OpenInventory(InputAction.CallbackContext obj)
        {
            inventory.SetActive(!inventory.activeSelf);
        }

        private void OpenMenu(InputAction.CallbackContext obj)
        {
            if (optionsMenu.activeSelf)
            {
                mainMenu.SetActive(true);
                optionsMenu.SetActive(false);
                return;
            }

            if (SceneManager.GetActiveScene().buildIndex != SceneNumber.MainMenuScene)
            {
                mainMenu.SetActive(!mainMenu.activeSelf);
            }
        }

        private void OnEnable()
        {
            _uiActions.Enable();
        }

        private void OnDisable()
        {
            _uiActions.Disable();
        }


        private void Start()
        {
            var settings = SettingsManager.GameSettings;

            nameInput.text = settings.Username;
            urlInput.text = settings.ServerUrl;
        }
        
        public void ToMainMenu()
        {
            if (SceneManager.GetActiveScene().buildIndex != SceneNumber.MainMenuScene)
            {
                GameManager.Instance.CloseSession();
                SceneManager.LoadScene(SceneNumber.MainMenuScene);
            }
        }

        public void StartGame()
        {
            mainMenu.SetActive(false);
            SceneManager.LoadScene(SceneNumber.SandBox);
        }

        public void SetActiveMainMenu(bool active)
        {
            if (SceneManager.GetActiveScene().buildIndex != SceneNumber.MainMenuScene)
            {
                mainMenu.SetActive(!mainMenu.activeSelf);
            }
        }

        public void ApplySettings()
        {
            var username = nameInput.text;
            var url = urlInput.text;
            
            SettingsManager.BatchSave(new GameSettings
            {
                Username = username,
                ServerUrl = url
            });
            
            optionsMenu.SetActive(false);
            mainMenu.SetActive(true);
        }
        
        public void Exit()
        {
            Application.Quit(0);
        }
    }
}