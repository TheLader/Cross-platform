using System.ComponentModel.DataAnnotations;

namespace Lab5.Models
{
    public class BoardGameViewModel
    {
        [Required]
        [Range(1, 250, ErrorMessage = "Координати повинні бути в діапазоні від 1 до 250.")]
        [Display(Name = "Координата M")]
        public int M { get; set; }

        [Required]
        [Range(1, 250, ErrorMessage = "Координати повинні бути в діапазоні від 1 до 250.")]
        [Display(Name = "Координата N")]
        public int N { get; set; }

        public int? Result { get; set; }
    }
}
