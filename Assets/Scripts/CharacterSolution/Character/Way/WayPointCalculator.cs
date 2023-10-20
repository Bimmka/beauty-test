using UnityEngine;

namespace CharacterSolution.Character.Way
{
    public class WayPointCalculator
    {
        private readonly Transform _character;
        private readonly float _chanceForLeftMove;
        private readonly float _chanceForUpMove;
        private readonly float _maxWidthShift;
        private readonly float _maxHeightShift;

        public WayPointCalculator(Transform character, float chanceForLeftMove, float chanceForUpMove, float maxWidthShift, float maxHeightShift)
        {
            _maxHeightShift = maxHeightShift;
            _maxWidthShift = maxWidthShift;
            _chanceForUpMove = chanceForUpMove;
            _chanceForLeftMove = chanceForLeftMove;
            _character = character;
        }
        
        public Vector3 GetRandomPoint()
        {
            float randomValue = Random.Range(0, 1f);
            bool isLeftMove = randomValue <= _chanceForLeftMove;
            randomValue = Random.Range(0, 1f);

            bool isUpMove = randomValue <= _chanceForUpMove;

            Vector3 finishPosition = _character.position;

            finishPosition += isLeftMove ? Vector3.left : Vector3.right * Random.Range(0, _maxWidthShift);
            finishPosition += isUpMove ? Vector3.up : Vector3.down * Random.Range(0, _maxHeightShift);

            return finishPosition;
        }
    }
}