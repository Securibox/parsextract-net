using NUnit.Framework;
using Securibox.ParseXtract.Models;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Securibox.ParseXtract.Tests
{
    public class TestParse
    {
        private readonly Client _client;

        public TestParse()
        {
            _client = new Client("[ClientId]", "[ClientSecret]");
        }

        [Test]
        public async Task Test1()
        {
            var document = LoadDocument(@"C:\Path\To\File.pdf");
            var xdata = await _client.ParseAsync(document);
            Assert.Pass();
        }

        Document LoadDocument(string documentPath)
        {
            if (!File.Exists(documentPath))
                throw new FileNotFoundException();

            FileInfo fInfo = new FileInfo(documentPath);
            var fileContentbytes = File.ReadAllBytes(documentPath);
            var fileContentBase64Encoded = Convert.ToBase64String(fileContentbytes);

            return new Document()
            {
                FileName = fInfo.Name,
                Base64Content = fileContentBase64Encoded
            };
        }

    }
}