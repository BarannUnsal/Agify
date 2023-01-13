using Agify.Domain.Enums;

namespace Agify.Domain.Entities
{
    public class User
    {
        public int Id{ get; set; }
        public string? Age { get; set; }
        public string? Name { get; set; }
        public string? Count { get; set; }

        public string? StageOfLife
        {
            get
            {
                if (int.Parse(Age) >= 0 && int.Parse(Age) < 7)
                {
                    return ((StageOfLife)1).ToString();
                }
                else if (int.Parse(Age) >= 7 && int.Parse(Age) < 14)
                {
                    return ((StageOfLife)2).ToString();
                }
                else if (int.Parse(Age) >= 14 && int.Parse(Age) < 28)
                {
                    return ((StageOfLife)3).ToString();
                }
                else if (int.Parse(Age) >= 28 && int.Parse(Age) < 50)
                {
                    return ((StageOfLife)4).ToString();
                }
                else if (int.Parse(Age) >= 50 && int.Parse(Age) < 70)
                {
                    return ((StageOfLife)5).ToString();
                }
                else if (int.Parse(Age) >= 70)
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
