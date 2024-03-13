using System;
using System.Linq;
using CMcG.CodeAlignment.Business;

namespace CMcG.CodeAlignment
{
    public class KeyLookup
    {
        public KeyLookup(Key key)
        {
            this.Value = key;

            this.Description =
                (key >= Key.D0 && key <= Key.D9) ? key.ToString().Substring(1) :
                (key == Key.OpenBrackets       ) ? "Open Brackets"  :
                (key == Key.CloseBrackets      ) ? "Close Brackets" :
                (key == Key.EqualsPlus         ) ? "Equals Plus"    : key.ToString();
        }

        public Key    Value       { get; set; }
        public String Description { get; set; }

        private static readonly Key[] _keys = (Key[])Enum.GetValues(typeof(Key));

        public static KeyLookup[] GetKeys()
        {
            return _keys.Select(x => new KeyLookup(x)).ToArray();
        }
    }
}