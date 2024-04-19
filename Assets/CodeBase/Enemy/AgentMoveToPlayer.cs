using CodeBase.Infrastructure.Factory;
using UnityEngine;
using UnityEngine.AI;

namespace CodeBase.Enemy
{
    public class AgentMoveToPlayer: MonoBehaviour
    {
        private const float MinimalDistance = 1;
        
        [SerializeField] private NavMeshAgent _agent;
        private Transform _heroTransform;
        private IGameFactory _gameFactory;

        public void Construct(Transform heroTransform) =>
            _heroTransform = heroTransform;

        private void Update()
        {
            SetDestinationForAgent();
        }

        private void SetDestinationForAgent()
        {
            if (HeroInitialized() && HeroNotReached())
                _agent.destination = _heroTransform.position;
        }

        private bool HeroInitialized() =>
            _heroTransform != null;

        private bool HeroNotReached() =>
            Vector3.Distance(_agent.transform.position, _heroTransform.position) >= MinimalDistance;
    }
}