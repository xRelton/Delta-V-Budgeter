using UnityEngine;

public class StarSystemData : MonoBehaviour {
    public SystemPosition[] SolarSystem() {
        SystemPosition[] Sol = new SystemPosition[5];

        // Star
        Sol[0] = new SystemPosition("Sun", 23649, 2);
        Sol[0].AddSub("Low Sun (10Mm)", 178107);
        Sol[0].AddSub("Sun", 618107);

        // Inner Planets
        Sol[1] = new SystemPosition("Mercury", 7906, 3);
        Sol[1].AddSub("Mercury", 6310);
        Sol[1].AddSub("Mercury (100km)", 7530);
        Sol[1].AddSub("Mercury", 10592);

        Sol[2] = new SystemPosition("Venus", 5821, 3, true);
        Sol[2].AddSub("Venus", 359);
        Sol[2].AddSub("Venus (400km)", 3298);
        Sol[2].AddSub("Venus", 33003);

        Sol[3] = new SystemPosition("Earth", 5541, 4, true);
        Sol[3].AddSub("Moon", 93, 3);
        Sol[3].SubPositions[0].AddSub("Moon", 145);
        Sol[3].SubPositions[0].AddSub("Moon (100km)", 821);
        Sol[3].SubPositions[0].AddSub("Moon", 2542);
        Sol[3].AddSub("Geostationary", 772, 1);
        Sol[3].SubPositions[1].AddSub("Geostationary (35786km)", 1472);
        Sol[3].AddSub("Low Earth (250km)", 3212);
        Sol[3].AddSub("Earth", 12468);

        Sol[4] = new SystemPosition("Mars", 5153, 5, true);
        Sol[4].AddSub("Mars", 673);
        Sol[4].AddSub("Deimos", 1009, 3);
        Sol[4].SubPositions[1].AddSub("Deimos", 649);
        Sol[4].SubPositions[1].AddSub("Deimos (1km)", 652);
        Sol[4].SubPositions[1].AddSub("Deimos", 656);
        Sol[4].AddSub("Phobos", 1404, 3);
        Sol[4].SubPositions[2].AddSub("Phobos", 535);
        Sol[4].SubPositions[2].AddSub("Phobos (1km)", 538);
        Sol[4].SubPositions[2].AddSub("Phobos", 544);
        Sol[4].AddSub("Mars (200km)", 2102);
        Sol[4].AddSub("Mars", 5680);

        /*/ Asteroid Belt
        Sol[5] = new SystemPosition("Vesta", 4230, 3);
        Sol[6] = new SystemPosition("Ceres", 3851, 3);
        Sol[7] = new SystemPosition("Pallas", 3848, 3);
        Sol[8] = new SystemPosition("Hygiea", 3551, 3);

        // Outer Planets
        Sol[9] = new SystemPosition("Jupiter", 2452, 2, true);
        Sol[10] = new SystemPosition("Saturn", 1465, 2, true);
        Sol[11] = new SystemPosition("Uranus", 775, 2, true);
        Sol[12] = new SystemPosition("Neptune", 506, 2, true);

        // Kuiper Belt
        Sol[13] = new SystemPosition("Orcus", 396, 2);
        Sol[14] = new SystemPosition("Pluto", 389, 2);
        Sol[15] = new SystemPosition("Haumea", 356, 2);
        Sol[16] = new SystemPosition("Quaoar", 352, 2);
        Sol[17] = new SystemPosition("Makemake", 339, 2);
        Sol[18] = new SystemPosition("Gonggong", 232, 2);
        Sol[19] = new SystemPosition("Eris", 230, 2);
        Sol[20] = new SystemPosition("Sedna", 30, 2);*/

        return Sol;
    }
}

/* Distance from prev data
 * 
        // Star
        Sol[0] = new SystemPosition("Sun", 0, 2);

        Sol[0].AddSub("Low Solar Orbit (10Mm)", 178107);
        Sol[0].AddSub("Sun", 440000);

        // Inner Planets
        Sol[1] = new SystemPosition("Mercury", 15743, 3);
        Sol[2] = new SystemPosition("Venus", 2085, 3, true);
        Sol[3] = new SystemPosition("Earth", 280, 4, true);
        Sol[4] = new SystemPosition("Mars", 388, 5, true);

        // Asteroid Belt
        Sol[5] = new SystemPosition("Vesta", 923, 3);
        Sol[6] = new SystemPosition("Ceres", 379, 3);
        Sol[7] = new SystemPosition("Pallas", 3, 3);
        Sol[8] = new SystemPosition("Hygiea", 297, 3);

        // Outer Planets
        Sol[9] = new SystemPosition("Jupiter", 1099, 2, true);
        Sol[10] = new SystemPosition("Saturn", 987, 2, true);
        Sol[11] = new SystemPosition("Uranus", 690, 2, true);
        Sol[12] = new SystemPosition("Neptune", 269, 2, true);

        // Kuiper Belt
        Sol[13] = new SystemPosition("Orcus", 110, 2);
        Sol[14] = new SystemPosition("Pluto", 7, 2);
        Sol[15] = new SystemPosition("Haumea", 33, 2);
        Sol[16] = new SystemPosition("Quaoar", 4, 2);
        Sol[17] = new SystemPosition("Makemake", 13, 2);
        Sol[18] = new SystemPosition("Gonggong", 107, 2);
        Sol[19] = new SystemPosition("Eris", 2, 2);
        Sol[20] = new SystemPosition("Sedna", 200, 2);

        // System
        Sol[21] = new SystemPosition("Sol", 30);
 *
 */