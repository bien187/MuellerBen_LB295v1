using System.ComponentModel.DataAnnotations;

namespace LA_295_0108_CRUD_L.Model
{
    public class Genre
    {
        [Key]
        public int GenreId { get; set; }
        public string Name { get; set; }
    }
}
