using System;
using UnityEngine;

namespace Assets.Scripts.Managers
{
    [Serializable]
    public class ManagedAudioSource
    {
        public AudioSourceType Type;

        public AudioSource Source; 
    }
}
