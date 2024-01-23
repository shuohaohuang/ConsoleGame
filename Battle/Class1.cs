﻿using Constants;
using Checker;
using System.Security;
using static System.Net.Mime.MediaTypeNames;
using System.Threading;
using System.Diagnostics.Metrics;
using System.Globalization;

namespace BattleMethod
{
    public class Battle
    {
        public static void RandomOrder(int[] turn)
        {
            Random random = new Random();
            for (int i = 0; i < turn.Length; i++)
            {
                int j = 0;
                do
                {
                    bool repeated = false;
                    int aux = random.Next(4);
                    for (j = 0; j < turn.Length && !repeated; j++)
                    {
                        if (aux == turn[j])
                            repeated = true;
                    }
                    if (!repeated)
                        turn[i] = aux;
                } while (j != 4);

            }
        }
        public static void Attack(
            int atackerId,
            int defensorId,
            float[,] currentStats,
            string[] names,
            bool isGuarding = false
        )
        {
            float inflictedDamage;
            bool failedAttack,
                criticalAttack;
            int hpId = 0, attackId = 1, reductionId = 2;
            failedAttack = Battle.Probability(Constant.FailedAttackProbability);
            if (!failedAttack)
            {
                criticalAttack = Battle.Probability(Constant.CriticalProbability);
                if (criticalAttack)
                    Console.WriteLine(Constant.CriticalAttackMsg, names[atackerId]);
                inflictedDamage = Battle.CalculateDamage(
                    currentStats[atackerId, attackId],
                    currentStats[defensorId, reductionId],
                    criticalAttack,
                    isGuarding
                );
                Battle.InformAction(names[atackerId], names[defensorId], inflictedDamage);
                currentStats[defensorId, hpId] = Battle.RemainedHp(currentStats[defensorId, hpId], inflictedDamage);
            }
            else
            {
                Console.WriteLine(Constant.FailedAttackMsg, names[atackerId]);
            }
        }
        public static float CalculateDamage(
           float attackerAd,
           float defenderReduction,
           bool criticAttack,
           bool isGuarding
       )
        {
            const float CriticalEffect = 2;
            const float Percentage = 100,
                One = 1;
            defenderReduction = isGuarding
                ? defenderReduction * Constant.GuardEffect
                : defenderReduction;
            if (criticAttack)
                return Math.Abs(
                    attackerAd * (One - (defenderReduction / Percentage)) * CriticalEffect
                );

            return Math.Abs(attackerAd * (One - (defenderReduction / Percentage)));
        }

        public static float CalculateDamage(
            float attackerAd,
            float defenderReduction
        )
        {
            const float Percentage = 100,
                One = 1;

            return Math.Abs(attackerAd * (One - (defenderReduction / Percentage)));
        }

        public static float RemainedHp(float currentHp, float receivedDamage)
        {
            return receivedDamage > currentHp ? Constant.Zero : currentHp - receivedDamage;
        }

        public static void InformAction(
            string attackerName,
            string defenderName,
            float inflictedDamage
        )
        {
            Console.WriteLine(Constant.AttackMsg, attackerName, inflictedDamage, defenderName);
        }

        public static bool Probability(float probability)
        {
            const int MaxProbability = 100;

            Random random = new();

            return Check.InRange(random.Next(MaxProbability), probability);
        }

        public static void ArcherAbility(int casterId, int defensorId, int monsterStun,int[] abilityEffecet,int[] currentCD, string[] names)
        {
            monsterStun = abilityEffecet[casterId];
            currentCD[casterId] = Constant.SkillCd;
            Console.WriteLine(
                Constant.ArcherAbility,
                names[casterId],
                names[defensorId],
                abilityEffecet[casterId]
            );
        }
        public static void BarbarianAbility(int casterId, ref int duration,int [] currentCD, float[,] currentStats, int[] abilityEffect, string[] names)
        {
            currentStats[casterId, 2] = abilityEffect[casterId];
            currentCD[casterId] = Constant.SkillCd;
            Console.WriteLine(
                Constant.BarbarianAbility,
                names[casterId],
                Constant.BarbarianSkillDuration
            );
            duration = Constant.BarbarianSkillDuration;
        }

        public static void MageAbility(int casterId, int defensorId, int[] abilityEffecet, int[] currentCD, float[,] currentStats, string[] names)
        {
            currentCD[casterId] = Constant.SkillCd;
            
           
            float damage = Battle.CalculateDamage(currentStats[casterId, 1], currentStats[defensorId,2])*abilityEffecet[casterId];

            Console.WriteLine(
                Constant.MageAbility,
                names[casterId],
                damage,
                names[defensorId]
            );

            currentStats[defensorId, 0] = RemainedHp(currentStats[defensorId, 0], damage);
            
        }

        public static void DruidAbility(int casterId, float[,] currentStats,float[,] maxStats, int[] abilityEffect,string[] names)
        {
            for (int i = 0; i < currentStats.GetLength(0)-1; i++){
                if (Check.GreaterThan(currentStats[i,0]))
                {
                    float HealAmount = currentStats[i, 0] + abilityEffect[casterId] > maxStats[i, 0] 
                        ? (maxStats[i, 0] - currentStats[i, 0]) 
                        : abilityEffect[casterId];
                    Console.WriteLine(
                        Constant.DruidAbility,
                        names[casterId],
                        names[i],
                        HealAmount
                    );
                    
                }
            }
            
        }

    }
}
