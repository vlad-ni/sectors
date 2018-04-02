using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using BusinessService.BLL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using TestTask.Models;
using TestTask.Mappers;


namespace TestTask.Controllers
{
    public class HomeController : Controller
    {
        private const string SessionUserId = "SessionUserId";

        private readonly IUserAnswersBL _userAnswersBL;
        private readonly ISectorsBL _sectorsBL;

        public HomeController(IUserAnswersBL userAnswersBL, ISectorsBL sectorsBL)
        {
            _userAnswersBL = userAnswersBL;
            _sectorsBL = sectorsBL;
        }

        [HttpGet]
        public IActionResult UserAnswers()
        {
            var allSectors = UserAnswersMapper.MapSectorsToFormattedSelectList(_sectorsBL.GetAllSectors());

            try
            {
                if (HttpContext.Session.Keys.Contains(SessionUserId))
                {
                    if (Guid.TryParse(HttpContext.Session.GetString(SessionUserId), out Guid userId))
                    {
                        var userAnswers = _userAnswersBL.GetUserAnswer(userId);
                        return View(UserAnswersMapper.MapToViewModel(userAnswers, allSectors));
                    }
                }
                else
                {
                    TempData["Error"] = TempData["Info"] = null;
                }
            }
            catch (Exception e)
            {
                TempData["Error"] = e.Message;
                HttpContext.Session.Remove(SessionUserId);
            }

            return View(new UserAnswersViewModel { AllSectors = allSectors });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult UserAnswers(UserAnswersViewModel model)
        {
            TempData["Error"] = TempData["Info"] = null;

            if (!ModelState.IsValid)
            {
                model.AllSectors = UserAnswersMapper.MapSectorsToFormattedSelectList(_sectorsBL.GetAllSectors());
                return View(model);
            }

            try
            {
                var entityModel = UserAnswersMapper.MapToEntity(model);
                if (HttpContext.Session.Keys.Contains(SessionUserId) && Guid.TryParse(HttpContext.Session.GetString(SessionUserId), out Guid userId))
                {
                    userId = _userAnswersBL.UpdateUserAnswer(userId, entityModel);
                }
                else
                {
                    userId = _userAnswersBL.AddUserAnswer(entityModel);
                }

                HttpContext.Session.SetString(SessionUserId, userId.ToString());

                TempData["Info"] = Resources.Messages.DataSaved;
            }
            catch (Exception e)
            {
                TempData["Error"] = e.Message;
            }

            return RedirectToAction(nameof(UserAnswers));
        }
    }
}
