using Core;
using NUnit.Framework;

namespace TestCore
{
    [TestFixture]
    public class ParametersEnumExtensionTest
    {
        private const string TestGetDescription_ReturnValue_TestName =
            "Когда вызывается метод GetDescription для параметра {2}, он должен вернуть его " +
            "описание в виде строки";

        [TestCase(ParametersEnum.GearDiameter, "Gear Diameter \"D\":", TestName =
            TestGetDescription_ReturnValue_TestName)]
        [TestCase(ParametersEnum.HoleDiameter, "Hole Diameter \"d\":", TestName =
            TestGetDescription_ReturnValue_TestName)]
        [TestCase(ParametersEnum.Height, "Height \"H\":", TestName = 
            TestGetDescription_ReturnValue_TestName)]
        [TestCase(ParametersEnum.ToothLength, "Tooth Length \"A\":", TestName = 
            TestGetDescription_ReturnValue_TestName)]
        [TestCase(ParametersEnum.ToothWidth, "Tooth Width \"B\":", TestName = 
            TestGetDescription_ReturnValue_TestName)]
        [TestCase((ParametersEnum)5, "Parameter", TestName = 
            "Когда вызывается метод GetDescription для некорректного значения," +
            " он должен вернуть строку Parameter")]
        public void TestGetDescription_ReturnValue(ParametersEnum parameter, string expected)
        {
            // Act
            var actual = parameter.GetDescription();

            // Assert
            Assert.AreEqual(expected, actual);
        }
    }
}