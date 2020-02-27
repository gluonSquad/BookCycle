namespace BookCycle.Core.Models
{
    public class Liked : BaseModel
    {

        public virtual Book Book{ get; set; }
        public virtual User LikedUser { get; set; }
    }
}
