using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace senai.inlock.webApi.Domains
{
    public class JogoDomain
    {
        public int idJogo { get; set; }

        [Required(ErrorMessage ="Informe o nome do jogo")]
        public string nomeJogo { get; set; }
        public string descricao { get; set; }
        public Nullable<DateTime> dataLancamento { get; set; }
        public float valor { get; set; }
        public int idEstudio { get; set; }

        public EstudioDomain estudio { get; set; }

    }
}
