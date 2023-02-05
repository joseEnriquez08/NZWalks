namespace NzWalksAPI.Models.Domain
{
    public class Walk
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        public double Length { get; set; }
        public Guid RegionId { get; set; }
        public Guid WalkDifficultyId { get; set; }


        //Navigation Properties:
        //one walk to one region
        public Region Region { get; set; }

        //one walk to one region
        public WalkDifficulty WalkDifficulty { get; set; }
    }
}
