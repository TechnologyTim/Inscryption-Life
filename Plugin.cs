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
    [BepInPlugin(PluginGuid, PluginName, PluginVersion)]
    [BepInDependency("cyantist.inscryption.api", BepInDependency.DependencyFlags.HardDependency)]
    public partial class Plugin : BaseUnityPlugin
    {
        private const string PluginGuid = "technologytim.inscryption.inscryption-life";
        private const string PluginName = "inscryption-life";
        private const string PluginVersion = "0.0.1";

        private void Awake()
        {
            Logger.LogInfo($"Loaded {PluginName}!");

            // abilities
            createHeadcrabInfest();
            createHeadcrabbed();
            createZombieTrapAbility();
            createGrigoriTrapSpawner();

            // cards
            createCombine();
            createHeadcrab();
            createHeadcrabZombie();
            create9mmHandgun();
            createAntlion();
            createBreen();
            createBullchicken();
            createBullchicken2();
            createCombineElite();
            createCrowbar();
            createGrigori();
            createZombieCorpse();
            createZombieTrap();
            createStrider();
            createMale07();
        }

        private void createCombine()
        {
            List<CardMetaCategory> metaCategories = new List<CardMetaCategory>();
            metaCategories.Add(CardMetaCategory.ChoiceNode);
            metaCategories.Add(CardMetaCategory.TraderOffer);

            List<Ability> abilities = new List<Ability>();
            abilities.Add(Ability.Sniper);
            abilities.Add(Ability.DeathShield);

            byte[] imgBytes = System.IO.File.ReadAllBytes(Path.Combine(this.Info.Location.Replace("InscryptionLife.dll", ""), "Artwork/soldier.png"));
            Texture2D tex = new Texture2D(2, 2);
            tex.LoadImage(imgBytes);

            byte[] imgBytes1 = System.IO.File.ReadAllBytes(Path.Combine(this.Info.Location.Replace("InscryptionLife.dll", ""), "Artwork/soldierdecal.png"));
            Texture2D tex1 = new Texture2D(2, 2);
            tex1.LoadImage(imgBytes1);

            List<Texture> decals = new();
            decals.Add(tex1);

            NewCard.Add
            (
                "combine",
                "Combine Soldier",
                2,
                2,
                metaCategories,
                CardComplexity.Intermediate,
                CardTemple.Nature,
                description: "A feared soldier of the infinite Combine empire.",
                energyCost: 6,
                abilities: abilities,
                defaultTex: tex,
                decals: decals
            );

        }

        private void createCombineElite()
        {
            List<CardMetaCategory> metaCategories = new List<CardMetaCategory>();
            metaCategories.Add(CardMetaCategory.ChoiceNode);
            metaCategories.Add(CardMetaCategory.TraderOffer);

            List<Ability> abilities = new List<Ability>();
            abilities.Add(Ability.Sniper);
            abilities.Add(Ability.DeathShield);

            byte[] imgBytes = System.IO.File.ReadAllBytes(Path.Combine(this.Info.Location.Replace("InscryptionLife.dll", ""), "Artwork/combine_elite.png"));
            Texture2D tex = new Texture2D(2, 2);
            tex.LoadImage(imgBytes);

            byte[] imgBytes1 = System.IO.File.ReadAllBytes(Path.Combine(this.Info.Location.Replace("InscryptionLife.dll", ""), "Artwork/combine_elite_decal.png"));
            Texture2D tex1 = new Texture2D(2, 2);
            tex1.LoadImage(imgBytes1);

            List<Texture> decals = new();
            decals.Add(tex1);

            NewCard.Add
            (
                "combine_elite",
                "Combine Elite Soldier",
                2, // attack
                3, // health
                metaCategories,
                CardComplexity.Intermediate,
                CardTemple.Nature,
                description: "The horrifying Combine Elite Soldier. The speical forces of the Combine empire.",
                bonesCost: 4,
                abilities: abilities,
                defaultTex: tex
            );

        }

        private void createHeadcrab()
        {
            List<CardMetaCategory> metaCategories = new List<CardMetaCategory>();
            metaCategories.Add(CardMetaCategory.ChoiceNode);
            metaCategories.Add(CardMetaCategory.TraderOffer);

            List<Ability> abilities = new List<Ability>();
            abilities.Add(HeadcrabInfest.ability);
            abilities.Add(Ability.Brittle);

            byte[] imgBytes = System.IO.File.ReadAllBytes(Path.Combine(this.Info.Location.Replace("InscryptionLife.dll", ""), "Artwork/headcrab.png"));
            Texture2D tex = new Texture2D(2, 2);
            tex.LoadImage(imgBytes);

            NewCard.Add
            (
                "headcrab",
                "Headcrab",
                2, // attack
                1, // health
                metaCategories,
                CardComplexity.Intermediate,
                CardTemple.Nature,
                description: "The horrible headcrab that can infect you.",
                energyCost: 2,
                bonesCost: 2,
                abilities: abilities,
                defaultTex: tex
            );

        }

        private void createHeadcrabZombie()
        {
            List<CardMetaCategory> metaCategories = new List<CardMetaCategory>();
            metaCategories.Add(CardMetaCategory.ChoiceNode);
            metaCategories.Add(CardMetaCategory.TraderOffer);

            List<Ability> abilities = new List<Ability>();
            abilities.Add(Ability.BoneDigger);
            abilities.Add(Ability.GainBattery);

            byte[] imgBytes = System.IO.File.ReadAllBytes(Path.Combine(this.Info.Location.Replace("InscryptionLife.dll", ""), "Artwork/soldier.png"));
            Texture2D tex = new Texture2D(2, 2);
            tex.LoadImage(imgBytes);


            NewCard.Add
            (
                "headcrab_zombie",
                "Headcrab Zombie",
                1, // attack
                1, // health
                metaCategories,
                CardComplexity.Intermediate,
                CardTemple.Nature,
                description: "The slow and threatening Headcrab Zombie.",
                bonesCost: 4,
                abilities: abilities,
                defaultTex: tex
            );

        }

        private void createGrigori()
        {
            List<CardMetaCategory> metaCategories = new List<CardMetaCategory>();
            metaCategories.Add(CardMetaCategory.ChoiceNode);
            metaCategories.Add(CardMetaCategory.TraderOffer);

            List<Ability> abilities = new List<Ability>();
            abilities.Add(GrigoriTrapSpawner.ability);


            byte[] imgBytes = System.IO.File.ReadAllBytes(Path.Combine(this.Info.Location.Replace("InscryptionLife.dll", ""), "Artwork/soldier.png"));
            Texture2D tex = new Texture2D(2, 2);
            tex.LoadImage(imgBytes);


            NewCard.Add
            (
                "father_grigori",
                "Father Grigori",
                1, // attack
                4, // health
                metaCategories,
                CardComplexity.Intermediate,
                CardTemple.Nature,
                description: "The insane priest from Ravenholm.",
                bloodCost: 1,
                energyCost: 4,
                abilities: abilities,
                defaultTex: tex
            );

        }

        private void createZombieCorpse()
        {
            List<CardMetaCategory> metaCategories = new List<CardMetaCategory>();

            List<Ability> abilities = new List<Ability>();
            // abilities.Add(Ability.BoneDigger);


            byte[] imgBytes = System.IO.File.ReadAllBytes(Path.Combine(this.Info.Location.Replace("InscryptionLife.dll", ""), "Artwork/soldier.png"));
            Texture2D tex = new Texture2D(2, 2);
            tex.LoadImage(imgBytes);


            NewCard.Add
            (
                "headcrab_zombie_corpse",
                "Zombie Corpse",
                0, // attack
                2, // health
                metaCategories,
                CardComplexity.Intermediate,
                CardTemple.Nature,
                description: "A Headcrab zombie, but dead.",
                abilities: abilities,
                defaultTex: tex
            );

        }

        private void createZombieTrap()
        {
            List<CardMetaCategory> metaCategories = new List<CardMetaCategory>();

            List<Ability> abilities = new List<Ability>();
            abilities.Add(ZombieTrap.ability);


            byte[] imgBytes = System.IO.File.ReadAllBytes(Path.Combine(this.Info.Location.Replace("InscryptionLife.dll", ""), "Artwork/soldier.png"));
            Texture2D tex = new Texture2D(2, 2);
            tex.LoadImage(imgBytes);


            NewCard.Add
            (
                "zombie_trap",
                "Zombie trap",
                0, // attack
                1, // health
                metaCategories,
                CardComplexity.Intermediate,
                CardTemple.Nature,
                description: "A Headcrab zombie, but dead.",
                abilities: abilities,
                defaultTex: tex
            );

        }

        private void create9mmHandgun()
        {
            List<CardMetaCategory> metaCategories = new List<CardMetaCategory>();
            metaCategories.Add(CardMetaCategory.ChoiceNode);

            List<Ability> abilities = new List<Ability>();
            abilities.Add(Ability.Sniper);
            abilities.Add(Ability.Brittle);

            byte[] imgBytes = System.IO.File.ReadAllBytes(Path.Combine(this.Info.Location.Replace("InscryptionLife.dll", ""), "Artwork/soldier.png"));
            Texture2D tex = new Texture2D(2, 2);
            tex.LoadImage(imgBytes);


            NewCard.Add
            (
                "9mm_handgun",
                "9mm semi-automatic handgun",
                2, // attack
                1, // health
                metaCategories,
                CardComplexity.Intermediate,
                CardTemple.Nature,
                description: "Your trusty weapon, the 9mm semi-automatic handgun.",
                bonesCost: 2,
                abilities: abilities,
                defaultTex: tex
            );

        }

        private void createCrowbar()
        {
            List<CardMetaCategory> metaCategories = new List<CardMetaCategory>();
            metaCategories.Add(CardMetaCategory.ChoiceNode);

            List<Ability> abilities = new List<Ability>();
            abilities.Add(Ability.Brittle);

            byte[] imgBytes = System.IO.File.ReadAllBytes(Path.Combine(this.Info.Location.Replace("InscryptionLife.dll", ""), "Artwork/soldier.png"));
            Texture2D tex = new Texture2D(2, 2);
            tex.LoadImage(imgBytes);


            NewCard.Add
            (
                "crowbar",
                "Grodon Freeman's crowbar",
                3, // attack
                1, // health
                metaCategories,
                CardComplexity.Intermediate,
                CardTemple.Nature,
                description: "Gordon's trusty weapon and popluar Half-Life symbol.",
                bloodCost: 1,
                abilities: abilities,
                defaultTex: tex
            );

        }

        private void createMale07()
        {
            List<CardMetaCategory> metaCategories = new List<CardMetaCategory>();

            List<Ability> abilities = new List<Ability>();
            // abilities.Add(Ability.BoneDigger);
            // abilities.Add(Ability.GainBattery);

            byte[] imgBytes = System.IO.File.ReadAllBytes(Path.Combine(this.Info.Location.Replace("InscryptionLife.dll", ""), "Artwork/soldier.png"));
            Texture2D tex = new Texture2D(2, 2);
            tex.LoadImage(imgBytes);

            NewCard.Add
            (
                "male07",
                "Male07 rebel",
                1, // attack
                1, // health
                metaCategories,
                CardComplexity.Intermediate,
                CardTemple.Nature,
                description: "The default option.",
                energyCost: 1,
                abilities: abilities,
                defaultTex: tex
            );

        }

        private void createStrider()
        {
            List<CardMetaCategory> metaCategories = new List<CardMetaCategory>();
            metaCategories.Add(CardMetaCategory.Rare);

            List<CardAppearanceBehaviour.Appearance> appearanceBehaviour = new List<CardAppearanceBehaviour.Appearance>();
            appearanceBehaviour.Add(CardAppearanceBehaviour.Appearance.RareCardBackground);

            List<Ability> abilities = new List<Ability>();
            abilities.Add(Ability.SplitStrike);
            abilities.Add(Ability.Sniper);
            abilities.Add(Ability.DeathShield);

            byte[] imgBytes = System.IO.File.ReadAllBytes(Path.Combine(this.Info.Location.Replace("InscryptionLife.dll", ""), "Artwork/soldier.png"));
            Texture2D tex = new Texture2D(2, 2);
            tex.LoadImage(imgBytes);

            byte[] imgBytes1 = System.IO.File.ReadAllBytes(Path.Combine(this.Info.Location.Replace("InscryptionLife.dll", ""), "Artwork/soldierdecal.png"));
            Texture2D tex1 = new Texture2D(2, 2);
            tex1.LoadImage(imgBytes1);

            List<Texture> decals = new();
            decals.Add(tex1);

            NewCard.Add
            (
                "strider",
                "The Strider",
                3, // attack
                7, // health
                metaCategories,
                CardComplexity.Intermediate,
                CardTemple.Nature,
                description: "The combines walking inflantery.",
                bloodCost: 2,
                energyCost: 3,
                bonesCost: 4,
                abilities: abilities,
                appearanceBehaviour: appearanceBehaviour,
                defaultTex: tex,
                decals: decals
            );

        }

        private void createAntlion()
        {
            List<CardMetaCategory> metaCategories = new List<CardMetaCategory>();
            metaCategories.Add(CardMetaCategory.Rare);

            List<CardAppearanceBehaviour.Appearance> appearanceBehaviour = new List<CardAppearanceBehaviour.Appearance>();
            appearanceBehaviour.Add(CardAppearanceBehaviour.Appearance.RareCardBackground);

            List<Ability> abilities = new List<Ability>();
            abilities.Add(Ability.Flying);
            abilities.Add(Ability.DrawCopy);

            List<Trait> traits = new List<Trait>();
            traits.Add(Trait.Ant);

            List<SpecialTriggeredAbility> specialAbilities = new List<SpecialTriggeredAbility>();
            specialAbilities.Add(SpecialTriggeredAbility.Ant);

            byte[] imgBytes = System.IO.File.ReadAllBytes(Path.Combine(this.Info.Location.Replace("InscryptionLife.dll", ""), "Artwork/soldier.png"));
            Texture2D tex = new Texture2D(2, 2);
            tex.LoadImage(imgBytes);

            byte[] imgBytes1 = System.IO.File.ReadAllBytes(Path.Combine(this.Info.Location.Replace("InscryptionLife.dll", ""), "Artwork/soldierdecal.png"));
            Texture2D tex1 = new Texture2D(2, 2);
            tex1.LoadImage(imgBytes1);

            List<Texture> decals = new();
            decals.Add(tex1);

            NewCard.Add
            (
                "antlion",
                "The Antlion",
                0, // attack
                2, // health
                metaCategories,
                CardComplexity.Intermediate,
                CardTemple.Nature,
                description: "Part of the Xen wildlife, the Anltion is not very strong, but is still theatening when used in groups.",
                bloodCost: 1,
                energyCost: 3,
                specialStatIcon: SpecialStatIcon.Ants,
                traits: traits,
                specialAbilities: specialAbilities,
                appearanceBehaviour: appearanceBehaviour,
                abilities: abilities,
                defaultTex: tex,
                decals: decals
            );

        }

        private void createBullchicken()
        {
            List<CardMetaCategory> metaCategories = new List<CardMetaCategory>();
            metaCategories.Add(CardMetaCategory.ChoiceNode);
            metaCategories.Add(CardMetaCategory.TraderOffer);

            List<Ability> abilities = new List<Ability>();
            abilities.Add(Ability.Sniper);
            abilities.Add(Ability.Transformer);
            abilities.Add(Ability.Deathtouch);

            byte[] imgBytes = System.IO.File.ReadAllBytes(Path.Combine(this.Info.Location.Replace("InscryptionLife.dll", ""), "Artwork/soldier.png"));
            Texture2D tex = new Texture2D(2, 2);
            tex.LoadImage(imgBytes);

            byte[] imgBytes1 = System.IO.File.ReadAllBytes(Path.Combine(this.Info.Location.Replace("InscryptionLife.dll", ""), "Artwork/soldierdecal.png"));
            Texture2D tex1 = new Texture2D(2, 2);
            tex1.LoadImage(imgBytes1);

            List<Texture> decals = new();
            decals.Add(tex1);

            NewCard.Add
            (
                "bullchicken",
                "The Bullsquid",
                1, // attack
                3, // health
                metaCategories,
                CardComplexity.Intermediate,
                CardTemple.Nature,
                description: "Sadly forgotten since Half-Life 1, but worshipped at bullsquid.com",
                bloodCost: 1,
                bonesCost: 3,
                abilities: abilities,
                defaultTex: tex,
                decals: decals,
                evolveId: new EvolveIdentifier("bullchicken2", 1)
            );

        }

        private void createBullchicken2()
        {
            List<CardMetaCategory> metaCategories = new List<CardMetaCategory>();

            List<Ability> abilities = new List<Ability>();
            abilities.Add(Ability.Transformer);

            byte[] imgBytes = System.IO.File.ReadAllBytes(Path.Combine(this.Info.Location.Replace("InscryptionLife.dll", ""), "Artwork/soldier.png"));
            Texture2D tex = new Texture2D(2, 2);
            tex.LoadImage(imgBytes);

            byte[] imgBytes1 = System.IO.File.ReadAllBytes(Path.Combine(this.Info.Location.Replace("InscryptionLife.dll", ""), "Artwork/soldierdecal.png"));
            Texture2D tex1 = new Texture2D(2, 2);
            tex1.LoadImage(imgBytes1);

            List<Texture> decals = new();
            decals.Add(tex1);

            NewCard.Add
            (
                "bullchicken2",
                "The Bullsquid",
                1, // attack
                3, // health
                metaCategories,
                CardComplexity.Intermediate,
                CardTemple.Nature,
                description: "Sadly forgotten since Half-Life 1, but worshipped at bullsquid.com",
                bloodCost: 1,
                bonesCost: 3,
                abilities: abilities,
                defaultTex: tex,
                decals: decals,
                evolveId: new EvolveIdentifier("bullchicken", 1)
            );

        }

        private void createBreen()
        {
            List<CardMetaCategory> metaCategories = new List<CardMetaCategory>();
            metaCategories.Add(CardMetaCategory.ChoiceNode);
            metaCategories.Add(CardMetaCategory.TraderOffer);

            List<Ability> abilities = new List<Ability>();
            abilities.Add(Ability.BuffNeighbours);

            byte[] imgBytes = System.IO.File.ReadAllBytes(Path.Combine(this.Info.Location.Replace("InscryptionLife.dll", ""), "Artwork/soldier.png"));
            Texture2D tex = new Texture2D(2, 2);
            tex.LoadImage(imgBytes);

            byte[] imgBytes1 = System.IO.File.ReadAllBytes(Path.Combine(this.Info.Location.Replace("InscryptionLife.dll", ""), "Artwork/soldierdecal.png"));
            Texture2D tex1 = new Texture2D(2, 2);
            tex1.LoadImage(imgBytes1);

            List<Texture> decals = new();
            decals.Add(tex1);

            NewCard.Add
            (
                "breen",
                "Dr. Wallace Breen",
                0, // attack
                1, // health
                metaCategories,
                CardComplexity.Intermediate,
                CardTemple.Nature,
                description: "Once the Black Mesa administrator, now Dr. Breen is the face of the feared Combine on the Earth.",
                bloodCost: 4,
                abilities: abilities,
                defaultTex: tex,
                decals: decals
            );

        }

    }

}
