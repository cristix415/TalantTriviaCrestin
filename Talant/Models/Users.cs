using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Talant
{
    public class Users
    {
        public virtual int Id { get; set; }
        public virtual string Nume { get; set; }
        public virtual string Prenume { get; set; }
        public virtual string Adresa { get; set; }
        public virtual string Localitate { get; set; }
        public virtual string Judet { get; set; }
        public virtual string Tara { get; set; }
        [Display(Name = "User Name")]
        public virtual string Username { get; set; }
        public virtual string Pwd { get; set; }
    }
}