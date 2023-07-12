using System.Threading.Tasks;
using BrainstormSessions.Core.Interfaces;
using BrainstormSessions.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using Serilog.Core;

namespace BrainstormSessions.Controllers
{
    public class SessionController : Controller
    {
        private readonly IBrainstormSessionRepository _sessionRepository;
        private readonly Logger _log;

        public SessionController(IBrainstormSessionRepository sessionRepository)
        {
            _sessionRepository = sessionRepository;
            _log = new LoggerConfiguration().CreateLogger();
        }

        public async Task<IActionResult> Index(int? id)
        {
            _log.Information("Fetching session by ID: {SessionId}", id);

            if (!id.HasValue)
            {
                _log.Debug("No session ID provided, redirecting to Home/Index");
                return RedirectToAction(actionName: nameof(Index),
                    controllerName: "Home");
            }

            var session = await _sessionRepository.GetByIdAsync(id.Value);
            if (session == null)
            {
                _log.Warning("Session not found for ID: {SessionId}", id);
                return Content("Session not found.");
            }

            var viewModel = new StormSessionViewModel()
            {
                DateCreated = session.DateCreated,
                Name = session.Name,
                Id = session.Id
            };
            
            _log.Debug("Session found: {@Session}", session);
            return View(viewModel);
        }
    }
}
