using System.Linq;
using CodeBase.Logic;
using UnityEngine;

namespace CodeBase.Enemy
{
    [RequireComponent(typeof(EnemyAnimator))]
    public class Attack : MonoBehaviour
    {
        public float AttackCooldown = 3f;
        public float Cleavage = 0.5f;
        public float EffectiveDistance = 0.5f;
        public float Damage = 10f;
        [SerializeField] private EnemyAnimator _animator;

        private Transform _heroTransform;
        private float _attackCooldown;
        private bool _isAttacking;
        private int _layerMask;
        private bool _IsAttackActive;
        private readonly Collider[] _hits = new Collider[1];

        public void Construct(Transform heroTransform) =>
            _heroTransform = heroTransform;

        private void Awake()
        {
            _layerMask = 1 << LayerMask.NameToLayer("Player");
        }

        private void Update()
        {
            UpdateCooldown();

            if (CanAttack())
                StartAttack();
        }

        private void OnAttack()
        {
            if (Hit(out Collider hit))
            {
                PhysicsDebug.DrawDebug(StartPoint(), Cleavage, 1);
                hit.transform.GetComponent<IHealth>().TakeDamage(Damage);
            }
        }

        private void OnAttackEnded()
        {
            _attackCooldown = AttackCooldown;
            _isAttacking = false;
        }

        public void EnableAttack() =>
            _IsAttackActive = true;

        public void DisableAttack() =>
            _IsAttackActive = false;

        private bool Hit(out Collider hit)
        {
            int hitCount = Physics.OverlapSphereNonAlloc(StartPoint(), Cleavage, _hits, _layerMask);
            hit = _hits.FirstOrDefault();
            return hitCount > 0;
        }

        private Vector3 StartPoint() =>
            new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z) +
            transform.forward * EffectiveDistance;

        private void StartAttack()
        {
            transform.LookAt(_heroTransform);
            _animator.PlayAttack();

            _isAttacking = true;
        }

        private void UpdateCooldown()
        {
            if (!IsCooldownUp())
                _attackCooldown -= Time.deltaTime;
        }

        private bool CanAttack() =>
            _IsAttackActive && !_isAttacking && IsCooldownUp();

        private bool IsCooldownUp() =>
            _attackCooldown <= 0f;
    }
}