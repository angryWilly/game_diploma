using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace CodeBase.Enemy
{
  [RequireComponent(typeof(Attack))]
  public class CheckAttackRange : MonoBehaviour
  {
    [SerializeField] private Attack _attack;
    [SerializeField] private TriggerObserver _triggerObserver;

    private void Start()
    {
      _triggerObserver.TriggerEnter += OnTriggerEnter;
      _triggerObserver.TriggerExit += OnTriggerExit;
      
      _attack.DisableAttack();
    }

    private void OnTriggerExit(Collider obj)
    {
      _attack.DisableAttack();
    }

    private void OnTriggerEnter(Collider obj)
    {
      _attack.EnableAttack();
    }

    private void OnDestroy()
    {
      _triggerObserver.TriggerEnter -= OnTriggerEnter;
      _triggerObserver.TriggerExit -= OnTriggerExit;
    }
  }
}