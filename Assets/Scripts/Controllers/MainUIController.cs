﻿using System;
using Constants;
using Managers;
using Models;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Controllers
{
    public class MainUIController : MonoBehaviour
    {
        [SerializeField] private GameObject mainMenu;
        [SerializeField] private GameObject optionsMenu;
        
        [SerializeField] private TMP_InputField nameInput;
        [SerializeField] private TMP_InputField urlInput;
        
        private void Start()
        {
            var settings = SettingsManager.GameSettings;

            nameInput.text = settings.Username;
            urlInput.text = settings.ServerUrl;
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape) &&
                SceneManager.GetActiveScene().buildIndex != SceneNumber.MainMenuScene)
            {
                mainMenu.SetActive(!mainMenu.activeSelf);
            }
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