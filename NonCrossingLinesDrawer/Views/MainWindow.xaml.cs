using NonCrossingLinesDrawer.Exceptions;
using NonCrossingLinesDrawer.Extensions;
using NonCrossingLinesDrawer.Views;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace NonCrossingLinesDrawer
{
    public partial class MainWindow : Window
    {
        private int _drawAreaSize = 100;
        private Point _firstPoint = null;
        private Point _secondPoint = null;
        private AdjacencyList _pixelsAdjacency;
        private Random _colorRand;

        public MainWindow()
        {
            InitializeComponent();
            InitalizeEmptyDrawingArea();
            _colorRand = new Random(Guid.NewGuid().GetHashCode());
        }

        private void InitalizeEmptyDrawingArea()
        {
            for (int i = 0; i < _drawAreaSize; i++)
            {
                var row = new RowDefinition();
                var column = new ColumnDefinition();

                drawingArea.RowDefinitions.Add(row);
                drawingArea.ColumnDefinitions.Add(column);

                for (int j = 0; j < _drawAreaSize; j++)
                {
                    var canvas = new Canvas();
                    canvas.Background = new SolidColorBrush(Colors.White);
                    canvas.MouseDown += drawingArea_MouseDown;

                    Grid.SetColumn(canvas, i);
                    Grid.SetRow(canvas, j);

                    drawingArea.Children.Add(canvas);
                }
            }
            _pixelsAdjacency = new AdjacencyList(_drawAreaSize);
        }

        private void drawingArea_MouseDown(object sender, MouseButtonEventArgs e)
        {
            var gridCanvas = sender as Canvas;
            int _row = gridCanvas.GetRowNumber();
            int _column = gridCanvas.GetColumnNumber();

            if (_firstPoint.IsNull() && _secondPoint.IsNull())
            {
                _firstPoint = new Point(_column, _row, _row * _drawAreaSize + _column);
                ClearUiLabels();
                firstPointLabel.Content = _firstPoint.Number;
            }
            else if (_firstPoint.IsNotNull() && _secondPoint.IsNull())
            {
                _secondPoint = new Point(_column, _row, _row * _drawAreaSize + _column);
                secondPointLabel.Content = _secondPoint.Number;
                var color = GetRandomColor();

                try
                {
                    var _pathFinder = new BreadthPathFinder(_pixelsAdjacency.List);
                    var line = _pathFinder.GetLineBeetwen(_firstPoint, _secondPoint);
                    DrawLine(line, color);
                    lineWidthLabel.Content = $"{line.Count} points";

                    _pixelsAdjacency.RemoveAdjacency(line.Path);
                }
                catch (NoPathException noPathExp)
                {
                    MessageBox.Show(noPathExp.Message, "Can not draw line !", MessageBoxButton.OK, MessageBoxImage.Information);
                    ClearUiLabels();
                }
                catch (Exception)
                {
                    MessageBox.Show("Unidentified problem occured", "Can not draw line !", MessageBoxButton.OK, MessageBoxImage.Error);
                    ClearUiLabels();
                }
                finally
                {
                    RemoveSelectedPoints();
                }

            }
        }

        private Color GetRandomColor()
        {
            return Color.FromRgb((byte)_colorRand.Next(1, 255), (byte)_colorRand.Next(1, 255), (byte)_colorRand.Next(1, 255));
        }

        private void ClearUiLabels()
        {
            lineWidthLabel.Content = string.Empty;
            lineColor.Background = new SolidColorBrush(Colors.Transparent);
            firstPointLabel.Content = string.Empty;
            secondPointLabel.Content = string.Empty;
        }

        private void DrawLine(Line line, Color color)
        {
            
            lineColor.Background = new SolidColorBrush(color);

            foreach (var point in line.Path)
            {
                var pixel = drawingArea.Children
                    .OfType<Canvas>()
                    .FirstOrDefault(e => Grid.GetColumn(e) == point.X && Grid.GetRow(e) == point.Y);

                pixel.Background = new SolidColorBrush(color);
            }
        }

        private void MenuItem_Click_Clear(object sender, RoutedEventArgs e)
        {
            var result = MessageBox.Show("Are sure that you want to clear all drawn lines?", "Clear drawing area", MessageBoxButton.YesNo);
            if (result == MessageBoxResult.Yes)
            {
                RemoveSelectedPoints();
                ClearUiLabels();

                foreach (var gridCanvas in drawingArea.Children.OfType<Canvas>())
                {
                    gridCanvas.Background = new SolidColorBrush(Colors.White);
                }

                _pixelsAdjacency = new AdjacencyList(_drawAreaSize);
            }

        }

        private void RemoveSelectedPoints()
        {
            _firstPoint = null;
            _secondPoint = null;
        }

        private void MenuItem_Click_Exit(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void MenuItem_Click_About(object sender, RoutedEventArgs e)
        {
            AboutWindow aboutW = new AboutWindow();
            aboutW.Owner = this;
            aboutW.ShowDialog();
        }
    }
}
