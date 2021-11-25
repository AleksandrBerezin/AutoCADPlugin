using Core;
using NUnit.Framework;

namespace TestCore
{
    [TestFixture]
    public class GearParametersListTest
    {
        [TestCase(TestName =
            "Когда вызывается метод SetDefault, значения коллекции должны установиться по умолчанию")]
        public void TestSetDefault_GoodScenario()
        {
            // Arrange
            var expected = new GearParametersList();

            // Act
            var actual = new GearParametersList();
            actual.SetDefault();

            // Assert
            CollectionAssert.AreEqual(expected, actual);
        }

        [TestCase(TestName =
            "При получении значения с помощью индексатора возвращается соответствующий параметр")]
        public void TestIndexerGet_ReturnValue()
        {
            // Arrange
            var expected = new GearParameter(ParametersEnum.GearDiameter, 24, 60, 40);

            // Act
            var parametersList = new GearParametersList();
            var actual = parametersList[ParametersEnum.GearDiameter];

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestCase(TestName =
            "При установке значения с помощью индексатора, параметр записывается в коллекцию")]
        public void TestIndexerSet_GoodScenario()
        {
            // Arrange
            var expected = new GearParameter(ParametersEnum.GearDiameter, 24, 60, 50);

            // Act
            var parametersList = new GearParametersList
            {
                [ParametersEnum.GearDiameter] = expected
            };
            var actual = parametersList[ParametersEnum.GearDiameter];

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestCase(TestName = "При изменении значения ширины зубца, должно измениться" +
                             " максимальное значение для количества зубцов")]
        public void TestOnToothWidthChanged_ChangeMaxTeethCount()
        {
            // Arrange
            var expected = 16;
            
            // Act
            var parametersList = new GearParametersList
            {
                [ParametersEnum.ToothWidth] =
                {
                    Value = 5
                }
            };
            var teethCount = parametersList[ParametersEnum.TeethCount];
            var actual = teethCount.Max;

            // Assert
            Assert.AreEqual(expected, actual);
        }

        private const string TestOnGearDiameterChanged_ChangeLimits_TestName =
            "При изменении значения диаметра шестерни на {3}, должны измениться" +
            " ограничения для параметра {0} на Min = {1} и Max = {2}";

        [TestCase(ParametersEnum.HoleDiameter, 4, 15, 60,
            TestName = TestOnGearDiameterChanged_ChangeLimits_TestName)]
        [TestCase(ParametersEnum.ToothLength, 12, 30, 60,
            TestName = TestOnGearDiameterChanged_ChangeLimits_TestName)]
        [TestCase(ParametersEnum.ToothWidth, 5, 15, 60,
            TestName = TestOnGearDiameterChanged_ChangeLimits_TestName)]
        public void TestOnGearDiameterChanged_ChangeLimits(ParametersEnum name,
            int expectedMin, int expectedMax, int value)
        {
            // Act
            var parametersList = new GearParametersList
            {
                [ParametersEnum.GearDiameter] =
                {
                    Value = value
                }
            };
            var changedParameter = parametersList[name];
            var actualMin = changedParameter.Min;
            var actualMax = changedParameter.Max;

            // Assert
            Assert.Multiple(() =>
            {
                Assert.AreEqual(expectedMin, actualMin);
                Assert.AreEqual(expectedMax, actualMax);
            });
        }
    }
}