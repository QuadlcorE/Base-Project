using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EventHandler : MonoBehaviour
{
    public string GameplayScene;
    
    public IEnumerator StartChangeScene(float timeTillChange)
    {
        Debug.Log("I was summoned");
        yield return new WaitForSeconds(timeTillChange);
        Debug.Log("And I ran");
        SceneManager.LoadScene(GameplayScene);
    }

    public void StartStageSelectorScene()
    {
        SceneManager.LoadScene("PowerUp-Select-Scene");
    }
}
