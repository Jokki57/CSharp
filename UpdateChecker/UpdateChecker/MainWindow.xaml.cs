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
        public MainWindow()
        {
            InitializeComponent();

            //System.Windows.Forms.NotifyIcon ni = new System.Windows.Forms.NotifyIcon();
            //ni.Visible = true;
            //ni.DoubleClick += delegate(object sender, EventArgs args)
            //{
            //    this.Show();
            //    this.WindowState = WindowState.Normal;
            //};

            itemsController = new ItemsController(sourceList, destinationList);

            sourceButton.Click += (sender, args) =>
            {
                System.Windows.Forms.OpenFileDialog openFile = new System.Windows.Forms.OpenFileDialog();
                if (openFile.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    itemsController.AddSource(openFile.FileName);
                }
            };
            destinationButton.Click += (sender, args) =>
            {
                System.Windows.Forms.OpenFileDialog openFile = new System.Windows.Forms.OpenFileDialog();
                if (openFile.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    itemsController.AddDestination(openFile.FileName);
                }
            };
        }

        protected override void OnStateChanged(EventArgs e)
        {
            base.OnStateChanged(e);
            if (this.WindowState == System.Windows.WindowState.Minimized)
            {
                this.Hide();
            }
        }

        private void createListField(ListBox list)
        {
            StackPanel stack = new StackPanel();

        }
    }
}
