/*
 * Programmer(s):      Gong-Hao
 * Date:               11/14/2019
 * What the code does: Location of Account.
 */

using System;

namespace AccountLibrary
{
    // 0000 =  0 = None
    // 0001 =  1 = Sylvania
    // 0010 =  2 = RockCreek,
    // 0011 =  3 = Sylvania,  RockCreek
    // 0100 =  4 = Southeast
    // 0101 =  5 = Sylvania,  Southeast
    // 0110 =  6 = RockCreek, Southeast
    // 0111 =  7 = Sylvania,  RockCreek,  Southeast
    // 1000 =  8 = Cascade
    // 1001 =  9 = Sylvania,  Cascade
    // 1010 = 10 = RockCreek, Cascade
    // 1011 = 11 = Sylvania,  RockCreek,  Cascade
    // 1100 = 12 = Southeast, Cascade
    // 1101 = 13 = Sylvania,  Southeast,  Cascade
    // 1110 = 14 = RockCreek, Southeast,  Cascade
    // 1111 = 15 = Sylvania,  RockCreek,  Southeast,  Cascade
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
