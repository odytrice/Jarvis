using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Jarvis.Controllers
{
    public class AudioController : Controller
    {
        // GET: Audio
        public ActionResult Query(string text)
        {
            var x = new WebClient();
            var content = x.DownloadData("http://translate.google.com/translate_tts?tl=en&q=" + Uri.EscapeDataString(text));
            return File(content, "audio/vnd.wave", "audio.wav");
        }
    }
}