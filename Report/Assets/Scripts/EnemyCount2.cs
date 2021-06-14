using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EnemyCount2 : MonoBehaviour
{
    public int killcount = 0;
    int enemycount = 40;
    public Text enemyStatus;

    // Start is called before the first frame update
    void Start()
    {
        enemyStatus.text = "좀비 : " + killcount + "/" + enemycount;
    }

    // Update is called once per frame
    void Update()
    {
        if(killcount == enemycount)
        {
            SceneManager.LoadScene("Scene3Start");
        }
    }

    public void addkill()
    {
        ++killcount;
        enemyStatus.text = "좀비 : " + killcount + "/" + enemycount;
        print(killcount);
    }
}
