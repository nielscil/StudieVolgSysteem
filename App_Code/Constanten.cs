﻿using System;
using System.IO;
using System.Collections.Generic;
using System.Web;
using System.Web.Security;
using WebMatrix.Data;
using WebMatrix.WebData;
using System.Web.Helpers;

/// Constante waardes
/// </summary>
public static class Constants
{
    //database naam invullen
    public static string DBName = "Demo";
    //meer constanten toevoegen

    public static void SentMail(string emailadres,string onderwerp,string body)
    {
        string filepath = HttpContext.Current.Server.MapPath("~/Content/email.html");
        string emailtemplate = File.ReadAllText(filepath);
        emailtemplate = emailtemplate.Replace("{0}", onderwerp).Replace("{1}", body).Replace("{2}", DateTime.Now.Year.ToString());
        WebMail.Send(emailadres, onderwerp,isBodyHtml: true,body: emailtemplate);
    }

    public static void SentMail(string emailadres, string onderwerp, string body,string buttonlink,string buttontext)
    {
        string filepath = HttpContext.Current.Server.MapPath("~/Content/emailbutton.html");
        string emailtemplate = File.ReadAllText(filepath);
        emailtemplate = emailtemplate.Replace("{0}", onderwerp).Replace("{1}", body).Replace("{2}",buttonlink).Replace("{3}",buttontext).Replace("{4}", DateTime.Now.Year.ToString());
        WebMail.Send(emailadres, onderwerp, isBodyHtml: true, body: emailtemplate);
    }
}