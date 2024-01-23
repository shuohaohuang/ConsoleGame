using Constants;

namespace Stats
{
    public class Stat
    {

        public static void SetPlayerCap(int hero, float[,,] defaultValues, float[,] maxValues, bool isHero, string difficulty)
        {
            if (difficulty.Equals(Constant.DifficultyDifficult) || difficulty.Equals(Constant.DifficultyEasy))
            {
                DefaultLevel(hero, defaultValues, maxValues, isHero, difficulty);
            }
            else
            {
                // RandomLevel(CharacterStats);
            }
        }

        public static void DefaultLevel(int hero, float[,,] defaultValues, float[,] maxValues, bool isHero, string difficulty)
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
