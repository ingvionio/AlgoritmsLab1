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
using System.Windows.Controls;
using OxyPlot.Legends;

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
                !int.TryParse(RepetitionsBox.Text, out int repetitions) ||
                nMin <= 0 || nMax <= 0 || step <= 0 || nMin > nMax || repetitions <= 0)
            {
                MessageBox.Show("Некорректные значения параметров.");
                return;
            }

            var selectedAlgorithms = AlgorithmsListBox.SelectedItems.Cast<ListBoxItem>().Select(item => item.Content.ToString()).ToList();
            if (selectedAlgorithms.Count == 0)
            {
                MessageBox.Show("Выберите хотя бы один алгоритм.");
                return;
            }

            _cancellationTokenSource = new CancellationTokenSource();
            RunButton.IsEnabled = false;
            CancelButton.IsEnabled = true;

            SortingProgressBar.Value = 0;

            try
            {
                PlotModel model = new PlotModel { Title = "Сравнение алгоритмов"};
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

                int totalSteps = selectedAlgorithms.Count * ((nMax - nMin) / step + 1) * repetitions;
                int currentStep = 0;

                foreach (var algorithmName in selectedAlgorithms)
                {
                    Algoritm algorithm = GetSelectedAlgorithm(algorithmName);

                    List<TimeSpan> times = await Task.Run(() =>
                    {
                        var result = TimeCounter.TimeCount(nMin, nMax, algorithm, step, _cancellationTokenSource.Token, repetitions);

                        currentStep += ((nMax - nMin) / step + 1) * repetitions;
                        Dispatcher.Invoke(() =>
                        {
                            SortingProgressBar.Value = (double)currentStep / totalSteps * 100;
                        });
                        return result;
                    });

                    Dispatcher.Invoke(() =>
                    {
                        AddSeriesToPlot(model, times, nMin, step, algorithmName);
                    });
                }

                PlotView.Model = model;
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

                SortingProgressBar.Value = 0;
            }
        }

        private void AddSeriesToPlot(PlotModel model, List<TimeSpan> times, int nMin, int step, string algorithmName)
        {
            LineSeries series = new LineSeries
            {
                Title = algorithmName,
                MarkerType = MarkerType.Circle,
                MarkerSize = 4,
                StrokeThickness = 2
            };

            switch (algorithmName)
            {
                case "Bubble Sort":
                    series.Color = OxyColors.Green;
                    series.MarkerFill = OxyColors.DarkGreen;
                    break;
                case "Quick Sort":
                    series.Color = OxyColors.Red;
                    series.MarkerFill = OxyColors.DarkRed;
                    break;
                case "Tim Sort":
                    series.Color = OxyColors.Blue;
                    series.MarkerFill = OxyColors.DarkBlue;
                    break;
                case "Heap Sort":
                    series.Color = OxyColors.Yellow;
                    series.MarkerFill = OxyColors.DarkGoldenrod;
                    break;
                case "Gnome Sort":
                    series.Color = OxyColors.Purple;
                    series.MarkerFill = OxyColors.DarkViolet;
                    break;
                case "Bingo Sort":
                    series.Color = OxyColors.Orange;
                    series.MarkerFill = OxyColors.DarkOrange;
                    break;
                case "Horner Method":
                    series.Color = OxyColors.Brown;
                    series.MarkerFill = OxyColors.SaddleBrown;
                    break;
                case "Multiply Elements":
                    series.Color = OxyColors.Cyan;
                    series.MarkerFill = OxyColors.DarkCyan;
                    break;
                case "Naive Assessment":
                    series.Color = OxyColors.Magenta;
                    series.MarkerFill = OxyColors.DarkMagenta;
                    break;
                case "PowAlgorithm":
                    series.Color = OxyColors.Lime;
                    series.MarkerFill = OxyColors.DarkOliveGreen;
                    break;
                case "QuickPow":
                    series.Color = OxyColors.Pink;
                    series.MarkerFill = OxyColors.DeepPink;
                    break;
                case "RecPow":
                    series.Color = OxyColors.Teal;
                    series.MarkerFill = OxyColors.DarkSlateGray;
                    break;
                case "Sum":
                    series.Color = OxyColors.Gold;
                    series.MarkerFill = OxyColors.Goldenrod;
                    break;
                default:
                    series.Color = OxyColors.Black;
                    series.MarkerFill = OxyColors.Gray;
                    break;
            }

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
                $"{algorithmName} (Апроксимация)"
            );
            approximationSeries.Color = OxyColors.OrangeRed;

            // Добавляем графики на PlotModel
            model.Series.Add(series);
            model.Series.Add(approximationSeries);
        }

        private Algoritm GetSelectedAlgorithm(string algorithmName)
        {
            return algorithmName switch
            {
                "Bubble Sort" => new BubbleSort(),
                "Heap Sort" => new HeapSort(),
                "Horner Method" => new HornerMethod(),
                "Gnome Sort" => new GnomeSort(),
                "Multiply Elements" => new MultiplyElements(),
                "Naive Assessment" => new NaiveAssessment(),
                "PowAlgorithm" => new PowAlgorithm(),
                "QuickPow" => new QuickPow(),
                "Quick Sort" => new QuickSortAlgoritm(),
                "RecPow" => new RecPow(),
                "Sum" => new Sum(),
                "Tim Sort" => new TimSort(),
                "Bingo Sort" => new BingoSort(),
                "Const" => new Const(),
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

        private void OpenAlgorithmsButton_Click(object sender, RoutedEventArgs e)
        {
            AlgorithmsPopup.IsOpen = true;
        }
    }
}