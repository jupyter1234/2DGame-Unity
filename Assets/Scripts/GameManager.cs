using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    //점수, 스테이지, 생명 관리
    public int totalPoint;
    public int stagePoint;
    public int stageIndex;
    public int health;
    public PlayerMove player;
    public GameObject[] Stages;

    //UI관리
    public Image[] UIhealth;
    public TextMeshProUGUI UIPoint;
    public TextMeshProUGUI UIStage;
    public GameObject UIRestartBtn;

    void Update()
    {
        UIPoint.text = (totalPoint + stagePoint).ToString();        
    }
    public void NextStage()
    {
        //Change Stage
        if(stageIndex < Stages.Length - 1)
        {
            Stages[stageIndex].SetActive(false);
            stageIndex++;
            Stages[stageIndex].SetActive(true);
            PlayerReposition();

            UIStage.text = "STAGE " + (stageIndex + 1);
          
        }
        else //Game Clear
        {
            //Player Control Lock
            Time.timeScale = 0;
            //Result UI
            Debug.Log("Game Clear!");
            //Restart Button UI
            
            TextMeshProUGUI btnText = UIRestartBtn.GetComponentInChildren<TextMeshProUGUI>();
            btnText.text = "Clear!";
            UIRestartBtn.SetActive(true);

        }

        //Calculate Point
        totalPoint += stagePoint;
        stagePoint = 0;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        //땅에 떨어짐
        if (collision.gameObject.tag == "Player")
        {
            
            //Player Reposition
            if(health > 1)
            {
                PlayerReposition();
            }

            //Health Down (마지막 체력에서 낭떠러지일 때 원위치 x)
            HealthDown();
        }
            
    }
    
    void PlayerReposition()
    {
        player.transform.position = new Vector3(-8.33f, -0.5707802f, -1);
        player.VelocityZero();
    }

    public void Restart()
    {
        SceneManager.LoadScene("Game");
    }
    public void HealthDown()
    {
        if (health > 1)
        {
            health--;
            UIhealth[health].color = new Color(1, 0, 0, 0.4f);
        }
            
        else
        {
            //All Health UI Off
            UIhealth[0].color = new Color(1, 0, 0, 0.4f);
            //Player Die Effect
            player.OnDie();
            //Result UI
            Debug.Log("Game Over");
            //Retry Button UI
            UIRestartBtn.SetActive(true);
        }
    }
}
