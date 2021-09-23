using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InstantEatService.Logging
{
    public class Logger
    {

        public void Logging(string methodName)
        {
            var method = methodName;
            _logger.LogTrace($"{method}is worked out");
        }
    }
}
