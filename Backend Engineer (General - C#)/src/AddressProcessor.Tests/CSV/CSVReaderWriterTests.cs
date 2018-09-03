using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AddressProcessing.CSV;
using NUnit.Framework;
using static AddressProcessing.CSV.CSVReaderWriter;

namespace Csv.Tests
{
    [TestFixture]
    public class CSVReaderWriterTests
    {
        private const string TestInputFile = @"test_data\contacts.csv";
        private const string TestInputFileForEditing = @"test_data\contactsForEditing.csv";

        [Test]
        public void canOpenAndCloseFile()
        {
            CSVReaderWriter rw = new CSVReaderWriter();

            rw.Open(TestInputFile, Mode.Read);
            rw.Close();
        }

        [Test]
        public void canReadFileDisgardingOutput()
        {
            CSVReaderWriter rw = new CSVReaderWriter();

            rw.Open(TestInputFile, Mode.Read);

            string col1 = "", col2 = "";
            bool result = rw.Read( col1, col2);
            rw.Close();

            Assert.That(result, Is.True);
        }

        [Test]
        public void canReadFileKeepOutput()
        {
            CSVReaderWriter rw = new CSVReaderWriter();

            rw.Open(TestInputFile, Mode.Read);

            string col1="", col2="";
            bool result = rw.Read(out col1, out col2);
            rw.Close();

            Assert.That(col1, Is.Not.Empty);
            Assert.That(col2, Is.Not.Empty);
            Assert.That(result, Is.True);
        }

        [Test]
        [ExpectedException("System.Exception", ExpectedMessage = "Attempt to write to file while in read mode")]
        public void canNotWriteToFileIfInReadMode()
        {
            CSVReaderWriter rw = new CSVReaderWriter();

            try
            {
                rw.Open(TestInputFileForEditing, Mode.Read);
                rw.Write( new string[]{ "some data col1","some more data for col2"} );
            } finally
            {
                rw.Close();
            }

        }

        [Test]
        [ExpectedException("System.Exception", ExpectedMessage = "Attempt to read from file while in Write mode")]
        public void canNotReadFromFileIfInWriteMode()
        {
            CSVReaderWriter rw = new CSVReaderWriter();

            try
            {
                rw.Open(TestInputFileForEditing, Mode.Write);
                string col1 = "", col2 = "";
                bool result = rw.Read(col1, col2);
            }
            finally
            {
                rw.Close();
            }

        }

        [Test]
        public void canWriteToFile()
        {
            CSVReaderWriter rw = new CSVReaderWriter();
            //write  could generate a random string to compare with the read but keeping it simple for now
            rw.Open(TestInputFileForEditing, Mode.Write);
            rw.Write(new string[] { "some data col1 asdfasdfasdf", "some more data for col2" });
            rw.Close();

            //read what was written
            rw.Open(TestInputFileForEditing, Mode.Read);

            string col1 = "", col2 = "";
            bool result = rw.Read(out col1, out col2);
            rw.Close();

            Assert.That(col1, Is.EquivalentTo("some data col1 asdfasdfasdf"));
            Assert.That(col2, Is.EquivalentTo("some more data for col2"));
            Assert.That(result, Is.True);
        }
    }
}
