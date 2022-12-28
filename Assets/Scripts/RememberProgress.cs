using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RememberProgress : MonoBehaviour
{
    bool saveProgress;

    public void SaveIt()
    {
        saveProgress = GameManager.instance.saved;
    }

    private void Update()
    {
        if (saveProgress)
        {
            
        }
    }
}
