﻿using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Security;
using WebMatrix.Data;
using WebMatrix.WebData;

public static class Constants{
    //database naam invullen
    public static string DBName = "Demo";
    //meer constanten toevoegen
}

public class Gebruiker
{
    public int GebruikerID {get;set;}
    public string Email {get;set;}
    public string[] Role {get;set;}
    public string Naam {get;set;}
    public string Achternaam {get;set;}
    public string Woonplaats {get;set;}
    public string Adres {get;set;}
    public string TelefoonNr {get;set;}
    public string Postcode {get;set;}

    public Gebruiker(int userID,string email,string[] role,string name,string surname,string hometown,string adress, string telefoonnr,string postcode)
    {
        this.Email = email;
        this.GebruikerID = userID;
        this.Role = role;
        this.Naam = name;
        this.Achternaam = surname;
        this.Woonplaats = hometown;
        this.Adres = adress;
        this.TelefoonNr = telefoonnr;
        this.Postcode = postcode;
    }
    public Gebruiker(string email,string[] role,string name,string surname,string hometown,string adress, string telefoonnr,string postcode)
    {
        this.Email = email;
        this.Role = role;
        this.Naam = name;
        this.Achternaam = surname;
        this.Woonplaats = hometown;
        this.Adres = adress;
        this.TelefoonNr = telefoonnr;
        this.Postcode = postcode;
    }

    /// <summary>
    /// Geeft lijst met alle gebruikers
    /// </summary>
    /// <returns>lijst van Gebruiker</returns>
    public static List<Gebruiker> GetAllUsers()
    {
        Database db = Database.Open(Constants.DBName);
        var rows = db.Query("SELECT * FROM gebruikers;");
        db.Close();
        List<Gebruiker> lijst = new List<Gebruiker>();
        foreach(var r in rows)
        {
            Gebruiker g = new Gebruiker(r.GebruikerID, r.Email, Roles.GetRolesForUser(r.Email),r.Naam, r.Achternaam, r.Woonplaats, r.Adres, r.Telefoon_nr, r.Postcode);
           lijst.Add(g);
        }
        return lijst;
    }

    /// <summary>
    /// Geeft een gebruiker terug
    /// </summary>
    /// <param name="userid">gebruikerid van gebruiker</param>
    /// <returns>gebruiker</returns>
    public static Gebruiker GetUser(int userid)
    {
        Database db = Database.Open(Constants.DBName);
        var row = db.QuerySingle("SELECT * FROM gebruikers WHERE gebruikerid=@0 ;",userid);
        db.Close();
        if(row == null)
            throw new Exception("Niet gevonden");
        return new Gebruiker(row.GebruikerID,row.Email,Roles.GetRolesForUser(row.Email),row.Naam,row.Achternaam,row.Woonplaats,row.Adres,row.Telefoon_nr,row.Postcode);
    }

    public static Gebruiker GetUser(string passreset)
    {
        Database db = Database.Open(Constants.DBName);
        var r1 = db.QuerySingle("SELECT userid FROM webpages_Membership WHERE PasswordResetToken=@0",passreset);
        if(r1 == null){
            throw new Exception("Niet gevonden");
        }
        var row = db.QuerySingle("SELECT * FROM gebruikers WHERE gebruikerid=();",r1.UserID);
        db.Close();
        if(row == null)
            throw new Exception("Niet gevonden");
        return new Gebruiker(row.GebruikerID,row.Email,Roles.GetRolesForUser(row.Email),row.Naam,row.Achternaam,row.Woonplaats,row.Adres,row.Telefoon_nr,row.Postcode);
    }

    /// <summary>
    /// Maakt gebruiker aan.
    /// </summary>
    /// <param name="email">email</param>
    /// <param name="roles">rollen</param>
    /// <param name="wachtwoord">wachtwoord</param>
    /// <param name="naam">naam</param>
    /// <param name="achternaam">achternaam</param>
    /// <param name="woonplaats">woonplaats</param>
    /// <param name="adres">adres</param>
    /// <param name="telefoonnr">telefoonnummer</param>
    /// <param name="postcode">postcode</param>
    /// <returns>true als het goed gegaan is, false wanneer dit niet het geval is</returns>
    public static bool AddUser(string email,string[] roles,string wachtwoord,string naam,string achternaam,string woonplaats,string adres, string telefoonnr,string postcode)
    {
        Database db = Database.Open(Constants.DBName);
        db.Execute("INSERT INTO gebruikers (Email,naam,achternaam,woonplaats,adres,telefoon_nr,postcode) Values (@0,@1,@2,@3,@4,@5,@6);",email,naam,achternaam,woonplaats,adres,telefoonnr,postcode);
        db.Close();
        try
        {
            WebSecurity.CreateAccount(email, wachtwoord, false);
            Roles.AddUserToRoles(email, roles);
        }
        catch
        {
            return false;
        }
        return true;
    }

    /// <summary>
    /// Update een bepaalde gebruiker
    /// </summary>
    /// <param name="gebruikerid">gebruikeid</param>
    /// <param name="roles">rollen</param>
    /// <param name="naam">naam</param>
    /// <param name="achternaam">achternaam</param>
    /// <param name="woonplaats">woonplaats</param>
    /// <param name="adres">adres</param>
    /// <param name="telefoonnr">telefoonnummer</param>
    /// <param name="postcode">postcode</param>
    /// <returns>true als updaten succesvol is, anders false</returns>
    public static bool UpdateUser(int gebruikerid,string[] roles,string naam,string achternaam,string woonplaats,string adres, string telefoonnr,string postcode)
    {
        Database db = Database.Open(Constants.DBName);
        try
        {
            db.Execute("UPDATE gebruikers SET naam=@0,achternaam=@1,woonplaats=@2,adres=@3,telefoon_nr=@4,postcode=@5 WHERE gebruikersid=@6 );", naam, achternaam, woonplaats, adres, telefoonnr, postcode, gebruikerid);
            db.Close();
            string gebruikersnaam = GetUser(gebruikerid).Email;
            foreach (string s in roles)
            {
                if (!Roles.IsUserInRole(gebruikersnaam, s))
                {
                    Roles.AddUserToRole(gebruikersnaam, s);
                }
            }
        }
        catch
        {
            return false;
        }
        return true;
    }

    /// <summary>
    /// reset wachtwoord
    /// </summary>
    /// <param name="gebruikersid">gebruikerid</param>
    public static void ResetPass(int gebruikersid)
    {
        WebSecurity.GeneratePasswordResetToken(GetUser(gebruikersid).Email);
    }

    /// <summary>
    /// Verwijderd gebruiker
    /// </summary>
    /// <param name="userid">gebruikerid</param>
    /// <returns>true als het succesvol is, anders false</returns>
    public static bool DeleteUser(int userid)
    {
        Database db = Database.Open(Constants.DBName);
        db.Execute("DELETE FROM webpages_Membership WHERE userid=@0;",userid);
        db.Execute("DELETE FROM webpages_usersinroles WHERE userid=@0;",userid);
        db.Execute("DELETE FROM gebruikers WHERE gebruikerid=@0",userid);
        var row = db.QuerySingle("SELECT * FROM gebruikers WHERE gebruikerid=@0",userid);
        db.Close();
        if(row == null)
            return true;
        else
            return false;
    }

    public static List<Gebruiker> SearchUsers(string input)
    {
        Database db = Database.Open(Constants.DBName);
        List<Gebruiker> lijst = new List<Gebruiker>();
        if(input.Contains(" "))
        {
            string[] raw = input.Split(' ');
            foreach(string ra in raw)
            {
                if(ra.Length > 3)
                {
                    string r1 = ra + "%";
                    var rows = db.Query("SELECT * FROM gebruikers WHERE email LIKE @0 OR naam LIKE @1 OR achternaam LIKE @2;",r1 ,r1,r1);
                    foreach(var r in rows)
                    {
                        bool alreadyIn = false;
                        foreach(Gebruiker g1 in lijst)
                        {
                            if (g1.Naam == r.Naam && g1.Achternaam == r.Achternaam && g1.Email == r.Email)
                            {
                                alreadyIn = true;
                                break;
                            }
                        }
                        if(!alreadyIn)
                        {
                            Gebruiker g = new Gebruiker(r.GebruikerID, r.Email, Roles.GetRolesForUser(r.Email), r.Naam, r.Achternaam, r.Woonplaats, r.Adres, r.Telefoon_nr, r.Postcode);
                            lijst.Add(g);
                        }
                    }
                }
            }
        }
        else
        {
            input += "%";
            var rows = db.Query("SELECT * FROM gebruikers WHERE email LIKE @0 OR naam LIKE @1 OR achternaam LIKE @2;",input,input,input);
            foreach(var r in rows)
            {
                Gebruiker g = new Gebruiker(r.GebruikerID, r.Email, Roles.GetRolesForUser(r.Email),r.Naam, r.Achternaam, r.Woonplaats, r.Adres, r.Telefoon_nr, r.Postcode);
                lijst.Add(g);
            }
        }
        db.Close();
        return lijst;
    }
}

public class Student : Gebruiker
{
    public int SLBerID {get;set;}
    public string Opmerkingen {get;set;}
    public string StudentNr {get;set;}

    public Student(int studentID,string email,string[] role,string name,string surname,string hometown,string adress, string telefoonnr,string postcode,int slberID,string opmerking,string studentNr) : base(studentID,email,role,name,surname,hometown,adress,telefoonnr,postcode)
    {
        SLBerID = slberID;
        Opmerkingen = opmerking;
        StudentNr = studentNr;
    }
    public Student(string email,string[] role,string name,string surname,string hometown,string adress, string telefoonnr,string postcode,int slberID,string opmerking,string studentNr) : base(email,role,name,surname,hometown,adress,telefoonnr,postcode)
    {
        SLBerID = slberID;
        Opmerkingen = opmerking;
        StudentNr = studentNr;
    }

    /// <summary>
    /// Verwijderd student
    /// </summary>
    /// <param name="userid">gebruiker id</param>
    /// <returns>true als het succesvol is, anders false</returns>
    public static bool DeleteUser(int userid)
    {
        Database db = Database.Open(Constants.DBName);
        db.Execute("DELETE FROM webpages_Membership WHERE userid=@0;",userid);
        db.Execute("DELETE FROM webpages_usersinroles WHERE userid=@0;",userid);
        db.Execute("DELETE FROM gebruikers WHERE gebruikerid=@0",userid);
        db.Execute("DELETE FROM student WHERE studentid=@0",userid);
        var row = db.QuerySingle("SELECT * FROM gebruikers WHERE gebruikerid=@0",userid);
        db.Close();
        if(row == null)
            return true;
        else
            return false;
    }

    /// <summary>
    /// Maakt gebruiker aan.
    /// </summary>
    /// <param name="email">email</param>
    /// <param name="wachtwoord">wachtwoord</param>
    /// <param name="roles">rollen</param>
    /// <param name="naam">naam</param>
    /// <param name="achternaam">achternaam</param>
    /// <param name="woonplaats">woonplaats</param>
    /// <param name="adres">adres</param>
    /// <param name="telefoonnr">telefoonnummer</param>
    /// <param name="postcode">postcode</param>
    /// <param name="slberID">slb id</param>
    /// <param name="opmerking">opmerking</param>
    /// <param name="studentNr">student nummer</param>
    /// <returns>true als het goed gegaan is, false wanneer dit niet het geval is</returns>
    public static bool AddUser(string email,string wachtwoord,string[] roles,string naam,string achternaam,string woonplaats,string adres, string telefoonnr,string postcode,int slberID,string opmerking,string studentNr)
    {
        Database db = Database.Open(Constants.DBName);
        db.Execute("INSERT INTO gebruikers (Email,naam,achternaam,woonplaats,adres,telefoon_nr,postcode) Values (@0,@1,@2,@3,@4,@5,@6);",email,naam,achternaam,woonplaats,adres,telefoonnr,postcode);
        int id = db.QueryValue("SELECT gebruikerID FROM gebruikers WHERE email = @0",email);
        db.Execute("INSERT INTO Student (SLBerID,opmerking,studentnummer,studentID) VALUES (@0,@1,@2,@3)",slberID,opmerking,studentNr,id);
         db.Close();
        try{
            WebSecurity.CreateAccount(email, wachtwoord, false);
            Roles.AddUserToRoles(email, roles);
        }
        catch
        {
            return false;
        }
        return true;
    }

    /// <summary>
    /// Update een bepaalde student
    /// </summary>
    /// <param name="gebruikerid">gebruikeid</param>
    /// <param name="roles">rollen</param>
    /// <param name="naam">naam</param>
    /// <param name="achternaam">achternaam</param>
    /// <param name="woonplaats">woonplaats</param>
    /// <param name="adres">adres</param>
    /// <param name="telefoonnr">telefoonnummer</param>
    /// <param name="postcode">postcode</param>
    /// <param name="slberID">slber id</param>
    /// <param name="opmerking">opmerking</param>
    /// <param name="studentNr">studenten nummer</param>
    /// <returns>true als updaten succesvol is, anders false</returns>
    public static bool UpdateUser(int gebruikerid,string[] roles,string naam,string achternaam,string woonplaats,string adres, string telefoonnr,string postcode,int slberID,string opmerking,string studentNr)
    {
        Database db = Database.Open(Constants.DBName);
        try
        {
            db.Execute("UPDATE gebruikers SET naam=@0,achternaam=@1,woonplaats=@2,adres=@3,telefoon_nr=@4,postcode=@5 WHERE gebruikerid=@6;", naam, achternaam, woonplaats, adres, telefoonnr, postcode, gebruikerid);
            db.Execute("UPDATE Student SET SLBerID=@0,opmerking=@1,studentnummer=@2 WHERE studentid=@3;", slberID, opmerking, studentNr, gebruikerid);
            db.Close();
            string gebruikersnaam = GetUser(gebruikerid).Email;
            foreach (string s in roles)
            {
                if (!Roles.IsUserInRole(gebruikersnaam, s))
                {
                    Roles.AddUserToRole(gebruikersnaam, s);
                }
            }
        }
        catch
        {
            return false;
        }
        return true;
    }

    public static List<Student> GetStudents()
    {
        Database db = Database.Open(Constants.DBName);
        var rows = db.Query("SELECT * FROM gebruikers,student WHERE gebruikerID = studentID");
        db.Close();
        List<Student> lijst = new List<Student>();
        foreach(var r in rows)
        {

            Student s = new Student(r.GebruikerID, r.Email, Roles.GetRolesForUser(r.Email), r.Naam, r.Achternaam, r.Woonplaats, r.Adres, r.Telefoon_nr, r.postcode, r.SLBerID, r.Opmerking, r.StudentNummer);
            lijst.Add(s);
        }
        return lijst;
    }

    public static List<Student> GetStudents(int SLBerID)
    {
        Database db = Database.Open(Constants.DBName);
        var rows = db.Query("SELECT * FROM gebruikers,student WHERE gebruikerID = studentID AND slberid =@0",SLBerID);
        db.Close();
        List<Student> lijst = new List<Student>();
        foreach(var r in rows)
        {

            Student s = new Student(r.GebruikerID, r.Email, Roles.GetRolesForUser(r.Email), r.Naam, r.Achternaam, r.Woonplaats, r.Adres, r.Telefoon_nr, r.postcode, r.SLBerID, r.Opmerking, r.StudentNummer);
            lijst.Add(s);
        }
        return lijst;
    }

    public static Student GetStudent(int studentid)
    {
         Database db = Database.Open(Constants.DBName);
        var r = db.QuerySingle("SELECT * FROM gebruikers,student WHERE gebruikerID = studentID AND gebruikerID =@0",studentid);
        db.Close();
        if(r == null)
            throw new Exception("Geen student gevonden");
        return new Student(r.GebruikerID,r.Email,Roles.GetRolesForUser(r.Email),r.Naam,r.Achternaam,r.Woonplaats,r.Adres,r.Telefoon_nr,r.postcode,r.SLBerID,r.Opmerking,r.StudentNummer);
    }
}

public class SLB : Gebruiker
{
    public int SLBerID {get;set;}

    public SLB(int SLBerID,string email,string[] roles,string naam,string achternaam,string woonplaats,string adres,string telefoonnr,string postcode) : base(SLBerID,email,roles,naam,achternaam,woonplaats,adres,telefoonnr,postcode)
    {
        
    }

    public SLB(string email,string[] roles,string naam,string achternaam,string woonplaats,string adres,string telefoonnr,string postcode) : base(email,roles,naam,achternaam,woonplaats,adres,telefoonnr,postcode)
    {
        
    }

    public static bool HasStudent(int slbid)
    {
        Database db = Database.Open(Constants.DBName);
        int r = db.QueryValue("SELECT COUNT(studentID) FROM Student WHERE slberid=@0", slbid);
        if (r > 0)
            return true;
        else
            return false;
    }

    public static List<SLB> GetSLBers()
    {
        Database db = Database.Open(Constants.DBName);
        var rows = db.Query("SELECT * FROM gebruikers WHERE gebruikerID in (SELECT UserID FROM webpages_UsersInRoles WHERE RoleID = 2);");
        db.Close();
        List<SLB> lijst = new List<SLB>();
        foreach(var r in rows)
        {
            SLB slb = new SLB(r.GebruikerID,r.Email,Roles.GetRolesForUser(r.Email),r.Naam,r.Achternaam,r.Woonplaats,r.Adres,r.Telefoon_nr,r.Postcode);
            lijst.Add(slb);
        }
        
        return lijst;
    }
    public static SLB GetSLBer(int slbid)
    {
        Database db = Database.Open(Constants.DBName);
        var r = db.QuerySingle("SELECT * FROM gebruikers WHERE gebruikerID in (SELECT userID FROM webpages_UsersInRoles WHERE RoleID = 2) AND gebruikerID =@0;",slbid);
        db.Close();
        if(r == null)
            throw new Exception("Geen SLB'er gevonden");
        return new SLB(r.GebruikerID,r.Email,Roles.GetRolesForUser(r.Email),r.Naam,r.Achternaam,r.Woonplaats,r.Adres,r.Telefoon_nr,r.Postcode);
    }

}

public class Evaluatie
{
    public int GesprekID {get;set;}
    public string Besproken {get;set;}
    public DateTime DatumVolgendeGesprek {get;set;}
    public int AgendaID {get;set;}
    public string LocatieVolgendeGesprek{get;set;}
    public Evaluatie(int gesprekID,string besproken,DateTime datumVolgendeGesprek,int agendaID, string locatieVolgendeGesprek) 
    {
        this.GesprekID = gesprekID;
        this.Besproken = besproken;
        this.DatumVolgendeGesprek = datumVolgendeGesprek;
        this.AgendaID = agendaID;
        this.LocatieVolgendeGesprek = locatieVolgendeGesprek;
    }
    public Evaluatie(string besproken,DateTime datumVolgendeGesprek, int agendaID,string locatieVolgendeGesprek) 
    {
        this.Besproken = besproken;
        this.DatumVolgendeGesprek = datumVolgendeGesprek;
        this.AgendaID = agendaID;
        this.LocatieVolgendeGesprek = locatieVolgendeGesprek;
    }
    public static int AddEvaluatie(string besproken, DateTime datumVolgendeGesprek,int agendaID,string locatieVolgendeGesprek) 
    {
        Database db = Database.Open(Constants.DBName);
        db.Execute("INSERT INTO Evaluatie(besproken,agendaid) VALUES (@0,@1)", besproken,agendaID);
        var id = db.QueryValue("SELECT gesprekid FROM Evaluatie where agendaID=@0",agendaID);
        db.Close();
        if(id == null)
            return -1;
        else
            return (int)id;
    }
    public static int AddEvaluatie(string besproken, int agendaID)
    {
        Database db = Database.Open(Constants.DBName);
        db.Execute("INSERT INTO Evaluatie(besproken,agendaid) VALUES (@0,@1)", besproken, agendaID);
        var id = db.QueryValue("SELECT gesprekid FROM Evaluatie where agendaID=@0", agendaID);
        db.Close();
        if (id == null)
            return -1;
        else
            return (int)id;
    }
}

public class Afspraak 
{
    public int afspraakID {get;set;}
    public string opmerking {get;set;}
    public bool afgevinkt {get;set;}
    public int GesprekID {get;set;}
    public Afspraak(int AfspraakID,string Opmerking, bool Afgevinkt, int gesprekID)
    {
        this.afspraakID = AfspraakID;
        this.opmerking = Opmerking;
        this.afgevinkt = Afgevinkt;
        this.GesprekID = gesprekID;
    }
    public Afspraak(string Opmerking, bool Afgevinkt, int gesprekID)
    {
        this.opmerking = Opmerking;
        this.afgevinkt = Afgevinkt;
        this.GesprekID = gesprekID;
    }
    public static void AddAfspraak(string Opmerking, int gesprekID) 
    {
        Database db = Database.Open(Constants.DBName);
        db.Execute("INSERT INTO Afspraken(Opmerking,Afgevinkt,gesprekID) VALUES (@0,@1,@2)", Opmerking,false,gesprekID);
        db.Close();
    }
}
