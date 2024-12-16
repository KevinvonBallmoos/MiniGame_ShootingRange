
namespace Settings
{
    /// <summary>
    /// Object class to cache file objects for serializing it into a json file
    /// </summary>
    /// <para name="author">Kevin von Ballmoos</para>
    /// <para name="date">07.12.2024</para>
    public class SettingsSerializeObject
    {
        public readonly string PlayerName;
        public readonly bool IsEasterEggFound;
        public readonly SettingsObject Settings;

        /// <summary>
        /// Constructor to set the settings
        /// </summary>
        /// <param name="isEasterEggFound">True if the Easter egg was found, else false</param>
        /// <param name="settings">The settings object</param>
        /// <param name="playerName">The name of the player</param>
        public SettingsSerializeObject(bool isEasterEggFound, SettingsObject settings, string playerName)
        {
            PlayerName = playerName;
            IsEasterEggFound = isEasterEggFound;
            Settings = settings;
        }
    }
}