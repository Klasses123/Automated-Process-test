using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CostsTest
{
    //класс, описывающий запись в исходной таблице costs
    public class Entity
    {
        [Key]
        public string ID { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public decimal? AdultsMainWeekday { get; set; }
        public decimal? AdultsMainWeekend { get; set; }
        public decimal? ChildrenMainWeekday { get; set; }
        public decimal? ChildrenMainWeekend { get; set; }
        public decimal? AdultsAdditionalWeekday { get; set; }
        public decimal? AdultsAdditionalWeekend { get; set; }
        public decimal? ChildrenAdditionalWeekday { get; set; }
        public decimal? ChildrenAdditionalWeekend { get; set; }
    }
}
