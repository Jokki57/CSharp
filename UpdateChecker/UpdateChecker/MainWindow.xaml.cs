using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;
using System.Text.RegularExpressions;
using UpdateChecker.Classes;

namespace UpdateChecker
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ItemsController itemsController;
		private System.Windows.Forms.NotifyIcon notifyIcon;
        public MainWindow()
        {
            InitializeComponent();

			notifyIcon = new System.Windows.Forms.NotifyIcon();
			notifyIcon.Icon = new System.Drawing.Icon("../../Icon1.ico");
			notifyIcon.Text = "File tracker";
			notifyIcon.DoubleClick += (sender, args) =>
			{
				this.Show();
				this.WindowState = WindowState.Normal;
			};

            itemsController = new ItemsController(sourceList, destinationList);

            sourceButtonAdd.Click += (sender, args) =>
            {
                System.Windows.Forms.OpenFileDialog openFile = new System.Windows.Forms.OpenFileDialog();
                if (openFile.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    itemsController.AddSource(openFile.FileName);
                }
            };
            destinationButtonAdd.Click += (sender, args) =>
            {
                System.Windows.Forms.OpenFileDialog openFile = new System.Windows.Forms.OpenFileDialog();
                if (openFile.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    itemsController.AddDestination(openFile.FileName);
                }
            };
			sourceButtonRemove.Click += (sender, args) =>
			{
				int index = sourceList.SelectedIndex;
				if (index != -1)
				{
					itemsController.RemoveSource(index);
				}
			};
			destinationButtonRemove.Click += (sender, args) =>
			{
				int index = destinationList.SelectedIndex;
				if (index != -1)
				{
					itemsController.RemoveDestination(index);
				}
			};
        }

        protected override void OnStateChanged(EventArgs e)
        {
            base.OnStateChanged(e);
            if (this.WindowState == System.Windows.WindowState.Minimized)
            {
                this.Hide();
				notifyIcon.Visible = true;
            }
			else if (this.WindowState == System.Windows.WindowState.Normal)
			{
				notifyIcon.Visible = false;
			}
        }

		protected override void OnClosing(System.ComponentModel.CancelEventArgs e)
		{
			base.OnClosing(e);
			notifyIcon.Visible = false;
		}
    }
}
