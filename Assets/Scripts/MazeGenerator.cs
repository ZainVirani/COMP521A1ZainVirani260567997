using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeGenerator : MonoBehaviour {

    float x = 261.39f;
    float y = -47.81f;
    float z = 242.06f;
    int alcoves = 3;
    char direction = 'N'; //assume N is towards the finish
    GameObject corridor;
    GameObject alcove;
    GameObject alcoveCorridor;
    GameObject left;
    GameObject right;
    GameObject mazePiece;
    GameObject alcovePiece;
    GameObject[,] cells = new GameObject[5,14]; 
    int currentX = 2;
    int currentY = 0;
    int nextX = 2;
    int nextY = 0;
    //int message = 0;
    int cell = -1;
    int maxLength = 13;
    /*
    rules:
    
    */
    // Use this for initialization
    void Start() {
        //Debug.Log("Maze Generation start " + message);
        //message++;
        corridor = Resources.Load("Corridor") as GameObject;
        alcove = Resources.Load("Alcove") as GameObject;
        alcoveCorridor = Resources.Load("AlcoveCorridor") as GameObject;
        left = Resources.Load("Left") as GameObject;
        right = Resources.Load("Right") as GameObject;
        while(currentY < maxLength) //until we reach the final cell
        {
            //Debug.Log("Maze Generation step " + message);
            //message++;
            //randomly choose a frontier cell that has not been visited
            nextX = currentX;
            nextY = currentY;
            if (currentY == 0 && currentX == 0)
            {
                cell = Random.Range(2, 4); //don't consider down or left
            }
            else if (currentY == 0 && currentX == 4)
            {
                cell = Random.Range(1, 3); //don't consider down or right
            }
            else if (currentY == maxLength && currentX == 4)
            {
                cell = Random.Range(0, 2); //don't consider up or right
            }
            else if (currentY == maxLength && currentX == 0)
            {
                cell = Random.Range(1, 3); //don't consider up or left
                if (cell == 1)
                {
                    cell--;
                }
                else
                {
                    cell++;
                }
            }
            else if (currentX == 0)
            {
                cell = Random.Range(2, 4);
                /*while (cell == 1)
                {
                    cell = Random.Range(0, 4); //we dont consider left cell
                }*/
            }
            else if (currentX == 4)
            {
                cell = Random.Range(1, 4);
                while (cell == 3)
                {
                    cell = Random.Range(1, 4); //we dont consider right cell
                }
            }
            else if (currentY == 0)
            {
                cell = Random.Range(1, 4);
                /*while (cell == 0)
                {
                    cell = Random.Range(0, 4); //we dont consider down cell
                }*/
            }
            else if (currentY == maxLength)
            {
                cell = Random.Range(0, 4);
                while (cell == 2)
                {
                    cell = Random.Range(0, 4); //we dont consider down cell
                }
            }
            else
            {
                cell = Random.Range(0, 4);
            }
            switch (cell) //get correct coordinates
            {
                case 0: //down
                    nextY--;
                    //direction = 'S';
                    break;
                case 1: //left
                    nextX--;
                    //direction = 'W';
                    break;
                case 2: //up
                    nextY++;
                    //direction = 'N';
                    break;
                case 3: //right
                    nextX++;
                    //direction = 'E';
                    break;
            }
            //Debug.Log("catch test " + nextX + " " + nextY);
            if(cells[nextX, nextY] != null)
            {
                //Debug.Log("CATCH");
                switch (cell) //get correct coordinates
                {
                    case 0: //down
                        nextY++;
                        //direction = 'S';
                        break;
                    case 1: //left
                        nextX++;
                        //direction = 'W';
                        break;
                    case 2: //up
                        nextY--;
                        //direction = 'N';
                        break;
                    case 3: //right
                        nextX--;
                        //direction = 'E';
                        break;
                }
                continue;
            }
            if(currentX > 0 && currentX < 4 && currentY < 14 && currentY > 0)
            {
                if (cells[currentX, currentY + 1] != null && cells[currentX, currentY - 1] != null && cells[currentX - 1, currentY] != null && cells[currentX + 1, currentY] != null)
                {
                    //Debug.Log("CATCH 22");
                    currentX = 2;
                    currentY = 0;
                    for (int i = 0; i < 5; i++)
                    {
                        for (int j = 0; j < 14; j++)
                        {
                            cells[i, j] = null;
                        }
                    }
                    nextX = 2;
                    nextY = 0;
                    continue;
                }
            }
            
            //Debug.Log("cell chosen: " + cell + " " + message);
            //message++;
            //attempts = new int[4];
            //fill in current cell with the right cell type
            switch (cell) //get correct coordinates
            {
                case 0: //down
                    switch (direction) //where we're facing coming into the next cell
                    {
                        case 'N': //North
                            //Debug.Log("impossible case 1");
                            break; //impossible, to face north we came from south
                        case 'E': //East
                                  //right turn
                            cells[currentX, currentY] = right; // Instantiate(right) as GameObject;
                            break;
                        case 'W': //West
                            cells[currentX, currentY] = left;// Instantiate(left) as GameObject;
                            break;
                        case 'S': //South
                            cells[currentX, currentY] = corridor;// Instantiate(corridor) as GameObject;
                            break;
                    }
                    direction = 'S';
                    break;
                case 1: //left
                    switch (direction)
                    {
                        case 'N': //North
                            cells[currentX, currentY] = left;// Instantiate(left) as GameObject;
                            break;
                        case 'E': //East
                            //Debug.Log("impossible case 2");
                            break; //impossible
                        case 'W': //West
                            cells[currentX, currentY] = corridor;// Instantiate(corridor) as GameObject;
                            break;
                        case 'S': //South
                            cells[currentX, currentY] = right;// Instantiate(right) as GameObject;
                            break;
                    }
                    direction = 'W';
                    break;
                case 2: //up
                    switch (direction)
                    {
                        case 'N': //North
                            cells[currentX, currentY] = corridor;// Instantiate(corridor) as GameObject;
                            break;
                        case 'E': //East
                            cells[currentX, currentY] = left;// Instantiate(left) as GameObject;
                            break;
                        case 'W': //West
                            cells[currentX, currentY] = right;// Instantiate(right) as GameObject;
                            break;
                        case 'S': //South
                            //Debug.Log("impossible case 3");
                            break; //impossible
                    }
                    direction = 'N';
                    break;
                case 3: //right
                    switch (direction)
                    {
                        case 'N': //North
                            cells[currentX, currentY] = right;// Instantiate(right) as GameObject;
                            break;
                        case 'E': //East
                            cells[currentX, currentY] = corridor;// Instantiate(corridor) as GameObject;
                            break;
                        case 'W': //West
                            //Debug.Log("impossible case 4");
                            break; //impossible
                        case 'S': //South
                            cells[currentX, currentY] = left;// Instantiate(left) as GameObject;
                            break;
                    }
                    direction = 'E';
                    break;
            }
            //Debug.Log(cells[currentX, currentY].name + " " + currentX + " " + currentY + " " + message);
            //message++;
            //Debug.Log(nextX + " " + nextY + " " + message);
            //message++;
            currentX = nextX; //update coordinates
            currentY = nextY;


        }

        //we are now on the last row
        //we need to turn in the right direction to face the exit

        if(currentX == 2)
        {
            switch (direction)
            {
                case 'N': //North
                    cells[currentX, currentY] = corridor;// Instantiate(right) as GameObject;
                    break;
                case 'E': //East
                    cells[currentX, currentY] = left;// Instantiate(corridor) as GameObject;
                    break;
                case 'W': //West
                    //Debug.Log("impossible case 4");
                    break; //impossible
                case 'S': //South
                    cells[currentX, currentY] = right;// Instantiate(left) as GameObject;
                    break;
            }
        }
        else if(currentX == 0) //turn right
        {
            cells[currentX, currentY] = right;// Instantiate(right) as GameObject;
            cells[currentX + 1, currentY] = corridor;
            cells[currentX + 2, currentY] = left;
            //Debug.Log(cells[currentX, currentY].name + " " + message);
            //Debug.Log(cells[currentX + 1, currentY].name + " " + message);
            //Debug.Log(cells[currentX + 2, currentY].name + " " + message);

        }
        else if (currentX == 1) //turn right
        {
            cells[currentX, currentY] = right;
            cells[currentX + 1, currentY] = left;
            //Debug.Log(cells[currentX, currentY].name + " " + message);
            //Debug.Log(cells[currentX + 1, currentY].name + " " + message);
        }
        else if(currentX == 3)
        {
            cells[currentX, currentY] = left;
            cells[currentX - 1, currentY] = right;
            //Debug.Log(cells[currentX, currentY].name + " " + message);
            //Debug.Log(cells[currentX - 1, currentY].name + " " + message);
        }
        else if (currentX == 4)
        {
            cells[currentX, currentY] = left;// Instantiate(right) as GameObject;
            cells[currentX - 1, currentY] = corridor;
            cells[currentX - 2, currentY] = right;
            //Debug.Log(cells[currentX, currentY].name + " " + message);
            //Debug.Log(cells[currentX - 1, currentY].name + " " + message);
            //Debug.Log(cells[currentX - 2, currentY].name + " " + message);
        }

        //we have now set up all cells except alcoves
        //set up alcove cells next to corridors, change corridors to alcoveCorridors

        //instantiate pieces with rotation
        currentX = 2;
        currentY = 0;
        char prevDir = 'N';
        while(currentY < maxLength + 1)
        {
            
            
            mazePiece = Instantiate(cells[currentX, currentY]) as GameObject;
            mazePiece.transform.position = new Vector3(x, y, z);
            switch (mazePiece.name)
            {
                case "Corridor(Clone)":
                    //Debug.Log("Cor");
                    switch (prevDir)
                    {
                        case 'N':
                            int chanceN = Random.Range(0, 2);
                            if (alcoves > 0 && chanceN == 1) //random alcove
                            {
                                if(currentX != 0 && currentX != 4 && cells[currentX - 1, currentY] == null && cells[currentX + 1, currentY] == null)
                                {
                                    int choice = Random.Range(0, 2);
                                    if(choice == 0)
                                    {
                                        Destroy(mazePiece);
                                        mazePiece = Instantiate(alcoveCorridor) as GameObject;
                                        mazePiece.transform.position = new Vector3(x, y, z);
                                        alcovePiece = Instantiate(alcove) as GameObject;
                                        //Debug.Log("RANDOM ALCOVE");
                                        alcoves--;
                                        alcovePiece.transform.position = new Vector3(x - 6, y, z);
                                        mazePiece.transform.localRotation *= Quaternion.Euler(0, 180, 0);
                                        alcovePiece.transform.localRotation *= Quaternion.Euler(0, 180, 0);
                                        Vector3 v3 = mazePiece.transform.position;
                                        v3.x += 2.07f;
                                        v3.z += -2.19f;
                                        mazePiece.transform.position = v3;
                                        Vector3 v3a = alcovePiece.transform.position;
                                        v3a.x += 2.29f;
                                        v3a.z += -2.21f;
                                        alcovePiece.transform.position = v3a;
                                    }
                                    else
                                    {
                                        Destroy(mazePiece);
                                        mazePiece = Instantiate(alcoveCorridor) as GameObject;
                                        mazePiece.transform.position = new Vector3(x, y, z);
                                        alcovePiece = Instantiate(alcove) as GameObject;
                                        //Debug.Log("RANDOM ALCOVE");
                                        alcoves--;
                                        alcovePiece.transform.position = new Vector3(x + 6, y, z);
                                    }
                                }
                                else if(currentX != 0 && cells[currentX - 1,currentY] == null) //another valid spot
                                {
                                    Destroy(mazePiece);
                                    mazePiece = Instantiate(alcoveCorridor) as GameObject;
                                    mazePiece.transform.position = new Vector3(x, y, z);
                                    alcovePiece = Instantiate(alcove) as GameObject;
                                    //Debug.Log("RANDOM ALCOVE");
                                    alcoves--;
                                    alcovePiece.transform.position = new Vector3(x - 6, y, z);
                                    mazePiece.transform.localRotation *= Quaternion.Euler(0, 180, 0);
                                    alcovePiece.transform.localRotation *= Quaternion.Euler(0, 180, 0);
                                    Vector3 v3 = mazePiece.transform.position;
                                    v3.x += 2.07f;
                                    v3.z += -2.19f;
                                    mazePiece.transform.position = v3;
                                    Vector3 v3a = alcovePiece.transform.position;
                                    v3a.x += 2.29f;
                                    v3a.z += -2.21f;
                                    alcovePiece.transform.position = v3a;
                                }
                                else if(currentX != 4 && cells[currentX + 1, currentY] == null) //another valid spot
                                {
                                    Destroy(mazePiece);
                                    mazePiece = Instantiate(alcoveCorridor) as GameObject;
                                    mazePiece.transform.position = new Vector3(x, y, z);
                                    alcovePiece = Instantiate(alcove) as GameObject;
                                    //Debug.Log("RANDOM ALCOVE");
                                    alcoves--;
                                    alcovePiece.transform.position = new Vector3(x + 6, y, z);
                                }
                                else if (currentX == 0 || currentX == 4) //valid spot
                                {
                                    Destroy(mazePiece);
                                    mazePiece = Instantiate(alcoveCorridor) as GameObject;
                                    mazePiece.transform.position = new Vector3(x, y, z);
                                    alcovePiece = Instantiate(alcove) as GameObject;
                                    //Debug.Log("RANDOM ALCOVE");
                                    alcoves--;
                                    if (currentX == 0)
                                    {
                                        alcovePiece.transform.position = new Vector3(x - 6, y, z);
                                        mazePiece.transform.localRotation *= Quaternion.Euler(0, 180, 0);
                                        alcovePiece.transform.localRotation *= Quaternion.Euler(0, 180, 0);
                                        Vector3 v3 = mazePiece.transform.position;
                                        v3.x += 2.07f;
                                        v3.z += -2.19f;
                                        mazePiece.transform.position = v3;
                                        Vector3 v3a = alcovePiece.transform.position;
                                        v3a.x += 2.29f;
                                        v3a.z += -2.21f;
                                        alcovePiece.transform.position = v3a;
                                    }
                                    else
                                    {
                                        alcovePiece.transform.position = new Vector3(x + 6, y, z);
                                    }
                                }
                            }
                            currentY++;
                            z += 4;
                            break;
                        case 'E':
                            /*int chanceE = Random.Range(0, 10);
                            if (alcoves > 0 )//&& chanceE == 4) //random alcove
                            {
                                if (currentY != 0 && currentY != 13 && cells[currentX, currentY-1] == null && cells[currentX, currentY+1] == null)
                                {
                                    int choice = Random.Range(0, 2);
                                    if (choice == 0)
                                    {
                                        Destroy(mazePiece);
                                        mazePiece = Instantiate(alcoveCorridor) as GameObject;
                                        mazePiece.transform.position = new Vector3(x, y, z);
                                        alcovePiece = Instantiate(alcove) as GameObject;
                                        alcovePiece.transform.localRotation *= Quaternion.Euler(0, 90, 0);
                                        //Debug.Log("RANDOM ALCOVE");
                                        alcoves--;
                                        alcovePiece.transform.position = new Vector3(x + 6, y, z);
                                       
                                        Vector3 v3 = mazePiece.transform.position;
                                        v3.x += 2.12f;
                                        mazePiece.transform.position = v3;
                                        mazePiece.transform.localRotation *= Quaternion.Euler(0, 90, 0);
                                        
                                    }
                                    else
                                    {
                                        Destroy(mazePiece);
                                        mazePiece = Instantiate(alcoveCorridor) as GameObject;
                                        mazePiece.transform.position = new Vector3(x, y, z);
                                        
                                        Vector3 v3 = mazePiece.transform.position;
                                        v3.z += -2.17f;
                                        mazePiece.transform.position = v3;
                                        mazePiece.transform.localRotation *= Quaternion.Euler(0, -90, 0);
                                        alcovePiece = Instantiate(alcove) as GameObject;
                                        //Debug.Log("RANDOM ALCOVE");
                                        alcoves--;
                                        alcovePiece.transform.localRotation *= Quaternion.Euler(0, -90, 0);
                                        alcovePiece.transform.position = new Vector3(x + 6, y, z);
                                    }
                                }
                                else if (currentY != 0 && cells[currentX, currentY-1] == null) //another valid spot
                                {
                                    Destroy(mazePiece);
                                    mazePiece = Instantiate(alcoveCorridor) as GameObject;
                                    mazePiece.transform.position = new Vector3(x, y, z);
                                    alcovePiece = Instantiate(alcove) as GameObject;
                                    alcovePiece.transform.localRotation *= Quaternion.Euler(0, 90, 0);
                                    //Debug.Log("RANDOM ALCOVE");
                                    alcoves--;
                                    alcovePiece.transform.position = new Vector3(x + 6, y, z);

                                    Vector3 v3 = mazePiece.transform.position;
                                    v3.x += 2.12f;
                                    mazePiece.transform.position = v3;
                                    mazePiece.transform.localRotation *= Quaternion.Euler(0, 90, 0);
                                }
                                else if (currentY != 13 && cells[currentX, currentY+1] == null) //another valid spot
                                {
                                    Destroy(mazePiece);
                                    mazePiece = Instantiate(alcoveCorridor) as GameObject;
                                    mazePiece.transform.position = new Vector3(x, y, z);

                                    Vector3 v3 = mazePiece.transform.position;
                                    v3.z += -2.17f;
                                    mazePiece.transform.position = v3;
                                    mazePiece.transform.localRotation *= Quaternion.Euler(0, -90, 0);
                                    alcovePiece = Instantiate(alcove) as GameObject;
                                    //Debug.Log("RANDOM ALCOVE");
                                    alcoves--;
                                    alcovePiece.transform.localRotation *= Quaternion.Euler(0, -90, 0);
                                    alcovePiece.transform.position = new Vector3(x + 6, y, z);
                                }
                                else if (currentY == 0 || currentY == 13) //valid spot
                                {
                                    if (currentY == 0)
                                    {
                                        Destroy(mazePiece);
                                        mazePiece = Instantiate(alcoveCorridor) as GameObject;
                                        mazePiece.transform.position = new Vector3(x, y, z);
                                        alcovePiece = Instantiate(alcove) as GameObject;
                                        alcovePiece.transform.localRotation *= Quaternion.Euler(0, 90, 0);
                                        //Debug.Log("RANDOM ALCOVE");
                                        alcoves--;
                                        alcovePiece.transform.position = new Vector3(x + 6, y, z);

                                        Vector3 v3 = mazePiece.transform.position;
                                        v3.x += 2.12f;
                                        mazePiece.transform.position = v3;
                                        mazePiece.transform.localRotation *= Quaternion.Euler(0, 90, 0);
                                    }
                                    else
                                    {
                                        Destroy(mazePiece);
                                        mazePiece = Instantiate(alcoveCorridor) as GameObject;
                                        mazePiece.transform.position = new Vector3(x, y, z);

                                        Vector3 v3 = mazePiece.transform.position;
                                        v3.z += -2.17f;
                                        mazePiece.transform.position = v3;
                                        mazePiece.transform.localRotation *= Quaternion.Euler(0, -90, 0);
                                        alcovePiece = Instantiate(alcove) as GameObject;
                                        //Debug.Log("RANDOM ALCOVE");
                                        alcoves--;
                                        alcovePiece.transform.localRotation *= Quaternion.Euler(0, -90, 0);
                                        alcovePiece.transform.position = new Vector3(x + 6, y, z);
                                    }
                                }
                            }*/
                            currentX++;
                            x += 4;
                            mazePiece.transform.localRotation *= Quaternion.Euler(0, 90, 0); // this add a 90 degrees rotation
                            Vector3 v30 = mazePiece.transform.position;
                            v30.x += 2.15f;
                            mazePiece.transform.position = v30;
                            break;
                        case 'W':
                            currentX--;
                            x -= 4;
                            mazePiece.transform.localRotation *= Quaternion.Euler(0, 90, 0); // this add a 90 degrees rotation
                            Vector3 v31 = mazePiece.transform.position;
                            v31.x += 2.15f;
                            mazePiece.transform.position = v31;
                            break;
                        case 'S':
                            int chanceS = Random.Range(0, 2);
                            //Debug.Log("SOUTH");
                            if (alcoves > 0 && chanceS == 1) //random alcove
                            {
                                if (currentX != 0 && currentX != 4 && cells[currentX - 1, currentY] == null && cells[currentX + 1, currentY] == null)
                                {
                                    int choice = Random.Range(0, 2);
                                    if (choice == 0)
                                    {
                                        Destroy(mazePiece);
                                        mazePiece = Instantiate(alcoveCorridor) as GameObject;
                                        mazePiece.transform.position = new Vector3(x, y, z);
                                        alcovePiece = Instantiate(alcove) as GameObject;
                                        //Debug.Log("RANDOM ALCOVE");
                                        alcoves--;
                                        alcovePiece.transform.position = new Vector3(x - 6, y, z);
                                        mazePiece.transform.localRotation *= Quaternion.Euler(0, 180, 0);
                                        alcovePiece.transform.localRotation *= Quaternion.Euler(0, 180, 0);
                                        Vector3 v3 = mazePiece.transform.position;
                                        v3.x += 2.07f;
                                        v3.z += -2.19f;
                                        mazePiece.transform.position = v3;
                                        Vector3 v3a = alcovePiece.transform.position;
                                        v3a.x += 2.29f;
                                        v3a.z += -2.21f;
                                        alcovePiece.transform.position = v3a;
                                    }
                                    else
                                    {
                                        Destroy(mazePiece);
                                        mazePiece = Instantiate(alcoveCorridor) as GameObject;
                                        mazePiece.transform.position = new Vector3(x, y, z);
                                        alcovePiece = Instantiate(alcove) as GameObject;
                                        //Debug.Log("RANDOM ALCOVE");
                                        alcoves--;
                                        alcovePiece.transform.position = new Vector3(x + 6, y, z);
                                    }
                                }
                                else if (currentX != 0 && cells[currentX - 1, currentY] == null) //another valid spot
                                {
                                    Destroy(mazePiece);
                                    mazePiece = Instantiate(alcoveCorridor) as GameObject;
                                    mazePiece.transform.position = new Vector3(x, y, z);
                                    alcovePiece = Instantiate(alcove) as GameObject;
                                    //Debug.Log("RANDOM ALCOVE");
                                    alcoves--;
                                    alcovePiece.transform.position = new Vector3(x - 6, y, z);
                                    mazePiece.transform.localRotation *= Quaternion.Euler(0, 180, 0);
                                    alcovePiece.transform.localRotation *= Quaternion.Euler(0, 180, 0);
                                    Vector3 v3 = mazePiece.transform.position;
                                    v3.x += 2.07f;
                                    v3.z += -2.19f;
                                    mazePiece.transform.position = v3;
                                    Vector3 v3a = alcovePiece.transform.position;
                                    v3a.x += 2.29f;
                                    v3a.z += -2.21f;
                                    alcovePiece.transform.position = v3a;
                                }
                                else if (currentX != 4 && cells[currentX + 1, currentY] == null) //another valid spot
                                {
                                    Destroy(mazePiece);
                                    mazePiece = Instantiate(alcoveCorridor) as GameObject;
                                    mazePiece.transform.position = new Vector3(x, y, z);
                                    alcovePiece = Instantiate(alcove) as GameObject;
                                    //Debug.Log("RANDOM ALCOVE");
                                    alcoves--;
                                    alcovePiece.transform.position = new Vector3(x + 6, y, z);
                                }
                                else if (currentX == 0 || currentX == 4) //valid spot
                                {
                                    Destroy(mazePiece);
                                    mazePiece = Instantiate(alcoveCorridor) as GameObject;
                                    mazePiece.transform.position = new Vector3(x, y, z);
                                    alcovePiece = Instantiate(alcove) as GameObject;
                                    //Debug.Log("RANDOM ALCOVE");
                                    alcoves--;
                                    if (currentX == 0)
                                    {
                                        alcovePiece.transform.position = new Vector3(x - 6, y, z);
                                        mazePiece.transform.localRotation *= Quaternion.Euler(0, 180, 0);
                                        alcovePiece.transform.localRotation *= Quaternion.Euler(0, 180, 0);
                                        Vector3 v3 = mazePiece.transform.position;
                                        v3.x += 2.07f;
                                        v3.z += -2.19f;
                                        mazePiece.transform.position = v3;
                                        Vector3 v3a = alcovePiece.transform.position;
                                        v3a.x += 2.29f;
                                        v3a.z += -2.21f;
                                        alcovePiece.transform.position = v3a;
                                    }
                                    else
                                    {
                                        alcovePiece.transform.position = new Vector3(x + 6, y, z);
                                    }
                                }
                            }
                            currentY--;
                            z -= 4;
                            break;
                    }
                    break;
                case "Left(Clone)":
                    //Debug.Log("Lef");
                    switch (prevDir)
                    {
                        case 'N':
                            prevDir = 'W';
                            currentX--;
                            x -= 4;
                            break;
                        case 'E':
                            prevDir = 'N';
                            currentY++;
                            mazePiece.transform.localRotation *= Quaternion.Euler(0, 90, 0);
                            Vector3 v30 = mazePiece.transform.position;
                            v30.x += 2.35f;
                            mazePiece.transform.position = v30;
                            z += 4;
                            break;
                        case 'W':
                            prevDir = 'S';
                            currentY--;
                            mazePiece.transform.localRotation *= Quaternion.Euler(0, -90, 0);
                            Vector3 v31 = mazePiece.transform.position;
                            v31.z += -2.323f;
                            mazePiece.transform.position = v31;
                            z -= 4;
                            break;
                        case 'S':
                            prevDir = 'E';
                            currentX++;
                            mazePiece.transform.localRotation *= Quaternion.Euler(0, 180, 0);
                            Vector3 v32 = mazePiece.transform.position;
                            v32.x += 2.385f;
                            v32.z += -2.264f;
                            mazePiece.transform.position = v32;
                            x += 4;
                            break;
                    }
                    break;
                case "Right(Clone)":
                    //Debug.Log("Rig");
                    switch (prevDir)
                    {
                        case 'S':
                            prevDir = 'W';
                            currentX--;
                            mazePiece.transform.localRotation *= Quaternion.Euler(0, 180, 0);
                            Vector3 v30 = mazePiece.transform.position;
                            v30.x += 2.21f;
                            v30.z += -2.36f;
                            mazePiece.transform.position = v30;
                            x -= 4;
                            break;
                        case 'W':
                            prevDir = 'N';
                            currentY++;
                            mazePiece.transform.localRotation *= Quaternion.Euler(0, -90, 0);
                            Vector3 v31 = mazePiece.transform.position;
                            v31.z += -2.3f;
                            mazePiece.transform.position = v31;
                            z += 4;
                            break;
                        case 'E':
                            prevDir = 'S';
                            currentY--;
                            mazePiece.transform.localRotation *= Quaternion.Euler(0, 90, 0);
                            Vector3 v32 = mazePiece.transform.position;
                            v32.x += 2.28f;
                            mazePiece.transform.position = v32;
                            z -= 4;
                            break;
                        case 'N':
                            prevDir = 'E';
                            currentX++;
                            x += 4;
                            break;
                    }
                    break;
            }
            
        }
        if (alcoves == 1) //not all alcoves placed
        {
            Destroy(GameObject.FindGameObjectWithTag("OPTION1"));
            //Debug.Log("one option");
        }
        if (alcoves == 2) //not all alcoves placed
        {
            Destroy(GameObject.FindGameObjectWithTag("OPTION1"));
            Destroy(GameObject.FindGameObjectWithTag("OPTION2"));
            //Debug.Log("two option");
        }
        if (alcoves == 3) //not all alcoves placed
        {
            Destroy(GameObject.FindGameObjectWithTag("OPTION1"));
            Destroy(GameObject.FindGameObjectWithTag("OPTION2"));
            Destroy(GameObject.FindGameObjectWithTag("OPTION3"));
            //Debug.Log("three option");
        }
        //Debug.Log("ALCOVES PRESENT: " + (3 - alcoves));
    }

    // Update is called once per frame
    void Update() {
        
	}
}
