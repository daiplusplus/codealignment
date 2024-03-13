using System;
using System.IO;
using System.Linq;
using System.Xml.Serialization;
using Xunit;
using CMcG.CodeAlignment.Business;

namespace CMcG.CodeAlignment.Test.Business
{
    [Serializable]
    public class TestKeyShortcut
    {
        [Fact]
        public void SerializeAndGet()
        {
            KeyShortcut shortcut = new KeyShortcut
            {
                AddSpace       = true,
                AlignFromCaret = true,
                Alignment      = " = ",
                Language       = "CSharp",
                UseRegex       = true,
                Value          = Key.EqualsPlus
            };

            String xml        = KeyShortcut.Serialize(new[] { shortcut });
            KeyShortcut[] cloneArray = KeyShortcut.Get(xml);
            Assert.Single(cloneArray);

            KeyShortcut clone = cloneArray[0];

            Assert.Equal(shortcut.AddSpace,       clone.AddSpace);
            Assert.Equal(shortcut.AlignFromCaret, clone.AlignFromCaret);
            Assert.Equal(shortcut.Alignment,      clone.Alignment);
            Assert.Equal(shortcut.Language,       clone.Language);
            Assert.Equal(shortcut.UseRegex,       clone.UseRegex);
            Assert.Equal(shortcut.Value,          clone.Value);
        }
    }
}