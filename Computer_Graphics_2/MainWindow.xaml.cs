﻿using Microsoft.Win32;
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

namespace Computer_Graphics_2
{

    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Load_Image(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFile = new();
            openFile.Filter = "Image files| *.jpg; *.png";
            openFile.FilterIndex = 1;
            if (openFile.ShowDialog() == true)
            {
                imagePicture.Source = new BitmapImage(new Uri(openFile.FileName));
            }
        }

        private void Smoothing_Filter(object sender, RoutedEventArgs e)
        {
            if(imagePicture.Source != null)
            {
                SmoothingFilter img = new(imagePicture.Source as BitmapImage);
                imagePicture.Source = img.Test();
            }
            else
            {
                MessageBox.Show("Wrong format or you did not select any image");
            }
        }

        private void Median_Filter(object sender, RoutedEventArgs e)
        {
            if(imagePicture.Source != null)
            {
                MedianFilter img = new(imagePicture.Source as BitmapImage);
                imagePicture.Source = img.Filtr();
            }
            else
            {
                MessageBox.Show("Wrong format or you did not select any image");
            }

        }

        private void Sobel_Click(object sender, RoutedEventArgs e)
        {
            if (imagePicture.Source != null)
            {
                SobelFilter img = new(imagePicture.Source as BitmapImage);
                imagePicture.Source = img.convertbtn_Click();
            }
            else
            {
                MessageBox.Show("Wrong format or you did not select any image");
            }
        }
        private void Dilatation_Click(object sender, RoutedEventArgs e)
        {
            if (imagePicture.Source != null)
            {
                Dilatation img = new(imagePicture.Source as BitmapImage);
                imagePicture.Source = img.Dilation(5);
            }
            else
            {
                MessageBox.Show("Wrong format or you did not select any image");
            }
        }

        private void Erosion_Click(object sender, RoutedEventArgs e)
        {
            if (imagePicture.Source != null)
            {
                Erosion img = new(imagePicture.Source as BitmapImage);
                imagePicture.Source = img.ErodeImage();
            }
            else
            {
                MessageBox.Show("Wrong format or you did not select any image");
            }
        }
    }
    
}
