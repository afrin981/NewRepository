using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public JsonResult CountofCharacters(string strArray)
        {            
            List<TestModel> lstTestModel = new List<TestModel>();
            var charGroups = (from s in strArray
                              group s by s into g
                              select new
                              {
                                  character = g.Key,
                                  count = g.Count(),
                              }).OrderBy(c => c.count);

            foreach (var x in charGroups)
            {
                TestModel objTestModel = new TestModel();
                objTestModel.Character = x.character;
                objTestModel.Count = x.count;
                lstTestModel.Add(objTestModel);
            }          
            return Json(lstTestModel);
        }

        
        public string ReverseString(string strInputString)
        {
            string reversedWords = string.Empty;
            if (!String.IsNullOrEmpty(strInputString))
            {
                reversedWords = string.Join(" ",
                                    strInputString.Split(' ')
                                        .Select(x => new String(x.Reverse().ToArray()))
                                        .ToArray());                
            }
            return reversedWords;
        }

        public string ReverseStringWithoutSplit(string strInputString)
        {
            string output = string.Empty;
            StringBuilder strb = new StringBuilder();
            List<char> charlist = new List<char>();
            for (int c = 0; c < strInputString.Length; c++)
            {
                if (strInputString[c] == ' ' || c == strInputString.Length - 1)
                {
                    if (c == strInputString.Length - 1)
                        charlist.Add(strInputString[c]);
                    for (int i = charlist.Count - 1; i >= 0; i--)
                        strb.Append(charlist[i]);
                    strb.Append(' ');
                    charlist = new List<char>();
                }
                else
                    charlist.Add(strInputString[c]);
            }
            output = strb.ToString();
            return output;
        }
    }
}