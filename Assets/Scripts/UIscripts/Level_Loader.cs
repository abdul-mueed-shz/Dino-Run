using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level_Loader : MonoBehaviour
{
    public GameObject mainMenuPlayButton;
    public GameObject mainMenuSettings;
    public GameObject mainMenuLevels;
    public GameObject mainMenuBG;
    
    public GameObject gameLogo;

    public DinoMovement DinoMain;

    public GameObject SettingsUI;
    public GameObject mainMenuUI;

    public GameObject bossTAP;

    public GameObject hungerBar;
    public static int levelIndex;

    public int attackDoneCount = 0;

    public int attackLimit = 20;

    private void Start() {

        DinoMain = FindObjectOfType<DinoMovement>();

        SettingsUI.SetActive(false);
        mainMenuUI.SetActive(true);
       
        mainMenuBG.SetActive(false);
        gameLogo.SetActive(true);
        bossTAP.SetActive(false);
        hungerBar.SetActive(false);

        // recentLevel = SceneManager.GetActiveScene();
        // Debug.Log(recentLevel.name);
    }
    public void restartLevel()
    {
        Debug.Log(levelIndex);
        SceneManager.LoadScene(levelIndex);
    }
    public void inFight()
    {
        
        mainMenuSettings.SetActive(false);
        mainMenuLevels.SetActive(false);
        bossTAP.SetActive(true);
    }

    private bool isAttacking = false;
    public void initiateAttack()
    {
        if(attackDoneCount < attackLimit && !isAttacking)
        {
            isAttacking = true;
            DinoMain._anim.SetInteger("attackNb",Random.Range(0,3));
            DinoMain._anim.SetTrigger("attackBoss");
            StartCoroutine("attackCounter");
            Debug.Log(attackDoneCount);
        }
        else if(attackDoneCount == attackLimit)
        {
            if(DinoMain.currentHealth > 30)
            {   
                GameObject clone = (GameObject)Instantiate (DinoMain.explosion, DinoMain.bossRef.transform.position, Quaternion.identity);
                DinoMain._anim.SetTrigger("lookBack");
                Destroy(DinoMain.bossRef);
                Destroy(clone,1f);
                StartCoroutine("nextScene");
            }
            else if(DinoMain.currentHealth < 30)
            {
                DinoMain._anim.SetTrigger("death");
                StartCoroutine("retryScreen");
            }
        }

    }
    IEnumerator nextScene()
    {
        yield return new WaitForSeconds(2.4f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
    }
    IEnumerator retryScreen()
    {
        
        yield return new WaitForSeconds(1.6f);
        SceneManager.LoadScene("Retry");
    }
    IEnumerator attackCounter()
    {
        
        yield return new WaitForSeconds(1f);
        attackDoneCount++;
        isAttacking = false;
    }
    
    public void LoadLevel()//Its actually start the game. PLayed by pressing the play button
    {
        DinoMain.hasStarted = true;
        hungerBar.SetActive(true);
        gameLogo.SetActive(false);
        mainMenuPlayButton.SetActive(false);
    }

    public void LoadmainMenuSettings()// settings menu
    {

        Debug.Log("Done");
        DinoMain.paused = true;
        DinoMain.hasStarted =false;

        SettingsUI.SetActive(true);
        mainMenuPlayButton.SetActive(false);
        mainMenuSettings.SetActive(false);
        hungerBar.SetActive(false);
        mainMenuLevels.SetActive(false);
    }

    public void resume() // resuming the session after closing the settings
    {
        Debug.Log("Resumed");
        DinoMain.hasStarted = true;  
        SettingsUI.SetActive(false);
        mainMenuSettings.SetActive(true);
        hungerBar.SetActive(true);
        gameLogo.SetActive(false);
    }
    

    public void LoadLevelMenu()
    {
        SceneManager.LoadScene("mainMenuLevels");
    }

    public void win()
    {
        SceneManager.LoadScene("Level 1");
    }
    public void closeGame()
    {
        Application.Quit();
    }
    
}
