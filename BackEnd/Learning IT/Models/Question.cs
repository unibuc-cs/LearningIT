﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Learning_IT.Models
{
    public class Question
    {

        [Key]
        public int Id { get; set; }
        [Required]
        [Column(TypeName = "nvarchar(256)")]
        public string Description { get; set; }
        public virtual IList<QuestionExam> QuestionExams { get; set; }

        public virtual IList<AnswerQuestion> AnswerQuestions { get; set; }
    }
}
