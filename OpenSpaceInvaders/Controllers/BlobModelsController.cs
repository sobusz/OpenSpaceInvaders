using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using OpenSpaceInvaders.Data;
using OpenSpaceInvaders.Models;
using OpenSpaceInvaders.Utilities;

namespace BlobUploadWebApp.Controllers
{

    public class BlobModelsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public BlobModelsController(UserManager<IdentityUser> userManager, ApplicationDbContext context)
        {
            _userManager = userManager;
            _context = context;
            utility = new BlobUtility(accountName, accountKey);
        }

        BlobUtility utility;
        string accountName = "openspaceinvadersstorage";
        string accountKey = "BHSo33+41kh7SZ00VFbBapx/l2M5S8skYrtiVm5vns8LkGqI6Z3mgMXDGcJdv8A4kFwCJacINSR70YoGhGp6kQ==";

        [Authorize]
        public IActionResult MediaFileUpload()
        {
            string loggedInUserId = _userManager.GetUserId(User);
            List<BlobModel> userMedia = (from a in _context.BlobModel where a.UserId == loggedInUserId select a).ToList();
            ViewBag.PhotoCount = userMedia.Count;
            return View(userMedia);
        }

        [Authorize]
        [HttpPost]

        public ActionResult UploadMediaFile(IFormFile file)                 
        {
            if (file != null)
            {
                string ContainerName = "imagecontainer";            
                string fileName = Path.GetFileName(file.FileName);
                Stream imageStream = file.OpenReadStream();
                var result = utility.UploadBlobAsync(fileName, ContainerName, (IFormFile)file);
                if (result != null)
                {
                    string loggedInUserId = _userManager.GetUserId(User);
                    BlobModel usermedium = new BlobModel();

                    try
                    {
                        usermedium.UserId = loggedInUserId;    
                        usermedium.MediaUrl = result.Result.Uri.ToString();                 
                        usermedium.MediaFileName = result.Result.Name;                      
                        usermedium.MediaFileType = result.Result.Name.Split('.').Last();    
                    }
                    catch
                    {
                        Console.WriteLine($"Unable to parse '{loggedInUserId}'");
                    }

                    _context.BlobModel.Add(usermedium);
                    _context.SaveChanges();
                    return RedirectToAction("MediaFileUpload");
                }
                else
                {
                    return RedirectToAction("MediaFileUpload");
                }
            }
            else
            {
                return RedirectToAction("MediaFileUpload");
            }
        }

        [Authorize]

        public ActionResult DeleteMediaFile(int id)
        {
            BlobModel userMedium = _context.BlobModel.Find(id);
            _context.BlobModel.Remove(userMedium);
            _context.SaveChanges();
            string BlobNameToDelete = userMedium.MediaUrl.Split('/').Last();
            utility.DeleteBlob(BlobNameToDelete, "imagecontainer");
            return RedirectToAction("MediaFileUpload");
        }
    }
}