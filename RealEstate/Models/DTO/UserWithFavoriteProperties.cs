namespace RealEstate.Models.DTO
{
    public class UserWithFavoriteProperties
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public List<int> FavoriteProperties { get; set; }
    }

}
