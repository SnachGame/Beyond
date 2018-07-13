using System.Linq;
using UnityEngine;

namespace Assets.Menu
{
    public class MenuStarter : MonoBehaviour
    {
        private GameObject menu;
        private Canvas canvas;
        private RectTransform mainMenuPanel;
        private RectTransform optionsPanel;
        private RectTransform graphicsPanel;
        private RectTransform soundPanel;
        private RectTransform controlsPanel;

        private void Start()
        {
            menu = Instantiate(Resources.Load("MenuUI")) as GameObject;
            menu.SetActive(false);

            InitUiElements();

            canvas.sortingOrder = 1;
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (menu.activeSelf)
                {
                    menu.SetActive(false);
                    Time.timeScale = 0;
                }
                else
                {
                    menu.SetActive(true);
                    Time.timeScale = 1;

                    SetDefaultActive();
                }
            }       
        }

        private void SetDefaultActive()
        {
            mainMenuPanel.gameObject.SetActive(true);
            optionsPanel.gameObject.SetActive(false);
            graphicsPanel.gameObject.SetActive(true);
            soundPanel.gameObject.SetActive(false);
            controlsPanel.gameObject.SetActive(false);
        }

        private void InitUiElements()
        {
            canvas = menu.GetComponent<Canvas>();

            RectTransform[] rectTransform = canvas.GetComponentsInChildren<RectTransform>(true);

            mainMenuPanel = rectTransform.FirstOrDefault(x => x.name == "MainMenuPanel");
            optionsPanel = rectTransform.FirstOrDefault(x => x.name == "OptionsPanel");
            graphicsPanel = rectTransform.FirstOrDefault(x => x.name == "GraphicsPanel");
            soundPanel = rectTransform.FirstOrDefault(x => x.name == "SoundPanel");
            controlsPanel = rectTransform.FirstOrDefault(x => x.name == "ControlsPanel");
        }
    }
}