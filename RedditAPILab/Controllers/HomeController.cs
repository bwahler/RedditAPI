using Newtonsoft.Json.Linq;
using RedditAPILab.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace RedditAPILab.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Aww()
        {
            HttpWebRequest request = WebRequest.CreateHttp("https://www.reddit.com/r/aww/.json");
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            StreamReader sr = new StreamReader(response.GetResponseStream());
            string api = sr.ReadToEnd();
            sr.Close();
                        
            JToken redditData = JToken.Parse(api);
            List<JToken> aww = redditData["data"]["children"].ToList();
            List<RedditLinks> post = new List<RedditLinks>();
            for(int i = 0; i < aww.Count; i++)
            {
                RedditLinks rl = new RedditLinks();
                rl.Title = aww[i]["data"]["title"].ToString();
                rl.Image = aww[i]["data"]["thumbnail"].ToString();
                rl.Link = "http://reddit.com/" + aww[i]["data"]["permalink"].ToString();
                post.Add(rl);
            }
            return View(post);
        }
    }
}