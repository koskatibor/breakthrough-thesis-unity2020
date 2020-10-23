using UnityEngine;

public class SelectPawn : MonoBehaviour
{    
		Material WhitePawnMaterial;
		Material GreenPawnMaterial;		
		Material GreenCubeMaterial;
		Material RedCubeMaterial;
		FieldController Controller;
		GameObject ChessTable;

		// Start is called before the first frame update
		void Start()
    {
				GameObject materialLoader;
				ChessTable = GameObject.Find("chess-table");
				Controller = ChessTable.GetComponent<FieldController>();

				materialLoader = GameObject.Find("WhitePawn");
				WhitePawnMaterial = materialLoader.GetComponent<Renderer>().material;
				materialLoader = GameObject.Find("GreenPawn");
				GreenPawnMaterial = materialLoader.GetComponent<Renderer>().material;
				materialLoader = GameObject.Find("GreenCube");
				GreenCubeMaterial = materialLoader.GetComponent<Renderer>().material;
				materialLoader = GameObject.Find("RedCube");
				RedCubeMaterial = materialLoader.GetComponent<Renderer>().material;
		}

    // Update is called once per frame
    void Update()
    {
        if (mouseNotOver)
        {
            if (Input.GetMouseButtonDown(0))
            {
								this.gameObject.GetComponent<MeshRenderer>().material = WhitePawnMaterial;
								//thisPawn.material.color = originalColor;								
								isSelected = false;
            }
        }
    }		

    bool mouseNotOver = false;    
    public bool isSelected = false;

		private void OnMouseDown()
		{
				//Debug.Log("Rajta van az egér: " + this.name);
				mouseNotOver = false;
				if (Controller.playersTurn)
				{
						Controller.SetDefaultCubeColors();
						this.gameObject.GetComponent<MeshRenderer>().material = GreenPawnMaterial;
						//thisPawn.material.color = highlightedColor;
						isSelected = true;
						this.Move();
				}
				else
				{
						; //Ha nem a játékos köre, de próbál bábut választani.
				}
		}		

    void OnMouseExit()
    {
        mouseNotOver = true;
    }

    void Move()
    {
        int sor = 0, oszlop = 0;
        GameObject ChessTable = GameObject.Find("chess-table");
        FieldController Controller = ChessTable.GetComponent<FieldController>();

        if (Mathf.Approximately(this.transform.position.z,0.0f))
            sor = 7;
        else if (Mathf.Approximately(this.transform.position.z, -2.7f))
            sor = 6;
        else if (Mathf.Approximately(this.transform.position.z, -5.4f))
            sor = 5;
        else if (Mathf.Approximately(this.transform.position.z, -8.1f))
            sor = 4;
        else if (Mathf.Approximately(this.transform.position.z, -10.8f))
            sor = 3;
        else if (Mathf.Approximately(this.transform.position.z, -13.5f))
            sor = 2;
        else if (Mathf.Approximately(this.transform.position.z, -16.2f))
            sor = 1;
        else if (Mathf.Approximately(this.transform.position.z, -18.9f))
            sor = 0;


        if (Mathf.Approximately(this.transform.position.x, 0.0f))
            oszlop = 0;
        else if (Mathf.Approximately(this.transform.position.x, -2.7f))
            oszlop = 1;
        else if (Mathf.Approximately(this.transform.position.x, -5.4f))
            oszlop = 2;
        else if (Mathf.Approximately(this.transform.position.x, -8.1f))
            oszlop = 3;
        else if (Mathf.Approximately(this.transform.position.x, -10.8f))
            oszlop = 4;
        else if (Mathf.Approximately(this.transform.position.x, -13.5f))
            oszlop = 5;
        else if (Mathf.Approximately(this.transform.position.x, -16.2f))
            oszlop = 6;
        else if (Mathf.Approximately(this.transform.position.x, -18.9f))
            oszlop = 7;

        Debug.Log("Kijelölt elem! Sor: " + sor + " Oszlop: " + oszlop);

        if (sor!=0)
        {
            if (oszlop == 0)
            {
                //Bal szélen bábu kijelölve
                //Egyenes lépés
                if (Controller.Table[sor - 1, oszlop] == 0)
                {
                    for (int i = 0; i < 64; i++)
                    {
                        if (Mathf.Approximately(Controller.Cubes[i].transform.position.z, this.transform.position.z - 2.7f) && Mathf.Approximately(Controller.Cubes[i].transform.position.x, this.transform.position.x))
                        {
														Controller.Cubes[i].material = GreenCubeMaterial;
                            //Controller.Cubes[i].material.color = greenCube;
                        }
                    }
                }
                else
                {
                    for (int i = 0; i < 64; i++)
                    {
                        if (Mathf.Approximately(Controller.Cubes[i].transform.position.z, this.transform.position.z - 2.7f) && Mathf.Approximately(Controller.Cubes[i].transform.position.x, this.transform.position.x))
                        {
														Controller.Cubes[i].material = RedCubeMaterial;
														//Controller.Cubes[i].material.color = redCube;
                        }
                    }
                }

                //Előre Jobbra lépés
                if (Controller.Table[sor - 1, oszlop + 1] == 0 || Controller.Table[sor - 1, oszlop + 1] == 1)
                {
                    for (int i = 0; i < 64; i++)
                    {
                        if (Mathf.Approximately(Controller.Cubes[i].transform.position.z, this.transform.position.z - 2.7f) && Mathf.Approximately(Controller.Cubes[i].transform.position.x, this.transform.position.x - 2.7f))
                        {
														Controller.Cubes[i].material = GreenCubeMaterial;
														//Controller.Cubes[i].material.color = greenCube;
                        }
                    }
                }
                else
                {
                    for (int i = 0; i < 64; i++)
                    {
                        if (Mathf.Approximately(Controller.Cubes[i].transform.position.z, this.transform.position.z - 2.7f) && Mathf.Approximately(Controller.Cubes[i].transform.position.x, this.transform.position.x - 2.7f))
                        {
														Controller.Cubes[i].material = RedCubeMaterial;
														//Controller.Cubes[i].material.color = redCube;
                        }
                    }
                }
            }


            if (oszlop > 0 && oszlop < 7)
            {
                //Nem szélen van kijelölve
                //Előre Balra lépés                
                if (Controller.Table[sor - 1, oszlop - 1] == 0 || Controller.Table[sor - 1, oszlop - 1] == 1)
                {                    
                    for (int i = 0; i < 64; i++)
                    {
                        if (Mathf.Approximately(Controller.Cubes[i].transform.position.z, this.transform.position.z - 2.7f) && Mathf.Approximately(Controller.Cubes[i].transform.position.x, this.transform.position.x + 2.7f))
                        {
														Controller.Cubes[i].material = GreenCubeMaterial;
														//Controller.Cubes[i].material.color = greenCube;
                        }                        
                    }
                }
                else
                {
                    for (int i = 0; i < 64; i++)
                    {
                        if (Mathf.Approximately(Controller.Cubes[i].transform.position.z, this.transform.position.z - 2.7f) && Mathf.Approximately(Controller.Cubes[i].transform.position.x, this.transform.position.x + 2.7f))
                        {
														Controller.Cubes[i].material = RedCubeMaterial;
														//Controller.Cubes[i].material.color = redCube;
                        }
                    }
                }

                //Egyenes Lépés
                if (Controller.Table[sor - 1, oszlop] == 0)
                {
                    for (int i = 0; i < 64; i++)
                    {
                        if (Mathf.Approximately(Controller.Cubes[i].transform.position.z, this.transform.position.z - 2.7f) && Mathf.Approximately(Controller.Cubes[i].transform.position.x, this.transform.position.x))
                        {
														Controller.Cubes[i].material = GreenCubeMaterial;
														//Controller.Cubes[i].material.color = greenCube;
                        }
                    }
                }
                else
                {
                    for (int i = 0; i < 64; i++)
                    {
                        if (Mathf.Approximately(Controller.Cubes[i].transform.position.z, this.transform.position.z - 2.7f) && Mathf.Approximately(Controller.Cubes[i].transform.position.x, this.transform.position.x))
                        {
														Controller.Cubes[i].material = RedCubeMaterial;
														//Controller.Cubes[i].material.color = redCube;
                        }
                    }
                }
                
                //Előre Jobbra lépés
                if (Controller.Table[sor - 1, oszlop + 1] == 0 || Controller.Table[sor - 1, oszlop + 1] == 1)
                {                    
                    for (int i = 0; i < 64; i++)
                    {
                        if (Mathf.Approximately(Controller.Cubes[i].transform.position.z, this.transform.position.z - 2.7f) && Mathf.Approximately(Controller.Cubes[i].transform.position.x, this.transform.position.x - 2.7f))
                        {
														Controller.Cubes[i].material = GreenCubeMaterial;
														//Controller.Cubes[i].material.color = greenCube;
                        }
                    }
                }
                else
                {
                    for (int i = 0; i < 64; i++)
                    {
                        if (Mathf.Approximately(Controller.Cubes[i].transform.position.z, this.transform.position.z - 2.7f) && Mathf.Approximately(Controller.Cubes[i].transform.position.x, this.transform.position.x - 2.7f))
                        {
														Controller.Cubes[i].material = RedCubeMaterial;
														//Controller.Cubes[i].material.color = redCube;
                        }
                    }
                }
            }


            if (oszlop == 7)
            {
                //Jobb szélen van kijelölve
                //Egyenes lépés
                if (Controller.Table[sor - 1, oszlop] == 0)
                {
                    for (int i = 0; i < 64; i++)
                    {
                        if (Mathf.Approximately(Controller.Cubes[i].transform.position.z, this.transform.position.z - 2.7f) && Mathf.Approximately(Controller.Cubes[i].transform.position.x, this.transform.position.x))
                        {
														Controller.Cubes[i].material = GreenCubeMaterial;
														//Controller.Cubes[i].material.color = greenCube;
                        }
                    }
                }
                else
                {
                    for (int i = 0; i < 64; i++)
                    {
                        if (Mathf.Approximately(Controller.Cubes[i].transform.position.z, this.transform.position.z - 2.7f) && Mathf.Approximately(Controller.Cubes[i].transform.position.x, this.transform.position.x))
                        {
														Controller.Cubes[i].material = RedCubeMaterial;
														//Controller.Cubes[i].material.color = redCube;
                        }
                    }
                }

                //Előre Balra lépés
                if (Controller.Table[sor - 1, oszlop - 1] == 0 || Controller.Table[sor - 1, oszlop - 1] == 1)
                {
                    for (int i = 0; i < 64; i++)
                    {
                        if (Mathf.Approximately(Controller.Cubes[i].transform.position.z, this.transform.position.z - 2.7f) && Mathf.Approximately(Controller.Cubes[i].transform.position.x, this.transform.position.x + 2.7f))
                        {
														Controller.Cubes[i].material = GreenCubeMaterial;
														//Controller.Cubes[i].material.color = greenCube;
                        }
                    }
                }
                else
                {
                    for (int i = 0; i < 64; i++)
                    {
                        if (Mathf.Approximately(Controller.Cubes[i].transform.position.z, this.transform.position.z - 2.7f) && Mathf.Approximately(Controller.Cubes[i].transform.position.x, this.transform.position.x + 2.7f))
                        {
														Controller.Cubes[i].material = RedCubeMaterial;
														//Controller.Cubes[i].material.color = redCube;
                        }
                    }
                }
            }
        }
    }
}
