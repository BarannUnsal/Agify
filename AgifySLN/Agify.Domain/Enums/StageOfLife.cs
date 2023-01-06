using System.ComponentModel.DataAnnotations;

namespace Agify.Domain.Enums
{
    public enum StageOfLife
    {
        [Display(Name = "Early Childhood")]
        Early_Child_Hood,

        [Display(Name = "Childhood")]
        Childhood,

        [Display(Name = "Adolescence")]
        Adolescence,

        [Display(Name = "Youth")]
        Youth,

        [Display(Name = "Maturity")]
        Maturity,

        [Display(Name = "Old Age")]
        Old_age
    }
}
