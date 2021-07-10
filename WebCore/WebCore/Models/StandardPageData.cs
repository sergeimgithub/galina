using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebCore.Models
{
    public class StandardPageData
    {
        public StandardPageData(string pageTitle)
        {
            this.PageTitle = pageTitle;
        }

        public string PageTitle;
        public List<string> Lines = new List<string>();
    }
}
