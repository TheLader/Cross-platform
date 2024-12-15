using System.ComponentModel.DataAnnotations;

namespace Lab5.Models
{
    public class GoGameViewModel
    {
        [Required]
        [Range(1, 19, ErrorMessage = "Розмір дошки повинен бути між 1 і 19.")]
        [Display(Name = "Розмір дошки (N)")]
        public int BoardSize { get; set; }

        [Required]
        [Display(Name = "Лінії дошки (по одній на кожен рядок)")]
        public string BoardLinesInput { get; set; }


        public List<string> BoardLines
        {
            get
            {
                if (string.IsNullOrWhiteSpace(BoardLinesInput))
                {
                    return new List<string>();
                }

                try
                {
                    return BoardLinesInput.Split("\r\n").ToList();
                }
                catch
                {
                    return new List<string>();
                }
            }
        }

        public int? GroupsInAtari { get; set; }
    }
}
