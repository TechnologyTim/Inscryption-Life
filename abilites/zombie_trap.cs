using BepInEx;
using BepInEx.Logging;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;
using System.IO;
using DiskCardGame;
using HarmonyLib;
using UnityEngine;
using APIPlugin;

namespace InscryptionLife
{
    public partial class Plugin
    {
        private NewAbility createZombieTrapAbility()
        {
            AbilityInfo info = ScriptableObject.CreateInstance<AbilityInfo>();
            info.powerLevel = 3;
            info.rulebookName = "Zombie trap";
            info.rulebookDescription = "A card bearing this sigil will kill the opposing card when struck, leaving a Zombie corpse behind.";
            info.metaCategories = new List<AbilityMetaCategory> { AbilityMetaCategory.Part1Rulebook, AbilityMetaCategory.Part1Modular };

            byte[] imgBytes = System.IO.File.ReadAllBytes(Path.Combine(this.Info.Location.Replace("InscryptionLife.dll", ""), "Artwork/ability_zombietrap.png"));
            Texture2D tex = new Texture2D(2, 2);
            tex.LoadImage(imgBytes);

            NewAbility zombieTrap = new NewAbility(info, typeof(ZombieTrap), tex);
            ZombieTrap.ability = zombieTrap.ability;
            return zombieTrap;
        }

    }

    public class ZombieTrap : AbilityBehaviour
    {
        public override Ability Ability
        {
            get
            {
                return ability;
            }
        }

        public static Ability ability;

    }

    namespace DiskCardGame
    {
        // Token: 0x0200034B RID: 843
        public class SteelTrap : DrawCreatedCard
        {
            // Token: 0x1700029E RID: 670
            // (get) Token: 0x0600141B RID: 5147 RVA: 0x000444CB File Offset: 0x000426CB
            public override Ability Ability
            {
                get
                {
                    return Ability.SteelTrap;
                }
            }

            // Token: 0x1700029F RID: 671
            // (get) Token: 0x0600141C RID: 5148 RVA: 0x000444CF File Offset: 0x000426CF
            public override CardInfo CardToDraw
            {
                get
                {
                    return CardLoader.GetCardByName("headcrab_zombie_corpse");
                }
            }

            // Token: 0x0600141D RID: 5149 RVA: 0x000444DB File Offset: 0x000426DB
            public override bool RespondsToTakeDamage(PlayableCard source)
            {
                return Singleton<BoardManager>.Instance is BoardManager3D;
            }

            // Token: 0x0600141E RID: 5150 RVA: 0x000444EA File Offset: 0x000426EA
            public override IEnumerator OnTakeDamage(PlayableCard source)
            {
                yield return new WaitForSeconds(0.65f);
                AudioController.Instance.PlaySound3D("sacrifice_default", MixerGroup.TableObjectsSFX, base.Card.transform.position, 1f, 0f, null, null, null, null, false);
                yield return new WaitForSeconds(0.1f);
                base.Card.Anim.LightNegationEffect();
                base.Card.SwitchToPortrait(ResourceBank.Get<Sprite>("Art/Cards/Portraits/portrait_trap_closed"));
                AudioController.Instance.PlaySound3D("dial_metal", MixerGroup.TableObjectsSFX, base.Card.transform.position, 1f, 0f, null, null, null, null, false);
                yield return new WaitForSeconds(0.75f);
                yield break;
            }

            // Token: 0x0600141F RID: 5151 RVA: 0x00043F69 File Offset: 0x00042169
            public override bool RespondsToDie(bool wasSacrifice, PlayableCard killer)
            {
                return !wasSacrifice && base.Card.OnBoard;
            }

            // Token: 0x06001420 RID: 5152 RVA: 0x000444F9 File Offset: 0x000426F9
            public override IEnumerator OnDie(bool wasSacrifice, PlayableCard killer)
            {
                if (base.Card.Slot.opposingSlot.Card != null)
                {
                    yield return base.PreSuccessfulTriggerSequence();
                    yield return new WaitForSeconds(0.25f);
                    yield return base.Card.Slot.opposingSlot.Card.Die(false, base.Card, true);
                    if (Singleton<BoardManager>.Instance is BoardManager3D)
                    {
                        yield return new WaitForSeconds(0.5f);
                        yield return base.CreateDrawnCard();
                        yield return base.LearnAbility(0.5f);
                    }
                }
                yield break;
            }
        }
    }
}
