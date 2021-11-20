using System;
using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.Geometry;
using Core;

namespace Builder
{
    /// <summary>
    /// Класс для постройки модели зубчатой шестерни
    /// </summary>
    public class GearBuilder
    {
        /// <summary>
        /// Номер объекта
        /// </summary>
        private ObjectId _gearId;

        /// <summary>
        /// Удаление старого объекта из документа
        /// </summary>
        /// <param name="transaction"></param>
        private void ClearDocument(Transaction transaction)
        {
            if (_gearId == new ObjectId())
            {
                return;
            }

            var activeDocument = Application.DocumentManager.MdiActiveDocument;
            var gear = transaction.GetObject(_gearId, OpenMode.ForWrite);
            activeDocument.SendStringToExecute("._zoom e ", false, false, false);
            activeDocument.Editor.Regen();

            gear.Erase(true);
        }

        /// <summary>
        /// Построение шестерни
        /// </summary>
        /// <param name="parameters"></param>
        public void BuildGear(GearParametersList parameters)
        {
            // Получение текущего документа и базы данных
            var activeDocument = Application.DocumentManager.MdiActiveDocument;
            var database = activeDocument.Database;

            // Блокирование документа
            using (var documentLock = activeDocument.LockDocument())
            {
                // Начало транзакции
                using (var transaction = database.TransactionManager.StartTransaction())
                {
                    ClearDocument(transaction);
                    
                    var blockTable = transaction.GetObject(database.BlockTableId,
                        OpenMode.ForRead) as BlockTable;
                    var blockTableRecord = transaction.GetObject(blockTable[BlockTableRecord.ModelSpace],
                        OpenMode.ForWrite) as BlockTableRecord;

                    // Получение параметров
                    var height = parameters[ParametersEnum.Height].Value;
                    var gearDiameter = parameters[ParametersEnum.GearDiameter].Value;
                    var holeDiameter = parameters[ParametersEnum.HoleDiameter].Value;
                    var toothLength = parameters[ParametersEnum.ToothLength].Value;
                    var toothWidth = parameters[ParametersEnum.ToothWidth].Value;

                    // Создание шестерни с отверстием
                    var gear = CreateGearWithHole(gearDiameter, holeDiameter, height);

                    // Создание углубления и вычитание его из объекта шестерни
                    var deepening = CreateDeepening(gearDiameter, holeDiameter);
                    SubtractDeepeningFromGear(gear, deepening, height);

                    // Создание зубов и добавление к объекту шестерни
                    var tooth = Create3DTooth(gearDiameter, toothLength, toothWidth, height);
                    CreateTeethPolarArray(gear, tooth, 8);

                    // Добавление нового объекта в таблицу
                    blockTableRecord.AppendEntity(gear);
                    transaction.AddNewlyCreatedDBObject(gear, true);
                    
                    _gearId = gear.Id;

                    // Сохранение изменений в базе данных
                    transaction.Commit();
                }
            }
        }

        /// <summary>
        /// Создание объекта шестерни и вырезание отверстия
        /// </summary>
        /// <param name="gearDiameter"></param>
        /// <param name="holeDiameter"></param>
        /// <param name="height"></param>
        /// <returns></returns>
        private Solid3d CreateGearWithHole(double gearDiameter, double holeDiameter, double height)
        {
            // Создание шестерни, представленной цилиндром
            var gear = new Solid3d();
            gear.SetDatabaseDefaults();
            gear.CreateFrustum(height, gearDiameter, gearDiameter, gearDiameter);

            // Создание отверстия
            var hole = new Solid3d();
            hole.SetDatabaseDefaults();
            hole.CreateFrustum(height, holeDiameter, holeDiameter, holeDiameter);

            // Вырезание отверстия 
            gear.BooleanOperation(BooleanOperationType.BoolSubtract, hole);

            // Перемещение шестерни вверх, чтобы объект находился выше плоскости XY
            gear.TransformBy(Matrix3d.Displacement(new Point3d(0, 0, height / 2.0) -
                                                   Point3d.Origin));

            return gear;
        }

        /// <summary>
        /// Вычитание углублений из шестерни
        /// </summary>
        /// <param name="gear"></param>
        /// <param name="deepening"></param>
        /// <param name="height"></param>
        private void SubtractDeepeningFromGear(Solid3d gear, Region deepening, double height)
        {
            var deepeningHeight = height * 0.2;

            // Нижнее углубление
            var bottomArea = new Solid3d();
            bottomArea.Extrude(deepening, deepeningHeight, 0);
            gear.BooleanOperation(BooleanOperationType.BoolSubtract, bottomArea);

            // Верхнее углубление
            var topArea = new Solid3d();
            topArea.Extrude(deepening, deepeningHeight, 0);
            topArea.TransformBy(Matrix3d.Displacement(new Point3d(0, 0, height - deepeningHeight) -
                                                      Point3d.Origin));
            gear.BooleanOperation(BooleanOperationType.BoolSubtract, topArea);
        }

        /// <summary>
        /// Создание углубления
        /// </summary>
        /// <param name="gearDiameter"></param>
        /// <param name="holeDiameter"></param>
        /// <returns></returns>
        private Region CreateDeepening(double gearDiameter, double holeDiameter)
        {
            // Расстояние между внутренним и внешним диаметрами
            var innerLength = gearDiameter - holeDiameter;
            var borderLength = innerLength * 0.15;

            // Create two in memory circles
            var outerCircle = new Circle();
            outerCircle.SetDatabaseDefaults();
            outerCircle.Center = Point3d.Origin;
            outerCircle.Radius = gearDiameter - borderLength;

            var innerCircle = new Circle();
            innerCircle.SetDatabaseDefaults();
            innerCircle.Center = Point3d.Origin;
            innerCircle.Radius = holeDiameter + borderLength;

            // Adds the circle to an object array
            var objectCollection = new DBObjectCollection();
            objectCollection.Add(outerCircle);
            objectCollection.Add(innerCircle);

            // Calculate the regions based on each closed loop
            var regionCollection = Region.CreateFromCurves(objectCollection);
            var innerRegion = regionCollection[0] as Region;
            var outerRegion = regionCollection[1] as Region;

            // Subtract region 1 from region 2
            outerRegion.BooleanOperation(BooleanOperationType.BoolSubtract, innerRegion);
            innerRegion.Dispose();

            // Dispose of the in memory objects not appended to the database
            outerCircle.Dispose();
            innerCircle.Dispose();

            return outerRegion;
        }

        /// <summary>
        /// Зуб имеет форму трапеции. Точки A и D - вершины нижнего основания.
        /// Точки B и C - вершины верхнего основания.
        /// </summary>
        /// <param name="gearDiameter"></param>
        /// <param name="toothLength"></param>
        /// <param name="toothWidth"></param>
        /// <returns></returns>
        private Polyline Create2DTooth(double gearDiameter, double toothLength, double toothWidth)
        {
            // Угол наклона боковой стороны 15 градусов
            var angle = 15 * Math.PI / 180;

            var gearWithToothLength = gearDiameter + toothLength;

            var b = new Point2d(gearWithToothLength, toothWidth / 2);
            var c = new Point2d(b.X, -b.Y);

            var aY = b.Y + toothLength * Math.Tan(angle);
            // Подставляем aY в уравнение окружности
            var aX = Math.Sqrt(gearDiameter * gearDiameter - aY * aY);

            var a = new Point2d(aX, aY);
            var d = new Point2d(a.X, -a.Y);

            // Создание трапеции в виде замкнутой ломаной линии
            var polyline = new Polyline();
            polyline.SetDatabaseDefaults();
            polyline.AddVertexAt(0, a, 0, 0, 0);
            polyline.AddVertexAt(1, b, 0, 0, 0);
            polyline.AddVertexAt(2, c, 0, 0, 0);
            polyline.AddVertexAt(3, d, 0, 0, 0);

            polyline.Closed = true;

            return polyline;
        }

        /// <summary>
        /// Создание трапеции и её вытягивание на расстояние <see cref="height"/>
        /// </summary>
        /// <param name="gearDiameter"></param>
        /// <param name="toothLength"></param>
        /// <param name="toothWidth"></param>
        /// <param name="height"></param>
        /// <returns></returns>
        private Solid3d Create3DTooth(double gearDiameter, double toothLength, double toothWidth, double height)
        {
            var polyline = Create2DTooth(gearDiameter, toothLength, toothWidth);

            // Добавление ломаной линии в массив объектов
            var objectCollection = new DBObjectCollection();
            objectCollection.Add(polyline);

            // Создание региона из массива объектов
            var regionCollection = Region.CreateFromCurves(objectCollection);
            var region = regionCollection[0] as Region;

            var tooth = new Solid3d();
            tooth.Extrude(region, height, 0);

            return tooth;
        }

        /// <summary>
        /// Создание нескольких равноудаленных друг от друга зубов вокруг начала координат.
        /// Полченные зубы объединяются с объектом шестерни
        /// </summary>
        /// <param name="gear"></param>
        /// <param name="tooth"></param>
        /// <param name="teethCount"></param>
        private void CreateTeethPolarArray(Solid3d gear, Solid3d tooth, int teethCount)
        {
            // Угол между соседними зубами в радианах
            double angle = 360.0 / teethCount * Math.PI / 180;

            for (int i = 0; i < teethCount; i++)
            {
                var newTooth = tooth.Clone() as Solid3d;
                newTooth.TransformBy(Matrix3d.Rotation(angle * i, Vector3d.ZAxis, Point3d.Origin));
                gear.BooleanOperation(BooleanOperationType.BoolUnite, newTooth);
            }
        }
    }
}
