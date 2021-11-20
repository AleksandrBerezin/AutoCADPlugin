using Core;
using NUnit.Framework;

namespace TestCore
{
    [TestFixture]
    public class GearParameterTest
    {
        private readonly GearParameter _testParameter =
            new GearParameter(ParametersEnum.GearDiameter, 24, 60, 40);

        [TestCase(TestName = "Позитивный тест геттера Name")]
        public void TestNameGet_GoodScenario()
        {
            // Arrange
            var expected = ParametersEnum.GearDiameter;

            // Act
            var actual = _testParameter.Name;

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestCase(TestName = "Позитивный тест геттера Min")]
        public void TestMinGet_GoodScenario()
        {
            // Arrange
            var expected = 24;

            // Act
            var actual = _testParameter.Min;

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestCase(TestName = "Позитивный тест геттера Max")]
        public void TestMaxGet_GoodScenario()
        {
            // Arrange
            var expected = 60;

            // Act
            var actual = _testParameter.Max;

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestCase(TestName = "Позитивный тест геттера Value")]
        public void TestValueGet_GoodScenario()
        {
            // Arrange
            var expected = 50;

            // Act
            var parameter = _testParameter.Clone() as GearParameter;
            parameter.Value = expected;
            var actual = parameter.Value;

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestCase(TestName = "Позитивный тест сеттера Value")]
        public void TestValueSet_GoodScenario()
        {
            // Arrange
            var expected = 50;

            // Act
            var parameter = _testParameter.Clone() as GearParameter;
            parameter.Value = expected;
            var actual = parameter.Value;

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestCase(TestName = "Позитивный тест геттера Description")]
        public void TestDescriptionGet_GoodScenario()
        {
            // Arrange
            var expected = ParametersEnum.GearDiameter.GetDescription();

            // Act
            var actual = _testParameter.Description;

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestCase(TestName = "Позитивный тест геттера Limits")]
        public void TestLimitsGet_GoodScenario()
        {
            // Arrange
            var expected = "(24-60 mm)";

            // Act
            var actual = _testParameter.Limits;

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestCase(TestName = "Позитивный тест геттера IsValidData")]
        public void TestIsValidDataGet_GoodScenario()
        {
            // Arrange
            var expected = true;

            // Act
            var actual = _testParameter.IsValidData;

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestCase(TestName = "Позитивный тест геттера Error")]
        public void TestErrorGet_GoodScenario()
        {
            // Arrange
            var expected = string.Empty;

            // Act
            var actual = _testParameter.Error;

            // Assert
            Assert.AreEqual(expected, actual);
        }

        private const string TestIndexer_ReturnValue_ErrorMessage =
            "Parameter GearDiameter should be more then 24 and less then 60 mm.";

        private const string TestIndexer_ReturnValue_TestName =
            "При вызове индексатора для значения {0} возвращается текст ошибки {1}";

        [TestCase(40, "", TestName =
            TestIndexer_ReturnValue_TestName)]
        [TestCase(10, TestIndexer_ReturnValue_ErrorMessage, TestName = 
            TestIndexer_ReturnValue_TestName)]
        [TestCase(100, TestIndexer_ReturnValue_ErrorMessage, TestName = 
            TestIndexer_ReturnValue_TestName)]
        public void TestIndexerGet_ReturnValue(int value, string errorMessage)
        {
            // Arrange
            var expected = errorMessage;

            // Act
            var parameter = new GearParameter(ParametersEnum.GearDiameter, 24, 60, value);
            var actual = parameter[nameof(GearParameter.Value)];

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestCase(TestName = "При сравнении одинаковых объектов возращается истина")]
        public void TestEquals_GoodScenario_ReturnTrue()
        {
            // Arrange
            var expected = _testParameter;

            // Act
            var actual = _testParameter.Clone() as GearParameter;
            var isEqual = actual.Equals(expected);

            // Assert
            Assert.IsTrue(isEqual);
        }

        [TestCase(TestName = "При сравнении различных объектов возращается ложь")]
        public void TestEquals_DifferentValues_ReturnFalse()
        {
            // Arrange
            var expected = _testParameter;

            // Act
            var actual = _testParameter.Clone() as GearParameter;
            actual.Value = 50;
            var isEqual = actual.Equals(expected);

            // Assert
            Assert.IsFalse(isEqual);
        }

        [TestCase(TestName = "При сравнении с нулевым объектом возращается ложь")]
        public void TestEquals_NullValue_ReturnFalse()
        {
            // Act
            var actual = _testParameter;
            var isEqual = actual.Equals(null);

            // Assert
            Assert.IsFalse(isEqual);
        }

        [TestCase(TestName = "При клонировании объекта создается его точная копия")]
        public void TestClone_GoodScenario_ReturnValue()
        {
            // Arrange
            var expected = _testParameter;

            // Act
            var actual = _testParameter.Clone() as GearParameter;
            var isEqual = actual.Equals(expected);

            // Assert
            Assert.IsFalse(!isEqual);
        }
    }
}