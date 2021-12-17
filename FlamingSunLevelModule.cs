using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ThunderRoad;
using UnityEngine;
using SnippetCode;
using UnityEngine.Events;

namespace FlamingSun
{
    public class FlamingSunLevelModule : LevelModule
    {
        private bool sunsSpawned = false;
        private List<Item> ListSunsSpawned = new List<Item>();
        private float timerBeforeWaveOfFireballs = 10f;
        private float timerDurationWaveOfFireballs = 30f;
        private float timer = 0f;
        private float timerThrowTotalDelay = 0.3f;
        private float timerDelay = 0f;
        private bool waveOfFireballsActive = false;
        private int numOfFireballs = 1;
        private float sizeFireball = 1f;
        private float chancesOfRandomTarget = 20;
        private List<Creature> ListOfCreatureInRadiusOfFire;
        public LevelModuleSurvival levelModuleSurvival;
        public WaveSpawner waveSpawner;
        public override IEnumerator OnLoadCoroutine()
        {
            EventManager.onLevelLoad += EventManager_onLevelLoad;
            EventManager.onLevelUnload += EventManager_onLevelUnload;
            return base.OnLoadCoroutine();
        }
        private void EventManager_onLevelUnload(LevelData levelData, EventTime eventTime)
        {
            ListSunsSpawned.Clear();
            timer = 0f;
            timerDelay = 0f;
            waveOfFireballsActive = false;
            sunsSpawned = false;
        }

        private void EventManager_onLevelLoad(LevelData levelData, EventTime eventTime)
        {
            if (levelData.id != "CharacterSelection" || levelData.id != "Dungeon" || levelData.id != "Master" && !sunsSpawned)
            {
                switch (levelData.id)
                {
                    case "Arena":
                        Catalog.GetData<ItemData>("FlamingSunBig").SpawnAsync(item =>
                        {
                            item.disallowDespawn = true;
                            item.rb.isKinematic = true;
                            item.isFlying = true;
                            item.transform.position = new Vector3(0f, 12f, 0f);
                            ListSunsSpawned.Add(item);
                        });
                        sunsSpawned = true;
                        break;
                    case "Citadel":
                        Catalog.GetData<ItemData>("FlamingSunBig").SpawnAsync(item =>
                        {
                            item.disallowDespawn = true;
                            item.rb.isKinematic = true;
                            item.isFlying = true;
                            item.transform.position = new Vector3(-29f, 99f, 1f);
                            ListSunsSpawned.Add(item);
                        });
                        /*Catalog.GetData<ItemData>("FlamingSunBig").SpawnAsync(item =>
                        {
                            item.disallowDespawn = true;
                            item.rb.isKinematic = true;
                            item.isFlying = true;
                            item.transform.position = new Vector3(26f, 99f, 30f);
                            ListSunsSpawned.Add(item);
                        });
                        Catalog.GetData<ItemData>("FlamingSunBig").SpawnAsync(item =>
                        {
                            item.disallowDespawn = true;
                            item.rb.isKinematic = true;
                            item.isFlying = true;
                            item.transform.position = new Vector3(19.5f, 115f, 1f);
                            ListSunsSpawned.Add(item);
                        });
                        Catalog.GetData<ItemData>("FlamingSunBig").SpawnAsync(item =>
                        {
                            item.disallowDespawn = true;
                            item.rb.isKinematic = true;
                            item.isFlying = true;
                            item.transform.position = new Vector3(-52f, 135f, .6f);
                            ListSunsSpawned.Add(item);
                        });*/
                        sunsSpawned = true;
                        break;
                    case "Market":
                        Catalog.GetData<ItemData>("FlamingSunBig").SpawnAsync(item =>
                        {
                            item.disallowDespawn = true;
                            item.rb.isKinematic = true;
                            item.isFlying = true;
                            item.transform.position = new Vector3(-4.5f, 18f, -2f);
                            ListSunsSpawned.Add(item);
                        });
                        sunsSpawned = true;
                        break;
                    case "Ruins":
                        Catalog.GetData<ItemData>("FlamingSunBig").SpawnAsync(item =>
                        {
                            item.disallowDespawn = true;
                            item.rb.isKinematic = true;
                            item.isFlying = true;
                            item.transform.position = new Vector3(0f, 20f, 42.5f);
                            ListSunsSpawned.Add(item);
                        });
                        sunsSpawned = true;
                        break;
                    case "Canyon":
                        Catalog.GetData<ItemData>("FlamingSunBig").SpawnAsync(item =>
                        {
                            item.disallowDespawn = true;
                            item.rb.isKinematic = true;
                            item.isFlying = true;
                            item.transform.position = new Vector3(10f, 18f, -2.5f);
                            ListSunsSpawned.Add(item);
                        });
                        sunsSpawned = true;
                        break;
                    default:

                        break;
                }
            }
        }

        public override void Update()
        {
            
            if(sunsSpawned)
            {
                CalculateWave();
                if (waveOfFireballsActive)
                {
                    if (timerDelay < timerThrowTotalDelay)
                    {
                        timerDelay += Time.deltaTime;
                    }
                    else
                    {
                        foreach (Item sun in ListSunsSpawned)
                        {
                            SpawnAndLaunchFireball(sun);
                        }
                        timerDelay = 0f;
                    }
                }
            }
            GameManager.local.StartCoroutine(CleanFireballs());
        }

        private void CalculateWave()
        {
            if(!waveOfFireballsActive)
            {
                if (timer < timerBeforeWaveOfFireballs)
                {
                    timer += Time.deltaTime;
                }
                else
                {
                    timer = 0;
                    waveOfFireballsActive = true;
                }
            }
            else
            {
                if(timer < timerDurationWaveOfFireballs)
                {
                    timer += Time.deltaTime;
                }
                else
                {
                    timer = 0;
                    waveOfFireballsActive = false;
                }
            }
        }

        private void SpawnAndLaunchFireball(Item origin)
        {
            foreach (Item sun in ListSunsSpawned)
            {
                ListOfCreatureInRadiusOfFire = SnippetCode.SnippetCode.CreaturesInRadius(sun.transform.position, 150).ToList();
            }
            for (int i = 0; i <= numOfFireballs; i++)
            {
                Vector3 offset;
                if (UnityEngine.Random.Range(0, 100) >= chancesOfRandomTarget)
                {
                    offset = (SnippetCode.SnippetCode.GetRandomRagdollPart(ListOfCreatureInRadiusOfFire[UnityEngine.Random.Range(0, ListOfCreatureInRadiusOfFire.Count())]).transform.position
                        - origin.transform.position).normalized * (8.15f + (sizeFireball * 0.15f));
                }
                else
                {
                    offset = Quaternion.Euler(UnityEngine.Random.value * 360.0f,
                    UnityEngine.Random.value * 360.0f,
                    UnityEngine.Random.value * 360.0f)
                    * SnippetCode.SnippetCode.forward * (8.15f + (sizeFireball * 0.15f));
                }
                Catalog.GetData<ItemData>("ProjectileFlamingSun").SpawnAsync(projectile =>
                {
                    projectile.disallowDespawn = true;
                    projectile.transform.position = origin.transform.position + offset;
                    projectile.rb.useGravity = false;
                    projectile.rb.velocity = Vector3.zero;
                    projectile.transform.localScale = SnippetCode.SnippetCode.one * sizeFireball;
                    foreach (CollisionHandler collisionHandler in projectile.collisionHandlers)
                    {
                        foreach (Damager damager in collisionHandler.damagers)
                            damager.Load(Catalog.GetData<DamagerData>("Fireball"), collisionHandler);
                    }
                    ItemMagicProjectile component = projectile.GetComponent<ItemMagicProjectile>();
                    if (component)
                    {
                        component.guided = false;
                        component.speed = 0;
                        component.Fire((projectile.transform.position - origin.transform.position).normalized * 30f, Catalog.GetData<EffectData>("SpellFireball"));
                        component.transform.localScale = SnippetCode.SnippetCode.one * sizeFireball;
                    }
                    projectile.isThrowed = true;
                    projectile.isFlying = true;
                    projectile.Throw(flyDetection: Item.FlyDetection.Forced);
                });
            }
        }

        IEnumerator CleanFireballs()
        {
            for (int index = Item.allActive.Count - 1; index >= 0; --index)
            {
                if (Item.allActive[index].itemId == "ProjectileFlamingSun" && (Time.time - Item.allActive[index].spawnTime) > 10f)
                {
                    Item.allActive[index].Despawn();
                }
            }
            yield return null;
        }
    }
}
