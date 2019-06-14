using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace ngxsis.ViewModel
{
    public class UserRoleViewModel
    {
        public long Id { get; set; }

        public bool IsDeleted { get; set; }

        public long AddrbookId { get; set; }

        public long RoleId { get; set; }

        public string RoleName { get; set; }
    }
}
