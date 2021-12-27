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
        private NewAbility createHeadcrabbed()
        {
            AbilityInfo info = ScriptableObject.CreateInstance<AbilityInfo>();
            info.powerLevel = 3;
            info.rulebookName = "Headcrabbed";
            info.rulebookDescription = "This card will turn into a Headcrab Zombie on death.";
            info.metaCategories = new List<AbilityMetaCategory> { AbilityMetaCategory.Part1Rulebook, AbilityMetaCategory.Part1Modular };

            byte[] imgBytes = System.IO.File.ReadAllBytes(Path.Combine(this.Info.Location.Replace("InscryptionLife.dll", ""), "Artwork/headcrabbed.png"));
            Texture2D tex = new Texture2D(2, 2);
            tex.LoadImage(imgBytes);

            NewAbility headcrabbed = new NewAbility(info, typeof(Headcrabbed), tex);
            Headcrabbed.ability = headcrabbed.ability;
            return headcrabbed;
        }

    }

    public class Headcrabbed : Brittle
	{

		public override Ability Ability
		{
			get
			{
				return ability;
			}
		}

		public override bool RespondsToOtherCardDie(PlayableCard card, CardSlot deathSlot, bool fromCombat, PlayableCard killer)
		{
			return card == base.Card && fromCombat && base.Card.OnBoard;
		}

		public override IEnumerator OnOtherCardDie(PlayableCard card, CardSlot deathSlot, bool fromCombat, PlayableCard killer)
		{
			yield return base.PreSuccessfulTriggerSequence();
			yield return new WaitForSeconds(0.1f);
			yield return Singleton<BoardManager>.Instance.CreateCardInSlot(CardLoader.GetCardByName("headcrab_zombie"), base.Card.Slot, 0.1f, true);
			yield return base.LearnAbility(0.5f);
			yield break;
		}

        // Token: 0x0600130C RID: 4876 RVA: 0x0004342E File Offset: 0x0004162E
        public override bool RespondsToSlotTargetedForAttack(CardSlot slot, PlayableCard attacker)
        {
            return attacker == base.Card;
        }

        // Token: 0x0600130D RID: 4877 RVA: 0x0004343C File Offset: 0x0004163C
        public override IEnumerator OnSlotTargetedForAttack(CardSlot slot, PlayableCard attacker)
        {
            this.startedAttack = true;
            yield break;
        }

        // Token: 0x0600130E RID: 4878 RVA: 0x0004344B File Offset: 0x0004164B
        public override bool RespondsToAttackEnded()
        {
            return this.startedAttack;
        }

        // Token: 0x0600130F RID: 4879 RVA: 0x00043453 File Offset: 0x00041653
        public override IEnumerator OnAttackEnded()
        {
            yield return base.PreSuccessfulTriggerSequence();
            if (base.Card != null && !base.Card.Dead)
            {
                if (!SaveManager.SaveFile.IsPart2)
                {
                    Singleton<ViewManager>.Instance.SwitchToView(View.Board, false, false);
                    yield return new WaitForSeconds(0.1f);
                }
                yield return base.Card.Die(false, null, true);
                yield return base.LearnAbility(0.25f);
                if (!SaveManager.SaveFile.IsPart2)
                {
                    yield return new WaitForSeconds(0.1f);
                }
            }
            yield break;
        }

        // Token: 0x04000022 RID: 34
        public static Ability ability;


	}
}
