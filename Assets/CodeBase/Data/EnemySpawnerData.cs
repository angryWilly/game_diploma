using System;
using CodeBase.Enemy;
using CodeBase.Infrastructure.StaticData;
using UnityEngine;

namespace CodeBase.Data
{
    [Serializable]
    public class EnemySpawnerData
    {
        public string Id;
        public MonsterTypeId MonsterTypeId;
        public Vector3 Position;

        public EnemySpawnerData(string id, MonsterTypeId monsterTypeId, Vector3 position)
        {
            Id = id;
            MonsterTypeId = monsterTypeId;
            Position = position;
        }
    }
}