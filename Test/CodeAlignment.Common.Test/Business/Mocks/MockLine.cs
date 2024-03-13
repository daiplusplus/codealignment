using System;
using System.Linq;
using System.Collections.Generic;
using CMcG.CodeAlignment.Business;

namespace CMcG.CodeAlignment.Test.Business.Mocks
{
    public class MockLine : ILine
    {
        public Int32 Position { get; set; }
        public String Text     { get; set; }
    }
}
