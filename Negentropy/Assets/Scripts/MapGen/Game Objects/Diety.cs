using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface Diety
{
    string name{get; set;}
    Ability ability{get;}
    Demand demand{get; set;}
    double satisfaction{get; set;}

    Demand updateDemand();
}
