using System.Collections;
using CodeBase.Data;
using TMPro;
using UnityEngine;

namespace CodeBase.Enemy.LootEnemy
{
    public class LootPiece : MonoBehaviour
    {
        public GameObject Skull;
        public GameObject PickUpFxPrefab;
        public TextMeshPro LootText;
        public GameObject PickUpPopUp;

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

        private void OnTriggerEnter(Collider other) => PickUp();

        private void PickUp()
        {
            if (_isPickedUp)
                return;

            _isPickedUp = true;

            UpdateWorldData();
            HideSkull();
            PlayPickUpFx();
            ShowText();
            StartCoroutine(StartDestroyTimer());
        }

        private void UpdateWorldData() =>
            _worldData.LootData.Collect(_loot);

        private void HideSkull() =>
            Skull.SetActive(false);

        private void PlayPickUpFx() =>
            Instantiate(PickUpFxPrefab, transform.position, Quaternion.identity);

        private void ShowText()
        {
            LootText.text = $"{_loot.Value}";
            PickUpPopUp.SetActive(true);
        }

        private IEnumerator StartDestroyTimer()
        {
            yield return new WaitForSeconds(1.5f);
            Destroy(gameObject);
        }
    }
}