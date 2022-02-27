using Assets.Scripts.Managers;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts.Managers
{
    public sealed class AudioManager : MonoBehaviour
    {
        internal static AudioManager Instance { get; private set; }

        [SerializeField]
        private List<ManagedAudioSource> _sounds;

        private AudioSource Get(AudioSourceType type) => _sounds.Where(t => t.Type == type).Select(t => t.Source).FirstOrDefault();

        public void Play(AudioSourceType type)
        {
            var src = Get(type);
            if (src)
                src.Play();
        }

        public void Stop(AudioSourceType type)
        {
            var src = Get(type);
            if(src)
                src.Stop();
        }

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }
}