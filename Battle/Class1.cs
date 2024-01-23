using Constants;
using System.Security;

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

    }
}
