using System.Collections.Generic;
using UnityEngine;

public class ShipData : MonoBehaviour {
    // Start is called before the first frame update
    void Start() {
        LaunchSystem[] Rockets = new LaunchSystem[1];
        Rockets[0] = new LaunchSystem("Starship", new List<Stage> { new Stage(330, 180, 3780), new Stage(363, 100, 1300) }, "US", true);
    }
}

// Store in order of (Private / Public) // (Country) // (Ship)
// Store data for: isp, dry mass, wet mass

public class LaunchSystem {
    public string Name { get; }
    public List<Stage> Stages { get; }
    public string Country { get; }
    public bool Private { get; }
    public LaunchSystem(string name, List<Stage> stages, string country = "US", bool privateSystem = false) {
        Name = name;
        Stages = stages;
        Country = country;
        Private = privateSystem;
    }
}

public class Stage {
    public int isp { get; }
    public int DryMass { get; }
    public int WetMass { get; }

    public Stage(int specificImpulse, int dryMass, int wetMass) {
        isp = specificImpulse;
        DryMass = dryMass;
        WetMass = wetMass;
    }
}