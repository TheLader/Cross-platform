using System.ComponentModel.DataAnnotations;

namespace Lab5.Models
{
    public class MaxProfitViewModel
    {
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Кількість цін повинна бути більше нуля.")]
        [Display(Name = "Кількість цін")]
        public int PricesCount { get; set; }

        [Required]
        [Display(Name = "Ціни (розділені пробілами)")]
        public string PricesInput { get; set; }

        public int[] Prices
        {
            get
            {
                if (string.IsNullOrWhiteSpace(PricesInput))
                {
                    return new int[0];
                }

                try
                {
                    return PricesInput.Split(' ').Select(int.Parse).ToArray();
                }
                catch
                {
                    return new int[0];
                }
            }
        }

        public int? Result { get; set; }
    }
}
