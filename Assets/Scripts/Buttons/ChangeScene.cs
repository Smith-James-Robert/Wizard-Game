using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.SceneManagement;
public class ChangeScene : MonoBehaviour
{

    [SerializeField]
    private string toScene;

    public void clicked()
    {
        SceneManager.LoadScene(toScene);
    }
}
