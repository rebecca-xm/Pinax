using Microsoft.AspNetCore.Mvc;
using Pinax.Application.Progetti;

namespace Pinax.API.Controllers
{
	[ApiController] //abilita i comportamenti da API: validazione automatica, binding dei parametri
	[Route("api/progetti")] //indirizzo base del controller
	public class ProgettiController : ControllerBase //ControllerBase: controller senza viste, solo API
	{
		private readonly IProgettoService _progettoService; //usa il servizio di Application, non il repository né il DbContext

		public ProgettiController(IProgettoService progettoService)
		{
			_progettoService = progettoService;
		}

		[HttpGet] //risponde a GET api/progetti
		public async Task<ActionResult<IReadOnlyList<ProgettoDto>>> GetAll(
			[FromQuery] bool includiArchiviati = false, //letto dall'URL: api/progetti?includiArchiviati=true
			CancellationToken cancellationToken = default) //lo passa ASP.NET Core, si annulla se il client chiude la richiesta
		{
			var progetti = await _progettoService.GetAllAsync(includiArchiviati, cancellationToken);

			return Ok(progetti); //200 OK con la lista in JSON
		}
	}
}