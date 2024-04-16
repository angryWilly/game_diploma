using System;
using CodeBase.Infrastructure.Factory;
using CodeBase.Infrastructure.Services;
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

        private void Start()
        {
            _gameFactory = AllServices.Container.Single<IGameFactory>();

            if (_gameFactory.HeroGameObject != null) 
                InitializeHeroTransform();
            else
                _gameFactory.HeroCreated += OnHeroCreated;
        }

        private void Update()
        {
            if (HeroInitialized() && HeroNotReached()) 
                _agent.destination = _heroTransform.position;
        }

        private bool HeroInitialized() =>
            _heroTransform != null;

        private void OnHeroCreated() =>
            InitializeHeroTransform();

        private void InitializeHeroTransform() => 
            _heroTransform = _gameFactory.HeroGameObject.transform;

        private bool HeroNotReached() =>
            Vector3.Distance(_agent.transform.position, _heroTransform.position) >= MinimalDistance;
    }
}