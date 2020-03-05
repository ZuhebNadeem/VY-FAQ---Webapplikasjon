using System.ComponentModel.DataAnnotations;

namespace SPA_Angular_Core.Models
{
    public class DomeneFAQ
{
    public int id { get; set; }
    public string kategori { get; set; }
    public string spmTekst { get; set; }
    public string svarTekst { get; set; }
    public int likerKlikk { get; set; }

    }

}
