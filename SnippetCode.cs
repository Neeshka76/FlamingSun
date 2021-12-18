using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Threading.Tasks;
using ThunderRoad;
using UnityEngine;
using SnippetCode;
using System.Collections;

namespace SnippetCode
{
    static class SnippetCode
    {
        /// <summary>
        /// Vector pointing away from the palm
        /// </summary>
        public static Vector3 PalmDir(this RagdollHand hand) => -hand.transform.forward;

        /// <summary>
        /// Vector pointing in the direction of the thumb
        /// </summary>
        public static Vector3 ThumbDir(this RagdollHand hand) => (hand.side == Side.Right) ? hand.transform.up : -hand.transform.up;

        /// <summary>
        /// Vector pointing away in the direction of the fingers
        /// </summary>
        public static Vector3 PointDir(this RagdollHand hand) => -hand.transform.right;

        /// <summary>
        /// Get a point above the player's hand
        /// </summary>
        public static Vector3 PosAboveBackOfHand(this RagdollHand hand) => hand.transform.position - hand.transform.right * 0.1f + hand.transform.forward * 0.2f;

        public static void SetVFXProperty<T>(this EffectInstance effect, string name, T data)
        {
            if (effect == null)
                return;
            if (data is Vector3 v)
            {
                foreach (EffectVfx effectVfx in effect.effects.Where<Effect>((Func<Effect, bool>)(fx => fx is EffectVfx effectVfx17 && effectVfx17.vfx.HasVector3(name))))
                    effectVfx.vfx.SetVector3(name, v);
            }
            else if (data is float f2)
            {
                foreach (EffectVfx effectVfx2 in effect.effects.Where<Effect>((Func<Effect, bool>)(fx => fx is EffectVfx effectVfx18 && effectVfx18.vfx.HasFloat(name))))
                    effectVfx2.vfx.SetFloat(name, f2);
            }
            else if (data is int i3)
            {
                foreach (EffectVfx effectVfx2 in effect.effects.Where<Effect>((Func<Effect, bool>)(fx => fx is EffectVfx effectVfx19 && effectVfx19.vfx.HasInt(name))))
                    effectVfx2.vfx.SetInt(name, i3);
            }
            else if (data is bool b4)
            {
                foreach (EffectVfx effectVfx2 in effect.effects.Where<Effect>((Func<Effect, bool>)(fx => fx is EffectVfx effectVfx20 && effectVfx20.vfx.HasBool(name))))
                    effectVfx2.vfx.SetBool(name, b4);
            }
            else
            {
                if (!(data is Texture t5))
                    return;
                foreach (EffectVfx effectVfx2 in effect.effects.Where<Effect>((Func<Effect, bool>)(fx => fx is EffectVfx effectVfx21 && effectVfx21.vfx.HasTexture(name))))
                    effectVfx2.vfx.SetTexture(name, t5);
            }
        }
        public static object GetVFXProperty(this EffectInstance effect, string name)
        {
            foreach (Effect effect1 in effect.effects)
            {
                if (effect1 is EffectVfx effectVfx1)
                {
                    if (effectVfx1.vfx.HasFloat(name))
                        return effectVfx1.vfx.GetFloat(name);
                    if (effectVfx1.vfx.HasVector3(name))
                        return effectVfx1.vfx.GetVector3(name);
                    if (effectVfx1.vfx.HasBool(name))
                        return effectVfx1.vfx.GetBool(name);
                    if (effectVfx1.vfx.HasInt(name))
                        return effectVfx1.vfx.GetInt(name);
                }
            }
            return null;
        }
        public static Vector3 zero = Vector3.zero;
        public static Vector3 one = Vector3.one;
        public static Vector3 forward = Vector3.forward;
        public static Vector3 right = Vector3.right;
        public static Vector3 up = Vector3.up;
        public static Vector3 back = Vector3.back;
        public static Vector3 left = Vector3.left;
        public static Vector3 down = Vector3.down;

        public static bool XBigger(this Vector3 vec) => Mathf.Abs(vec.x) > Mathf.Abs(vec.y) && Mathf.Abs(vec.x) > Mathf.Abs(vec.z);

        public static bool YBigger(this Vector3 vec) => Mathf.Abs(vec.y) > Mathf.Abs(vec.x) && Mathf.Abs(vec.y) > Mathf.Abs(vec.z);

        public static bool ZBigger(this Vector3 vec) => Mathf.Abs(vec.z) > Mathf.Abs(vec.x) && Mathf.Abs(vec.z) > Mathf.Abs(vec.y);
        public static Vector3 Velocity(this RagdollHand hand) => Player.local.transform.rotation * hand.playerHand.controlHand.GetHandVelocity();
        public static T GetOrAddComponent<T>(this GameObject obj) where T : Component
        {
            return obj.GetComponent<T>() ?? obj.AddComponent<T>();
        }
        public static bool IsPlayer(this RagdollPart part) => part?.ragdoll?.creature.isPlayer == true;
        public static bool IsImportant(this RagdollPart part)
        {
            var type = part.type;
            return type == RagdollPart.Type.Head
                   || type == RagdollPart.Type.Torso
                   || type == RagdollPart.Type.LeftHand
                   || type == RagdollPart.Type.RightHand
                   || type == RagdollPart.Type.LeftFoot
                   || type == RagdollPart.Type.RightFoot;
        }
        /// <summary>
        /// Get a creature's part from a PartType
        /// </summary>
        public static RagdollPart GetPart(this Creature creature, RagdollPart.Type partType)
            => creature.ragdoll.GetPart(partType);

        /// <summary>
        /// Get a creature's head
        /// </summary>
        public static RagdollPart GetHead(this Creature creature) => creature.ragdoll.headPart;

        /// <summary>
        /// Get a creature's torso
        /// </summary>
        public static RagdollPart GetTorso(this Creature creature) => creature.GetPart(RagdollPart.Type.Torso);

        public static Vector3 GetChest(this Creature creature) => Vector3.Lerp(creature.GetTorso().transform.position,
            creature.GetHead().transform.position, 0.5f);
        public static IEnumerable<Creature> CreaturesInRadius(Vector3 position, float radius)
        {
            return Creature.allActive.Where(creature => (creature.GetChest() - position).sqrMagnitude < radius * radius);
        }
        public static void Depenetrate(this Item item)
        {
            foreach (var handler in item.collisionHandlers)
            {
                foreach (var damager in handler.damagers)
                {
                    damager.UnPenetrateAll();
                }
            }
        }
        /// <summary>
        /// Get a creature's random part
        /// </summary>
        /*
        Head
        Neck
        Torso
        LeftArm
        RightArm
        LeftHand
        RightHand
        LeftLeg
        RightLeg
        LeftFoot
        RightFoot
        */
        public static RagdollPart GetRandomRagdollPart(this Creature creature)
        {
            Array values = Enum.GetValues(typeof(RagdollPart.Type));
            return creature.ragdoll.GetPart((RagdollPart.Type) values.GetValue(UnityEngine.Random.Range(0, values.Length)));
        }

        public static bool returnWaveStarted()
        {
            int nbWaveStarted = 0;
            foreach (WaveSpawner waveSpawner in WaveSpawner.instances)
            {
                if (waveSpawner.isRunning)
                {
                    nbWaveStarted++;
                }
            }
            return nbWaveStarted != 0 ? true : false;
        }
    }
}
