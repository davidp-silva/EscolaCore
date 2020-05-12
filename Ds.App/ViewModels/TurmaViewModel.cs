﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Ds.App.ViewModels
{
    public class TurmaViewModel
    {
        [Key]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [StringLength(100, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres", MinimumLength = 2)]
        public string NomeTurma { get; set; }

        public Guid SerieId { get; set; }
        //public IEnumerable<TurmaDisciplina> TurmaDisciplinas { get; set; }

        //public IEnumerable<TurmaAluno> TurmaAlunos { get; set; }

        public SerieViewModel Serie { get; set; }
    }
}
