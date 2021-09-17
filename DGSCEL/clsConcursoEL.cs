using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DGSCEL
{
    public class clsConcursoEL
    {
        public string Codigo { get; set; }
        public string IdFuncionario { get; set; }
        public string Titulo { get; set; }
        public string Descripcion { get; set; }
        public string TipoProceso { get; set; }
        public string CorreoPrincipal { get; set; }
        public string CorreoSecundario { get; set; }
        public string TelefonoPrincipal { get; set; }
        public string TelefonoSecundario { get; set; }
        public DateTime Fecha { get; set; }
        public DateTime Hora { get; set; }
        public string Estado { get; set; }
    }
}
