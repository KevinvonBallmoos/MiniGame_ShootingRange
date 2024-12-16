using TMPro;
using UnityEngine;

namespace Menu
{
    /// <summary>
    /// TutorialHandler handles scrolling between the tutorial pages
    /// </summary>
    /// <para name="author">Kevin von Ballmoos</para>
    /// <para name="date">07.12.2024</para>
    public class TutorialHandler : MonoBehaviour
    {
        // Pages
        [SerializeField] private GameObject[] _tutorialPages;
        [SerializeField] private TextMeshProUGUI _textPageNumber;

        private int pageIndex;
        
        #region Update
        
        /// <summary>
        /// Update is called once per frame
        /// </summary>
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.A))
            {
                ScrollThroughTutorial("A");
            }
            else if (Input.GetKeyDown(KeyCode.D))
            {
                ScrollThroughTutorial("D");
            }
        }

        /// <summary>
        /// Scrolls to the next or last page
        /// </summary>
        /// <param name="keyName">Key that was pressed</param>
        private void ScrollThroughTutorial(string keyName)
        {
            switch (keyName)
            {
                case "A":
                    switch (pageIndex)
                    {
                        case 1:
                            _tutorialPages[1].SetActive(false);
                            _tutorialPages[0].SetActive(true);
                            pageIndex = 0;
                            break;
                        case 2:
                            _tutorialPages[2].SetActive(false);
                            _tutorialPages[1].SetActive(true);
                            pageIndex = 1;
                            break;
                    }
                    break;
                case "D":
                    switch (pageIndex)
                    {
                        case 0:
                            _tutorialPages[0].SetActive(false);
                            _tutorialPages[1].SetActive(true);
                            pageIndex = 1;
                            break;
                        case 1:
                            _tutorialPages[1].SetActive(false);
                            _tutorialPages[2].SetActive(true);
                            pageIndex = 2;
                            break;
                    }
                    break;
            }
            _textPageNumber.text = $"{pageIndex + 1} / 3";
        }
        
        #endregion
    }
}