using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WDCM_Api.Auth;
using WDCM_Api.Entities;
using WDCM_Api.Entities.Response;
using WDCM_Api.Model;
using WDCM_Api.Repository.Interfaces;

namespace WDCM_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserRepository _repository;
        private readonly IJwtFactory _jwtFactory;
        private readonly JwtIssuerOptions _jwtOptions;

        public AuthController(IUserRepository repository, IJwtFactory jwtFactory, IOptions<JwtIssuerOptions> jwtOptions)
        {
            _repository = repository;
            _jwtFactory = jwtFactory;
            _jwtOptions = jwtOptions.Value;
        }

        [HttpPost("login")]
        public async Task<ActionResult> Get(LoginModel model)
        {
            User user = null;
            if (model.Phone != null)
            {
                user =  _repository.AuthenticateByPhone(model.Phone, model.Password);
            }

            if (user!= null)
            {
                TimeSpan expiredDate = DateTime.Now - user.CreateDate.Value;
                int date = expiredDate.Days;
                if (date >= 7)
                {
                    return BadRequest(new ApiResponse(false, null, "Hết thời hạn sử dụng miễn phí. Vui lòng liên hệ admin để được sử dụng.", 1));
                }
            }
            

            if (user == null)
            {
                //Tài khoản hoặc mật khẩu không đúng!
                return BadRequest(new ApiResponse(false,null,"Số điện thoại hoặc mật khẩu không đúng.", statusCode: 0));
            }
            else
            {
                var identity = _jwtFactory.GenerateClaimsIdentity(user.Id.ToString(), user.Id.ToString());
                return Ok(new ApiResponse()
                {
                    IsSuccess = true,
                    Content = new AuthenticateReponse()
                    {
                        AccessToken = await _jwtFactory.GenerateEncodedToken(user.Id.ToString(), identity),
                        ExpireIn = (int)_jwtOptions.ValidFor.TotalSeconds,
                        Id = user.Id,
                        Phone = user.Phone,
                        Password = user.Password,
                        Role = user.Role,
                        IsPaid = model.IsPaid,
                        CreateDate = DateTime.Now
                    },
                    Message = null
                });
            }
        }

        [HttpPost("register")]
        public async Task<ActionResult> Register(LoginModel model)
        {
            bool IsExistPhone = await _repository.IsExistPhone(model.Phone);
            if (IsExistPhone)
            {
                return BadRequest(new ApiResponse(false, null, "Số điện thoại đã tồn tại.", 0));// sdt da ton tai
            }

            bool IsExistSerial = await _repository.IsExistSerial(model.SerialDevice);
            if (IsExistSerial)
            {
                return BadRequest(new ApiResponse(false, null, "Bạn đã có tài khoản vui lòng liên hệ admin.", 0));
            }

            User user = new User() {
                Id = Guid.NewGuid(),
                Phone = model.Phone,
                Password= model.Password,
                SerialDevice = model.SerialDevice,
                IsPaid = model.IsPaid,
                CreateDate = DateTime.Now
            };

            try
            {
                await _repository.Create(user);
                var identity = _jwtFactory.GenerateClaimsIdentity(user.Id.ToString(), user.Id.ToString());
                return Ok(new ApiResponse()
                {
                    IsSuccess = true,
                    Content = new AuthenticateReponse()
                    {
                        AccessToken = await _jwtFactory.GenerateEncodedToken(user.Id.ToString(), identity),
                        ExpireIn = (int)_jwtOptions.ValidFor.TotalSeconds,
                        Id = user.Id,
                        Phone = user.Phone,
                        Password = user.Password,
                        Role = user.Role,
                        IsPaid = user.IsPaid,
                        CreateDate = user.CreateDate
                    },
                    Message = null
                });
            }
            catch(Exception ex)
            {
                return BadRequest(new ApiResponse(false, null, "Loi he thong", -1));// loi he thong
            }
            

        }
    }
}
