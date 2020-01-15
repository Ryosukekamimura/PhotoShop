using Microsoft.Win32;
using OpenCvSharp;
using OpenCvSharp.Extensions;
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

namespace PhotoShop
{
    /// <summary>
    /// MainWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class MainWindow : System.Windows.Window
    {
        public string ImageSourcePath = null;
        public int first_image_skip = 0;



        public MainWindow()
        {
            InitializeComponent();
        }

        private void open_button_Click(object sender, RoutedEventArgs e)
        {
            //最初はタブが生成されているのでスキップする。
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
            {
                ImageSourcePath = openFileDialog.FileName;

            }
            BitmapImage bitmapImage = new BitmapImage(new Uri(ImageSourcePath));

            if (first_image_skip == 0)
            {
                imageBox01.Source = bitmapImage;
                first_image_skip++;
            }
            else
            {
                TabItem tabItem = new TabItem()
                {
                    Header = "Test",
                    Name = "test",
                };
                tabControl.Items.Add(tabItem);

            }




        }

        private void attribute_Click(object sender, RoutedEventArgs e)
        {
            SubAttributeWindow sAW = new SubAttributeWindow();
            sAW.Owner = this;
            sAW.Left = sAW.Owner.Left + sAW.Owner.Width - sAW.Width;
            sAW.Top = sAW.Owner.Top + sAW.Owner.Height - sAW.Height;

            BitmapImage bitmapImage = new BitmapImage(new Uri(ImageSourcePath));
            sAW.documentProperty_Width.Text = bitmapImage.PixelWidth.ToString();
            sAW.documentProperty_Height.Text = bitmapImage.PixelHeight.ToString();





            sAW.Show();
        }

        private void attribute_DragOver(object sender, DragEventArgs e)
        {
            SubAttributeWindow sAW = new SubAttributeWindow();
            sAW.Owner = this;
            sAW.Show();
        }

        private void attribute_Drop(object sender, DragEventArgs e)
        {
            SubAttributeWindow sAW = new SubAttributeWindow();
            sAW.Owner = this;
            sAW.Show();
        }

        private void gray_button_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            using (Mat mat = new Mat(ImageSourcePath))
            using (Mat matGray = mat.CvtColor(ColorConversionCodes.BGR2GRAY))
            {
                //Cv2.ImShow("grayscale_show", matGray);
                //imageBox01.Source = matGray;
                BitmapSource bitmapSource = BitmapSourceConverter.ToBitmapSource(matGray);
                imageBox01.Source = bitmapSource;
            }
        }

        private void Gaussian_filter_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {

            byte[,] img = GaussianFilter.LoadImageGray(ImageSourcePath);
            const int kernelSize = 3;
            double[,] kernel = new double[kernelSize, kernelSize]
            {
                {1/16.0, 1/8.0,1/16.0 },
                {1/8.0, 1/4.0,1/8.0 },
                {1/16.0, 1/8.0,1/16.0 },

            };
            byte[,] img2 = GaussianFilter.Filter(img, kernel);



            //画像保存
            GaussianFilter.SaveImagr(img2, "dst.jpg");

        }

        private void test_Click(object sender, RoutedEventArgs e)
        {
            //ここはテスト画面です。
            //他のウィンドウに移動し、テストしてください。
            Test testWindow = new Test();
            testWindow.Show();
        }

        private void test1_Click(object sender, RoutedEventArgs e)
        {
            //ここはテスト用xamlです。
            Test1 test1 = new Test1();
            test1.Show();
        }
    }
}
