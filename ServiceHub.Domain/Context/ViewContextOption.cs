using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceHub.Domain.Context
{
    public class ViewContextOption
    {
        public ViewContextOption(bool disableCache)
        {
            this.DisableCache = disableCache;
        }

        public bool DisableCache { get; set; }
    }
}