using CodeBase.Data;
using UnityEngine;

namespace CodeBase.Enemy
{
    public class LootPiece : MonoBehaviour
    {
        private Loot _loot;
        private bool _isPickedUp;
        private WorldData _worldData;

        public void Construct(WorldData worldData)
        {
            _worldData = worldData;
        }

        public void Initialize(Loot loot)
        {
            _loot = loot;
        }

        private void OnTriggerEnter(Collider other) => 
            PickUp();

        private void PickUp()
        {
            if (_isPickedUp)
                return;
            
            _isPickedUp = true;

            _worldData.LootData.Collect(_loot);
        }
    }
}