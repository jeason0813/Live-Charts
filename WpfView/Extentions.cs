using System.Linq;
using System.Windows;
using LiveCharts.Dtos;

namespace LiveCharts.Wpf
{
    /// <summary>
    /// 
    /// </summary>
    public static class Extentions
    {
        /// <summary>
        /// Converts a point at screen to chart values scale
        /// </summary>
        /// <param name="chart">Target chart</param>
        /// <param name="screenPoint">point in screen</param>
        /// <param name="axisX">axis x index</param>
        /// <param name="axisY">axis y index</param>
        /// <returns></returns>
        public static Point ConvertToChartValues(this Chart2D chart, Point screenPoint, int axisX = 0, int axisY = 0)
        {
            if (chart.Core == null || chart.FirstDimension == null || chart.FirstDimension.Any(x => x.Core == null)) return new Point();

            var uw = new CorePoint(
                chart.FirstDimension[axisX].Core.EvaluatesUnitWidth
                    ? ChartFunctions.GetUnitWidth(AxisOrientation.X, chart.Core, axisX) / 2
                    : 0,
                chart.SecondDimension[axisY].Core.EvaluatesUnitWidth
                    ? ChartFunctions.GetUnitWidth(AxisOrientation.Y, chart.Core, axisY) / 2
                    : 0);

            return new Point(
                ChartFunctions.FromPlotArea(screenPoint.X - uw.X, AxisOrientation.X, chart.Core, axisX),
                ChartFunctions.FromPlotArea(screenPoint.Y - uw.Y, AxisOrientation.Y, chart.Core, axisY));
        }

        /// <summary>
        /// Converts a chart values pair to pixels
        /// </summary>
        /// <param name="chart">Target chart</param>
        /// <param name="chartPoint">point in screen</param>
        /// <param name="axisX">axis x index</param>
        /// <param name="axisY">axis y index</param>
        /// <returns></returns>
        public static Point ConvertToPixels(this Chart2D chart, Point chartPoint, int axisX = 0, int axisY = 0)
        {
            if (chart.Core == null || chart.FirstDimension.Any(x => x.Core == null)) return new Point();

            var uw = new CorePoint(
                chart.FirstDimension[axisX].Core.EvaluatesUnitWidth
                    ? ChartFunctions.GetUnitWidth(AxisOrientation.X, chart.Core, axisX) / 2
                    : 0,
                chart.SecondDimension[axisY].Core.EvaluatesUnitWidth
                    ? ChartFunctions.GetUnitWidth(AxisOrientation.Y, chart.Core, axisY) / 2
                    : 0);

            return new Point(
                ChartFunctions.ToPlotArea(chartPoint.X, AxisOrientation.X, chart.Core, axisX) + uw.X,
                ChartFunctions.ToPlotArea(chartPoint.Y, AxisOrientation.Y, chart.Core, axisY) + uw.Y);
        }

        /// <summary>
        /// Converts a ChartPoint to Point
        /// </summary>
        /// <param name="chartPoint">point to convert</param>
        /// <returns></returns>
        public static Point AsPoint(this ChartPoint chartPoint)
        {
            return new Point(chartPoint.X, chartPoint.Y);
        }

        /// <summary>
        /// Converts a CorePoint to Point
        /// </summary>
        /// <param name="point">point to convert</param>
        /// <returns></returns>
        internal static Point AsPoint(this CorePoint point)
        {
            return new Point(point.X, point.Y);
        }
    }
}