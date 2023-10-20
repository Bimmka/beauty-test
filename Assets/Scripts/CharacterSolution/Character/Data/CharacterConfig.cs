using UnityEngine;

namespace CharacterSolution.Character.Data
{
    [CreateAssetMenu(fileName = "CharacterConfig", menuName = "StaticData/Character/Create Character Config", order = 52)]
    public class CharacterConfig : ScriptableObject
    {
        public float MoveSpeed;
        [Range(0, 1f)] public float ChanceToMoveLeft;
        [Range(0, 1f)] public float ChanceToMoveUp;
        public float WidthShift;
        public float HeightShift;
        public float TimeInIdle;
        public float DistanceAccuracy;
    }
}