using UnityEngine;

namespace CharacterSolution.Character.Base
{
    public class CharacterMove
    {
        private readonly Transform _view;
        private readonly float _moveSpeed;

        public CharacterMove(Transform view, float moveSpeed)
        {
            _moveSpeed = moveSpeed;
            _view = view;
        }

        public void MoveTo(Vector3 to, float deltaTime)
        {
            Vector3 direction = (to - _view.transform.position).normalized;
            _view.transform.position += direction * _moveSpeed * deltaTime;
        }
    }
}