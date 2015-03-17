using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Security;
using WebMatrix.Data;
using WebMatrix.WebData;

public class Algoritme
{
    enum Prioriteit {Laag=1, Gemiddeld, Hoog};
    public Algoritme(){}

    //Berekent de prioriteit.

    public static int ber_Prioriteit(int studID)
    {
        int prioLevel = 0;
        int count = Cijfer.GetGradesFromStudent(studID).Count;

        //Berekend het prioProcent van de student.
        foreach (Cijfer c in Cijfer.GetGradesFromStudent(studID))
        {
            //Als het een cijfer is waar 1 ec gemist is.
            if(c.Cijfertje < 6 && (c.EcS == 1 && (c.EcS != c.gekregenEcS)))
                    prioLevel = prioLevel + 5;

            //Als het een cijfer is waar 1 ec gemist is.
            if(c.Cijfertje < 6 && (c.EcS == 2 && (c.EcS != c.gekregenEcS)))
                    prioLevel = prioLevel + 10;

            //Als het een cijfer is waar 1 ec gemist is.
             if(c.Cijfertje < 6 && (c.EcS == 3 && (c.EcS != c.gekregenEcS)))
                    prioLevel = prioLevel +  15;

            //Als het een cijfer is waar 1 ec gemist is.
            if(c.Cijfertje < 6 && (c.EcS > 3 && (c.EcS != c.gekregenEcS)))
                    prioLevel = prioLevel +  20;
        }

        return prioLevel;
    }
}   

