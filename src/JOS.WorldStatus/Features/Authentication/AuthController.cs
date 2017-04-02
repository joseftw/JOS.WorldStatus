using System;
using System.Linq;
using System.Security.Claims;
using JOS.WorldStatus.Features.Shared;
using Microsoft.AspNetCore.Mvc;

namespace JOS.WorldStatus.Features.Authentication
{
	[Route("[controller]")]
	public class AuthController : ControllerBase
	{
		private readonly IUserStore _userStore;
		private readonly JwtTokenGenerator _jwtTokenGenerator;

		public AuthController(
			JwtTokenGenerator jwtTokenGenerator,
			IUserStore userStore)
		{
			_jwtTokenGenerator = jwtTokenGenerator ?? throw new ArgumentNullException(nameof(jwtTokenGenerator));
			_userStore = userStore ?? throw new ArgumentNullException(nameof(userStore));
		}

		[Route("login")]
		[HttpPost]
		public IActionResult Login([FromBody]LoginInputModel loginInput)
		{
			if (_userStore.IsValid(loginInput.Username, loginInput.Password))
			{
				return new ObjectResult(new
				{
					Success = true,
					AccessToken = _jwtTokenGenerator.CreateToken(loginInput.Username, Enumerable.Empty<Claim>())
				});
			}

			return new ObjectResult(
				new
				{
					Success = false
				}
			);
		}
	}
}