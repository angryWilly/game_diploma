using CodeBase.Infrastructure.Services;
using CodeBase.Infrastructure.States;
using UnityEngine;

namespace CodeBase.Logic
{
    public class LevelTransferTrigger : MonoBehaviour
    {
        private const string PlayerTag = "Player";
        [SerializeField] private string _transferTo;
        private IGameStateMachine _stateMachine;

        public void Construct(IGameStateMachine stateMachine) => 
            _stateMachine = stateMachine;

        private bool _isTriggered;
        
        private void Awake()
        {
            _stateMachine = AllServices.Container.Single<IGameStateMachine>();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (_isTriggered)
                return;
            
            if (other.CompareTag(PlayerTag))
            {
                _stateMachine.Enter<LoadLevelState, string>(_transferTo);
                _isTriggered = true;
            }
        }
    }
}