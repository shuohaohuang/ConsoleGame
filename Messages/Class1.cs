using Checker;
using Constants;

namespace Menssages
{
    public class Msg
    {
        public static void ErrorCommand(ref bool moreAttempts, ref int attempts, string outOfAttempts)
        {
            attempts--;
            moreAttempts = Check.GreaterThan(attempts);
            Console.WriteLine(
                moreAttempts
                    ? Constant.ErrorMsg
                    : outOfAttempts
            );
        }

        public static void CurrentHp(float[] Hp, string[] names)
        {

        }
        public static void ValidateInput(ref int remainingAttempts, ref bool hasMoreAttempts, bool validInput, string ErrorOutOfAttemptsMsg)
        {
            if (!validInput)
            {
                remainingAttempts--;
                hasMoreAttempts = Check.GreaterThan(remainingAttempts);
                Console.WriteLine(
                    hasMoreAttempts ? Constant.ErrorMsg : ErrorOutOfAttemptsMsg
                );
            }
            else
            {
                remainingAttempts = Constant.MaxAttempts;
                hasMoreAttempts = Check.GreaterThan(remainingAttempts);
            }

        }
        public static void NoticeOnCoolDown(int RemainingCD)
        {
            Console.WriteLine(Constant.OnCooldown, RemainingCD);
        }
    }
}
