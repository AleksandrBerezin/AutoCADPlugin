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
    }
}