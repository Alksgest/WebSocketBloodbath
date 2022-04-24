using Constants;
using Models;
using UnityEngine;

namespace Managers
{
    public static class SettingsManager
    {
        public static GameSettings GameSettings => GetGameSettings();
        
        public static void BatchSave(GameSettings settings)
        {
            SaveUsername(settings.Username);
            SaveServerUrl(settings.ServerUrl);
            
            PlayerPrefs.Save();
        }
        
        /// <summary>
        /// Method is using for saving name in batch save. 
        /// If you want to save single field, forceSave should equal true
        /// </summary>
        /// <param name="name"></param>
        /// <param name="forceSave"></param>
        public static void SaveUsername(string name, bool forceSave = false)
        {
            PlayerPrefs.SetString(SettingsName.Username, name);
            
            if (forceSave)
            {
                PlayerPrefs.Save();
            }
        }

        /// <summary>
        /// Method is using for saving server url in batch save. 
        /// If you want to save single field, forceSave should equal true
        /// </summary>
        /// <param name="url"></param>
        /// <param name="forceSave"></param>
        public static void SaveServerUrl(string url, bool forceSave = false)
        {
            PlayerPrefs.SetString(SettingsName.ServerUrl, url);
            
            if (forceSave)
            {
                PlayerPrefs.Save();
            }
        }

        private static GameSettings GetGameSettings()
        {
            return new GameSettings
            {
                Username = PlayerPrefs.GetString(SettingsName.Username),
                ServerUrl = PlayerPrefs.GetString(SettingsName.ServerUrl),
            };
        }
    }
}