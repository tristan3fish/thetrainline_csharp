using System;
using System.IO;

namespace AddressProcessing.CSV
{
    public class CsvReader : IDisposable
    {
        private StreamReader _readerStream = null;
        private char _separator;
        private string _fileName;

        public CsvReader(string fileName, char separator)
        {
            _fileName = fileName;
            _readerStream = File.OpenText(fileName);
            _separator = separator;
        }

        public string[] ReadLine()
        {
            string[] result = new string[0];
            string line = _readerStream.ReadLine();
            if (line != null)
            {
                result = line.Split(_separator);
            }
            return result;
        }

        public void Dispose()
        {
            if (_readerStream != null)
            {
                _readerStream.Close();
            }
        }
    }
}
