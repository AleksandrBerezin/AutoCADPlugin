using Core;
using NUnit.Framework;
using ViewModel;

namespace TestVM
{
    [TestFixture]
    public class MainVMTest
    {
        #region Test Properties

        [TestCase(TestName = "Позитивный тест геттера GearParameters")]
        public void TestGearParametersGet_GoodScenario()
        {
            // Arrange
            var expected = new GearParameters();

            // Act
            var mainVm = new MainVM();
            var actual = mainVm.GearParameters;

            // Assert
            Assert.Multiple(() =>
            {
                CollectionAssert.AreEqual(expected.ParametersList, actual.ParametersList);
                Assert.AreEqual(expected.ToothShape, actual.ToothShape);
            });
        }

        [TestCase(TestName = "Позитивный тест геттера и сеттера IsValidData")]
        public void TestIsValidDataSet_GoodScenario()
        {
            // Arrange
            var expected = false;

            // Act
            var mainVm = new MainVM
            {
                IsValidData = expected
            };
            var actual = mainVm.IsValidData;

            // Assert
            Assert.AreEqual(expected, actual);
        }

        #endregion

        #region Test Commands

        [TestCase(TestName = "При получении команды BuildCommand, она должна" +
                             " содержать обработчик")]
        public void TestBuildCommand_CanExecute()
        {
            // Act
            var mainVm = new MainVM();

            // Assert
            Assert.IsTrue(mainVm.BuildModelCommand.CanExecute(null));
        }

        [TestCase(TestName = "При получении команды SetDefaultCommand, она должна" +
                             " содержать обработчик")]
        public void TestSetDefaultCommand_CanExecute()
        {
            // Act
            var mainVm = new MainVM();

            // Assert
            Assert.IsTrue(mainVm.SetDefaultCommand.CanExecute(null));
        }

        #endregion

        #region Test Methods

        [TestCase(TestName = "Если все параметры содержыт корректные данные, " +
                             "в свойство IsValidData класса MainVM должно быть" +
                             " установлено значение true")]
        public void TestOnValidDataChanged_ValidData_SetTrue()
        {
            // Arrange
            var expected = true;

            // Act
            var mainVm = new MainVM();
            mainVm.GearParameters.ParametersList[0].IsValidData = false;
            mainVm.GearParameters.ParametersList[0].IsValidData = true;
            var actual = mainVm.IsValidData;

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestCase(TestName = "Если в один из параметров вводят некорректные данные, " +
                             "в свойство IsValidData класса MainVM должно быть" +
                             " установлено значение false")]
        public void TestOnValidDataChanged_InvalidData_SetFalse()
        {
            // Arrange
            var expected = false;

            // Act
            var mainVm = new MainVM();
            mainVm.GearParameters.ParametersList[0].IsValidData = false;
            var actual = mainVm.IsValidData;

            // Assert
            Assert.AreEqual(expected, actual);
        }

        #endregion
    }
}
