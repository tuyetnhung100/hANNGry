/*
 * Programmer(s):      Gong-Hao
 * Date:               10/13/2019
 * What the code does: Data access layer of Template.
 */

using System;
using System.Collections.Generic;

namespace TemplateLibrary
{
    public class TemplateDB
    {
        /// <summary>
        /// Load all templates.
        /// </summary>
        /// <param name="templates">result list of templates</param>
        /// <returns></returns>
        public static Boolean Load(ref List<Template> templates)
        {
            // todo: load real templates from DB
            LoadFakeTemplates(ref templates);
            return true;
        }

        /// <summary>
        /// Load fake templates. Only use for testing.
        /// </summary>
        /// <param name="templates">result list of templates</param>
        private static void LoadFakeTemplates(ref List<Template> templates)
        {
            templates.Add(new Template
            {
                TemplateId = 1,
                Name = "Basic Template",
                Message = @"Dear {$student.name},
There will be {$food} at the {$campus} campus in {$room} on {$date} from {$startTime} to {$endTime}.

Thanks,

{$staff.name}",
                CreatedAccountId = 1,
                CreatedDate = DateTime.Now
            });
            templates.Add(new Template
            {
                TemplateId = 2,
                Name = "Cool Template",
                Message = @"Dear {$student.name},
{$campus} campus will have {$eventName} at {$room} on {$date} from {$startTime} to {$endTime}.
We would like to invite you to join us.

Thanks,

{$staff.name}",
                CreatedAccountId = 1,
                CreatedDate = DateTime.Now
            });
            templates.Add(new Template
            {
                TemplateId = 3,
                Name = "Super Template",
                Message = "Super Template",
                CreatedAccountId = 1,
                CreatedDate = DateTime.Now
            });
            templates.Add(new Template
            {
                TemplateId = 4,
                Name = "Gorgeous Template",
                Message = "Gorgeous Template",
                CreatedAccountId = 2,
                CreatedDate = DateTime.Now
            });
            templates.Add(new Template
            {
                TemplateId = 5,
                Name = "Superior Template",
                Message = "Superior Template",
                CreatedAccountId = 2,
                CreatedDate = DateTime.Now
            });
        }
    }
}
