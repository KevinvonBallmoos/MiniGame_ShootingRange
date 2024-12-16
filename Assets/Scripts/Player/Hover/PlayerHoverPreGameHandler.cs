using InfoLabel;
using Player.Hover.Base;
using UnityEngine;

namespace Player.Hover
{
    /// <summary>
    /// This class handles the hover over game objects
    /// Weapon select and quit or start game
    /// </summary>
    /// <para name="author">Kevin von Ballmoos</para>
    /// <para name="date">17.11.2024</para>
    public class PlayerHoverPreGameHandler : PlayerHoverHandlerBase
    {
        #region OnPointer Methods
        
        /// <summary>
        /// Mouse entered the object
        /// Updates the according label
        /// </summary>
        /// <param name="hitObject">The object that was hit</param>
        protected override void OnPointerEnter(GameObject hitObject)
        {
            switch (hitObject.name)
            {
                case "QuitButton": 
                    InfoLabelHandler.Instance.SetInfoLabelText("Left", "You going somewhere? \n:(");
                    break;
                case "StartButton": 
                    InfoLabelHandler.Instance.SetInfoLabelText("Right","Are you ready? :)");
                    break;
                case "CubeGun": 
                    InfoLabelHandler.Instance.SetInfoLabelText("Front", "CUBE GUN \n[Bullets: Cubes]");
                    break;
                case "SphereGun": 
                    InfoLabelHandler.Instance.SetInfoLabelText("Front", "Sphere Gun \n[Bullets: Spheres]");
                    break;
            }
        }

        /// <summary>
        /// Mouse exited the object
        /// Updates the according label
        /// </summary>
        /// <param name="hitObject">The object that was hit</param>
        protected override void OnPointerExit(GameObject hitObject)
        {
            switch (hitObject.name)
            {
                case "QuitButton": 
                    InfoLabelHandler.Instance.SetInfoLabelText("Left", "/ / / / / / / / /");
                    break;
                case "StartButton": 
                    InfoLabelHandler.Instance.SetInfoLabelText("Right", @"\ \ \ \ \ \ \ \ \ ");
                    break;
                case "CubeGun": 
                    InfoLabelHandler.Instance.SetInfoLabelText("Front", "Hi :)\n\n<- Press E to select a Weapon ->");
                    break;
                case "SphereGun": 
                    InfoLabelHandler.Instance.SetInfoLabelText("Front", "Hi :)\n\n<- Press E to select a Weapon ->");
                    break;
            }
        }
        
        #endregion
    }
}