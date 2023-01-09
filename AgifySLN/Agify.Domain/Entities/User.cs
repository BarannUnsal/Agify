using Agify.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace Agify.Domain.Entities
{
    public class User
    {
        public int Id{ get; set; }
        public int? Age { get; set; }
        public string? Name { get; set; }
        public int? Count { get; set; }

        public string? StageOfLife
        {
            get
            {
                if (Age >= 0 && Age < 7)
                {
                    return ((StageOfLife)1).ToString();
                }
                else if (Age >= 7 && Age < 14)
                {
                    return ((StageOfLife)2).ToString();
                }
                else if (Age >= 14 && Age < 28)
                {
                    return ((StageOfLife)3).ToString();
                }
                else if (Age >= 28 && Age < 50)
                {
                    return ((StageOfLife)4).ToString();
                }
                else if (Age >= 50 && Age < 70)
                {
                    return ((StageOfLife)5).ToString();
                }
                else if (Age >= 70)
                {
                    return ((StageOfLife)6).ToString();
                }
                else
                {
                    return "";
                }
            }
            set { }
        }
    }
}
