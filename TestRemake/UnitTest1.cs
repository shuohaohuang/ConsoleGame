using Checker;
using Menssages;
using Utilities;
using BattleMethod;

namespace TestRemake
{
    [TestClass]
    public class Checker
    {
        [TestMethod]
        public void ValidateInput_True()
        {
            //arrange
            string input = "a";
            string[] correctInputs = { "a", "b", "c" };
            bool result;

            //act
            result = Check.ValidateInput(input, correctInputs);

            //assert
            Assert.IsTrue(result);
            
        }

        [TestMethod]
        public void ValidateInput_False()
        {
            //arrange
            string input = "d";
            string[] correctInputs = { "A", "B", "C" };
            bool result;

            //act
            result = Check.ValidateInput(input, correctInputs);

            //assert
            Assert.IsFalse(result);

        }

        [TestMethod]
        public void InRange_true()
        {
            //arrange
            int input = 20;
            int max = 20;
            bool result;

            //act
            result = Check.InRange(input, max);

            //assert
            Assert.IsTrue(result);

        }

        [TestMethod]
        public void InRange_false()
        {
            //arrange
            int input = 21;
            int max = 20;
            bool result;

            //act
            result = Check.InRange(input, max);

            //assert
            Assert.IsFalse(result);

        }

        [TestMethod]
        public void GreaterThanZero_Three_true()
        {
            //arrange
            int input = 3;
            bool result;

            //act
            result = Check.GreaterThanZero(input);

            //assert
            Assert.IsTrue(result);

        }

        [TestMethod]
        public void GreaterThanZero_Zero_false()
        {
            //arrange
            int input = 0;
            bool result;

            //act
            result = Check.GreaterThanZero(input);

            //assert
            Assert.IsFalse(result);

        }


    }

    [TestClass]
    public class Menssages
    {
        [TestMethod]
        public void ErrorCommand()
        {
            StringWriter sw = new();
            Console.SetOut(sw);
            bool moreAttemps=true;
            int attemps = 0;
            string outOfMsg = "a";


            Msg.ErrorCommand(ref moreAttemps,ref attemps, outOfMsg);

            string expected = "a";
            Assert.AreEqual(expected, sw.ToString().Trim());
        }

        [TestMethod]
        public void ErrorCommand2()
        {
            StringWriter sw = new();
            Console.SetOut(sw);
            bool moreAttemps = true;
            int attemps = 3;
            string outOfMsg = "a";


            Msg.ErrorCommand(ref moreAttemps, ref attemps, outOfMsg);

            string expected = "Wrong insert, try again";
            Assert.AreEqual(expected, sw.ToString().Trim());
        }

        [TestMethod]
        public void ValidateInput() 
        {
            StringWriter sw = new();
            Console.SetOut(sw);
            bool moreAttemps = false;
            bool validInput= false;
            int attemps = 3;
            string outOfMsg = "a";


            Msg.ValidateInput(ref attemps,ref moreAttemps, validInput, outOfMsg);

            string expected = "Wrong insert, try again";
            Assert.AreEqual(expected, sw.ToString().Trim());
        }
        [TestMethod]
        public void ValidateInput2()
        {
            StringWriter sw = new();
            Console.SetOut(sw);
            bool moreAttemps = false;
            bool validInput = true;
            int attemps = 3;
            string outOfMsg = "a";


            Msg.ValidateInput(ref attemps, ref moreAttemps, validInput, outOfMsg);

            string expected = "";
            Assert.AreEqual(expected, sw.ToString().Trim());
        }

        [TestMethod]
        public void NoticeOnCoolDown()
        {
            StringWriter sw = new();
            Console.SetOut(sw);
            int coolDown = 3;


            Msg.NoticeOnCoolDown(coolDown);

            string expected = $"Skill on Cooldown, {3} turns until available";
            Assert.AreEqual(expected, sw.ToString().Trim());
        }
    }

    [TestClass]
    public class Utilities
    {
        [TestMethod]
        public void NameMayus() 
        {
            string input = "marta";

            string result;

            result=Utility.NameMayus(input);

            Assert.AreEqual("Marta", result);
            
        }
    }

    [TestClass]
    public class Battle
    {
        [TestMethod]
        public void CalculateDamage1()
        {
            float ad = 100,
                defense = 10;
            bool critical=true,
                isGuarding=true;
            float result;

            result= Battle.CalculateDamage(ad, defense, critical, isGuarding);

            Assert.AreEqual(160,result);
        }

        [TestMethod]
        public void RemainedHp1()
        {
            float hp = 100,
                damage = 10;
            float result;

            result = Battle.RemainedHp(hp,damage);

            Assert.AreEqual(90, result);
        }

    }
}