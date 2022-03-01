using Assets.Scripts.Managers;
using UnityEngine;

namespace Assets.Scripts.Components
{
    public sealed class MusicComponent : MonoBehaviour
    {
        [SerializeField]
        private AudioSourceType _type;

        private void Start()
        {
            AudioManager.Instance.Play(_type);
        }

        private void OnDestroy()
        {
            AudioManager.Instance.Stop(_type);
        }
    }
}
