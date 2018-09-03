using System;
using System.IO;

namespace AddressProcessing.CSV
{
    /*
        2) Refactor this class into clean, elegant, rock-solid & well performing code, without over-engineering.
           Assume this code is in production and backwards compatibility must be maintained.
    */

    public class CSVReaderWriter
    {
        //** keeping this double responcibility class for "backwards compatibility"

        private CsvReader _csvReader;
        private CsvWriter _csvWriter;
        private Mode _fileMode;

        [Flags]
        public enum Mode { Read = 1, Write = 2 };

        public void Open(string fileName, Mode mode)
        {
            _fileMode = mode;

            if (mode == Mode.Read)
            {
                _csvReader = new CsvReader(fileName, '\t');
            }
            else
            {
                _csvWriter = new CsvWriter(fileName, '\t');
            }
        }

        public void Write(params string[] columns)
        {
            if (_fileMode == Mode.Read)
            {
                throw new Exception("Attempt to write to file while in read mode");
            }

            _csvWriter.Write(columns);
        }

        public bool Read(string column1, string column2)
        {
            //** keeping this method for "backwards compatibility"
            string col1 = "", col2 = "";
            return Read(out col1, out col2);
        }

        public bool Read(out string column1, out string column2)
        {
            if (_fileMode == Mode.Write)
            {
                throw new Exception("Attempt to read from file while in Write mode");
            }

            string[] line = _csvReader.ReadLine();
            column1 = null;
            column2 = null;
            bool result = false;
            if (line.Length != 0)
            {
                column1 = line[0];
                column2 = line[1];
                result = true;
            }
            return result;
        }

        public void Close()
        {
            if (_csvWriter != null)
            {
                _csvWriter.Dispose();
            }

            if(_csvReader != null)
            {
                _csvReader.Dispose();
            }
        }
    }
}
