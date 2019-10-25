/*
 * Programmer(s):      Gong-Hao
 * Date:               10/13/2019
 * What the code does: DataModel for blocks in template.
 */

using TagLibrary;

namespace Story2
{
    public class MessageBlock
    {
        public Tag Tag { get; set; }
        public bool IsTag { get; set; }
        public string Message { get; set; }
        public string Input { get; set; }
    }
}
