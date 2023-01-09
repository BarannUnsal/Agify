using System.ComponentModel.DataAnnotations;

namespace Agify.Domain.Enums
{
    public enum StageOfLife
    {
        [Display(Name = "Early Childhood")]
        Early_Child_Hood = 1,

        [Display(Name = "Childhood")]
        Childhood = 2,

        [Display(Name = "Adolescence")]
        Adolescence = 3,

        [Display(Name = "Youth")]
        Youth = 4,

        [Display(Name = "Maturity")]
        Maturity = 5,

        [Display(Name = "Old Age")]
        Old_age = 6
    }
}
