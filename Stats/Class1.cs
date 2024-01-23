using Constants;

namespace Stats
{
    public class Stat
    {
        public static void SetPlayerCap(
            int id,
            float[,,] defaultValues,
            float[,] maxValues,
            bool isHero,
            string difficulty
        )
        {
            if (
                difficulty.Equals(Constant.DifficultyDifficult)
                || difficulty.Equals(Constant.DifficultyEasy)
            )
            {
                DefaultLevel(id, defaultValues, maxValues, isHero, difficulty);
            }
            else
            {
                RandomLevel(id, defaultValues, maxValues);
            }
        }

        public static void RandomLevel(int id, float[,,] defaultValues, float[,] maxValues)
        {
            const int ReachMaxNum = 1;

            Random rnd = new();
            for (int statType = 0; statType < defaultValues.GetLength(0); statType++)
            {
                maxValues[id, statType] = rnd.Next(
                    (int)defaultValues[statType, Constant.MinValueRow, id],
                    (int)defaultValues[statType, Constant.MaxValueRow , id ]+ ReachMaxNum
                );
            }
        }

        public static void DefaultLevel(
            int hero,
            float[,,] defaultValues,
            float[,] maxValues,
            bool isHero,
            string difficulty
        )
        {
            int rowToPick;

            if (isHero)
            {
                rowToPick =
                    difficulty == Constant.DifficultyEasy
                        ? Constant.MaxValueRow
                        : Constant.MinValueRow;
            }
            else
            {
                rowToPick =
                    difficulty == Constant.DifficultyEasy
                        ? Constant.MinValueRow
                        : Constant.MaxValueRow;
            }

            for (int statType = 0; statType < maxValues.GetLength(1); statType++)
            {
                maxValues[hero, statType] = defaultValues[statType, rowToPick, hero];
            }
        }
    }
}
