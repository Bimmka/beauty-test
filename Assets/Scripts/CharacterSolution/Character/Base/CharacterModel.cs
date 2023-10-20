using System;
using System.Collections;
using CharacterSolution.Character.Way;
using CharacterSolution.CoroutineRunner;
using UnityEngine;

namespace CharacterSolution.Character.Base
{
    public class CharacterModel
    {
        private readonly CharacterView _view;
        private readonly WayPointCalculator _wayPointCalculator;
        private readonly CharacterMove _move;
        private readonly ICoroutineRunner _coroutineRunner;
        private readonly float _distanceAccuracy;
        private readonly float _timeInIdle;

        private Coroutine _currentCoroutine;

        public Transform ViewTransform => _view.transform;

        public CharacterModel(CharacterView view, WayPointCalculator wayPointCalculator, CharacterMove move,
            ICoroutineRunner coroutineRunner, float distanceAccuracy, float timeInIdle)
        {
            _coroutineRunner = coroutineRunner;
            _move = move;
            _timeInIdle = timeInIdle;
            _distanceAccuracy = distanceAccuracy;
            _wayPointCalculator = wayPointCalculator;
            _view = view;
        }

        public void Start()
        {
            _currentCoroutine = _coroutineRunner.StartCoroutine(GoToWayPoint(OnWayPointReached));
        }

        public void StopMove()
        {
            if (_currentCoroutine != null)
                _coroutineRunner.StopCoroutine(_currentCoroutine);

            _currentCoroutine = _coroutineRunner.StartCoroutine(WaitInIdle(OnIdleWaited));
        }

        private IEnumerator GoToWayPoint(Action callback)
        {
            Vector3 wayPoint = _wayPointCalculator.GetRandomPoint();
            int scaleX = 1;

            if (wayPoint.x >= _view.transform.position.x)
                scaleX = -1;

            _view.SetScale(scaleX);
            _view.StartWalkAnimation();

            while (Vector3.Distance(_view.transform.position, wayPoint) > _distanceAccuracy)
            {
                yield return null;
                _move.MoveTo(wayPoint, Time.deltaTime);
            }
            
            callback?.Invoke();
        }

        private IEnumerator WaitInIdle(Action callback)
        {
            _view.StartIdleAnimation();
            yield return new WaitForSecondsRealtime(_timeInIdle);
            
            callback?.Invoke();
        }

        private void OnWayPointReached()
        {
            _currentCoroutine = _coroutineRunner.StartCoroutine(WaitInIdle(OnIdleWaited));
        }

        private void OnIdleWaited()
        {
            _currentCoroutine = _coroutineRunner.StartCoroutine(GoToWayPoint(OnWayPointReached));
        }
    }
}