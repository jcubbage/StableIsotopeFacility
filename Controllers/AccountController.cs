using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SIFCore.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
//using AspNetCore.Security.CAS;

namespace SIFCore.Controllers
{
     [Authorize]
     public class AccountController : Controller
    {
        public string Message
        {
            set { TempData["Message"] = value; }
        }

        public string EmulationMessage 
        {             
            set => TempData["EmulationMessage"] = value;
        }

        [TempData]
        public string ErrorMessage { get; set; }


        private readonly SIFContext _dbContext;

        public AccountController(SIFContext dbContext)
        {            
            _dbContext = dbContext;
        }

        [AllowAnonymous]
        public IActionResult Denied()
        {
            return View();
        }


        private static bool ByteArraysEqual(byte[] a, byte[] b)
        {
            if (a == null && b == null)
            {
                return true;
            }
            if (a == null || b == null || a.Length != b.Length)
            {
                return false;
            }
            var areSame = true;
            for (var i = 0; i < a.Length; i++)
            {
                areSame &= (a[i] == b[i]);
            }
            return areSame;
        }

        [AllowAnonymous]
        public async Task<IActionResult> Login(string returnUrl = null)
        {
            // Clear the existing external cookie to ensure a clean login process
            await HttpContext.SignOutAsync("Cookies");

            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(string email, string password, string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;           
            var test = await _dbContext.Contacts.Where(c => c.Email == email).ToListAsync();
            foreach (Contacts contact in test)
            {
                if (VerifyPassword(password, contact))
                {
                    await CompleteSignIn(contact, false);

                    if (returnUrl != null)
                    {
                        return LocalRedirect(returnUrl);
                    }
                    return RedirectToAction("Index", "Order", new { area = "Client" });

                }
            }  
            // If we got this far, something failed, redisplay form
            ModelState.AddModelError("", "Invalid username or password");
            return View();
        }   

        public bool VerifyPassword(string password, Contacts contact)
        {
            byte[] hashed = KeyDerivation.Pbkdf2(
                    password: password,
                    salt: contact.Salt,
                    prf: KeyDerivationPrf.HMACSHA1,
                    iterationCount: 10000,
                    numBytesRequested: 256 / 8);
            if (ByteArraysEqual(hashed, contact.Password))
            {
                return true;
            }
            return false;
        } 

        public async Task CompleteSignIn(Contacts contact, bool isEmulation)
        {
            var claims = new List<Claim>
            {
                new Claim("user", contact.Email),
                new Claim("role", "Member"),
                new Claim("contactId", contact.Id.ToString())                
            };
            if(isEmulation)
            {
                claims.Add(new Claim("role", "Emulated"));
            }

            await HttpContext.SignInAsync(new ClaimsPrincipal(new ClaimsIdentity(claims, "Cookies", "user", "role")));

        }   

        [AllowAnonymous]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterModel model)
        {          
        
            var exists = await _dbContext.Contacts.Where(c => c.Email == model.Contact.Email).AnyAsync();
            if(exists)
            {
                ErrorMessage = $"User '{model.Contact.Email}' already exists";
                return RedirectToAction("Index","Home");
            }

            var newUser = new Contacts();

            newUser.Email = model.Contact.Email;
            newUser.FirstName = model.Contact.FirstName;
            newUser.LastName = model.Contact.LastName;
            newUser.Phone = model.Contact.Phone;
            
             if(ModelState.IsValid){
                _dbContext.Add(newUser);
                await _dbContext.SaveChangesAsync();
                Message = $"Account for '{model.Contact.Email}' created";
            } else {
                ErrorMessage = "Something went wrong";         
                return View();
            }
            await _dbContext.Entry(newUser).GetDatabaseValuesAsync();            
           
            newUser.Password = MakePasswords(newUser.Salt, model.Password);
            await _dbContext.SaveChangesAsync();            
            return RedirectToAction("Index", "Home");
        }   
        
        public byte[] MakePasswords(byte[] salt, string password)
        {
            byte[] hashed = KeyDerivation.Pbkdf2(
                password: password,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA1,
                iterationCount: 10000,
                numBytesRequested: 256 / 8);
            return hashed;          
        }     

    }

}