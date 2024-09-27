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

namespace Algoritms.WPFApp
{
    public partial class MainWindow : Window
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
                    TimeCounter.TimeCount(nMin, nMax, algorithm, step, _cancellationTokenSource.Token)
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
            model.Axes.Add(new LinearAxis { Position = AxisPosition.Bottom, Title = "Количество элементов" });
            model.Axes.Add(new LinearAxis { Position = AxisPosition.Left, Title = "Время (мс)" });

            LineSeries series = new LineSeries { Title = algorithmName, MarkerType = MarkerType.Circle };
            for (int i = 0; i < times.Count; i++)
            {
                series.Points.Add(new DataPoint(nMin + i * step, times[i].TotalMilliseconds));
            }
            model.Series.Add(series);

            PlotView.Model = model;
        }

        private Algoritm GetSelectedAlgorithm()
        {
            return AlgorithmSelector.SelectedIndex switch
            {
                0 => new BubbleSort(),
                1 => new QuickSortAlgoritm(),
                2 => new TimSort(),
                _ => null
            };
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            _cancellationTokenSource?.Cancel();
        }
    }
}