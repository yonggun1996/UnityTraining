using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    PlayerMove player;//플레이어의 체력을 가져오기 위해 선언
    public static GameManager gm;
    public GameObject gameLabel;
    Text gameText;

    private void Awake()
    {
        if (gm == null)
        {
            gm = this;//자기자신을 (메모리)할당해줍니다.
        }
    }

    public enum GameState
    {
        //게임의 상태를 나타내는 enum
        Ready, Run, GameOver
    }

    public GameState gState;
    // Start is called before the first frame update
    void Start()
    {
        gState = GameState.Ready;//우선은 준비상태로둔다.
        gameText = gameLabel.GetComponent<Text>();//게임 내 텍스프를 Ready로 설정
        gameText.text = "Ready...";
        gameText.color = new Color32(255, 185, 0, 255);//색상설정
        StartCoroutine(ReadyToStart());//게임 준비 -> 게임 실행상태로 하기 위한 함수호출
        player = GameObject.Find("Player").GetComponent<PlayerMove>();
    }

    IEnumerator ReadyToStart()
    {
        //2초동안 대기 후 Go로 텍스트를 바꿔준다.
        //0.5초동안 Go를 보여주고 게임이 실행된다
        //yield를 두번 쓸 수 있다.
        yield return new WaitForSeconds(2f);
        gameText.text = "Go!";

        yield return new WaitForSeconds(0.5f);
        gameLabel.SetActive(false);
        gState = GameState.Run;
    }

    // Update is called once per frame
    void Update()
    {
        if(player.hp <= 0)
        {
            gameText.text = "Game Over";
            gameLabel.SetActive(true);
            gameText.color = new Color32(255, 0, 0, 0);

            gState = GameState.GameOver;
        }
    }
}
