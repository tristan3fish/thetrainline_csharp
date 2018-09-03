using AddressProcessing.CSV;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AddressProcessing.Tests.CSV
{

    [TestFixture]
    public class CsvReaderTests
    {
        private const string TestInputBlankFile = @"test_data\contactsBlankFile.csv";
        private const string TestInput = @"test_data\contacts.csv";

        [Test]
        public void canReadBlankFile()
        {
            using (CsvReader fileReader = new CsvReader(TestInputBlankFile, '\t'))
            {
                string[] result = fileReader.ReadLine();

                Assert.That(result, Is.Empty);
            }
        }

        [Test]
        public void canReadFileOutput()
        {
            using (CsvReader fileReader = new CsvReader(TestInput, '\t'))
            {
                string[] result = fileReader.ReadLine();

                Assert.That(result, Is.Not.Empty);
            }
        }
    }
}
