using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace NonCrossingLinesDrawer
{
    public partial class MainWindow : Window
    {
        int selectedPoints = 0;
        int firstPoint = -1;
        int secondPoint = -1;
        int size = 80;

        AdjacencyMatrix pixelsAdjacency;
        public MainWindow()
        {
            InitializeComponent();

            for(int i=0;i<size;i++)
            {
                var row = new RowDefinition();

                var column = new ColumnDefinition();

                mainGrid.RowDefinitions.Add(row);
                mainGrid.ColumnDefinitions.Add(column);

                for (int j=0;j<size;j++)
                {
                    var canvas = new Canvas();
                    canvas.Background = new SolidColorBrush(Colors.White);
                    canvas.MouseEnter += Canvas_MouseEnter;
                    canvas.MouseLeave += Canvas_MouseLeave;
                    canvas.MouseDown += mainGrid_MouseDown;

                    Grid.SetColumn(canvas, i);
                    Grid.SetRow(canvas, j);

                    mainGrid.Children.Add(canvas);
                }
            }

            pixelsAdjacency = new AdjacencyMatrix(size);
            var matrix = pixelsAdjacency.Matrix;

            //rysowanie kolejnej linni wymaga nowego przeszukania bfs z nowym matrixem
        }

        private void Canvas_MouseLeave(object sender, MouseEventArgs e)
        {
            //(sender as Canvas).Background = new SolidColorBrush(Colors.White);
        }

        private void Canvas_MouseEnter(object sender, MouseEventArgs e)
        {
            //(sender as Canvas).Background = new SolidColorBrush(Colors.LightGray);
        }

        private void mainGrid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (selectedPoints < 1)
            {
               
                Canvas _btn = sender as Canvas;

                int _row = (int)_btn.GetValue(Grid.RowProperty);
                int _column = (int)_btn.GetValue(Grid.ColumnProperty);

                firstPoint = _row * size + _column;

                lineWidthLabel.Content = string.Empty;
                lineColor.Background = new SolidColorBrush(Colors.Transparent);
                firstPointLabel.Content = string.Empty;
                secondPointLabel.Content = string.Empty;

                firstPointLabel.Content = firstPoint;

                //_btn.Background = new SolidColorBrush(Colors.LightGray);
                selectedPoints += 1;
            }
            else if (firstPoint!=-1 && selectedPoints < 2)
            {

                Canvas _btn = sender as Canvas;

                int _row = (int)_btn.GetValue(Grid.RowProperty);
                int _column = (int)_btn.GetValue(Grid.ColumnProperty);

                secondPoint = _row * size + _column;
                secondPointLabel.Content = secondPoint;

                //_btn.Background = new SolidColorBrush(Colors.LightGray);
                selectedPoints += 1;

                if (selectedPoints == 2)
                {
                    try
                    {
                        var bfs2 = new BreadthPathFinder(firstPoint, pixelsAdjacency.Matrix);
                        bfs2.GetBFSPath();
                        var path2 = bfs2.RecreatePathToPoint(secondPoint, new List<int>());
                        DrawLine(path2);
                        lineWidthLabel.Content = $"{path2.Count} points";

                        pixelsAdjacency.RemoveAdjacency(path2);

                        secondPoint = -1;
                        firstPoint = -1;
                        selectedPoints = 0;
                    }
                    catch (Exception exp)
                    {
                        MessageBox.Show(ErrorMessages.CantDrawLine(firstPoint,secondPoint),"Can not draw line",MessageBoxButton.OK,MessageBoxImage.Information);
                        selectedPoints = 0;
                        firstPoint = -1;
                        secondPoint = -1;

                        lineWidthLabel.Content = string.Empty;
                        lineColor.Background = new SolidColorBrush(Colors.Transparent);
                        firstPointLabel.Content = string.Empty;
                        secondPointLabel.Content = string.Empty;
                    }
                }
            }
        }

        private void DrawLine(IEnumerable<int> linePath)
        {
            Random rnd = new Random(Guid.NewGuid().GetHashCode());
            var color = Color.FromRgb((byte)rnd.Next(1, 255), (byte)rnd.Next(1, 255), (byte)rnd.Next(1, 255));
            lineColor.Background = new SolidColorBrush(color);
            foreach (var point in linePath)
            {
                int i = point % size;
                int j = point / size;

                var pixel = mainGrid.Children.OfType<Canvas>().FirstOrDefault(e => Grid.GetColumn(e) == i && Grid.GetRow(e) == j);

                pixel.Background = new SolidColorBrush(color) ;
            }
        }
    }
}
