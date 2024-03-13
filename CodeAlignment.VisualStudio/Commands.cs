using System;

namespace CMcG.CodeAlignment
{
    static class Commands
    {
        // See $\CodeAlignment.VisualStudio\CodeAlignment.vsct

        public const UInt32 AlignBy             = 0x0100;
        public const UInt32 AlignByEquals       = 0x0200;
        public const UInt32 AlignByEqualsEquals = 0x0300;
        public const UInt32 AlignByMUnderscore  = 0x0400;
        public const UInt32 AlignByQuote        = 0x0500;
        public const UInt32 AlignByPeriod       = 0x0600;
        public const UInt32 AlignBySpace        = 0x0700;
        public const UInt32 AlignFromCaret      = 0x0800;// aka AlignByPosition
        public const UInt32 AlignByKey          = 0x0900;
        public const UInt32 AlignByColon        = 0x1000;
        public const UInt32 AlignByComma        = 0x2000;
    };
}