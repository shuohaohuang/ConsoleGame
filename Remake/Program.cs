using System;
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
            int roundCounter = 1,
                remainingAttempts = Constant.MaxAttempts,
                monsterStun = 0,
                BarbarianAbilityDuracion = 0;
            int[] characters =  { 0, 1, 2, 3, 4 },
                randomTurns =  { 4, 4, 4, 4 },
                abilityEffect =  { 2, 100, 3, 500 },
                currentCoolDown = [0, 0, 0, 0];
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

            float[,] maxStats = new float[5, 3],
                currentStats = new float[5, 3];

            bool validInput,
                hasRemainingAttemptsMenu = true,
                hasRemainingAttempts = true;
            bool[] isHero =  { true, true, true, true, false },
                isGuarding =  { false, false, false, false },
                Alive =  { true, true, true, true, true };

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
                boolValidInputs =  { Constant.Yes, Constant.No };

            Console.WriteLine(Constant.MenuMsg);
            //Start Menu
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
                    //rename Menu
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

                if (hasRemainingAttemptsMenu)
                {
                    //Rename characters
                    if (userCommand.ToUpper().Equals(Constant.Yes))
                    {
                        for (int i = 0; i < names.Length; i++)
                        {
                            string oldName = names[i];
                            Console.WriteLine(Constant.RequestNameMsg, names[i]);
                            names[i] = Utility.NameMayus(Console.ReadLine() ?? names[i]);
                        }
                    }

                    //Personalize Start or auto
                    if (difficulty.Equals(Constant.DifficultyPersonalized))
                    {
                        for (int character = 0; character < characters.Length; character++)
                        {
                            for (int statsType = 0; statsType < Constant.StatsTypes; statsType++)
                            {
                                do
                                {
                                    Console.WriteLine(
                                        $"{Constant.InsertRequestMsg}\n {StatsRequirementMsg[statsType]}",
                                        statsValues[statsType, Constant.MinValueRow, character],
                                        statsValues[statsType, Constant.MaxValueRow, character]
                                    );
                                    userValue = Convert.ToSingle(Console.ReadLine());
                                    validInput = Check.InRange(
                                        userValue,
                                        statsValues[statsType, Constant.MinValueRow, character],
                                        statsValues[statsType, Constant.MaxValueRow, character]
                                    );
                                    if (!validInput)
                                    {
                                        remainingAttempts--;
                                        hasRemainingAttempts = Check.GreaterThan(remainingAttempts);

                                        if (hasRemainingAttempts)
                                        {
                                            Console.WriteLine(Constant.ErrorMsg);
                                        }
                                        else
                                        {
                                            Console.WriteLine(Constant.DefaultHeroStatsMsg);
                                            maxStats[character, statsType] = statsValues[
                                                statsType,
                                                Constant.MinValueRow,
                                                character
                                            ];
                                        }
                                    }
                                    else
                                    {
                                        maxStats[character, statsType] = userValue;
                                    }
                                } while (!validInput && hasRemainingAttempts);
                                remainingAttempts = Constant.MaxAttempts;
                                hasRemainingAttempts = Check.GreaterThan(remainingAttempts);
                            }
                        }
                    }
                    else
                    {
                        for (int character = 0; character < characters.Length; character++)
                        {
                            Stat.SetPlayerCap(
                                character,
                                statsValues,
                                maxStats,
                                isHero[character],
                                difficulty
                            );
                        }
                    }

                    for(int i=0; i< maxStats.GetLength(0); i++)
                    {
                        for(int j = 0; j< maxStats.GetLength(1); j++)
                        {
                            currentStats[i, j] = maxStats[i,j];
                        }
                    }
                    //Start Battle phase
                    while (
                        (
                            Alive[Constant.ArcherId]
                            || Alive[Constant.BarbarianId]
                            || Alive[Constant.MageId]
                            || Alive[Constant.DruidId]
                        ) && Alive[Constant.MonsterId]
                    )
                    {
                        Console.WriteLine(Constant.Round, roundCounter);

                        //Random Attack order Method
                        Battle.RandomOrder(randomTurns);
                        for (int character = 0; character < characters.Length; character++)
                        {
                            // character <4 are heroes
                            if (character < Constant.MonsterId && Alive[Constant.MonsterId])
                            {
                                if (
                                    Check.GreaterThan(
                                        currentStats[randomTurns[character], Constant.HpValueColumn]
                                    )
                                )
                                {
                                    Console.WriteLine(
                                        Constant.RequestCommandMsg,
                                        names[randomTurns[character]]
                                    );
                                    do
                                    {
                                        userCommand = Console.ReadLine() ?? "";
                                        validInput = Check.ValidateInput(
                                            userCommand,
                                            threeValidInputs
                                        );
                                        Msg.ValidateInput(
                                            ref remainingAttempts,
                                            ref hasRemainingAttempts,
                                            validInput,
                                            Constant.DefaultCommandMsg
                                        );
                                        //if ability is on cooldown, user can repeat the insert
                                        if (
                                            userCommand.Equals(Constant.TwoStr)
                                            && Check.GreaterThan(
                                                currentCoolDown[randomTurns[character]]
                                            )
                                        )
                                        {
                                            Msg.NoticeOnCoolDown(
                                                currentCoolDown[randomTurns[character]]
                                            );
                                            validInput = !validInput;
                                        }
                                        ;
                                    } while (!validInput && hasRemainingAttempts);

                                    //The first switch is for common commands, and the nested one is for choosing the skill
                                    if (hasRemainingAttempts)
                                    {
                                        switch (userCommand)
                                        {
                                            case "1":
                                                Battle.Attack(
                                                    randomTurns[character],
                                                    Constant.MonsterId,
                                                    currentStats,
                                                    names
                                                );
                                                break;

                                            case "3":
                                                isGuarding[randomTurns[character]] = true;
                                                break;
                                            case "2":
                                                switch (randomTurns[character])
                                                {
                                                    case 0:
                                                        Battle.ArcherAbility(
                                                            randomTurns[character],
                                                            Constant.MonsterId,
                                                            ref monsterStun,
                                                            abilityEffect,
                                                            currentCoolDown,
                                                            names
                                                        );
                                                        break;
                                                    case 1:
                                                        Battle.BarbarianAbility(
                                                            randomTurns[character],
                                                            ref BarbarianAbilityDuracion,
                                                            currentCoolDown,
                                                            currentStats,
                                                            abilityEffect,
                                                            names
                                                        );
                                                        break;
                                                    case 2:
                                                        Battle.MageAbility(
                                                            randomTurns[character],
                                                            Constant.MonsterId,
                                                            abilityEffect,
                                                            currentCoolDown,
                                                            currentStats,
                                                            names
                                                        );
                                                        break;
                                                    case 3:
                                                        Battle.DruidAbility(
                                                            randomTurns[character],
                                                            currentCoolDown,
                                                            currentStats,
                                                            maxStats,
                                                            abilityEffect,
                                                            names
                                                        );
                                                        break;
                                                }
                                                break;
                                        }
                                    }
                                    //Check if the monster is still alive when an hero finishes turn
                                    Alive[Constant.MonsterId] = Check.GreaterThan(
                                        currentStats[Constant.MonsterId, Constant.HpId]
                                    );
                                }
                            }      //if !character<4 and monster isn't stunned then attacks
                            else if (!Check.GreaterThan(monsterStun))
                            {
                                Console.WriteLine(
                                    Constant.MonseterAttackMsg,
                                    names[Constant.MonsterId]
                                );
                                for (
                                    int objective = 0;
                                    objective < characters.Length - 1;
                                    objective++
                                )
                                {
                                    if (Check.GreaterThan(currentStats[objective, Constant.HpId]))
                                    {
                                        Battle.Attack(
                                            Constant.MonsterId,
                                            objective,
                                            currentStats,
                                            names,
                                            isGuarding[objective]
                                        );
                                    }
                                    //After attack Check if hero is strill alive
                                    Alive[objective] = Check.GreaterThan(
                                        currentStats[objective, Constant.HpId]
                                    );
                                }
                                for (int i = 0; i < Alive.Length; i++)
                                {
                                    Alive[i] = Check.GreaterThan(currentStats[i, Constant.HpId]);
                                }
                            }
                        }
                        //Display the lives in descending order 
                        
                        Battle.ShowStats(currentStats,names);

                        //Reset of variables
                        BarbarianAbilityDuracion--;
                        if (!Check.GreaterThan(BarbarianAbilityDuracion))
                        {
                            currentStats[Constant.BarbarianId, Constant.ReductionId] = maxStats[
                                Constant.BarbarianId,
                                Constant.ReductionId
                            ];
                        };

                        for (int i = 0;i < randomTurns.Length; i++)
                        {
                            randomTurns[i] = Constant.MonsterId;
                        }
                        
                        monsterStun--;
                        roundCounter++;
                        for (int i = 0; i < currentCoolDown.Length; i++)
                        {
                            currentCoolDown[i]--;
                        }
                    }
                }
            }
        }
    }
}
