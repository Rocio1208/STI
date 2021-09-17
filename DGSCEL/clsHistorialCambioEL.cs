using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DGSCEL
{
    public class clsHistorialCambioEL
    {
        public string Id { get; set; }
        public clsConcursoEL Concurso  { get; set; }
        public string IdFuncionario { get; set; }
        public DateTime Fecha { get; set; }
        public DateTime Hora { get; set; }

    }
}
