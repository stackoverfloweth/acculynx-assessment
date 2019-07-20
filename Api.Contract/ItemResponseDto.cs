using System.Collections.Generic;

namespace Api.Contract {
    public class ItemResponseDto<T> {
        public IEnumerable<T> Items { get; set; }
    }
}
