﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerInput : MonoBehaviour
{
    public static Vector3[] effect_positions;
    [SerializeField] GameObject[] judgePrefab = null;//판정 이미지
    [SerializeField] GameObject[] judgePos=null;

    void set_effect_positions()
    {
        effect_positions=new Vector3[16];

        effect_positions[15] = new Vector3(-1.131f,0.15f,-0.3f);            
        effect_positions[14] = new Vector3(-0.827f,0.15f,-0.3f);
        effect_positions[13] = new Vector3(-0.462f,0.15f,-0.3f);
        effect_positions[12] = new Vector3(-0.159f,0.15f,-0.3f);
        effect_positions[11] = new Vector3(0.148f,0.15f,-0.3f);
        effect_positions[10] = new Vector3(0.433f,0.15f,-0.3f);
        effect_positions[9] = new Vector3(0.827f,0.15f,-0.3f);
        effect_positions[8] = new Vector3(1.132f,0.15f,-0.3f);

        effect_positions[7] = new Vector3(1.131f,-0.181f,-0.3f);
        effect_positions[6] = new Vector3(-0.827f,-0.181f,-0.3f);
        effect_positions[5] = new Vector3(-0.462f,-0.181f,-0.3f);
        effect_positions[4] = new Vector3(-0.159f,-0.181f,-0.3f);
        effect_positions[3] = new Vector3(0.148f,-0.181f,-0.3f);
        effect_positions[2] = new Vector3(0.433f,-0.181f,-0.3f);
        effect_positions[1] = new Vector3(0.0003f,-0.181f,-0.3f);
        effect_positions[0] = new Vector3(1.132f,-0.181f,-0.3f);
    }

    TimingManager theTimingManager;
    public int timeVal;
    public static int playScore;
   

    private void Start()
    {
        theTimingManager = FindObjectOfType<TimingManager>();
        set_effect_positions();
    }

    void Update()
    {
        if (collision.hitCheck!=-1)//검사 후 돌 상태 -1로 돌려놓기.
        {
            timeVal=theTimingManager.CheckTiming();//가장 맞는 것 부터 0 1 2 3
            if (TimingManager.noteAns == collision.hitCheck)
            {
                feedbackAnim();
                playScore += (10 - timeVal * 2);//점수를 더해서 넣어준다.
                Debug.Log("올바른 돌 : "+collision.hitCheck);
                Debug.Log("$$누적 점수$$ " + playScore);
            }
            else
            {//디버깅용
                Debug.Log("틀린 돌 "+collision.hitCheck+"정답은  "+TimingManager.noteAns);
            }
            collision.hitCheck = -1;//검사 완료 후 돌 상태 -1로 돌려놓기
        }

        if (OVRInput.GetDown(OVRInput.Button.Two, OVRInput.Controller.Touch))
        {
            SceneManager.LoadScene("RealMain");
        }
    }

    void feedbackAnim()
    {
        Debug.Log("In Anim");
        judgePrefab[0].SetActive(false);//숨겼다가
        judgePrefab[1].SetActive(false);//숨겼다가
        judgePrefab[2].SetActive(false);//숨겼다가
        judgePrefab[3].SetActive(false);//숨겼다가
        judgePos[timeVal].transform.localPosition = effect_positions[TimingManager.noteAns];//옮기고
      //  judgePrefab[timeVal].transform.position = new Vector3(0f,0f,0f);//옮기고

        judgePrefab[timeVal].SetActive(true);//나타나기.
    }

   

}