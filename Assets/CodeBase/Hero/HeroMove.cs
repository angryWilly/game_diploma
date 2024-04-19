using CodeBase.Data;
using CodeBase.Infrastructure;
using CodeBase.Infrastructure.Services;
using CodeBase.Infrastructure.Services.Input;
using CodeBase.Infrastructure.Services.PersistentProgress;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace CodeBase.Hero
{
    [RequireComponent(typeof(CharacterController))]
    public class HeroMove : MonoBehaviour, ISavedProgress
    {
        [SerializeField] private float MovementSpeed = 10;
        
        private CharacterController _characterController;
        private IInputService _inputService;
        private HeroAnimator _heroAnimator;

        private void Awake()
        {
            _inputService = AllServices.Container.Single<IInputService>();

            _characterController = GetComponent<CharacterController>();
            _heroAnimator = GetComponent<HeroAnimator>();
        }

        public void Update()
        {
            var movementVector = Vector3.zero;
            
            if (!_heroAnimator.IsAttacking && _inputService.Axis.sqrMagnitude > 0.001f)
            {
                movementVector = Camera.main.transform.TransformDirection(_inputService.Axis);
                movementVector.y = 0;
                movementVector.Normalize();
                transform.forward = movementVector;
            }

            movementVector += Physics.gravity;
            _characterController.Move(movementVector * MovementSpeed * Time.deltaTime);
        }

        public void UpdateProgress(PlayerProgress progress) =>
            progress.WorldData.PositionOnLevel = new PositionOnLevel(CurrentLevel(), transform.position.AsVector3Data());

        private string CurrentLevel() =>
            SceneManager.GetActiveScene().name;

        public void LoadProgress(PlayerProgress progress)
        {
            if (CurrentLevel() == progress.WorldData.PositionOnLevel.Level)
            {
                Vector3Data savedPosition = progress.WorldData.PositionOnLevel.Position;
                if (savedPosition != null) 
                    Warp(to: savedPosition);
            }
        }

        private void Warp(Vector3Data to)
        {
            _characterController.enabled = false;
            transform.position = to.AsUnityVector3().AddY(_characterController.height);
            _characterController.enabled = true;
        }
    }
}