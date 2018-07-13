using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Assets.Menu
{
    public class MenuManager : MonoBehaviour
    {
        private Resolution[] availableResolutions;
        public Dropdown ResolutionDropdown;
        public Dropdown QualityDropdown;
        public Dropdown ScreenModeDropdown;
        public AudioMixer AudioMixer;
        public RectTransform MainMenuPanel;
        public RectTransform OptionsPanel;

        private void Awake()
        {
            InitResolutionDropdown();
            InitQualityDropdown();
            InitScreenModeDropdown();
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (OptionsPanel.gameObject.activeSelf)
                {
                    MainMenuPanel.gameObject.SetActive(true);
                    OptionsPanel.gameObject.SetActive(false);
                }
            }
        }

        public void LoadLevel()
        {
            SceneManager.LoadSceneAsync("DummyGameScene", LoadSceneMode.Single);
        }

        public void Quit()
        {
#if UNITY_EDITOR
            EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
        }

        public void SetQuality(int qualityIndex)
        {
            QualitySettings.SetQualityLevel(qualityIndex);
        }

        public void SetResolution(int resolutionIndex)
        {
            Resolution resolution = availableResolutions[resolutionIndex];
            Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
        }

        public void SetScreenMode(int screenModeIndex)
        {
            Screen.fullScreenMode = (FullScreenMode)screenModeIndex;
        }

        public void SetCommonVolume(float volume)
        {
            AudioMixer.SetFloat("volume", volume);
        }

        private void InitScreenModeDropdown()
        {
            if (ScreenModeDropdown == null)
                return;

            var options = new List<string>();
            foreach (FullScreenMode mode in (FullScreenMode[])Enum.GetValues(typeof(FullScreenMode)))
                options.Add(mode.ToString());

            ScreenModeDropdown.ClearOptions();

            ScreenModeDropdown.AddOptions(options);
            ScreenModeDropdown.value = (int)Screen.fullScreenMode;
            ScreenModeDropdown.RefreshShownValue();
        }

        private void InitQualityDropdown()
        {
            if (QualityDropdown == null)
                return;

            QualityDropdown.ClearOptions();
            QualityDropdown.AddOptions(QualitySettings.names.ToList());
            QualityDropdown.value = QualitySettings.GetQualityLevel();
            QualityDropdown.RefreshShownValue();
        }

        private void InitResolutionDropdown()
        {
            if (ResolutionDropdown == null)
                return;

            int currentResolutionIndex = 0;
            var resolutionOptions = new List<string>();
            availableResolutions = Screen.resolutions;        

            for (int i = 0; i < availableResolutions.Length; i++)
            {
                resolutionOptions.Add(availableResolutions[i].ToString());

                if (availableResolutions[i].Equals(Screen.currentResolution))
                    currentResolutionIndex = i;
            }

            ResolutionDropdown.ClearOptions();
            ResolutionDropdown.AddOptions(resolutionOptions);
            ResolutionDropdown.value = currentResolutionIndex;
            ResolutionDropdown.RefreshShownValue();
        }
    }

    public static class ResolutionExtension
    {
        public static bool Equals(this Resolution thisResolution, Resolution resolution)
        {
            return thisResolution.width == resolution.width &&
                thisResolution.height == resolution.height;
        }
    }
}