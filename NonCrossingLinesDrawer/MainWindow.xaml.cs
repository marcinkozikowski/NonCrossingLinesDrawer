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
        AdjacencyMatrix pixelsAdjacency;
        public MainWindow()
        {
            InitializeComponent();

            for(int i=0;i<20;i++)
            {
                var row = new RowDefinition();

                var column = new ColumnDefinition();

                mainGrid.RowDefinitions.Add(row);
                mainGrid.ColumnDefinitions.Add(column);

                for (int j=0;j<20;j++)
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

            pixelsAdjacency = new AdjacencyMatrix(20);
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

                firstPoint = _row * 20 + _column;

                _btn.Background = new SolidColorBrush(Colors.LightGray);
                selectedPoints += 1;
            }
            else if (firstPoint!=-1 && selectedPoints < 2)
            {

                Canvas _btn = sender as Canvas;

                int _row = (int)_btn.GetValue(Grid.RowProperty);
                int _column = (int)_btn.GetValue(Grid.ColumnProperty);

                secondPoint = _row * 20 + _column;

                _btn.Background = new SolidColorBrush(Colors.LightGray);
                selectedPoints += 1;

                if (selectedPoints == 2)
                {
                    try
                    {
                        var bfs2 = new BFS(firstPoint, pixelsAdjacency.Matrix);
                        bfs2.getBFSPath();
                        var path2 = bfs2.getBFSPathToPointR(secondPoint, new List<int>());
                        DrawLine(path2);

                        pixelsAdjacency.RemoveAdjacency(path2);

                        secondPoint = -1;
                        firstPoint = -1;
                        selectedPoints = 0;
                    }
                    catch (Exception exp)
                    {
                        MessageBox.Show(exp.ToString());
                        selectedPoints = 0;
                        firstPoint = -1;
                        secondPoint = -1;
                    }
                }
            }
        }

        private void DrawLine(IEnumerable<int> linePath)
        {
            Random rnd = new Random(Guid.NewGuid().GetHashCode());
            var color = Color.FromRgb((byte)rnd.Next(1, 255), (byte)rnd.Next(1, 255), (byte)rnd.Next(1, 255));

            foreach (var point in linePath)
            {
                int i = point % 20;
                int j = point / 20;

                var pixel = mainGrid.Children.OfType<Canvas>().FirstOrDefault(e => Grid.GetColumn(e) == i && Grid.GetRow(e) == j);

                pixel.Background = new SolidColorBrush(color) ;
            }
        }
    }
}
