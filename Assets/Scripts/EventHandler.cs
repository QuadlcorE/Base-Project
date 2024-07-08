using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EventHandler : MonoBehaviour
{
    public string GameplayScene;
    public GameObject AboutScreen;
    public GameObject InsertCoinsScreen;
    
    public IEnumerator StartChangeScene(float timeTillChange)
    {
        //Debug.Log("I was summoned");
        yield return new WaitForSeconds(timeTillChange);
        //Debug.Log("And I ran");
        SceneManager.LoadScene(GameplayScene);
    }

    public IEnumerator StartAboutScene()
    {
        AboutScreen.SetActive(true);
        InsertCoinsScreen.SetActive(false);
        yield return new WaitForSeconds(10f);
        AboutScreen.SetActive(false);
        InsertCoinsScreen.SetActive(true);
    }

    public void StartStageSelectorScene()
    {
        SceneManager.LoadScene("PowerUp-Select-Scene");
    }
}
