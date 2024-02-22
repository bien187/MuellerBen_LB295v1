namespace LA_295_0108_CRUD_L.Model;

/// <summary>
/// DTO für Mitarbeiter.
/// <see cref="Film"/>
/// </summary>
public class FilmDTO
{


        public int FilmId { get; set; }
        public string Titel { get; set; }
        public string Regisseur { get; set; }
        public int Erscheinungsjahr { get; set; }
  
}
