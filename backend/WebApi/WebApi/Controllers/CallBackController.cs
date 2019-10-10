using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using WebApi.Models;
using System.Configuration;

namespace WebApi.Controllers
{
    public class CallBackController : Controller
    {
        private static User userData = new User();
        private readonly UserContext _context;

        [HttpGet]
        public ActionResult<User> InstaData()
        {
            return userData;
        }
        public async Task<ActionResult> CallBack(string code)
        {
            if (!String.IsNullOrEmpty(code))
            {
                code = code.ToString();
                await GetDataInstagramToken(code);
            }
            return Redirect(ConfigurationManager.AppSettings["localhost2"]);
        }
        public async Task GetDataInstagramToken(string code)
        {
            User user2 = new User();

            try
            {

                var values = new Dictionary<string, string>    {
                      { "client_id",ConfigurationManager.AppSettings["client_id"]},
                      { "client_secret", ConfigurationManager.AppSettings["client_secret"]},
                      { "grant_type", "authorization_code" },
                      { "redirect_uri", ConfigurationManager.AppSettings["redirect_uri"]},
                      { "code", code}    
                };
                var content = new FormUrlEncodedContent(values);

                double IDCurent = 0;
                string accessToken = "";
                using (var client2 = new HttpClient())
                {
                    var response = await client2.PostAsync(ConfigurationManager.AppSettings["instagram_token"], content);
                    var responseString = await response.Content.ReadAsStringAsync();
                    var jsResult = (JObject)JsonConvert.DeserializeObject(responseString);
                     accessToken = (string)jsResult["access_token"];
                   string imageurl = (string)jsResult["user"]["profile_picture"];
                    user2.ProfePicture = imageurl;
                    string fullname = (string)jsResult["user"]["full_name"];
                    user2.Name = fullname;
                    IDCurent = (double)jsResult["user"]["id"];
                    user2.IDInsta = IDCurent;


                }

                using (var client = new HttpClient())
                {
                    var response = await client.GetAsync(ConfigurationManager.AppSettings["instagram_media"] + accessToken);
                    var responseString = await response.Content.ReadAsStringAsync();
                    var jsResult = (JObject)JsonConvert.DeserializeObject(responseString);
                    //data don t respond
                    //double likes  = (double)jsResult["user"]["likes"];
                    //double commens = (double)jsResult["user"]["comments"];
                    //string link = (string)jsResult["user"]["link"];

                    //user2.LinkBio = link;
                    //user2.Comments = commens;
                    user2.ClientId = ConfigurationManager.AppSettings["client_id"];
                    user2.ClientSecret = ConfigurationManager.AppSettings["client_secret"];
                }

                SaveData(user2);
         
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);

            }


        }

        public void SaveData(User user)
        {
            userData = user;
        }
        public User GetData()
        {
            return userData;
        }

       

    }
}