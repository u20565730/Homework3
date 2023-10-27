using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PagedList;
using PagedList.Mvc;

namespace u20565730_HWK03.Models
{
    public class CombinedBooks
    {
        public IPagedList<book> Book { get; set; }
        public IPagedList<student> Student { get; set; }
        public IEnumerable<borrow> Borrow { get; set; }
    }
}