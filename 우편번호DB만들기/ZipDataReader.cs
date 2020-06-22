using System;
using System.IO;

namespace 의료IT공학과.데이터베이스
{
    delegate void DataReady(string str);
    delegate void DataEnd();

    class ZipDataReader : StreamReader
    {

        public event DataReady zipDataRready = null;
        public event DataEnd zipDataEnd = null;

        public ZipDataReader(string Path) : base(Path) { }

        public void ReadAll()
        {
            while (EndOfStream == false)
            {
                string zipData = ReadLine();
                if (zipDataRready != null) zipDataRready(zipData);
            }
            if (zipDataEnd != null) zipDataEnd();
        }
    }
}