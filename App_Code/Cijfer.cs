using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Security;
using WebMatrix.Data;
using WebMatrix.WebData;

/// <summary>
/// Summary description for Cijfer
/// </summary>
public class Cijfer
{
    public int CijferID { get; set; }
    public int StudentID { get; set; }
    public int VakID { get; set; }
    public int Cijfertje { get; set; }

    public Cijfer(int cijferid, int studentid, int vakid, int cijfer)
    {
        this.CijferID = cijferid;
        this.StudentID = studentid;
        this.VakID = vakid;
        this.Cijfertje = cijfer;
    }

    public Cijfer(int studentid, int vakid, int cijfer)
    {
        this.StudentID = studentid;
        this.VakID = vakid;
        this.Cijfertje = cijfer;
    }

    public static List<Cijfer> GetGradesFromStudent(int studentid)
    {
        Database db = Database.Open(Constants.DBName);
        var rows = db.Query("SELECT * FROM cijfers WHERE studentid=@0", studentid);
        db.Close();
        List<Cijfer> list = new List<Cijfer>();
        foreach (var r in rows)
        {
            Cijfer c = new Cijfer(r.CijferID, r.StudentID, r.VakID, r.Cijfer);
            list.Add(c);
        }
        return list;
    }

    public static int Count()
    {
        Database db = Database.Open(Constants.DBName);
        int count = db.QueryValue("SELECT COUNT(cijferid) FROM cijfers;");
        db.Close();
        return count;
    }
}