using Manager;

namespace Settings
{
    /// <summary>
    /// SettingsViewModel handles data between controller and ui
    /// </summary>
    /// <para name="author">Kevin von Ballmoos</para>
    /// <para name="date">07.12.2024</para>
    public static class SettingsViewModel
    {
        public static string PlayerName { get; set; }
        public static bool IsEasterEggFound { get; set; }

        public static SettingsObject Settings { get; set; } = new ();
        
        /// <summary>
        /// Loads the settings from the 
        /// </summary>
        public static void LoadSettings()
        {
            var settings = SettingsJsonController.LoadSettings();
            
            IsEasterEggFound = settings.IsEasterEggFound;
            PlayerName = settings.PlayerName;
            
            Settings.MaxVolume = settings.Settings.MaxVolume;
            Settings.BackGroundMusicEnabled = settings.Settings.BackGroundMusicEnabled;
            Settings.BulletSoundEnabled = settings.Settings.BulletSoundEnabled;
        }
    }
}