using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace NonCrossingLinesDrawer
{
    public partial class MainWindow : Window
    {
        int selectedPoints = 0;
        public MainWindow()
        {
            InitializeComponent();

            for(int i=0;i<5;i++)
            {
                var row = new RowDefinition();

                var column = new ColumnDefinition();

                mainGrid.RowDefinitions.Add(row);
                mainGrid.ColumnDefinitions.Add(column);

                for (int j=0;j<5;j++)
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

            AdjacencyMatrix pixelsAdjacency = new AdjacencyMatrix(5);
            var matrix = pixelsAdjacency.Matrix;

            var bfs = new BFS(18, matrix);
            bfs.getBFSPath();
            var path = bfs.getBFSPathToPointR(6, new List<int>());
            //List<int> path = bfs.getShortestBFSPath(6);
            //textBlock.Text = pixelsAdjacency.PrintMatrixAsString();

            //Po narysowaniu linni nalezy usunac wszelki powiazania do punktow ktore wchodza w sklad linni z matrixa
            pixelsAdjacency.RemoveAdjacency(path);

            var bfs2 = new BFS(4, pixelsAdjacency.Matrix);
            bfs2.getBFSPath();
            var path2 = bfs2.getBFSPathToPointR(16, new List<int>());

            pixelsAdjacency.RemoveAdjacency(path2);

            var bfs3 = new BFS(21, pixelsAdjacency.Matrix);
            bfs3.getBFSPath();
            var path3 = bfs3.getBFSPathToPointR(23, new List<int>());

            pixelsAdjacency.RemoveAdjacency(path3);

            var bfs4 = new BFS(9, pixelsAdjacency.Matrix);
            bfs4.getBFSPath();
            var path4 = bfs4.getBFSPathToPointR(15, new List<int>());
            //List<int> path2 = bfs2.getShortestBFSPath(16);

            //rysowanie kolejnej linni wymaga nowego przeszukania bfs z nowym matrixem
        }

        private void Canvas_MouseLeave(object sender, MouseEventArgs e)
        {
            (sender as Canvas).Background = new SolidColorBrush(Colors.White);
        }

        private void Canvas_MouseEnter(object sender, MouseEventArgs e)
        {
            (sender as Canvas).Background = new SolidColorBrush(Colors.LightGray);
        }

        private void mainGrid_MouseDown(object sender, MouseButtonEventArgs e)
        {
                Canvas _btn = sender as Canvas;

                int _row = (int)_btn.GetValue(Grid.RowProperty);
                int _column = (int)_btn.GetValue(Grid.ColumnProperty);

                _btn.Background = new SolidColorBrush(Colors.LightGray);
        }
    }
}
