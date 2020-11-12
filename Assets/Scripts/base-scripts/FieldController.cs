using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class FieldController : MonoBehaviour
{
    public int[,] Table = new int[8, 8];
    public MeshRenderer[] Cubes = new MeshRenderer[64];
    //Color[] CubeColors = new Color[64];
		Material[] CubeMaterials = new Material[64];
		public GameObject[] BlackPawns = new GameObject[16];
    public GameObject[] WhitePawns = new GameObject[16];
		public bool AILostGame = false;
		public bool PlayerLostGame = false;
		public bool playersTurn = true; //True = Player's turn, False = AI Turn
		public int PassiveBlack = 0;
		public int PassiveWhite = 0;

		public void SetDefaultCubeColors()
    {
        for (int i = 0; i < 64; i++)
        {
						Cubes[i].material = CubeMaterials[i];            
        }
    }

    public bool FirstStart = true;

		// Start is called before the first frame update
		public void Start()
    {
        if (FirstStart)
        {
						if (!File.Exists("secretknowledge.txt"))
						{
								using (StreamWriter sw = new StreamWriter("secretknowledge.txt", true))
								{
										sw.WriteLine(string.Empty);
								}
						}
						if (!File.Exists("secretknowledge_white.txt"))
						{
								using (StreamWriter sw = new StreamWriter("secretknowledge_white.txt", true))
								{
										sw.WriteLine(string.Empty);
								}
						}

						for (int i = 0; i < 64; i++)
            {
								CubeMaterials[i] = Cubes[i].material;                
            }
						for (int k = 0; k < 16; k++)
						{
								WhitePawns[k].SetActive(true);
								BlackPawns[k].SetActive(true);
						}

						for (int i = 0; i < 8; i++)
						{
								for (int j = 0; j < 8; j++)
								{
										if (i < 2)
												Table[i, j] = 1;
										if (i > 1 && i < 6)
												Table[i, j] = 0;
										if (i > 5)
												Table[i, j] = 2;
								}
						}

						playersTurn = false;
						SetState(Table);

						playersTurn = true;
						SetState(Table);

						PlayerLostGame = false;
						AILostGame = false;
						
						FirstStart = false;	
        }
				
        for (int i = 0; i < 8; i++)
        {
            for (int j = 0; j < 8; j++)
            {
                Table[i, j] = 0;
            }
        }

        for (int i = 0; i < 16; i++)
        {
						int sor = 0, oszlop = 0;
						if (BlackPawns[i].active)
            {								
                if (Mathf.Approximately(BlackPawns[i].transform.position.z, 0.0f))
                    sor = 7;
                else if (Mathf.Approximately(BlackPawns[i].transform.position.z, -2.7f))
                    sor = 6;
                else if (Mathf.Approximately(BlackPawns[i].transform.position.z, -5.4f))
                    sor = 5;
                else if (Mathf.Approximately(BlackPawns[i].transform.position.z, -8.1f))
                    sor = 4;
                else if (Mathf.Approximately(BlackPawns[i].transform.position.z, -10.8f))
                    sor = 3;
                else if (Mathf.Approximately(BlackPawns[i].transform.position.z, -13.5f))
                    sor = 2;
                else if (Mathf.Approximately(BlackPawns[i].transform.position.z, -16.2f))
                    sor = 1;
                else if (Mathf.Approximately(BlackPawns[i].transform.position.z, -18.9f))
                    sor = 0;


                if (Mathf.Approximately(BlackPawns[i].transform.position.x, 0.0f))
                    oszlop = 0;
                else if (Mathf.Approximately(BlackPawns[i].transform.position.x, -2.7f))
                    oszlop = 1;
                else if (Mathf.Approximately(BlackPawns[i].transform.position.x, -5.4f))
                    oszlop = 2;
                else if (Mathf.Approximately(BlackPawns[i].transform.position.x, -8.1f))
                    oszlop = 3;
                else if (Mathf.Approximately(BlackPawns[i].transform.position.x, -10.8f))
                    oszlop = 4;
                else if (Mathf.Approximately(BlackPawns[i].transform.position.x, -13.5f))
                    oszlop = 5;
                else if (Mathf.Approximately(BlackPawns[i].transform.position.x, -16.2f))
                    oszlop = 6;
                else if (Mathf.Approximately(BlackPawns[i].transform.position.x, -18.9f))
                    oszlop = 7;

								if (sor == 7)
								{
										Debug.Log("Vesztes játék.");
										PlayerLostGame = true;
								}										

                Table[sor, oszlop] = 1;
            }

            if (WhitePawns[i].active)
            {
                if (Mathf.Approximately(WhitePawns[i].transform.position.z, 0.0f))
                    sor = 7;
                else if (Mathf.Approximately(WhitePawns[i].transform.position.z, -2.7f))
                    sor = 6;
                else if (Mathf.Approximately(WhitePawns[i].transform.position.z, -5.4f))
                    sor = 5;
                else if (Mathf.Approximately(WhitePawns[i].transform.position.z, -8.1f))
                    sor = 4;
                else if (Mathf.Approximately(WhitePawns[i].transform.position.z, -10.8f))
                    sor = 3;
                else if (Mathf.Approximately(WhitePawns[i].transform.position.z, -13.5f))
                    sor = 2;
                else if (Mathf.Approximately(WhitePawns[i].transform.position.z, -16.2f))
                    sor = 1;
                else if (Mathf.Approximately(WhitePawns[i].transform.position.z, -18.9f))
                    sor = 0;

                if (Mathf.Approximately(WhitePawns[i].transform.position.x , 0.0f))
                    oszlop = 0;
                else if (Mathf.Approximately(WhitePawns[i].transform.position.x, -2.7f))
                    oszlop = 1;
                else if (Mathf.Approximately(WhitePawns[i].transform.position.x, -5.4f))
                    oszlop = 2;
                else if (Mathf.Approximately(WhitePawns[i].transform.position.x, -8.1f))
                    oszlop = 3;
                else if (Mathf.Approximately(WhitePawns[i].transform.position.x, -10.8f))
                    oszlop = 4;
                else if (Mathf.Approximately(WhitePawns[i].transform.position.x, -13.5f))
                    oszlop = 5;
                else if (Mathf.Approximately(WhitePawns[i].transform.position.x, -16.2f))
                    oszlop = 6;
                else if (Mathf.Approximately(WhitePawns[i].transform.position.x, -18.9f))
                    oszlop = 7;

								if (sor == 0)
								{
										Debug.Log("Győztes játék.");
										AILostGame = true;
								}										

								Table[sor, oszlop] = 2;
            }
        }
    }

		public void SetState(int[,] newState)
		{
				int blackPawnCounter = 0;
				List<string> usedPawns = new List<string>();

				int whitePawnCounter = 0;
				List<string> usedWhitePawns = new List<string>();

				if (!playersTurn)
				{
						for (int i = 0; i < 8; i++)
						{
								for (int j = 0; j < 8; j++)
								{
										if (newState[i, j] == 1)
										{
												for (int k = 0; k < BlackPawns.Length; k++)
												{
														if (BlackPawns[k].activeSelf && !usedPawns.Contains(BlackPawns[k].name))
														{
																if (i == 7)
																{
																		BlackPawns[k].transform.position = new Vector3(j * -2.7f, 0.6f, 0.0f);
																}
																else
																{
																		BlackPawns[k].transform.position = new Vector3(j * -2.7f, 0.6f, -18.9f + (i * 2.7f));
																}
																usedPawns.Add(BlackPawns[k].name);
																break;
														}
												}

												//foreach (var item in Controller.BlackPawns)
												//{
												//		if (item.activeSelf && !usedPawns.Contains(item.name))
												//		{
												//				item.transform.position = new Vector3(j * -2.7f, 0.6f, -18.9f - (i * -2.7f));
												//				usedPawns.Add(item.name);
												//				break;
												//		}												
												//}										
												blackPawnCounter++;
										}
								}
						}
				}
				else
				{
						for (int i = 0; i < 8; i++)
						{
								for (int j = 0; j < 8; j++)
								{
										if (newState[i, j] == 2)
										{
												for (int k = 0; k < WhitePawns.Length; k++)
												{
														if (WhitePawns[k].activeSelf && !usedWhitePawns.Contains(WhitePawns[k].name))
														{
																if (i == 7)
																{
																		WhitePawns[k].transform.position = new Vector3(j * -2.7f, 0.6f, 0.0f);
																}
																else
																{
																		WhitePawns[k].transform.position = new Vector3(j * -2.7f, 0.6f, -18.9f + (i * 2.7f));
																}
																usedWhitePawns.Add(WhitePawns[k].name);
																break;
														}
												}

												whitePawnCounter++;
										}
								}
						}
				}				
		}

		public void HitCheck()
		{
				bool hit = false;

				for (int i = 0; i < 16; i++)
				{
						if (WhitePawns[i].active)
						{
								for (int j = 0; j < 16; j++)
								{
										if (BlackPawns[j].active)
										{
												if (Mathf.Approximately(Mathf.Clamp(WhitePawns[i].transform.position.x, WhitePawns[i].transform.position.x + Mathf.Epsilon, WhitePawns[i].transform.position.x - Mathf.Epsilon), Mathf.Clamp(BlackPawns[j].transform.position.x, BlackPawns[j].transform.position.x + Mathf.Epsilon, BlackPawns[j].transform.position.x - Mathf.Epsilon))
												&& Mathf.Approximately(Mathf.Clamp(WhitePawns[i].transform.position.z, WhitePawns[i].transform.position.z + Mathf.Epsilon, WhitePawns[i].transform.position.z - Mathf.Epsilon), Mathf.Clamp(BlackPawns[j].transform.position.z, BlackPawns[j].transform.position.z + Mathf.Epsilon, BlackPawns[j].transform.position.z - Mathf.Epsilon)))
												{
														if (playersTurn)
														{
																BlackPawns[j].transform.position = new Vector3(Random.Range(50f, 250f), Random.Range(50f, 250f), Random.Range(50f, 250f));
																BlackPawns[j].SetActive(false);
																Debug.Log("Leütött fekete bábú: " + PassiveBlack++);
																hit = true;
																break;
														}
														else
														{
																WhitePawns[i].transform.position = new Vector3(Random.Range(300f, 500f), Random.Range(300f, 500f), Random.Range(300f, 500f));
																WhitePawns[i].SetActive(false);
																Debug.Log("Leütött fehér bábú: " + PassiveWhite++);
																hit = true;
																break;
														}
												}
										}
								}
								if (hit)
										break;
						}
				}
		}
}
