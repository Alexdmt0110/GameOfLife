using System.Windows;

namespace IHM
{
	public partial class MainWindow : Window
	{
		public MainWindow()
		{
			InitializeComponent();

			MainFrame.Navigate(new GamePage());
		}

		protected override void OnPreviewKeyDown(System.Windows.Input.KeyEventArgs e)
		{
			if (e.Key == System.Windows.Input.Key.Escape)
			{
				WindowStyle = WindowStyle.SingleBorderWindow;
				ResizeMode = ResizeMode.CanResize;
				Topmost = false;
				WindowState = WindowState.Normal;
				WindowState = WindowState.Maximized;
				e.Handled = true;
			}
			base.OnPreviewKeyDown(e);
		}
	}
}