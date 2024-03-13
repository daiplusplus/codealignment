using System;
using System.IO;
using System.Xml.Serialization;

namespace CMcG.CodeAlignment.Business
{
    [Serializable]
    public class KeyShortcut
    {
        private static readonly XmlSerializer _keyShortcutArraySerializer = new XmlSerializer(typeof(KeyShortcut[]));

        public Key     Value          { get; set; }
        public String  Alignment      { get; set; }
        public String  Language       { get; set; }
        public Boolean AlignFromCaret { get; set; }
        public Boolean UseRegex       { get; set; }
        public Boolean AddSpace       { get; set; }

        public static KeyShortcut[] Get(String xml)
        {
            using (StringReader reader = new StringReader(xml))
            {
                return (KeyShortcut[])_keyShortcutArraySerializer.Deserialize(reader);
            }
        }

        public static String Serialize(KeyShortcut[] shortcuts)
        {
            using (StringWriter writer = new StringWriter())
            {
                _keyShortcutArraySerializer.Serialize(writer, shortcuts);
                return writer.ToString();
            }
        }
    }
}