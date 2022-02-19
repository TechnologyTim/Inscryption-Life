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
        private NewAbility createHeadcrabInfest()
        {
            AbilityInfo info = ScriptableObject.CreateInstance<AbilityInfo>();
            info.powerLevel = 3;
            info.rulebookName = "Headcrab Infestation";
            info.rulebookDescription = "Card bearing this sigil will infest other cards and turn them into Headcrab Zombies.";
            info.metaCategories = new List<AbilityMetaCategory> { AbilityMetaCategory.Part1Rulebook, AbilityMetaCategory.Part1Modular };

            byte[] imgBytes = System.IO.File.ReadAllBytes(Path.Combine(this.Info.Location.Replace("InscryptionLife.dll", ""), "Artwork/ability_latch.png"));
            Texture2D tex = new Texture2D(2, 2);
            tex.LoadImage(imgBytes);

            NewAbility headcrabInfest = new NewAbility(info, typeof(HeadcrabInfest), tex);
            HeadcrabInfest.ability = headcrabInfest.ability;
            return headcrabInfest;
        }

    }

    public class HeadcrabInfest : Latch
    {
        public override Ability Ability
        {
            get
            {
                return ability;
            }
        }

        public static Ability ability;

        // Token: 0x1700028B RID: 651
        // (get) Token: 0x060013D5 RID: 5077 RVA: 0x0004342A File Offset: 0x0004162A
        public override Ability LatchAbility
        {
            get
            {
                return Headcrabbed.ability;

            }
        }
    }
}
