﻿using System;

namespace Hurricane.Shared.Components.Logon
{
    public struct GameVersionStruct
    {
        public UInt16 Build;
        public Byte Expansion;
        public Byte Major;
        public Byte Minor;

        public override String ToString()
        {
            return String.Format("{0}.{1}.{2} {3}", this.Expansion, this.Major, this.Minor, this.Build);
        }
    }
}