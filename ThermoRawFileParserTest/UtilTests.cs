using System.Text.RegularExpressions;
using System.Web.UI.WebControls;
using NUnit.Framework;
using ThermoRawFileParser.Util;

namespace ThermoRawFileParserTest
{
    [TestFixture]
    public class UtilTests
    {
        [Test]
        public void TestRegex()
        {
            const string filterString = "ITMS + c NSI r d Full ms2 961.8803@cid35.00 [259.0000-1934.0000]";
            const string pattern = @"ms2 (.*?)@";
            
            Match result = Regex.Match(filterString, pattern);
            if (result.Success)
            {
                Assert.AreEqual("961.8803", result.Groups[1].Value);
            }
            else
            {
                Assert.Fail();
            }
        }

        [Test]
        public void TestTable()
        {
            TableData table = new TableData();
            table.AddRow(new System.Collections.Generic.Dictionary<string, string> { ["header1"] = "row1", ["header2"] = "row2" });
            table.AddRow(new System.Collections.Generic.Dictionary<string, string> { ["header1"] = "row1_1", ["header3"] = "row3" });
            table.AddRow(new System.Collections.Generic.Dictionary<string, string> { ["header4"] = "row4", ["header5"] = "row5" });
            table.AddRow(new System.Collections.Generic.Dictionary<string, string> { ["header2"] = "row2_1", ["header5"] = "row5_1" });

            Assert.IsTrue(table.Headers.Count == 5);
            table.Headers.SetEquals(new string[] { "header1", "header2", "header3", "header4", "header5" });
            Assert.IsTrue(table.Data.Count == 4);
        }
    }
}