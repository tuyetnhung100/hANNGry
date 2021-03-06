/*
 * Programmer(s):      Gong-Hao
 * Date:               10/13/2019
 * What the code does: Data model of Template.
 */

using System;

namespace TemplateLibrary
{
    public class Template
    {
        public int TemplateId { get; set; }
        public string Subject { get; set; }
        public string Message { get; set; }
        public int CreatedAccountId { get; set; }
        public DateTime CreatedDate { get; set; }
        public virtual string CreatedEmployeeName { get; set; }
        public override string ToString()
        {
            return Subject;
        }
    }
}
