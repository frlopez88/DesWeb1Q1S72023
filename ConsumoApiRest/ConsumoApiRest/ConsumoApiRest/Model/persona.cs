using System;
using System.Collections.Generic;
using System.Text;

namespace ConsumoApiRest.Model
{
    public class persona
    {

        public int id_persona { get; set; }
        public string nombre_persona { get;set; }
        public string apellido_persona { get;set; }
        public DateTime fecha_nacimiento { get;set; }

        public string toJson() { 
        
            return "{ \"nombre_persona\": \""+ nombre_persona + "\" , \"apellido_persona\": \""+ apellido_persona+ "\", \"fecha_nacimiento\": \" "+fecha_nacimiento+"\"  }";
                
        }

    }
}
