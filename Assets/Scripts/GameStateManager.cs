using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStateManager : MonoBehaviour {
    public void FadeIn() {
        GetComponent<Animator>().SetTrigger("FadeIn");
    }

    public void ReloadScene() {
        SceneManager.LoadScene(0);
    }
}
