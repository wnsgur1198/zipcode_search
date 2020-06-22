using System;
using System.IO;

namespace 의료IT공학과.데이터베이스
{
    class 우편번호DB만들기
    {
        static int count = 0;
        static LocalDB zipDB = null;

        static void Main()
        {
            zipDB = new LocalDB("Provider=Microsoft.ACE.OLEDB.12.0; " +
                                      "Data Source=../../../DBFiles/ZIPDB.accdb;  " +
                                      "Persist Security Info=False");
             zipDB.Open();

            //현재 테이블에 있는 내용을 모두 삭제
            zipDB.ExecuteNonQuery("delete from ZIPTable");
            Console.WriteLine("Table의 내용이 모두 지워졌습니다...");

            //텍스트파일에서 우편번호 정보를 읽어서 DB에 입력
            ZipDataReader zipTextFile = new ZipDataReader("../../../DBFiles/zipcode_20130201_UTF8.txt");

            zipTextFile.zipDataRready += new DataReady(InsertToDB);
            zipTextFile.zipDataEnd += new DataEnd(ZipEnd);

            //zipTextFile.zipDataRready += WriteToConsole;

            zipTextFile.ReadAll();

            //DB연결끊기 
            zipDB.Close();
        }//Main()

        static void ZipEnd()
        {
            Console.WriteLine("End Of File: total count: " + count);
        }

        static void InsertToDB(string zipStr)
        {
            string[] zipData = zipStr.Split('\t');

            string sql = string.Format("insert into ZIPTable " +
                             "(ZIPCODE, SIDO, GUGUN, DONG, RI, BUNJI) " +
                              "values ('{0}', '{1}', '{2}', '{3}', '{4}', '{5}')",
                                     zipData[0], zipData[1], zipData[2],
                                     zipData[3], zipData[4], zipData[5]);

            zipDB.ExecuteNonQuery(sql);

            count++;
            if ((count % 1000) == 0) Console.WriteLine(count); 
        }

    }//class 우편번호DB만들기
}//namespace