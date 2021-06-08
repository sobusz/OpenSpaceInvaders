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

        public BlobModelsController(UserManager<IdentityUser> userManager, ApplicationDbContext context) // Constructor
        {
            _userManager = userManager;
            _context = context;
            utility = new BlobUtility(accountName, accountKey);
        }

        BlobUtility utility;
        string accountName = "openspaceinvadersstorage";      // replace with your storage account Name here
        string accountKey = "BHSo33+41kh7SZ00VFbBapx/l2M5S8skYrtiVm5vns8LkGqI6Z3mgMXDGcJdv8A4kFwCJacINSR70YoGhGp6kQ==";  // replace with your storage account access key here

        [Authorize]
        public IActionResult MediaFileUpload()          // To load the blob media upload view (MediaFileUpload.cshtml file)
        {
            string loggedInUserId = _userManager.GetUserId(User);
            List<BlobModel> userMedia = (from a in _context.BlobModel where a.UserId == loggedInUserId select a).ToList();
            ViewBag.PhotoCount = userMedia.Count;
            return View(userMedia);
        }

        [Authorize]
        [HttpPost]

        public ActionResult UploadMediaFile(IFormFile file)                 // upload method to upload the file to blob storage, and store the returned URL of the file in the SQL database
        {
            if (file != null)
            {
                string ContainerName = "imagecontainer";            // replace with the container name. 
                string fileName = Path.GetFileName(file.FileName);
                Stream imageStream = file.OpenReadStream();
                var result = utility.UploadBlobAsync(fileName, ContainerName, (IFormFile)file);
                if (result != null)
                {
                    string loggedInUserId = _userManager.GetUserId(User);
                    BlobModel usermedium = new BlobModel();

                    try
                    {
                        usermedium.UserId = loggedInUserId;                                 //If the User ID is an integer then, Int32.Parse(loggedInUserId);
                        usermedium.MediaUrl = result.Result.Uri.ToString();                 // to insert the url of the blob to the database
                        usermedium.MediaFileName = result.Result.Name;                      // to insert the media file name to the database
                        usermedium.MediaFileType = result.Result.Name.Split('.').Last();    // to insert the media file type to the database
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
            utility.DeleteBlob(BlobNameToDelete, "imagecontainer");         // container name
            return RedirectToAction("MediaFileUpload");                             // return page
        }
    }
}