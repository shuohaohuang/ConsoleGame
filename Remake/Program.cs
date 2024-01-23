using System;
using System.Runtime.InteropServices;
using System.Threading;
using BattleMethod;
using Checker;
using Constants;
using Menssages;
using Stats;
using Utilities;

namespace Game
{
    class HVsM
    {
        public static void Main()
        {
            int roundCounter = 0,
                remainingAttempts = Constant.MaxAttempts;
            int[] characters = { 0, 1, 2, 3, 4 },
                randomTurns = { 4, 4, 4, 4 },
                abilityEffect = { 2, 100, 3, 500 },
                currentCoolDown = { 0, 0, 0, 0 };
            float userValue;

            float[,,] statsValues =
            {
                {
                    { 1500f, 3000, 1100, 2000, 7000 },
                    { 2000, 3750, 1500, 2500, 10000 }
                },
                {
                    { 200, 150, 300, 70, 300 },
                    { 300, 250, 400, 120, 400 }
                },
                {
                    { 25, 35, 20, 25, 20 },
                    { 35, 45, 35, 40, 30 }
                }
            };

            float[,] maxValues = new float[5, 3],
                currentValues = new float[5, 3];

            bool validInput,
                hasRemainingAttemptsMenu = true,
                hasRemainingAttempts = true;
            bool[] isHero = { true, true, true, true, false },
                isGuarding = { false, false, false, false },
                Alive = { true, true, true, true, true };

            string userCommand,
                difficulty = "0";

            string[] names =
                {
                    Constant.ArcherName,
                    Constant.BarbarianName,
                    Constant.MageName,
                    Constant.DruidName,
                    Constant.MonsterName
                },
                twoValidInputs = [Constant.OneStr, Constant.ZeroStr],
                threeValidInputs = [Constant.OneStr, Constant.TwoStr, Constant.ThreeStr],
                fourValidInputs =
                    [Constant.OneStr, Constant.TwoStr, Constant.ThreeStr, Constant.FourStr],
                StatsRequirementMsg =

                    [
                        Constant.HpMenuMsg + Constant.RangedInMsg,
                        Constant.AttackMenuMsg + Constant.RangedInMsg,
                        Constant.DmgReductionMenuMsg + Constant.RangedInMsg
                    ],
                boolValidInputs = { Constant.Yes, Constant.No };

            Console.WriteLine(Constant.MenuMsg);
            do
            {
                userCommand = Console.ReadLine() ?? "";
                validInput = Check.ValidateInput(userCommand, twoValidInputs);
                Msg.ValidateInput(
                    ref remainingAttempts,
                    ref hasRemainingAttemptsMenu,
                    validInput,
                    Constant.ErrorEndMsg
                );
            } while (!validInput && hasRemainingAttemptsMenu);
            if (userCommand.Equals(Constant.OneStr))
            {
                //difficulty Selector
                Console.WriteLine(Constant.DifficultyMenuMsg);
                do
                {
                    userCommand = Console.ReadLine() ?? "";
                    validInput = Check.ValidateInput(userCommand, fourValidInputs);
                    Msg.ValidateInput(
                        ref remainingAttempts,
                        ref hasRemainingAttemptsMenu,
                        validInput,
                        Constant.ErrorEndMsg
                    );
                    difficulty = userCommand;
                } while (!validInput && hasRemainingAttemptsMenu);

                if (hasRemainingAttemptsMenu)
                {
                    Console.WriteLine(Constant.RenameMsg);
                    do
                    {
                        userCommand = Console.ReadLine() ?? "";
                        validInput = Check.ValidateInput(userCommand, boolValidInputs);
                        Msg.ValidateInput(
                            ref remainingAttempts,
                            ref hasRemainingAttemptsMenu,
                            validInput,
                            Constant.ErrorEndMsg
                        );
                    } while (!validInput && hasRemainingAttemptsMenu);
                }

                if (userCommand.ToUpper().Equals(Constant.Yes))
                {
                    for (int i = 0; i < names.Length; i++)
                    {
                        string oldName = names[i];
                        Console.WriteLine(Constant.RequestNameMsg, names[i]);
                        names[i] = Utility.NameMayus(Console.ReadLine() ?? names[i]);
                    }
                }


            }
        }
    }
}
