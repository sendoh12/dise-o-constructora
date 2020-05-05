using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Modelos
{
    public class LoginViewModel
    {
        [Display(Name = "Usuario:")]
        public string USUARIOS_USUARIO { get; set; }

        [Display(Name = "clave de accesso:")]
        [DataType(DataType.Password)]
        public string USUARIOS_CONTRASEÑA { get; set; }

        [Display(Name = "Recordarme")]
        public bool Remember { get; set; }

    }
}
