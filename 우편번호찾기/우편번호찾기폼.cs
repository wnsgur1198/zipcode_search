using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.IO;

namespace 의료IT공학과.데이터베이스
{
    public class 우편번호찾기폼 : StackPanel
    {
        public TextBox 읍면동TextBox = null;
        public TextBox 주소TextBox = null;
        public ListBox 우편번호ListBox = null;

        public 우편번호찾기폼()
        {
            this.Background = Brushes.Beige;
            StackPanel 읍면동Stack = new StackPanel();
            읍면동Stack.Margin = new Thickness(10);
            읍면동Stack.Orientation = Orientation.Horizontal;

            Label lbl = new Label();
            lbl.Margin = new Thickness(10);
            lbl.Content = "읍면동:";

            읍면동TextBox = new TextBox();
            읍면동TextBox.Margin = new Thickness(10);
            읍면동TextBox.Width = 230;

            주소TextBox = new TextBox();
            주소TextBox.Margin = new Thickness(10);
            주소TextBox.Width = 300;

            우편번호ListBox = new ListBox();
            우편번호ListBox.Margin = new Thickness(10);
            우편번호ListBox.Width = 300;
            우편번호ListBox.Height = 300;

            읍면동Stack.Children.Add(lbl);
            읍면동Stack.Children.Add(읍면동TextBox);

            this.Children.Add(읍면동Stack);
            this.Children.Add(주소TextBox);
            this.Children.Add(우편번호ListBox);
        }
    }
}