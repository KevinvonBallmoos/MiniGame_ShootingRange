using System;
using System.IO;
using System.Text;
using Newtonsoft.Json;
using UnityEngine;

namespace Settings
{
    /// <summary>
    /// JsonController handles save and load of data involving Json Files
    /// </summary>
    /// <para name="author">Kevin von Ballmoos</para>
    /// <para name="date">07.12.2024</para>
    public static class SettingsJsonController
    {
        // Save path
        private static readonly string SavePath = Application.persistentDataPath + "/Settings.json";

        #region Settings

        /// <summary>
        /// Saves the settings in a json file
        /// </summary>
        public static void SaveSettings()
        {
            var settingObject = new SettingsSerializeObject(
                SettingsViewModel.IsEasterEggFound, 
                SettingsViewModel.Settings,
                SettingsViewModel.PlayerName);
            var json = JsonConvert.SerializeObject(settingObject, Formatting.Indented);
            File.WriteAllText(SavePath, json);
        }
        
        /// <summary>
        /// Loads the settings from the json file
        /// </summary>
        /// <returns>File serialize object</returns>
        public static SettingsSerializeObject LoadSettings()
        {
            try
            {
                var json = File.ReadAllText(SavePath, Encoding.UTF8);
                return JsonConvert.DeserializeObject<SettingsSerializeObject>(json);
            }
            catch (Exception)
            {
                return new SettingsSerializeObject(false, 
                    new SettingsObject
                    {
                        MaxVolume = 100, 
                        BackGroundMusicEnabled = true,
                        BulletSoundEnabled = true
                    }, 
                    "Player 1");
            }
        }
        
        #endregion
    }
}