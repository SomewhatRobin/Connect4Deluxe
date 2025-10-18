using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvisHand : MonoBehaviour
{
    //public GameObject theHand;
    public GameObject Player1Chip;
    public GameObject Player2Chip;
    public GameObject GarbChip;



    private GameObject nowPlaying;


    public KeyCode PlaceKey;
    public KeyCode AbiliKey;
    public KeyCode LeftKey;
    public KeyCode RightKey;



   // private bool myWay = true;
    public int selColumn = 3;
    public GameObject[] handOver;
    public GameObject[] starsAbove;

    // Start is called before the first frame update
    void Start()
    {
        nowPlaying = Player1Chip;
        transform.position = new Vector3 (handOver[3].transform.position.x, handOver[3].transform.position.y + 0.5f, handOver[3].transform.position.z);
    }



    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(PlaceKey))
        {
            PlaceChip(nowPlaying, selColumn);
            //PlaceChip calls SwapTurn
        }

        if (Input.GetKeyDown(LeftKey))
        {
            if(selColumn > 0)
            {
                selColumn--;
                transform.position = handOver[selColumn].transform.position;
            }
            
            //Else play nuh-uh.wav

        }

        if (Input.GetKeyDown(RightKey))
        {
            if (selColumn < 6)
            {
                selColumn++;
                transform.position = handOver[selColumn].transform.position;
            }

            //Else play nuh-uh.wav

        }

        if (Input.GetKeyDown(AbiliKey))
        {
            BoardFill();
            //This takes turn, so it calls SwapTurn
            SwapTurn();
        }


        void PlaceChip(GameObject curPlayer, int column)
        {
            Instantiate(curPlayer, handOver[column].transform.position, Quaternion.identity);
            SwapTurn();
        }



        void SwapTurn()
        {


            if (nowPlaying == Player1Chip)
            {
                nowPlaying = Player2Chip;
            }
            else if (nowPlaying == Player2Chip)
            {
                nowPlaying = Player1Chip;
            }
            /* //Forbidden Spray Nozzle
            if(selColumn < 5 && selColumn > 1 && myWay)
                {
                    selColumn--;
                }
            else if(selColumn <= 1 && myWay)
                {
                    myWay = false;
                    selColumn++;
                }
            else if (selColumn < 5 && selColumn > 1 && !myWay)
                {
                    selColumn++;
                }
            else if (selColumn >= 5 && !myWay)
                {
                    myWay = true;
                    selColumn--;
                }
            else
                {
                //   Debug.Log("HELP!");
                return;
                }

            //End of Nozzle
            */

        }

        void BoardFill()
        {
            //Quaternion.Euler(0f,90f,0f)

            //Add check for col 0 full
            Instantiate(GarbChip, starsAbove[0].transform.position, Quaternion.Euler(0f, 90f, 0f));

            //Add check for col 1 full
            Instantiate(GarbChip, starsAbove[1].transform.position, Quaternion.Euler(0f, 90f, 0f));

            //Add check for col 2 full
            Instantiate(GarbChip, starsAbove[2].transform.position, Quaternion.Euler(0f, 90f, 0f));

            //Add check for col 3 full
            Instantiate(GarbChip, starsAbove[3].transform.position, Quaternion.Euler(0f, 90f, 0f));
            //Possibly add a win con if this makes col 3 full


            //Add check for col 4 full
            Instantiate(GarbChip, starsAbove[4].transform.position, Quaternion.Euler(0f, 90f, 0f));

            //Add check for col 5 full
            Instantiate(GarbChip, starsAbove[5].transform.position, Quaternion.Euler(0f, 90f, 0f));

            //Add check for col 6 full
            Instantiate(GarbChip, starsAbove[6].transform.position, Quaternion.Euler(0f, 90f, 0f));



        }

    }
    }

