using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace curso.api.Model.Outputs
{
    public class ValidFieldsOutput
    {
        public IEnumerable<string> Errors { get; set; }

        public ValidFieldsOutput(IEnumerable<string> errors)
        {
            Errors = errors;
        }
    }
}
