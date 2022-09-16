using MiniSoftware;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using Xunit;

namespace MiniWordTests
{
    public class IssueTests
    {
        /// <summary>
        /// [support array list string to generate multiple row � Issue #11 � mini-software/MiniWord]
        /// (https://github.com/mini-software/MiniWord/issues/11)
        /// </summary>
        [Fact]
        public void TestIssue11()
        {
            {
                var path = PathHelper.GetTempFilePath();
                var templatePath = PathHelper.GetFile("TestIssue11.docx");
                var value = new Dictionary<string, object>()
                {
                    ["managers"] = new[] { "Jack", "Alan" },
                    ["employees"] = new[] { "Mike", "Henry" },
                };
                MiniWord.SaveAsByTemplate(path, templatePath, value);
                var xml = Helpers.GetZipFileContent(path, "word/document.xml");
                Assert.Contains("Jack", xml);
            }
            {
                var path = PathHelper.GetTempFilePath();
                var templatePath = PathHelper.GetFile("TestIssue11.docx");
                var value = new Dictionary<string, object>()
                {
                    ["managers"] = new List<string> { "Jack", "Alan" },
                    ["employees"] = new List<string> { "Mike", "Henry" },
                };
                MiniWord.SaveAsByTemplate(path, templatePath, value);
                var xml = Helpers.GetZipFileContent(path, "word/document.xml");
                Assert.Contains("Jack", xml);
            }
        }



        /// <summary>
        /// [Support image � Issue #3 � mini-software/MiniWord](https://github.com/mini-software/MiniWord/issues/3)
        /// </summary>
        [Fact]
        public void TestIssue3()
        {
            var path = PathHelper.GetTempFilePath();
            var templatePath = PathHelper.GetFile("TestBasicImage.docx");
            var value = new Dictionary<string, object>()
            {
                ["Logo"] = new MiniWordPicture() { Path= PathHelper.GetFile("DemoLogo.png"), Width= 180, Height= 180 }
            };
            MiniWord.SaveAsByTemplate(path, templatePath, value);
            var xml = Helpers.GetZipFileContent(path, "word/document.xml");
            Assert.Contains("<w:drawing>", xml);
        }

        /// <summary>
        /// [Fuzzy Regex replace similar key � Issue #5 � mini-software/MiniWord](https://github.com/mini-software/MiniWord/issues/5)
        /// </summary>
        [Fact]
        public void TestIssue5()
        {
            var path = PathHelper.GetTempFilePath();
            var templatePath = PathHelper.GetFile("TestBasicFill.docx");
            var value = new Dictionary<string, object>()
            {
                ["Name"] = "Jack",
                ["Company_Name"] = "MiniSofteware",
                ["CreateDate"] = new DateTime(2021, 01, 01),
                ["VIP"] = true,
                ["Points"] = 123,
                ["APP"] = "Demo APP",
            };
            MiniWord.SaveAsByTemplate(path, templatePath, value);
            //Console.WriteLine(path);
            var xml = Helpers.GetZipFileContent(path, "word/document.xml");
            Assert.DoesNotContain("Jack Demo APP Account Data", xml);
            Assert.Contains("MiniSofteware Demo APP Account Data", xml);
        }

        /// <summary>
        /// [Paragraph replace by tag � Issue #4 � mini-software/MiniWord](https://github.com/mini-software/MiniWord/issues/4)
        /// </summary>
        [Fact]
        public void TestIssue4()
        {
			var path = PathHelper.GetTempPath();
            var templatePath = PathHelper.GetFile("TestBasicFill.docx");
			var value = new Dictionary<string, object>()
			{
                ["Company_Name"] = "MiniSofteware",
                ["Name"] = "Jack",
				["CreateDate"] = new DateTime(2021, 01, 01),
				["VIP"] = true,
				["Points"] = 123,
				["APP"] = "Demo APP",
			};
			MiniWord.SaveAsByTemplate(path, templatePath, value);
			Console.WriteLine(path);
		}
    }

    internal static class Helpers
    {
        internal static string GetZipFileContent(string zipPath, string filePath)
        {
            var ns = new XmlNamespaceManager(new NameTable());
            ns.AddNamespace("x", "http://schemas.openxmlformats.org/spreadsheetml/2006/main");
            using (var stream = File.OpenRead(zipPath))
            using (ZipArchive archive = new ZipArchive(stream, ZipArchiveMode.Read, false, Encoding.UTF8))
            {
                var entry = archive.Entries.Single(w => w.FullName == filePath);
                using (var sheetStream = entry.Open())
                {
                    var doc = XDocument.Load(sheetStream);
                    return doc.ToString();
                }
            }
        }
    }

    internal static class PathHelper
    {
        public static string GetFile(string fileName,string folderName="docx")
        {
            return $@"../../../../../samples/{folderName}/{fileName}";
        }

        public static string GetTempPath(string extension = "docx")
        {
            var method = (new System.Diagnostics.StackTrace()).GetFrame(1).GetMethod();

            var path = Path.Combine(Path.GetTempPath(), $"{method.DeclaringType.Name}_{method.Name}.{extension}").Replace("<", string.Empty).Replace(">", string.Empty);
            if (File.Exists(path))
                File.Delete(path);
            return path;
        }

        public static string GetTempFilePath(string extension = "docx")
        {
            return Path.GetTempPath() + Guid.NewGuid().ToString() + "." + extension;
        }
    }
}
