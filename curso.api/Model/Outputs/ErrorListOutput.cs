using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace curso.api.Model.Outputs
{
    public class ErrorListOutput
    {
        public IEnumerable<string> Errors { get; set; }

        public ErrorListOutput(IEnumerable<string> errors)
        {
            Errors = errors;
        }
    }
}
