using System;
using System.Linq;
using System.Text.RegularExpressions;
using Xunit;

namespace StringCalculator
{
    public class RegExTests
    {
        [Fact]
        public void Test1()
        {
            var str = "blah blah blah 30.15$.";
            var regex = new Regex(@"\s\d+\.\d+\$\.");
            Assert.Equal(" 30.15$.", regex.Match(str).Value);
        }

        [Fact]
        public void Test2()
        {
            var str = "blah blah blah 30.15$.";
            var regex = new Regex(@"\s\d+(\.\d+)?\$\.");
            Assert.Equal(" 30.15$.", regex.Match(str).Value);
        }

        [Fact]
        public void Test3()
        {
            var str = "blah blah blah 30$.";
            var regex = new Regex(@"\s\d+(\.\d+)?\$\.");
            Assert.Equal(" 30$.", regex.Match(str).Value);
        }

        [Fact]
        public void Test4()
        {
            var str = "abc";
            var regex = new Regex(@"[^0-9]{3}");
            Assert.Equal("abc", regex.Match(str).Value);
        }

        [Fact]
        public void Test5()
        {
            var regex = new Regex(@"0*[1-9][0-9]*");
            Assert.Equal("00077", regex.Match("00077").Value);
            Assert.Equal("00077", regex.Match("00077bb").Value);
            Assert.Equal("007", regex.Match("007").Value);
        }

        [Fact]
        public void Test6()
        {
            var regex = new Regex(@"^//{1}[^0-9]+\n{1}");
            Assert.Equal("//***\n", regex.Match("//***\n0***9***7").Value);
            Assert.False(regex.IsMatch("/***\n0***9***7"));
            Assert.False(regex.IsMatch("123//***\n0***9***7"));
        }

        [Fact]
        public void Test7()
        {
            var regex = new Regex(@"^\//(\[{1}[^0-9]+\]{1})+[\n]+");
            Assert.Equal("//[***]\n", regex.Match("//[***]\n3***4***5***").Value);
            Assert.Equal("//[***][--]\n", regex.Match("//[***][--]\n3***4***5***").Value);
            Assert.False(regex.IsMatch("22//[***]"));
        }

        [Fact]
        public void Test10()
        {
            const string str = "//[***][--]\n1***2--3***4";
            const string pattern = "^//.*\n";
            var strValues = Regex.Replace(str, pattern, String.Empty);
            var delimiterPattern = new Regex(@"\[(?<val>.*?)\]");
            var delimiters = (from Match m in delimiterPattern.Matches(str) where m.Success select m.Groups["val"].Value).ToList();
            var values = strValues.Split(delimiters.ToArray(), StringSplitOptions.RemoveEmptyEntries);
            Assert.Equal(10, values.Select(Int32.Parse).Sum());
        }
    }
}
