using CodeBase.Infrastructure;
using CodeBase.Services.Input;
using UnityEngine;

namespace CodeBase.Hero
{
    [RequireComponent(typeof(CharacterController))]
    public class HeroMove : MonoBehaviour
    {
        [SerializeField] private float MovementSpeed = 10;
        
        private CharacterController _characterController;
        private HeroAnimator _heroAnimator;
        private IInputService _inputService;

        private void Awake()
        {
            _inputService = Game.InputService;

            _characterController = GetComponent<CharacterController>();
            _heroAnimator = GetComponent<HeroAnimator>();
        }
        
        private void Update()
        {
            var movementVector = Vector3.zero;
            
            if (!_heroAnimator.IsAttacking && _inputService.Axis.sqrMagnitude > 0.0001f)
            {
                movementVector = Camera.main.transform.TransformDirection(_inputService.Axis);
                movementVector.y = 0;
                movementVector.Normalize();

                transform.forward = movementVector;
            }

            movementVector += Physics.gravity;
            _characterController.Move(movementVector * (MovementSpeed * Time.deltaTime));
        }
    }
}