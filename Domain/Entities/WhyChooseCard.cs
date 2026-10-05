using Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class WhyChooseCard : BaseEntity
    {
        public int WhyChooseId { get; set; }
        public WhyChoose WhyChoose { get; set; } = null!;
        public string Icon { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public string Text { get; set; } = string.Empty;
        public int SortOrder { get; set; }
    }
}
