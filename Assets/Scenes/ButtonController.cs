using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonController : MonoBehaviour
{
    public void OnButtonPressed()
    {
        Debug.Log("Me apretaron");
        SceneManager.LoadScene("Vibora");
    }
}
