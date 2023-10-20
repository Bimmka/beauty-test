using UnityEngine;

public class FollowCamera : MonoBehaviour
{
   [SerializeField] private float _zOffset;
   
   private Transform _target;
   public void SetTarget(Transform target)
   {
      _target = target;
   }
   
   private void Update()
   {
      if (_target != null)
      {
         transform.position = _target.position;
         transform.position += Vector3.back * _zOffset;
      }
   }
}
