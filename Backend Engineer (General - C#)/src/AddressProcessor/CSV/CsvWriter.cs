using System;
using System.IO;

namespace AddressProcessing.CSV
{
    public class CsvWriter : IDisposable
    {
        private StreamWriter _writerStream = null;
        private char _separator;
        private string _fileName;

        public CsvWriter(string fileName, char separator)
        {
            _fileName = fileName;
            _writerStream = new FileInfo(fileName).CreateText();
            _separator = separator;
        }

        public void Write(params string[] columns)
        {
            string outPut = "";

            for (int i = 0; i < columns.Length; i++)
            {
                outPut += columns[i];
                if ((columns.Length - 1) != i)
                {
                    outPut += _separator;
                }
            }

            WriteLine(outPut);
        }

        public void Dispose()
        {
            if (_writerStream != null)
            {
                _writerStream.Close();
            }
        }

        private void WriteLine(string line)
        {
            if (_writerStream != null)
            {
                _writerStream.WriteLine(line);
            }
        }
    }
}
