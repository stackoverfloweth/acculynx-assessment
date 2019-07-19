using System;
using System.Collections.Generic;
using System.Text;

namespace Api.Contract {
    public class QuestionResponseDto {
        public IEnumerable<QuestionDto> Items { get; set; }
    }
}