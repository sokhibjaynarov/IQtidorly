using AutoMapper;
using IQtidorly.Api.Data.IRepositories.Base;
using IQtidorly.Api.Enums;
using IQtidorly.Api.Helpers;
using IQtidorly.Api.Interfaces;
using IQtidorly.Api.Models.Users;
using IQtidorly.Api.Response;
using IQtidorly.Api.Services.Base;
using IQtidorly.Api.ViewModels.Emails;
using IQtidorly.Api.ViewModels.Users;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace IQtidorly.Api.Services
{
    public class IdentityService : BaseService, IIdentityService
    {
        private const string EMAIL_SUBJECT_VERIFY_EMAIL = "Verify your email address";
        private const string EMAIL_SUBJECT_USER_REMOVAL = "Verify account removal";
        private const string EMAIL_SUBJECT_PASSWORD_RECOVERY = "Recovery you password";

        private readonly IEmailService _emailService;
        private readonly UserManager<User> _userManager;
        private readonly IMemoryCache _cache;

        public IdentityService(
            IHttpContextAccessor httpContextAccessor,
            IMapper mapper,
            IConfiguration configuration,
            IUnitOfWorkRepository unitOfWorkRepository,
            ILogger<IdentityService> logger,
            IEmailService emailService,
            UserManager<User> userManager,
            IMemoryCache cache) : base(httpContextAccessor, mapper, configuration, unitOfWorkRepository, logger)
        {
            _emailService = emailService;
            _userManager = userManager;
            _cache = cache;
        }

        public async Task<bool> CreatePasswordRecoveryEmailAsync(string emailAddress)
        {
            try
            {
                var user = _unitOfWorkRepository.UserRepository.GetAll().FirstOrDefault(l => l.Email.ToLower() == emailAddress.ToLower());
                if (user == null)
                {
                    throw new ErrorCodeException("User not found");
                }

                var verificationCode = CreateEmailVerificationCode(emailAddress, EmailType.PasswordRecovery.ToString());

                var emailBody = GetPasswordRecoveryEmailBody(verificationCode, user);
                var emailView = new EmailViewModel
                {
                    To = user.Email,
                    Subject = EMAIL_SUBJECT_PASSWORD_RECOVERY,
                    Body = emailBody,
                };

                await _emailService.SendEmailAsync(emailView);

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw;
            }
        }

        public async Task<GeneratedTokenViewModel> CreateTokenAsync(CreateTokenViewModel createTokenView)
        {
            try
            {
                var user = await _unitOfWorkRepository.UserRepository.GetAll().FirstOrDefaultAsync(u => u.Email.ToLower() == createTokenView.Email.ToLower());

                var isCorrectPassword = await _userManager.CheckPasswordAsync(user, createTokenView.Password);

                if (!isCorrectPassword)
                {
                    throw new ErrorCodeException("invalid email or password");
                }

                if (!user.EmailConfirmed)
                {
                    throw new ErrorCodeException("email is not verified");
                }

                var claims = new List<Claim>()
                {
                    new Claim("UserId", user.Id.ToString()),
                    new Claim(ClaimTypes.Email, user.Email),
                    new Claim("FirstName", user.FirstName),
                    new Claim("LastName", user.LastName ?? string.Empty),
                    new Claim(ClaimTypes.MobilePhone, user.PhoneNumber ??  string.Empty),
                };

                var issuer = _configuration.GetSection("JWT:Issuer").Value;
                var audience = _configuration.GetSection("JWT:Audience").Value;
                var key = _configuration.GetSection("JWT:Key").Value;
                var expireTime = _configuration.GetSection("JWT:Expire").Value;

                if (string.IsNullOrEmpty(key) || Encoding.UTF8.GetBytes(key).Length < 32)
                {
                    throw new Exception("JWT key must be at least 32 bytes long.");
                }

                var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
                var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
                var expirationData = DateTime.UtcNow.AddMinutes(double.Parse(expireTime));

                var tokenDescriptor = new JwtSecurityToken(issuer, audience, claims,
                                                            expires: expirationData,
                                                            signingCredentials: credentials);

                var token = new GeneratedTokenViewModel()
                {
                    Token = new JwtSecurityTokenHandler().WriteToken(tokenDescriptor),
                    ExpirationData = expirationData
                };

                return token;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw;
            }
        }

        public async Task<Guid> CreateUserAsync(CreateUserViewModel createUserView)
        {
            try
            {
                bool isEmailDuplicated = _unitOfWorkRepository.UserRepository.GetAll().Any(l => l.Email == createUserView.Email);
                if (isEmailDuplicated)
                {
                    throw new ErrorCodeException("email is duplicated");
                }

                var user = _mapper.Map<User>(createUserView);
                user.UserName = user.Email;
                user.CreatedDate = DateTime.UtcNow;
                var createdUserResult = await _userManager.CreateAsync(user);

                if (!createdUserResult.Succeeded)
                {
                    throw new ErrorCodeException(JsonConvert.SerializeObject(createdUserResult.Errors));
                }

                var setPasswordResult = await _userManager.AddPasswordAsync(user, createUserView.Password);

                if (!setPasswordResult.Succeeded)
                {
                    throw new ErrorCodeException(JsonConvert.SerializeObject(setPasswordResult.Errors));
                }

                var verificationCode = CreateEmailVerificationCode(user.Email, EmailType.EmailVerification.ToString());
                var verificationEmailBody = GetEmailVerificationEmailBody(verificationCode, user);
                var emailView = new EmailViewModel
                {
                    To = user.Email,
                    Subject = EMAIL_SUBJECT_VERIFY_EMAIL,
                    Body = verificationEmailBody,
                };


                await _emailService.SendEmailAsync(emailView);

                return Guid.NewGuid();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw;
            }
        }

        public async Task<bool> CreateUserRemovalEmailAsync(string password)
        {
            try
            {
                Guid currentUserId = WebHelper.GetUserId(_httpContextAccessor.HttpContext);

                var currentUser = await _unitOfWorkRepository.UserRepository.GetAsync(currentUserId);
                if (currentUser == null)
                {
                    throw new ErrorCodeException("User not found");
                }

                var isCorrectPassword = await _userManager.CheckPasswordAsync(currentUser, password);
                if (!isCorrectPassword)
                {
                    throw new ErrorCodeException("IncorrectPassword");
                }

                var result = CreateEmailVerificationCode(currentUser.Email, EmailType.UserRemoval.ToString());

                var emailBody = GetUserRemovalEmailBody(result, currentUser);
                var emailView = new EmailViewModel
                {
                    To = currentUser.Email,
                    Subject = EMAIL_SUBJECT_USER_REMOVAL,
                    Body = emailBody,
                };

                await _emailService.SendEmailAsync(emailView);

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw;
            }
        }

        public async Task<bool> RecoverPasswordAsync(RecoverPasswordViewModel recoverPasswordView)
        {
            try
            {
                var key = EmailType.PasswordRecovery.ToString() + "#" + recoverPasswordView.Email;
                if (_cache.TryGetValue(key, out string existValue) is false)
                    throw new ErrorCodeException("Code Already expired or not found");

                if (existValue != recoverPasswordView.VerificationCode)
                {
                    throw new ErrorCodeException("Incorrect verification code");
                }

                var user = await _unitOfWorkRepository.UserRepository.GetAll().FirstOrDefaultAsync(p => p.Email == recoverPasswordView.Email);

                if (user == null)
                {
                    throw new ErrorCodeException("user not found");
                }

                await _userManager.RemovePasswordAsync(user);
                var result = await _userManager.AddPasswordAsync(user, recoverPasswordView.NewPassword);

                _cache.Remove(key);

                if (result.Succeeded)
                {
                    return true;
                }

                throw new ErrorCodeException(JsonConvert.SerializeObject(result.Errors));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw;
            }
        }

        public async Task<bool> UpdatePasswordAsync(UpdatePasswordViewModel updatePasswordView)
        {
            try
            {
                Guid currentUserId = WebHelper.GetUserId(_httpContextAccessor.HttpContext);

                var currentUser = await _unitOfWorkRepository.UserRepository.GetAsync(currentUserId);
                if (currentUser == null)
                {
                    throw new ErrorCodeException("User not found");
                }

                var isCorrectPassword = await _userManager.CheckPasswordAsync(currentUser, updatePasswordView.OldPassword);

                if (!isCorrectPassword)
                {
                    throw new ErrorCodeException("invalid password");
                }

                var result = await _userManager.ChangePasswordAsync(currentUser, updatePasswordView.OldPassword, updatePasswordView.NewPassword);

                if (result.Succeeded)
                {
                    return true;
                }

                throw new ErrorCodeException(JsonConvert.SerializeObject(result.Errors));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw;
            }
        }

        public async Task<bool> VerifyEmailAsync(string email, string verificationCode)
        {
            try
            {
                var key = EmailType.EmailVerification.ToString() + "#" + email;
                if (_cache.TryGetValue(key, out string existValue) is false)
                    throw new ErrorCodeException("Code Already expired or not found");

                if (existValue != verificationCode)
                {
                    throw new ErrorCodeException("Incorrect verification code");
                }

                var user = await _unitOfWorkRepository.UserRepository.GetAll().FirstOrDefaultAsync(p => p.Email == email);

                if (user == null)
                {
                    throw new ErrorCodeException("User not found");
                }

                if (user.EmailConfirmed)
                {
                    throw new ErrorCodeException("Email Already verified");
                }

                user.EmailConfirmed = true;
                _unitOfWorkRepository.UserRepository.Update(user);
                _cache.Remove(key);

                if (await _unitOfWorkRepository.UserRepository.SaveChangesAsync() > 0)
                {
                    return true;
                }

                throw new ErrorCodeException("error");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw;
            }
        }

        public async Task<bool> VerifyUserRemovalAsync(string email, string verificationCode)
        {
            try
            {
                var key = EmailType.UserRemoval.ToString() + "#" + email;
                if (_cache.TryGetValue(key, out string existValue) is false)
                    throw new ErrorCodeException("Code Already expired or not found");

                if (existValue != verificationCode)
                {
                    throw new ErrorCodeException("Incorrect verification code");
                }

                var user = await _unitOfWorkRepository.UserRepository.GetAll().FirstOrDefaultAsync(p => p.Email == email);

                if (user == null)
                {
                    throw new ErrorCodeException("user not found");
                }

                _unitOfWorkRepository.UserRepository.Remove(user);
                _cache.Remove(key);

                if (await _unitOfWorkRepository.UserRepository.SaveChangesAsync() > 0)
                {
                    return true;
                }

                throw new ErrorCodeException("Error saving");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw;
            }
        }


        // Need to rethink about when front is ready
        private string GetEmailVerificationEmailBody(string verificationCode, User user)
        {
            string localIpAddress = _httpContextAccessor.HttpContext.Request.Host.Value.ToString();
            string confirmationLink = $"https://{localIpAddress}/api/Identity/VerifyEmail?email={user.Email}&verifactionCode={verificationCode}";

            var htmlEmailBody = $"<!DOCTYPE html>" +
                $"<html>" +
                $" <head>" +
                $"    <title>Email Confirmation</title>" +
                $"  </head>" +
                $"  <body>" +
                $"    <h1>Email Confirmation</h1>" +
                $"    <p>Dear {user.FirstName},</p>" +
                $"    <p>Thank you for registering. To confirm your email address, please click the button below.</p>" +
                $"   <a href=\"{confirmationLink}\" target=\"_blank\" style=\"display:inline-block;padding:12px 24px;background-color:#3366cc;color:#ffffff;font-size:18px;text-decoration:none;border-radius:5px;\">Confirm Email</a>" +
                $"    <p>If you did not initiate this request, please ignore this email.</p>" +
                $"    <p>Thank you for choosing our service!</p>" +
                $"    <p>Best regards</p>" +
                $"  </body>" +
                $"</html>";

            return htmlEmailBody;
        }

        private string GetUserRemovalEmailBody(string verificationCode, User user)
        {
            string localIpAddress = _httpContextAccessor.HttpContext.Request.Host.Value.ToString();
            string confirmationLink = $"https://{localIpAddress}/api/Identity/VerifyUserRemovalEmail?email={user.Email}&verifactionCode={verificationCode}";

            var htmlEmailBody = "<!DOCTYPE html>" +
                $"<html>" +
                $"  <head>" +
                $"    <meta charset=\"UTF-8\">" +
                $"    <title>Account Removal Verification</title>" +
                $"  </head>" +
                $"  <body>" +
                $"    <h1>Account Removal Verification</h1>" +
                $"   <p>Dear {user.FirstName},</p>" +
                $"    <p>We have received a request to remove your account. To confirm that you wish to proceed with this request, please click the button below.</p>" +
                $"    <p><strong>Note:</strong> If you did not request to remove your account, please ignore this email</p>" +
                $"    <a href=\"{confirmationLink}\" target=\"_blank\" style=\"display:inline-block;padding:12px 24px;background-color:#FF0000;color:#ffffff;font-size:18px;text-decoration:none;border-radius:5px;\">Verify Account Removal</a>" +
                $"    <p>Thank you for choosing our service!</p>" +
                $"    <p>Best regards</p>" +
                $"  </body>" +
                $"</html>";

            return htmlEmailBody;
        }

        private string GetPasswordRecoveryEmailBody(string verificationCode, User user)
        {
            string localIpAddress = _httpContextAccessor.HttpContext.Request.Host.Value.ToString();
            string confirmationLink = $"https://{localIpAddress}/api/Identity/RecoverPassword?verifactionCode={verificationCode}";

            var htmlEmailBody = $"<!DOCTYPE html>" +
                $"<html>" +
                $" <head>" +
                $"    <title>Password Recovery</title>" +
                $"  </head>" +
                $"  <body>" +
                $"    <h1>Password Recovery</h1>" +
                $"    <p>Dear {user.FirstName},</p>" +
                $"    <p>We recently received a request to reset your account password for roommates.uz</p>" +
                $"    <p>To complete the password reset process, please click the button below</p>" +
                $"   <a href=\"{confirmationLink}\" target=\"_blank\" style=\"display:inline-block;padding:12px 24px;background-color:#3366cc;color:#ffffff;font-size:18px;text-decoration:none;border-radius:5px;\">Recover Password</a>" +
                $"    <p>If you did not initiate this request, please ignore this email.</p>" +
                $"    <p>Thank you for choosing our service!</p>" +
                $"    <p>Best regards</p>" +
                $"  </body>" +
                $"</html>";

            return htmlEmailBody;
        }



        private bool TryToSetValueToCache(string key, string value)
        {
            try
            {
                if (_cache.TryGetValue(key, out string existValue) is true)
                    throw new ErrorCodeException("request already created");

                _cache.Set(key, value, TimeSpan.FromMinutes(1));

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw;
            }
        }

        private string CreateEmailVerificationCode(string address, string type)
        {
            try
            {
                var key = type + "#" + address;
                var value = Guid.NewGuid().ToString() + Guid.NewGuid().ToString();

                var result = TryToSetValueToCache(key, value);

                return value;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw;
            }
        }

    }
}
