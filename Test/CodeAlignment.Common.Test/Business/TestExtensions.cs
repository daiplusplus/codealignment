using System;
using System.Collections.Generic;
using System.Linq;

using CMcG.CodeAlignment.Business;

using Xunit;

namespace CMcG.CodeAlignment.Test.Business
{
    public class TestExtensions
    {
        [Fact]
        public void UpTo()
        {
            Assert.Equal(new Int32[] { 1,2,3,4,5 }, 1.UpTo(5));
            Assert.Equal(new Int32[] { },           1.UpTo(0));
            Assert.Equal(new Int32[] { 1 },         1.UpTo(1));
        }

        [Fact]
        public void DownTo()
        {
            Assert.Equal(new Int32[] { 5,4,3,2,1 }, 5.DownTo(1));
            Assert.Equal(new Int32[] { },           5.DownTo(6));
            Assert.Equal(new Int32[] { 1 },         1.DownTo(1));
        }

        [Fact]
        public void ReplaceTabs()
        {
            Assert.Equal("    ",      "\t"     .ReplaceTabs(4));
            Assert.Equal("|   |",     "|\t|"   .ReplaceTabs(4));
            Assert.Equal("|   |",     "| \t|"  .ReplaceTabs(4));
            Assert.Equal("|   |",     "|  \t|" .ReplaceTabs(4));
            Assert.Equal("|       |", "|   \t|".ReplaceTabs(4));
            Assert.Equal("|       |", "| \t\t|".ReplaceTabs(4));
        }

        [Fact]
        public void MaxBy()
        {
            String[] data = new[] { "A", "B", "C", "DZ" };
            _ = Assert.Throws<ArgumentNullException>    (() => ((IEnumerable<String>)null).MaxBy(x => x.Length));
            _ = Assert.Throws<ArgumentNullException>    (() => data.MaxBy<String, Int32>(null));
            _ = Assert.Throws<InvalidOperationException>(() => new String[0].MaxBy(x => x.Length));

            Assert.Equal("DZ", data.MaxBy(x => x.Length));
            Assert.Equal("A",  data.Take(3).MaxBy(x => x.Length));
        }

        [Fact]
        public void MaxItemsBy()
        {
            String[] data = new[] { "A", "B", "C", "AA", "BB", "DZZ" };
            _ = Assert.Throws<ArgumentNullException>    (() => ((IEnumerable<String>)null).MaxItemsBy(x => x.Length));
            _ = Assert.Throws<ArgumentNullException>    (() => data.MaxItemsBy<String, Int32>(null));
            _ = Assert.Throws<InvalidOperationException>(() => new String[0].MaxItemsBy(x => x.Length));

            Assert.Equal(new[] { "DZZ" },      data        .MaxItemsBy(x => x.Length));
            Assert.Equal(new[] { "AA", "BB" }, data.Take(5).MaxItemsBy(x => x.Length));
        }
    }
}
