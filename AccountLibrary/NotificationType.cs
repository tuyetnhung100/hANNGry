/*
 * Programmer(s):      Gong-Hao
 * Date:               11/14/2019
 * What the code does: NotificationType of Account.
 */

using System;

namespace AccountLibrary
{
    [Flags]
    public enum NotificationType
    {
        None = 0,
        Email = 1,
        SMS = 2
    }
}
