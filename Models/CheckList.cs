using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;
using System.Linq;
using System.Web;

namespace CheckListManager.Models
{
    public class CheckList
    {
        public int CheckListId { get; set; }
        public string Name { get; set; }
        public int UserId { get; set; }
    }

    public class CheckListItem
    {
        [Key, Column (Order = 0)]
        public int CheckListId { get; set; }
        [Key, Column (Order = 1)]
        public int Order { get; set; }

        public string Name { get; set; }
        public bool Active { get; set; }
    }

    public class User
    {
        public int UserId { get; set; }
        public string Name { get; set; }
    }
}