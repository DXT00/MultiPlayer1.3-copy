using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    Game game;
    bool flag = true;
    ViewPlayer currentViewPlayer;

    public Vector3 offset;
    private float smoothing = 5f;

    public void bind_currentViewPlayer(ViewPlayer currentViewPlayer)
    {
        this.currentViewPlayer = currentViewPlayer;
    }

    public void bind_Game(Game game)
    {
        this.game = game;
    }
   

    public void CameraUpdate()
    {
        //if (game.start_flag && flag == true)
        //{
        //    flag = false;

        //    Vector3 cur_pos = new Vector3(currentViewPlayer.transform.position.x, currentViewPlayer.transform.position.y, currentViewPlayer.transform.position.z - 8f);

        //    transform.position = cur_pos;
        //    offset = transform.position - currentViewPlayer.transform.position;

        //}
        if (game.start_flag)
        {
            // Vector3 viewPlayer_pos = new Vector3(currentViewPlayer.transform.position.x, currentViewPlayer.transform.position.y, currentViewPlayer.transform.position.z);


            //new Vector3(currentViewPlayer.transform.position.x, currentViewPlayer.transform.position.y, currentViewPlayer.transform.position.z) + offset;//new Vector3(currentViewPlayer.transform.position.x, transform.position.y, currentViewPlayer.transform.position.z - 8f);//

            //Vector3 tartgetCamPos = currentViewPlayer.transform.position;

            transform.position = currentViewPlayer.transform.position;
            Vector3 playerForward = currentViewPlayer.transform.TransformDirection(Vector3.right);//把player的x方向转换为世界坐标系

            transform.rotation = Quaternion.LookRotation(playerForward);
            transform.Translate(Vector3.back * 8f);

        // transform.position = Vector3.Lerp(transform.position, tartgetCamPos, smoothing * Time.deltaTime);//tartgetCamPos;

        }

    }




}

