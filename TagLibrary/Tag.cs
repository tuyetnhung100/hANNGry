/*
 * Programmer(s):      Gong-Hao
 * Date:               10/13/2019
 * What the code does: Data model of Tag.
 */

namespace TagLibrary
{
    public class Tag
    {
        public int TagId { get; set; }
        public TagType Type { get; set; }
        public string Name { get; set; }
    }
}
