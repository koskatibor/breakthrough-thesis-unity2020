using System.Collections;
using System.IO;
using System.Threading;
using UnityEngine;

public class Move : MonoBehaviour
{
		// Use this for initialization
		FieldController fc;
		public Material WhiteCubeMaterial;
		public Material GreenCubeMaterial;

		void Start()
		{
				GameObject materialLoader;

				materialLoader = GameObject.Find("GreenCube");
				GreenCubeMaterial = materialLoader.GetComponent<Renderer>().material;
				materialLoader = GameObject.Find("WhiteCube");
				WhiteCubeMaterial = materialLoader.GetComponent<Renderer>().material;

				GameObject ChessTable = GameObject.Find("chess-table");
				fc = ChessTable.GetComponent<FieldController>();
		}

		// Update is called once per frame

		void Update()
		{

		}

		public void OnMouseOver()
		{
				if (Input.GetMouseButtonDown(0))
				{
						if (this.GetComponent<Renderer>().material.color == GreenCubeMaterial.color)
						{
								for (int i = 0; i < 16; i++)
								{
										if (fc.WhitePawns[i].GetComponent<SelectPawn>().isSelected)
										{
												//Debug.Log(fc.table);
												fc.WhitePawns[i].GetComponent<Transform>().position = new Vector3(this.transform.position.x, fc.WhitePawns[i].GetComponent<Transform>().position.y, this.transform.position.z);
												fc.WhitePawns[i].GetComponent<Renderer>().material = WhiteCubeMaterial;												
												break;
										}
								}

								hitCheck();
								fc.Start();
								fc.playersTurn = false;
								fc.SetDefaultCubeColors();

								//AI Következik											
								StartCoroutine(EnemyMove());
						}
						else
						{
								fc.SetDefaultCubeColors();
						}
				}
		}

		IEnumerator EnemyMove()
		{
				Node root = new Node(fc.table, null, true, 0);
				Thread treebuild = new Thread(new ThreadStart(root.BuildTree));
				Thread minimax = new Thread(new ThreadStart(root.addTerminalMinimaxValues));
				treebuild.Start();
				while (true)
				{
						if (!(treebuild.ThreadState == ThreadState.Running))
								break;
				}
				minimax.Start();
				while (true)
				{
						if (!(minimax.ThreadState == ThreadState.Running))
								break;
				}
				//root.BuildTree();
				//root.addTerminalMinimaxValues();
				root.FillTreeWithMinimax();
				int x = root.getCounter();
				int[,] newState = new int[8, 8];
				int z;
				newState = root.getStateByID(z = root.getBestID());								
				Debug.Log("Best MiniMax: " + root.getMinimaxByID(root.getBestID()));
				setState(newState);
				hitCheck();
				Debug.Log("Best ID:" + root.getBestID());
				fc.table = newState;
				using (StreamWriter sr = new StreamWriter("developer_log.txt"))
				{
						sr.WriteLine("Best ID" + root.getBestID());
						for (int i = 0; i < 8; i++)
						{
								for (int j = 0; j < 8; j++)
								{
										sr.Write(fc.table[i, j].ToString() + "  ");
								}
								sr.WriteLine();
						}
						sr.WriteLine();
				}
				fc.Start();
				fc.playersTurn = true;
				yield return new WaitForSeconds(.0f);
		}

		public void setState(int[,] newState)
		{
				int blackPawnCounter = 0;

				for (int i = 0; i < 8; i++)
				{
						for (int j = 0; j < 8; j++)
						{
								if (newState[i, j] == 1)
								{
										if (fc.BlackPawns[blackPawnCounter].active)
										{
												fc.BlackPawns[blackPawnCounter].transform.position = new Vector3(j * -2.7f, 0.6f, -18.9f - (i * -2.7f));												
										}
										blackPawnCounter++;
								}
						}
				}
		}

		public void hitCheck()
		{
				bool hit = false;

				for (int i = 0; i < 16; i++)
				{
						if (fc.WhitePawns[i].active)
						{
								for (int j = 0; j < 16; j++)
								{
										if (fc.BlackPawns[j].active)
										{
												if (Mathf.Approximately(fc.WhitePawns[i].transform.position.x, Mathf.Clamp(fc.BlackPawns[j].transform.position.x, fc.BlackPawns[j].transform.position.x + Mathf.Epsilon, fc.BlackPawns[j].transform.position.x - Mathf.Epsilon))
												&& Mathf.Approximately(fc.WhitePawns[i].transform.position.z, Mathf.Clamp(fc.BlackPawns[j].transform.position.z, fc.BlackPawns[j].transform.position.z + Mathf.Epsilon, fc.BlackPawns[j].transform.position.z - Mathf.Epsilon)))
												{
														if (fc.playersTurn)
														{
																fc.BlackPawns[j].transform.position = new Vector3(Random.Range(50f, 250f), Random.Range(50f, 250f), Random.Range(50f, 250f));
																fc.BlackPawns[j].SetActive(false);
																hit = true;
																break;
														}
														else
														{
																fc.WhitePawns[i].transform.position = new Vector3(Random.Range(300f, 500f), Random.Range(300f, 500f), Random.Range(300f, 500f));
																fc.WhitePawns[i].SetActive(false);
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
