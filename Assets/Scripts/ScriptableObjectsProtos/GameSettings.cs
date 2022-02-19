using Assets.Scripts.Common;
using UnityEngine;

namespace Assets.Scripts.ScriptableObjectsProtos
{
    [CreateAssetMenu(fileName = "GameSettings", menuName = "ScriptableObjects/Local game settings")]
    public class GameSettings : ScriptableObject
    {
        public ControlledBy LeftControlledBy;

        public ControlledBy RightControlledBy;
    }
}
