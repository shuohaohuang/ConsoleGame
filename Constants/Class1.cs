﻿namespace Constants
{
    public class Constant
    {
        public const string AttackMenuMsg = "Attack: ",
            CriticalAttackMsg = "\n{0} has executed a critical hit.",
            CurrentStatus = "{0} : {1} Hp",
            DefaultCommandMsg = "Too many attempts, lose turn",
            DefaultDifficultyMsg = "Too many attempts, default difficulty: Random\n",
            DefaultHeroStatsMsg = "Too many attempts, assigning lowest stats\n",
            DefaultMonsterStatsMsg = "Too many attempts, assigning highest stats\n",
            DifficultyEasy = "1",
            DifficultyDifficult = "2",
            DifficultyPersonalized = "4",
            DifficultyMenuMsg =
                "Choose the difficulty:"
                + "\n\t1.Easy: highest stats for heroes, lowest stats for monster"
                + "\n\t2.Difficult: lowest stats for heroes, highest stats for monster"
                + "\n\t3.RandomStats: Is the goddess of luck smiling upon you?"
                + "\n\t4.Personalized: personalize your heroes attributes and monster\n",
            DmgReductionMenuMsg = "Damage  Reduction: ",
            EndMsg = "End of the game\n",
            ErrorEndMsg = "Too many attempts, end of the game\n",
            ErrorMsg = "Wrong insert, try again\n",
            FailedAttackMsg = "\n{0} has failed the attack",
            FourStr = "4",
            HpMenuMsg = "Hit Points: ",
            InsertRequestMsg = "Insert stat value",
            MenuMsg = "1. Start a new game" + "\n0. Exit\n",
            No = "N",
            OnCooldown = "Skill on Cooldown, {0} turns until available\n",
            OneStr = "1",
            RangedInMsg = "In range [{0}-{1}]",
            RenamedMsg = "{0}'s new name is {1}",
            RenameMsg = "Do you want rename characters:\n[Y/N]\n",
            RequestCommandMsg =
                "\nInsert {0}'s action"
                + "\n\t1.Normal attack"
                + "\n\t2. Character's ability"
                + "\n\t3. Guard \n",
            RequestNameMsg = "Insert {0}'s new name is ",
            RequestValueOfStatsMsg =
                "Next, you will enter the stats of {0} within the specified ranges.",
            Round = "Round {0}",
            ThreeStr = "3",
            TwoStr = "2",
            Yes = "Y",
            ZeroStr = "0",
            MonsterName = "Monster",
            ArcherName = "Archer",
            ArcherAbility = "{0} has stunned {1} for {2} turns",
            BarbarianName = "Barbarian",
            BarbarianAbility = "{0} is immune to damage for {1} turns",
            MageName = "Mage",
            MageAbility = "{0}' ability has dealt {1} damage to {2}",
            DruidName = "Druid",
            AttackMsg = "\n{0} has dealt {1} damage to {2}",
            DruidAbility = "{0} has healed {1} {2} hp",
            MonseterAttackMsg = "\n{0} is about to attack next",
            WinMsg = "You Win",
            LoseMsg = "You lose";

        public const int Zero = 0,
            One = 1,
            Two = 2,
            Three = 3,
            Four = 4,
            Hundred = 100,
            FiveHundred = 100,
            SkillCd = 5,
            StatsTypes = 3,
            RowsIteration = 1,
            RowToSetMaxValues = 2,
            RowCurrentValues = 3,
            MinValueRow = 0,
            MaxValueRow = 1,
            MaxAttempts = 3,
            CriticalProbability = 10,
            FailedAttackProbability = 5,
            HpValueColumn = 0,
            MaxStatsRow = 2,
            AttackValueColumn = 1,
            GuardEffect = 2,
            BarbarianSkillDuration = 2,
            ReductionValueColumn = 2,
            ArcherId = 0,
            BarbarianId = 1,
            MageId = 2,
            DruidId = 3,
            MonsterId = 4,
            HpId = 0,
            AttackId = 1,
            ReductionId = 2;
    }
}
