using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using EnergieatlasLeibnitz.Classes.BaseClasses;

namespace EnergieatlasLeibnitz.Classes
{
    class MapCanvas : Canvas
    {
        TileStorage stor = new TileStorage();
        Image map;
        bool mouseCaptured = false;
        Point previousMouse;
        Point currentPosition;
        Grid grid;
        int layer = 15;
        GridGenerator gridGenerator;
        Point center;
        Rect bounds;
        Rect gridBounds;

        public MapCanvas()
        {
            stor.Generate();
            currentPosition = new Point(0, 0);

            grid = new Grid();
            gridGenerator = new GridGenerator();

            grid = gridGenerator.GenerateGrid(layer);

            grid.ShowGridLines = true;

            FillGrid();

            Children.Add(grid);

            this.ClipToBounds = true;
            this.Focusable = true;
            this.FocusVisualStyle = null;
            this.SnapsToDevicePixels = true;
            this.MaxWidth = grid.ColumnDefinitions.Count * 256;
            this.MaxHeight = grid.RowDefinitions.Count * 256;

            this.SizeChanged += new SizeChangedEventHandler(this.size);

            gridBounds = grid.TransformToAncestor(this).TransformBounds(new Rect(0.0, 0.0, grid.ColumnDefinitions.Count * 256, grid.RowDefinitions.Count * 256));

            System.Diagnostics.Debug.WriteLine(gridBounds.Width);
            System.Diagnostics.Debug.WriteLine(gridBounds.Height);
        }

        public void SetImagePosition(Image map, int posX, int posY)
        {
            Canvas.SetTop(map, posY);
            Canvas.SetLeft(map, posX);
        }

        public void SetPosition(int posX, int posY)
        {
            Canvas.SetTop(grid, posY);
            Canvas.SetLeft(grid, posX);
        }

        public Image GetImage(string imagePath)
        {
            string projectLocation = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            projectLocation = projectLocation.Substring(0, projectLocation.LastIndexOf("EnergieatlasLeibnitz"));
            //projectLocation = Directory.GetParent(projectLocation).FullName;
            //projectLocation = Directory.GetParent(projectLocation).FullName;

            imagePath = Path.Combine(projectLocation, @"Energieatlas_v1.0\", imagePath);

            if (File.Exists(imagePath))
            {
                FileStream file = null;
                try
                {
                    file = File.OpenRead(imagePath);

                    var bitmap = new BitmapImage();

                    bitmap.BeginInit();
                    bitmap.CacheOption = BitmapCacheOption.OnLoad;
                    bitmap.StreamSource = file;
                    bitmap.EndInit();

                    bitmap.Freeze();
                    //return bitmap;

                    Image i = new Image();

                    i.Source = bitmap;

                    return i;
                }
                catch
                {
                    MessageBox.Show("Error loading file: " + imagePath);
                    return null;
                }
            }
            else
                MessageBox.Show("File does not exist: " + imagePath);
            return null;
        }

        public void FillGrid()
        {
            grid.Children.Clear();

            Tile tile = null;
            map = new Image();

            int count = 0;

            for (int i = 0; i < grid.RowDefinitions.Count; i++)
            {
                for (int j = 0; j < grid.ColumnDefinitions.Count; j++)
                {
                    switch (layer)
                    {
                        case 15: { tile = stor.tileArrayLayer15[i, j]; break; }
                        case 16: { tile = stor.tileArrayLayer16[i, j]; break; }
                        case 17: { tile = stor.tileArrayLayer17[i, j]; break; }
                        case 18: { tile = stor.tileArrayLayer18[i, j]; break; }
                    }

                    grid.Children.Add(GetImage(tile.imagePath));

                    UIElement e = grid.Children[count];

                    Grid.SetColumn(e, j);
                    Grid.SetRow(e, i);

                    count++;
                }
            }

            //for (int i = 0; i < this.ActualHeight / 256; i++)
            //{
            //    for (int j = 0; j < this.ActualWidth / 256; j++)
            //    {
            //        switch (layer)
            //        {
            //            case 15: { tile = stor.tileArrayLayer15[i, j]; break; }
            //            case 16: { tile = stor.tileArrayLayer16[i, j]; break; }
            //            case 17: { tile = stor.tileArrayLayer17[i, j]; break; }
            //            case 18: { tile = stor.tileArrayLayer18[i, j]; break; }
            //        }

            //        grid.Children.Add(GetImage(tile.imagePath));

            //        UIElement e = grid.Children[count];

            //        Grid.SetColumn(e, j);
            //        Grid.SetRow(e, i);

            //        count++;
            //    }
            //}
        }


        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonDown(e);
            this.Focus();
            if (this.CaptureMouse())
            {
                mouseCaptured = true;
                previousMouse = e.GetPosition(null);
            }
        }

        private void size(object sender, System.EventArgs e)
        {
            //FillGrid();

            bounds = new Rect(0.0, 0.0, this.ActualWidth, this.ActualHeight);

            System.Diagnostics.Debug.WriteLine("BOUNDS WIDTH: " + bounds.Width);
            System.Diagnostics.Debug.WriteLine("BOUNDS HEIGHT: " + bounds.Height);
        }

        protected override void OnMouseLeftButtonUp(MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonUp(e);
            this.ReleaseMouseCapture();
            mouseCaptured = false;
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);

            if (mouseCaptured)
            {
                if (bounds.Left > gridBounds.Right || bounds.Right < gridBounds.Left || bounds.Bottom < gridBounds.Top || bounds.Top > gridBounds.Bottom)
                {
                    System.Diagnostics.Debug.WriteLine("OUT OF BOUNDS");
                }
                else
                {
                    Point newMousePosition = e.GetPosition(null);
                    currentPosition.X += newMousePosition.X - previousMouse.X;
                    currentPosition.Y += newMousePosition.Y - previousMouse.Y;
                    SetPosition((int)currentPosition.X, (int)currentPosition.Y);
                    previousMouse = newMousePosition;

                    gridBounds.Location = new Point(newMousePosition.X - previousMouse.X, newMousePosition.Y - previousMouse.Y);

                    System.Diagnostics.Debug.WriteLine(gridBounds.Location);
                    System.Diagnostics.Debug.WriteLine(gridBounds.Left);
                }
            }
        }

        protected override void OnMouseWheel(MouseWheelEventArgs e)
        {
            base.OnMouseWheel(e);

            if(e.Delta > 0 && (layer != 18))
            {
                layer++;
                grid.Children.Clear();
                grid.ColumnDefinitions.Clear();
                grid.RowDefinitions.Clear();
                grid = gridGenerator.GenerateGrid(layer);

                this.MaxWidth = grid.ColumnDefinitions.Count * 256;
                this.MaxHeight = grid.RowDefinitions.Count * 256;

                FillGrid();
            }

            if(e.Delta < 0 && (layer != 15))
            {
                layer--;
                grid.Children.Clear();
                grid.ColumnDefinitions.Clear();
                grid.RowDefinitions.Clear();
                grid = gridGenerator.GenerateGrid(layer);

                this.MaxWidth = grid.ColumnDefinitions.Count * 256;
                this.MaxHeight = grid.RowDefinitions.Count * 256;

                FillGrid();
            }
        }
    }
}
