using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using JOS.WorldStatus.Features.Shared;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace JOS.WorldStatus.Features.Authentication {
	public class JwtTokenGenerator {
		private readonly JwtSettings _jwtSettings;

		public JwtTokenGenerator(IOptions<JwtSettings> jwtOptions) 
		{
			this._jwtSettings = jwtOptions.Value;
		}

		public string CreateToken(string username, IEnumerable<Claim> additionalClaims) 
		{
			var now = DateTime.UtcNow;
			var claims = new List<Claim>
			{
				new Claim(JwtRegisteredClaimNames.Sub, username),
				new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
				new Claim(JwtRegisteredClaimNames.Iat, now.ToUnixTimestamp().ToString(), ClaimValueTypes.Integer64),
			};

			var signingKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_jwtSettings.SecretKey));
			var jwt = new JwtSecurityToken(
				_jwtSettings.Issuer,
				_jwtSettings.Audience,
				claims.Concat(additionalClaims),
				now,
				now.AddYears(1),
				new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256)
			);

			return new JwtSecurityTokenHandler().WriteToken(jwt);
		}
	}
}