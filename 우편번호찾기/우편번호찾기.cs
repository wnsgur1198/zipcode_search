using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Controls;
using System.Windows.Media;
using System.IO;

namespace 의료IT공학과.데이터베이스
{
    class 우편번호찾기 : Window
    {

        LocalDB zipDB = new LocalDB("Provider=Microsoft.ACE.OLEDB.12.0; " +
                                  "Data Source=../../../DBFiles/ZIPDB.accdb;  " +
                                  "Persist Security Info=False");

        우편번호찾기폼 우편번호폼 = null;

        [STAThread]
        static void Main()
        {
            new Application().Run(new 우편번호찾기());
        }//Main

        //생성자
        public 우편번호찾기()
        {
            Title = "우편번호 찾기";
            SizeToContent = SizeToContent.WidthAndHeight;
            ResizeMode = ResizeMode.CanMinimize;

            우편번호폼 = new 우편번호찾기폼();
            Content = 우편번호폼;
            우편번호폼.읍면동TextBox.KeyDown +=new KeyEventHandler(읍면동TextBox_KeyDown);
            우편번호폼.우편번호ListBox.SelectionChanged += new SelectionChangedEventHandler(우편번호ListBox_SelectionChanged);

        }//우편번호찾기()

        void 읍면동TextBox_KeyDown(object sender, KeyEventArgs args)
        {
            if (args.Key != Key.Enter) return;
            TextBox box = sender as TextBox;
            if (string.IsNullOrEmpty(box.Text)) return;

            zipDB.Open();

            string sql = string.Format("select * from ZIPTable where DONG like '{0}%' " +
                                              "order by SIDO, GUGUN, DONG, RI, BUNJI ", box.Text);

            zipDB.ExecuteReader(sql);

            //리스트박스에 있는 내용을 모두 지우고...
            우편번호폼.우편번호ListBox.Items.Clear();

            //새로 읽은 내용으로 리스트박스 채우기...
            while (zipDB.Read())
            {
                string item = "";
                item += zipDB.GetData("ZIPCODE").ToString() + " ";
                item += zipDB.GetData("SIDO").ToString() + " ";
                item += zipDB.GetData("GUGUN").ToString() + " ";
                item += zipDB.GetData("DONG").ToString() + " ";
                item += zipDB.GetData("RI").ToString() + " ";
                item += zipDB.GetData("BUNJI").ToString() + " ";

                우편번호폼.우편번호ListBox.Items.Add(item);
            }//while

            zipDB.Close();

        }

        void 우편번호ListBox_SelectionChanged(object sender, SelectionChangedEventArgs args)
        {
            ListBox list = sender as ListBox;
            if (list == null) return;
            if (list.Items.Count == 0) return;
            우편번호폼.주소TextBox.Text = list.SelectedItem.ToString();
        }
    }//class

}//namespace