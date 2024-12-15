using Lab5.Models;
using LabsLibrary;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Lab5.Controllers
{
    public class LabsController : Controller
    {
        [Authorize]
        public IActionResult MaxProfit()
        {
            return View(new MaxProfitViewModel());
        }

        [Authorize]
        [HttpPost]
        public IActionResult MaxProfit(MaxProfitViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Перевірка на кількість цін
                int enteredPricesCount = model.Prices.Length;
                if (enteredPricesCount != model.PricesCount)
                {
                    ModelState.AddModelError(string.Empty, $"Кількість цін повинна бути {model.PricesCount}, але було введено {enteredPricesCount}.");
                }
                else
                {
                    try
                    {
                        // Обчислення максимального прибутку
                        int maxProfit = ProfitUtils.GetMaxProfit(model.Prices);
                        model.Result = maxProfit;
                    }
                    catch (ArithmeticException)
                    {
                        ModelState.AddModelError(string.Empty, "Некоректний масив цін. Перевірте введені дані.");
                    }
                }
            }

            return View(model);
        }


        [Authorize]
        public IActionResult GoGame()
        {
            return View(new GoGameViewModel());
        }

        [Authorize]
        [HttpPost]
        public IActionResult GoGame(GoGameViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    // Парсинг і валідація введених ліній
                    var board = model.BoardLines
                        .Select(line => LabsLibrary.GoGame.ParseAndValidateBoardLine(line, model.BoardSize))
                        .ToArray();

                    // Підрахунок груп в атарі
                    int groupsInAtari = LabsLibrary.GoGame.CountGroupsInAtari(board, model.BoardSize);
                    model.GroupsInAtari = groupsInAtari;
                }
                catch (ArgumentException ex)
                {
                    ModelState.AddModelError(string.Empty, ex.Message);
                }
            }

            return View(model);
        }

        [Authorize]
        public IActionResult BoardGame()
        {
            return View(new BoardGameViewModel());
        }

        [Authorize]
        [HttpPost]
        public IActionResult BoardGame(BoardGameViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    model.Result = LabsLibrary.BoardGame.GetResult(model.M, model.N);
                }
                catch (ArgumentOutOfRangeException ex)
                {
                    ModelState.AddModelError(string.Empty, ex.Message);
                }
            }

            return View(model);
        }
    }
}
