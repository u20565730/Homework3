using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PagedList;
namespace u20565730_HWK03.Models
{
    public class CombinedBorrows
    {

        public IPagedList<author> Author { get; set; }
        public IPagedList<borrow> Borrow { get; set; }
        public IPagedList<type> Type { get; set; }
    }
}