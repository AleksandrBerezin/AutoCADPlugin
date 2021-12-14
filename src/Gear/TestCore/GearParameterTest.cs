using Core;
using NUnit.Framework;

namespace TestCore
{
    [TestFixture]
    public class GearParameterTest
    {
        #region Constants

        private readonly GearParameter _testParameter =
            new GearParameter(ParametersEnum.GearDiameter, 24, 60, 40);

        #endregion

        #region Test Properties

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

        [TestCase(TestName = "Позитивный тест геттера и сеттера Value")]
        public void TestValueGetSet_GoodScenario()
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

        [TestCase(TestName = "Позитивный тест геттера и сеттера IsValidData")]
        public void TestIsValidDataGetSet_GoodScenario()
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

        #endregion

        #region Test Constructors

        private const string TestConstructor_CheckMinMaxLimits_ReturnValue_TestName =
            "При вызове конструктора для параметра {0} для ограничений от {1} до {2} " +
            "строка с ограничениями равняется {3}";

        [TestCase(ParametersEnum.GearDiameter, 40, 20, 30, "Error",
            TestName = TestConstructor_CheckMinMaxLimits_ReturnValue_TestName)]
        [TestCase(ParametersEnum.GearDiameter, 40, 20, 30, "Error",
            TestName = TestConstructor_CheckMinMaxLimits_ReturnValue_TestName)]
        [TestCase(ParametersEnum.GearDiameter, 24, 60, 40, "(24-60 mm)",
            TestName = TestConstructor_CheckMinMaxLimits_ReturnValue_TestName)]
        public void TestConstructor_CheckMinMaxLimits_ReturnValue(ParametersEnum name, int min,
            int max, int value, string expectedLimits)
        {
            // Act
            var parameter = new GearParameter(name, min, max, value);
            var actual = parameter.Limits;

            // Assert
            Assert.AreEqual(expectedLimits, actual);
        }

        #endregion

        #region Test Indexers

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

        #endregion

        #region Test Methods

        [TestCase(TestName = "При сравнении одинаковых объектов возращается истина")]
        public void TestEqualsAndClone_GoodScenario_ReturnTrue()
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

        #endregion
    }
}