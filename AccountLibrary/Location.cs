/*
 * Programmer(s):      Gong-Hao
 * Date:               11/14/2019
 * What the code does: Location of Account.
 */

using System;

namespace AccountLibrary
{
    [Flags]
    public enum Location
    {
        None = 0,
        Sylvania = 1,
        RockCreek = 2,
        Southeast = 4,
        Cascade = 8
    }
}
