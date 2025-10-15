using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using Logic;
using Grid = Logic.Grid;
using System.Windows.Threading;

namespace IHM;

public partial class GamePage : Page
{
    private Grid logicGrid;      
    private bool isMouseDown = false;
    private bool currentPaintState = false;
    private readonly DispatcherTimer _timer;
    private int _speedMs = 300;

    public GamePage()
    {
        InitializeComponent();
        this.Loaded += GamePage_Loaded;

        _timer = new DispatcherTimer();
        _timer.Interval = TimeSpan.FromMilliseconds(_speedMs);
        _timer.Tick += (_, __) => AdvanceOneGeneration();  

        GameCanvas.MouseLeftButtonUp += Canvas_MouseUp;
        GameCanvas.MouseMove += Canvas_MouseMove;
    }

    private void GamePage_Loaded(object sender, RoutedEventArgs e)
    {
        var rules = new GameRules();
        logicGrid = new Grid(50, 50, rules);

        logicGrid.GetCell(2, 3).IsAlive = true;
        logicGrid.GetCell(3, 4).IsAlive = true;
        logicGrid.GetCell(4, 2).IsAlive = true;
        logicGrid.GetCell(4, 3).IsAlive = true;
        logicGrid.GetCell(4, 4).IsAlive = true;

        DrawGrid();
    }

    private void DrawGrid()
    {
        const int cellSize = 20;
        GameCanvas.Children.Clear();

        for (int row = 0; row < logicGrid.Rows; row++)
        {
            for (int col = 0; col < logicGrid.Cols; col++)
            {
                var cell = logicGrid.GetCell(row, col);

                var rect = new Rectangle
                {
                    Width = cellSize,
                    Height = cellSize,
                    Fill = cell.IsAlive ? Brushes.White : Brushes.Black,
                    Stroke = Brushes.White,
                    StrokeThickness = 0.1
                };

                rect.Tag = new Point(row, col);
                rect.MouseLeftButtonDown += Rectangle_MouseLeftButtonDown; 

                Canvas.SetLeft(rect, col * cellSize);
                Canvas.SetTop(rect, row * cellSize);

                GameCanvas.Children.Add(rect);
            }
        }
    }


    private void Rectangle_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
    {
        if (sender is Rectangle rect && rect.Tag is Point point)
        {
            int row = (int)point.X;
            int col = (int)point.Y;

            var cell = logicGrid.GetCell(row, col);
            cell.IsAlive = !cell.IsAlive;
            rect.Fill = cell.IsAlive ? Brushes.White : Brushes.Black;

            isMouseDown = true;
            currentPaintState = cell.IsAlive;
            GameCanvas.CaptureMouse();
            e.Handled = true;
        }
    }

    private void Canvas_MouseUp(object sender, MouseButtonEventArgs e)
    {
        isMouseDown = false;
        GameCanvas.ReleaseMouseCapture();
    }

    private void Canvas_MouseMove(object sender, MouseEventArgs e)
    {
        if (!isMouseDown) return;

        var pos = e.GetPosition(GameCanvas);
        var hit = VisualTreeHelper.HitTest(GameCanvas, pos);
        if (hit?.VisualHit is Rectangle rect && rect.Tag is Point point)
        {
            int row = (int)point.X;
            int col = (int)point.Y;
            var cell = logicGrid.GetCell(row, col);

            if (cell.IsAlive != currentPaintState)
            {
                cell.IsAlive = currentPaintState;
                rect.Fill = currentPaintState ? Brushes.White : Brushes.Black;
            }
        }
    }

    private void AdvanceOneGeneration()
    {
        logicGrid.NextGeneration();
        DrawGrid();
    }

    private void Play()
    {
        if (_timer.IsEnabled) return;
        _timer.Interval = TimeSpan.FromMilliseconds(_speedMs);
        _timer.Start();
    }

    private void Pause()
    {
        if (!_timer.IsEnabled) return;
        _timer.Stop();
    }

    private void SetSpeed(int ms)
    {
        _speedMs = Math.Max(10, ms);
        _timer.Interval = TimeSpan.FromMilliseconds(_speedMs);
    }

    private void BtnNext_Click(object sender, RoutedEventArgs e) => AdvanceOneGeneration();
    private void BtnPlay_Click(object sender, RoutedEventArgs e) => Play();
    private void BtnPause_Click(object sender, RoutedEventArgs e) => Pause();

    private void SpeedSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
    {
        if (!IsLoaded || SpeedLabel == null) return;
        int ms = (int)e.NewValue;
        SpeedLabel.Text = ms.ToString();
        SetSpeed(ms);
    }
}
