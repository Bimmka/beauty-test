using CharacterSolution.Character.Base;
using CharacterSolution.Character.Data;
using CharacterSolution.Character.Way;
using CharacterSolution.CoroutineRunner;
using UnityEngine;

namespace CharacterSolution.Character.Factory
{
    public class CharacterFactory
    {
        private readonly ICoroutineRunner _coroutineRunner;
        private readonly CharacterView _characterViewPrefab;
        private readonly CharacterConfig _config;

        public CharacterFactory(ICoroutineRunner coroutineRunner, CharacterView characterViewPrefab, CharacterConfig config)
        {
            _config = config;
            _characterViewPrefab = characterViewPrefab;
            _coroutineRunner = coroutineRunner;
        }

        public CharacterModel Create()
        {
            CharacterView view = Object.Instantiate(_characterViewPrefab);
            CharacterMove move = new CharacterMove(view.transform, _config.MoveSpeed);
            WayPointCalculator wayPointCalculator = new WayPointCalculator(view.transform, _config.ChanceToMoveLeft,
                _config.ChanceToMoveUp, _config.WidthShift, _config.HeightShift);
            CharacterModel model = new CharacterModel(view, wayPointCalculator, move, _coroutineRunner, _config.DistanceAccuracy, _config.TimeInIdle);
            return model;
        }
    }
}