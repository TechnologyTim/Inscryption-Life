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
		private NewAbility createGrigoriTrapSpawner()
		{
			AbilityInfo info = ScriptableObject.CreateInstance<AbilityInfo>();
			info.powerLevel = 5;
			info.rulebookName = "Trap builder";
			info.rulebookDescription = "A card bearing this sigil spawns two Zombie traps beside it.";
			info.metaCategories = new List<AbilityMetaCategory> { AbilityMetaCategory.Part1Rulebook, AbilityMetaCategory.Part1Modular };

			byte[] imgBytes = System.IO.File.ReadAllBytes(Path.Combine(this.Info.Location.Replace("InscryptionLife.dll", ""), "Artwork/headcrabbed.png"));
			Texture2D tex = new Texture2D(2, 2);
			tex.LoadImage(imgBytes);

			NewAbility trapSpawner = new NewAbility(info, typeof(GrigoriTrapSpawner), tex);
			GrigoriTrapSpawner.ability = trapSpawner.ability;
			return trapSpawner;
		}
	}
	public class GrigoriTrapSpawner : CreateCardsAdjacent
	{

		public override Ability Ability
		{
			get
			{
				return ability;
			}
		}

		// Token: 0x1700025E RID: 606
		// (get) Token: 0x0600133E RID: 4926 RVA: 0x0004376C File Offset: 0x0004196C
		public override string SpawnedCardId
		{
			get
			{
				return "zombie_trap";
			}
		}

		// Token: 0x1700025F RID: 607
		// (get) Token: 0x0600133F RID: 4927 RVA: 0x00043773 File Offset: 0x00041973
		public override string CannotSpawnDialogue
		{
			get
			{
				return "Brother, I have not enough space here !";
			}
		}
		public static Ability ability;
	}

}
