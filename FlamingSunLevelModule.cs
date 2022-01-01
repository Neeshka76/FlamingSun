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
        private FlamingSunController flamingSunController;
        private bool sunsSpawned = false;
        private List<Item> ListSunsSpawned = new List<Item>();
        private float timer = 0f;
        private float timerDelay = 0f;
        private bool waveOfFireballsActive = false;
        private float sizeFireball = 1f;
        private List<Creature> ListOfCreatureInRadiusOfFire;
        private bool waveStarted = false;
        private string levelName;

        public override IEnumerator OnLoadCoroutine()
        {
            flamingSunController = GameManager.local.gameObject.GetComponent<FlamingSunController>();
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
            if(flamingSunController == null)
            {
                flamingSunController = GameManager.local.gameObject.GetComponent<FlamingSunController>();
            }
            levelName = levelData.id;
            if (flamingSunController?.data.FlamingSunSpawnGetSet == true)
            {
                SpawnTheSun(levelName);
            }
        }

        public override void Update()
        {
            if (flamingSunController?.data.FlamingSunSpawnGetSet == true)
            {
                if (sunsSpawned == true)
                {
                    waveStarted = SnippetCode.SnippetCode.returnWaveStarted();
                    if (!flamingSunController.data.FlamingSunAttackOutsideWavesGetSet && !waveStarted)
                    {
                        timer = 0f;
                        timerDelay = 0f;
                        waveOfFireballsActive = false;
                    }
                    if (sunsSpawned && (waveStarted || flamingSunController.data.FlamingSunAttackOutsideWavesGetSet) && flamingSunController.data.FlamingSunThrowFireballsGetSet)
                    {
                        CalculateWaveIdleActive();
                        if (waveOfFireballsActive)
                        {
                            if (timerDelay < flamingSunController.data.FlamingSunTickThrowFireballsDelayGetSet)
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
                else
                {
                    SpawnTheSun(levelName);
                }
            }
            else
            {
                if(sunsSpawned == true)
                {
                    DespawnSun();
                }
            }
        }

        private void SpawnTheSun(string levelName)
        {
            if (levelName != "CharacterSelection" || levelName != "Dungeon" || levelName != "Master" && !sunsSpawned)
            {
                switch (levelName)
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

        private void DespawnSun()
        {
            foreach(Item sun in ListSunsSpawned)
            {
                sun.Despawn();
            }
            ListSunsSpawned.Clear();
            timer = 0f;
            timerDelay = 0f;
            waveOfFireballsActive = false;
            sunsSpawned = false;
        }
        private void CalculateWaveIdleActive()
        {
            if(!waveOfFireballsActive)
            {
                if (timer < flamingSunController.data.FlamingSunTimeIdleFireballsGetSet)
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
                if(timer < flamingSunController.data.FlamingSunTimeActivateFireballsGetSet)
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
                ListOfCreatureInRadiusOfFire = SnippetCode.SnippetCode.CreaturesInRadius(sun.transform.position, flamingSunController.data.FlamingSunRadiusOfDetectionGetSet).ToList();
                if(flamingSunController.data.FlamingSunTargetPlayerGetSet == false)
                {
                    for(int i = ListOfCreatureInRadiusOfFire.Count - 1; i >= 0; i--)
                    {
                        if(ListOfCreatureInRadiusOfFire[i] == Player.currentCreature)
                        {
                            ListOfCreatureInRadiusOfFire.RemoveAt(i);
                        }   
                    }
                }
            }
            if (ListOfCreatureInRadiusOfFire.Count != 0)
            {
                for (int i = 0; i <= flamingSunController.data.FlamingSunNumFireballPerTickGetSet; i++)
                {
                    Vector3 offset;
                    if (UnityEngine.Random.Range(0, 100) >= flamingSunController.data.FlamingSunChancesOfRandomThrowGetSet)
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
                            component.allowDeflect = true;
                            component.deflectEffectData = Catalog.GetData<EffectData>("HitFireBallDeflect");
                            component.Fire((projectile.transform.position - origin.transform.position).normalized * 30f, Catalog.GetData<EffectData>("SpellFireball"));
                            component.transform.localScale = SnippetCode.SnippetCode.one * sizeFireball;
                        }
                        projectile.isThrowed = true;
                        projectile.isFlying = true;
                        projectile.Throw(flyDetection: Item.FlyDetection.Forced);
                    });
                }
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
