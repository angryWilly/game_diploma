using UnityEngine;

namespace CodeBase.Infrastructure
{
    public class GameFactory : IGameFactory
    {
        private const string HeroPath = "Prefabs/hero";
        private const string HudPath = "Hud/Hud";

        public GameObject CreateHero(GameObject at)
        {
            return Instantiate(HeroPath, at: at.transform.position);
        }

        public void CreateHud()
        {
            Instantiate(HudPath);
        }

        private static GameObject Instantiate(string path)
        {
            var prefab = Resources.Load<GameObject>(path);
            return Object.Instantiate(prefab);
        }

        private static GameObject Instantiate(string path, Vector3 at)
        {
            var prefab = Resources.Load<GameObject>(path);
            return Object.Instantiate(prefab, at, Quaternion.identity);
        }
    }
}