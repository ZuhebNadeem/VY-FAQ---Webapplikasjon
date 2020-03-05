using System.ComponentModel.DataAnnotations;


namespace SPA_Angular_Core.Models
{
    public class NyeSpm
    {
        [Required]
        [RegularExpression("^[a-zæøåA-ZÆØÅ]{2,30}$")]
        public string navn { get; set; }

        [Required]
        [RegularExpression("^[0-9]{8}$")]
        public string tlf { get; set; }

        [Required]
        [RegularExpression("^[^@]+@[A-Za-z]+[.]+[A-Za-z]{2,100}$")]
        public string epost { get; set; }

        [Required]
        [RegularExpression("^[a-zæøåA-ZÆØÅ. 0-9 \\-]{2,30}$")]
        public string spmTekst { get; set; }

        public string kategoriForNyeSpm { get; set; }

    }
}
