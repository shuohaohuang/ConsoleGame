namespace Checker
{
    public class Check
    {
        public static bool ValidateInput(string input, string[] validStrings)
        {
            bool Checker = false;

            input = input.ToUpper();

            for (int i = 0; i < validStrings.Length && !Checker; i++)
            {
                Checker = input.Equals(validStrings[i]);
            }
            return Checker;
        }

        public static bool InRange(float num, float max)
        {
            return num <= max;
        }

        public static bool InRange(float num, float min, float max)
        {
            return (num >= min && num <= max);
        }


        public static bool GreaterThan(float number)
        {
            const int Zero = 0;
            return number > Zero;
        }

        public static bool GreaterThan(float number, int compared)
        {
            return number > compared;
        }

        public static bool GreaterThan(float number, float compared)
        {
            return number > compared;
        }
    }
}
