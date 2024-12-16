namespace Settings
{
    /// <summary>
    /// Object class to store the settings objects with Information like
    /// max volume, bgm and bullet sound
    /// </summary>
    /// <para name="author">Kevin von Ballmoos</para>
    /// <para name="date">07.12.2024</para>
    [System.Serializable]
    public class SettingsObject
    {
        public float MaxVolume { get; set; }
        public bool BackGroundMusicEnabled { get; set; }
        public bool BulletSoundEnabled { get; set; }
    }
}