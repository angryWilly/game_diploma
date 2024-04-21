﻿using System.Collections.Generic;
using System.Threading.Tasks;
using CodeBase.Enemy.LootEnemy;
using CodeBase.Infrastructure.Services;
using CodeBase.Infrastructure.Services.PersistentProgress;
using CodeBase.StaticData;
using UnityEngine;

namespace CodeBase.Infrastructure.Factory
{
    public interface IGameFactory : IService
    {
        List<ISavedProgressReader> ProgressReaders { get; }
        List<ISavedProgress> ProgressWriters { get; }
        Task<GameObject> CreateMonster(MonsterTypeId typeId, Transform parent);
        GameObject CreateHero(Vector3 at);
        GameObject CreateHud();
        Task<LootPiece> CreateLoot();
        Task CreateSpawner(Vector3 at, string spawnerId, MonsterTypeId monsterTypeId);

        Task WarmUp();
        void CleanUp();
    }
}