using System;

namespace TemplateLibrary
{
    public class Template
    {
        public int TemplateId { get; set; }
        public string Name { get; set; }
        public string Message { get; set; }
        public int CreatedAccountId { get; set; }
        public DateTime CreatedDate { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }
}
