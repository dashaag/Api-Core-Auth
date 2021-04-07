using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Auth2.Entities
{
    public class UserAdditionalInfo
    {
        [Key]
        public string Id { get; set; }
        public string FullName { get; set; }
        public string Image { get; set; }
        public int Age { get; set; }

        /*Navigation Property*/

        public virtual User User { get; set; }
    }
}
