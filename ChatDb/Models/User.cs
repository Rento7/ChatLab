using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatDb.Models
{
    public class User
    {
        public Guid Id { get; set; }
        public string Login { get; set; } = null!;

        //TODO add encryption + salting
        public string Password { get; set; } = null!;
    }
}
