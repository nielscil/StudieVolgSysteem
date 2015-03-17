using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Security;
using WebMatrix.Data;
using WebMatrix.WebData;

/// <summary>
/// Cijfers uitwisselen met database
/// </summary>
public class Cijfer
{
    public int CijferID { get; set; }
    public int StudentID { get; set; }
    public int VakID { get; set; }
    public int Cijfertje { get; set; }
    public int EcS {get;set;}
    public int gekregenEcS {get;set;}
    /// <summary>
    /// Maakt cijfer aan
    /// </summary>
    /// <param name="cijferid">cijferid</param>
    /// <param name="studentid">studentid</param>
    /// <param name="vakid">vakid</param>
    /// <param name="cijfer">cijfer</param>
    public Cijfer(int cijferid, int studentid, int vakid, int cijfer)
    {
        this.CijferID = cijferid;
        this.StudentID = studentid;
        this.VakID = vakid;
        this.Cijfertje = cijfer;
    }
    /// <summary>
    /// Maakt cijfer aan
    /// </summary>
    /// <param name="studentid">studentid</param>
    /// <param name="vakid">vakid</param>
    /// <param name="cijfer">cijfer</param>
    public Cijfer(int studentid, int vakid, int cijfer)
    {
        this.StudentID = studentid;
        this.VakID = vakid;
        this.Cijfertje = cijfer;
    }
    /// <summary>
    /// Haalt lijst met cijfers op voor specifieke student
    /// </summary>
    /// <param name="studentid">studentid</param>
    /// <returns>lijst met cijfers</returns>
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
    /// <summary>
    /// Telt aantal cijfers in de database
    /// </summary>
    /// <returns>aantal cijfers</returns>
    public static int Count()
    {
        Database db = Database.Open(Constants.DBName);
        int count = db.QueryValue("SELECT COUNT(cijferid) FROM cijfers;");
        db.Close();
        return count;
    }
}