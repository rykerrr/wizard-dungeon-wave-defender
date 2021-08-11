using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WizardGame.Combat_System;

public class LoadElementAndSpellDB : MonoBehaviour
{
    void Start()
    {
        SpellDB.CallToLoad();
    }
}
