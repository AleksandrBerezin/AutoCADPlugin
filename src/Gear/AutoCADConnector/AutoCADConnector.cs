using System.Diagnostics;
using System.IO;
using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.Runtime;
using Builder;
using Core;
using Gear;
using Microsoft.VisualBasic.Devices;

namespace AutoCADConnector
{
    /// <summary>
    /// Класс для связи AutoCAD с плагином
    /// </summary>
    public class AutoCADConnector : IExtensionApplication
    {
        [CommandMethod("StartGearPlugin")]
        public static void StartGearPlugin()
        {
            Application.ShowModelessWindow(new MainWindow());
        }

        [CommandMethod("StressTesting", CommandFlags.Session)]
        public static void StressTesting()
        {
            var builder = new GearBuilder();
            var stopWatch = new Stopwatch();
            stopWatch.Start();
            var gearParameters = new GearParameters();
            var streamWriter = new StreamWriter($"log.txt", true);
            var currentProcess = Process.GetCurrentProcess();
            var count = 0;

            while (true)
            {
                builder.BuildGear(gearParameters);
                var computerInfo = new ComputerInfo();
                var usedMemory = (computerInfo.TotalPhysicalMemory - computerInfo.AvailablePhysicalMemory) *
                                 0.000000000931322574615478515625;
                streamWriter.WriteLine(
                    $"{++count}\t{stopWatch.Elapsed:hh\\:mm\\:ss\\.fff}\t{usedMemory}");
                streamWriter.Flush();
            }
        }

        /// <inheritdoc/>
        public void Initialize()
        {
        }

        /// <inheritdoc/>
        public void Terminate()
        {
        }
    }
}
