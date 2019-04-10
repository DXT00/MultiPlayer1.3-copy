using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;


public class ViewPlayer : MonoBehaviour
{
    GameObject player_instance;
    public int connectID;//与Player的connectID相同；


    readonly float speed = 60f;
    readonly float rotate_speed = 10f;
    readonly float gravity = 2f;
    public GameObject BulletPrefabs;
    public Transform bulletSpawn;

    Rigidbody PlayerRigidbody;
    List<CustomSyncMsg> msg_list;

    int touchTerrianMask;
    float max_y = 100f;



    float z = 0;
    float y = 0;
    float last_y = 2f;
    float x = 0;



    // float dist_sensitive = 20f;
    float rot_x = 0;
    float rot_y = 0;

    int key_x1 = 0;
    int key_x2 = 0;
    Transform Transform;

    public CameraFollow camera;
    public void Start()
    {
        msg_list = new List<CustomSyncMsg>();
        PlayerRigidbody = GetComponent<Rigidbody>();
        touchTerrianMask = LayerMask.GetMask("touchTerrian");
        Transform = gameObject.GetComponent<Transform>();
        PlayerRigidbody.freezeRotation = true;
    }

    public void FixedUpdate()
    {
        PlayerRigidbody.AddForce(Physics.gravity*5f, ForceMode.Acceleration);//给player添加重力
        x += Input.GetAxis("Horizontal");
        z += Input.GetAxis("Vertical");

        if (Input.GetKeyDown(KeyCode.Q))
        {
            key_x1++;
            rot_x = 0;
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            key_x2++;
            rot_x = 0;
        }

        if (Input.GetKeyUp(KeyCode.Q))
        {
            rot_x += key_x1 * (-rotate_speed);
            key_x1 = 0;
        }
        if (Input.GetKeyUp(KeyCode.E))
        {
            rot_x += key_x2 * (rotate_speed);
            key_x2 = 0;
        }

    }

    public List<CustomSyncMsg> get_local_input()
    {


        msg_list.Clear();
        if (x != 0 || z != 0)
        {
            CustomSyncMsg input_msg = new InputMessage(connectID, new Vector3(x, 0, z));
            msg_list.Add(input_msg);
        }

        x = 0;
        z = 0;
 
        if (rot_x != 0f || rot_y != 0f)
        {
            CustomSyncMsg rot_msg = new RotateMessage(connectID, new Vector2(rot_x, rot_y));
            msg_list.Add(rot_msg);
            rot_x = 0;
            rot_y = 0;
        }

        return msg_list;
    }
    public void Move(float horizontal, float vertical)//, float y
    {
  
      //  Vector3 cur_pos= Transform.position +(-Transform.forward )* horizontal * speed * 0.025f + Transform.right * vertical * speed * 0.025f +(- Transform.up) * 100f;


        // PlayerRigidbody.MovePosition(cur_pos);

        PlayerRigidbody.AddForce((-Transform.forward) * horizontal * speed );

        PlayerRigidbody.AddForce(Transform.right * vertical * speed);
       // PlayerRigidbody.AddForce((-Transform.up) * gravity);


        //Transform.position = new Vector3(Transform.position.x, y, Transform.position.z);




        // Debug.Log("PlayerRigidbody.position + movement = " + (PlayerRigidbody.position + movement).x + "PlayerRigidbody.position + movement " + (PlayerRigidbody.position + movement).y);

        //PlayerRigidbody.AddForce(transform.right * horizontal * speed, ForceMode.Impulse);
        //PlayerRigidbody.AddForce(transform.forward * vertical * speed, ForceMode.Impulse);
    }

    public void Rotate(float delta_x, float delta_y)
    {
        //现在只有绕自己的y轴旋转
        Transform.Rotate(Vector3.up, delta_x, Space.Self);

        Tool.Print("......................Rotating .........delta_x = " + delta_x.ToString());
    }


    void CmdShooting()
    {

        //GameObject bullet = Instantiate(BulletPrefabs, bulletSpawn.position, bulletSpawn.rotation);
        //bullet.GetComponent<Rigidbody>().velocity = transform.forward * 6f;


        //NetworkServer.Spawn(bullet);

        //Destroy(bullet, 2f);

    }


    public float move_on_the_ground()
    {
        Ray ray = new Ray(transform.position, -transform.up);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, max_y, touchTerrianMask))
        {
            return hit.point.y + 1f;
        }
        else
            return -101;
    }


    public void set_player_instance(GameObject instance)
    {
        player_instance = instance;
    }
    public float get_speed()
    {
        return speed;
    }
    public Rigidbody get_Rigidbody()
    {
        return PlayerRigidbody;
    }

    public void bind_cameraFollow(CameraFollow cameraFollow)
    {
        camera = cameraFollow;
    }
    public void bind_playerInstance(GameObject Instance)
    {
        player_instance = Instance;
    }
}

