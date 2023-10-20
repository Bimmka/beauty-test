using System.Collections;
using CharacterSolution.Character.Base;
using CharacterSolution.CoroutineRunner;
using UnityEngine;

namespace CharacterSolution.Interrupt
{
    public class InterruptionChecker
    {
        private readonly ICoroutineRunner _coroutineRunner;
        private readonly CharacterModel _model;

        private Coroutine _coroutine;

        public InterruptionChecker(ICoroutineRunner coroutineRunner, CharacterModel model)
        {
            _model = model;
            _coroutineRunner = coroutineRunner;
        }

        public void StartCheck()
        {
            if (_coroutine != null)
                _coroutineRunner.StopCoroutine(_coroutine);
            
            _coroutine = _coroutineRunner.StartCoroutine(CheckInterruption());
        }

        private IEnumerator CheckInterruption()
        {
            while (true)
            {
                if (Input.GetKeyDown(KeyCode.Space))
                    _model.StopMove();

                yield return null;
            }
        }
    }
}