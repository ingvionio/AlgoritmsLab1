using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using Algoritms.Logic;
using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;
using MathNet.Numerics;

namespace Algoritms.WPFApp
{
    public partial class MainWindow : System.Windows.Window
    {
        private CancellationTokenSource _cancellationTokenSource;

        public MainWindow()
        {
            InitializeComponent();
        }

        private async void RunButton_Click(object sender, RoutedEventArgs e)
        {
            if (!int.TryParse(MinElementsBox.Text, out int nMin) ||
                !int.TryParse(MaxElementsBox.Text, out int nMax) ||
                !int.TryParse(StepSizeBox.Text, out int step) ||
                nMin <= 0 || nMax <= 0 || step <= 0 || nMin > nMax)
            {
                MessageBox.Show("Некорректные значения параметров.");
                return;
            }

            // Проверка repetitions
            if (!int.TryParse(RepetitionsBox.Text, out int repetitions) || repetitions <= 0)
            {
                MessageBox.Show("Некорректное значение для Repetitions.");
                return;
            }

            Algoritm algorithm = GetSelectedAlgorithm();
            if (algorithm == null)
            {
                MessageBox.Show("Выберите алгоритм.");
                return;
            }

            _cancellationTokenSource = new CancellationTokenSource();
            RunButton.IsEnabled = false;
            CancelButton.IsEnabled = true;

            try
            {
                List<TimeSpan> times = await Task.Run(() =>
                    TimeCounter.TimeCount(nMin, nMax, algorithm, step, _cancellationTokenSource.Token, repetitions) // Используем repetitions из TextBox
                );

                Dispatcher.Invoke(() => UpdatePlot(times, nMin, step, algorithm.GetType().Name));
            }
            catch (OperationCanceledException)
            {
                MessageBox.Show("Операция отменена.");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Произошла ошибка: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                RunButton.IsEnabled = true;
                CancelButton.IsEnabled = false;
                _cancellationTokenSource.Dispose();
            }
        }

        private void UpdatePlot(List<TimeSpan> times, int nMin, int step, string algorithmName)
        {
            PlotModel model = new PlotModel { Title = $"Алгоритм на графике: {algorithmName}" };

            model.Axes.Add(new LinearAxis
            {
                Position = AxisPosition.Bottom,
                Title = "Количество элементов",
                MajorGridlineStyle = LineStyle.Solid,
                MinorGridlineStyle = LineStyle.Dot,
                TicklineColor = OxyColors.Black,
                AxislineColor = OxyColors.Black
            });

            model.Axes.Add(new LinearAxis
            {
                Position = AxisPosition.Left,
                Title = "Время (мс)",
                MajorGridlineStyle = LineStyle.Solid,
                MinorGridlineStyle = LineStyle.Dot,
                TicklineColor = OxyColors.Black,
                AxislineColor = OxyColors.Black
            });

            // Создаем точки для графика алгоритма
            LineSeries series = new LineSeries
            {
                Title = algorithmName,
                MarkerType = MarkerType.Circle,
                MarkerSize = 4,
                MarkerFill = OxyColors.Green,
                Color = OxyColors.DarkGreen,
                StrokeThickness = 2
            };
            for (int i = 0; i < times.Count; i++)
            {
                series.Points.Add(new DataPoint(nMin + i * step, times[i].TotalMilliseconds));
            }

            // Данные для апроксимации
            double[] xData = times.Select((t, i) => (double)(nMin + i * step)).ToArray();
            double[] yData = times.Select(t => t.TotalMilliseconds).ToArray();

            // Вычисляем коэффициенты полинома 3-й степени
            double[] polynomialCoefficients = GetPolynomialApproximation(xData, yData, 3);

            // Создаем FunctionSeries для апроксимации
            FunctionSeries approximationSeries = new FunctionSeries(
                x => polynomialCoefficients[0] + polynomialCoefficients[1] * x +
                     polynomialCoefficients[2] * x * x + polynomialCoefficients[3] * x * x * x,
                xData.Min(), xData.Max(), 0.1,
                "Апроксимация"
            );
            approximationSeries.Color = OxyColors.Red;

            // Добавляем графики на PlotModel
            model.Series.Add(series);
            model.Series.Add(approximationSeries);
            PlotView.Model = model;
        }

        private Algoritm GetSelectedAlgorithm()
        {
            return AlgorithmSelector.SelectedIndex switch
            {
                0 => new BubbleSort(),
                1 => new HeapSort(),
                2 => new HornerMethod(),
                3 => new GnomeSort(),
                4 => new MultiplyElements(),
                5 => new NaiveAssessment(),
                6 => new PowAlgorithm(),
                7 => new QuickPow(),
                8 => new QuickSortAlgoritm(),
                9 => new RecPow(),
                10 => new Sum(),
                11 => new TimSort(),
                _ => null
            };
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            _cancellationTokenSource?.Cancel();
        }

        // Метод для вычисления полиномиальной апроксимации
        private double[] GetPolynomialApproximation(double[] xData, double[] yData, int degree)
        {
            return Fit.Polynomial(xData, yData, degree);
        }
    }
}