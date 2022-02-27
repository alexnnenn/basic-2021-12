using Assets.Scripts.Managers;
using Assets.Scripts.ScriptableObjectsProtos;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Assets.Scripts.Menus
{
    public class PlaySettingsListener : MonoBehaviour
    {
        public Slider _musicSlider;
        public Slider _soundSlider;
        public GameSettings _settings;
        public AudioMixer _mixer;

        public void OnShownHidden()
        {
            if (DialogsController.Instance.IsShown(DialogType.PlaySettings))
            {
                Time.timeScale = 0f;
                _musicSlider.value = _mixer.GetFloat("Music", out var musicMixerValue) ? musicMixerValue : 0;
                _soundSlider.value = _mixer.GetFloat("Sound", out var soundMixerValue) ? soundMixerValue : 0;
            }
        }

        public void OnRestartClicked()
        {
            Time.timeScale = 1f;
            TransitionManager.ReloadScene();
            DialogsController.Instance.Show(DialogType.None);
        }

        public void OnMenuClicked()
        {
            Time.timeScale = 1f;
            DialogsController.Instance.Show(DialogType.MainMenu);
            SceneManager.LoadScene("MainMenu");
        }

        public void OnCloseClicked()
        {
            Time.timeScale = 1f;
            DialogsController.Instance.Show(DialogType.None);
        }

        public void OnMusicVolumeChanged()
        {
            var volume = _musicSlider.value;
            if (volume <= -50f)
                volume = -80f;
            _mixer.SetFloat("Music", volume);
        }

        public void OnSoundVolumeChanged()
        {
            var volume = _musicSlider.value;
            if (volume <= -50f)
                volume = -80f;
            _mixer.SetFloat("Sound", volume);
        }
    }
}