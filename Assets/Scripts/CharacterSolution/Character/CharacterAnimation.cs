using Spine.Unity;
using UnityEngine;

public class CharacterAnimation : MonoBehaviour
{
    [SerializeField] private SkeletonAnimation _skeletonAnimation;
    [SpineAnimation, SerializeField] private string _idleAnimationName;
    [SpineAnimation, SerializeField] private string _walkAnimationName;

    private Spine.AnimationState _spineAnimationState;
    private Spine.Skeleton _skeleton;
    
    private void Awake () 
    {
        _spineAnimationState = _skeletonAnimation.AnimationState;
        _skeleton = _skeletonAnimation.Skeleton;
    }

    public void StartWalkAnimation()
    {
        _spineAnimationState.SetAnimation(0, _walkAnimationName, true);
    }

    public void StartIdleAnimation()
    {
        _spineAnimationState.SetAnimation(0, _idleAnimationName, true);
    }

    public void SetScale(int scaleX)
    {
        _skeleton.ScaleX = scaleX;
    }
}
