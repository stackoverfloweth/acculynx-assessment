using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.Contract {
    public class AttemptedQuestionDto {
        public QuestionDto QuestionDto { get; set; }
        public AttemptDto AttemptDto { get; set; }
    }
}
