using CharacterSolution.Camera;
using CharacterSolution.Character.Base;
using CharacterSolution.Character.Data;
using CharacterSolution.Character.Factory;
using CharacterSolution.CoroutineRunner;
using CharacterSolution.Interrupt;
using UnityEngine;

namespace CharacterSolution.Bootstrapp
{
    public class CharacterSolutionBootstrapp : MonoBehaviour, ICoroutineRunner
    {
        [SerializeField] private CharacterConfig _characterConfig;
        [SerializeField] private CharacterView _characterView;
        [SerializeField] private FollowCamera _camera;

        private void Awake()
        {
            CharacterFactory factory = new CharacterFactory(this, _characterView, _characterConfig);

            CharacterModel model = factory.Create();
            model.Start();

            FollowCamera camera = Instantiate(_camera);
            camera.SetTarget(model.ViewTransform);

            InterruptionChecker checker = new InterruptionChecker(this, model);
            checker.StartCheck();
        }
    }
}
