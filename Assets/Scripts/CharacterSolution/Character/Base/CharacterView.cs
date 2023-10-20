using UnityEngine;

namespace CharacterSolution.Character.Base
{
    public class CharacterView : MonoBehaviour
    {
        [SerializeField] private CharacterAnimation _animation;

        public void SetScale(int scaleX)
        {
            _animation.SetScale(scaleX);
        }

        public void StartWalkAnimation()
        {
            _animation.StartWalkAnimation();
        }

        public void StartIdleAnimation()
        {
            _animation.StartIdleAnimation();
        }
    }
}