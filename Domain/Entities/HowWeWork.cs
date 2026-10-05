using Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class HowWeWork : BaseEntity
    {
        public string Eyebrow { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public ICollection<HowWeWorkCard> Cards { get; set; } = new List<HowWeWorkCard>();
    }
}
