using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;
using UnityEngine;

public class Move : MonoBehaviour
{
		// Use this for initialization
		FieldController Controller;
		public Material WhiteCubeMaterial;
		public Material GreenCubeMaterial;
		private static List<string> UsedStates = new List<string>();

		void Start()
		{
				GameObject materialLoader;

				materialLoader = GameObject.Find("GreenCube");
				GreenCubeMaterial = materialLoader.GetComponent<Renderer>().material;
				materialLoader = GameObject.Find("WhiteCube");
				WhiteCubeMaterial = materialLoader.GetComponent<Renderer>().material;

				GameObject ChessTable = GameObject.Find("chess-table");
				Controller = ChessTable.GetComponent<FieldController>();
		}

		public void OnMouseOver()
		{
				if (Input.GetMouseButtonDown(0))
				{
						if (this.GetComponent<Renderer>().material.color == GreenCubeMaterial.color)
						{								
								for (int i = 0; i < 16; i++)
								{
										if (Controller.WhitePawns[i].activeSelf && Controller.WhitePawns[i].GetComponent<SelectPawn>().isSelected)
										{
												var newPosition = new Vector3(this.transform.position.x, Controller.WhitePawns[i].GetComponent<Transform>().position.y, this.transform.position.z);
												//Debug.Log(Controller.Table);												
												Controller.WhitePawns[i].GetComponent<Transform>().position = newPosition;
												Controller.WhitePawns[i].GetComponent<Renderer>().material = WhiteCubeMaterial;
												break;
										}
								}

								HitCheck();
								Controller.Start();
								Controller.playersTurn = false;
								Controller.SetDefaultCubeColors();

								//AI Következik											
								StartCoroutine(EnemyMove());
						}
						else
						{
								Controller.SetDefaultCubeColors();
						}
				}
		}

		private static int StepCounter = 0;
		IEnumerator EnemyMove()
		{
				Node root = new Node(Controller.Table, null, true, 0);
				Thread treebuild = new Thread(new ThreadStart(root.BuildTree));
				Thread minimax = new Thread(new ThreadStart(root.AddTerminalMinimaxValues));
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
				//root.AddTerminalMinimaxValues();
				root.FillTreeWithMinimax(int.MinValue, int.MaxValue);
				int x = root.GetCounter();
				int[,] newState = new int[8, 8];
				int bestMinimax;
				int choosen_best_id;				
				StringBuilder sb = new StringBuilder();
				using (StreamWriter sr = new StreamWriter("developer_log.txt", true))
				{
						if(StepCounter < 5) //StepCounter < 4
						{
								newState = root.GetStateByID(choosen_best_id = root.GetBestID(true));
						}
						else
						{
								newState = root.GetStateByID(choosen_best_id = root.GetBestID(false));
						}

						for (int i = 0; i < 8; i++)
						{
								for (int j = 0; j < 8; j++)
								{
										sb.Append(newState[i, j]);
								}
						}
						UsedStates.Add(sb.ToString());
						sb.Clear();
						if (Controller.AILostGame)
						{
								using (StreamWriter sw = new StreamWriter("secretknowledge.txt", true))
								{
										foreach (string state in UsedStates)
										{
												sw.WriteLine(state);
										}
								}
						}

						bestMinimax = root.GetMinimaxByID(choosen_best_id);
						Debug.Log("Best ID / Best MiniMax:" + choosen_best_id + " / " + bestMinimax);						
						SetState(newState);
						HitCheck();						
						Controller.Table = newState;
				
						sr.WriteLine("Best ID" + choosen_best_id + " MiniMax = " + bestMinimax);
						for (int i = 0; i < 8; i++)
						{
								for (int j = 0; j < 8; j++)
								{
										sr.Write(Controller.Table[i, j].ToString() + "  ");
								}
								sr.WriteLine();
						}
						sr.WriteLine();						
				}
				StepCounter++;
				Controller.Start();
				Controller.playersTurn = true;
				yield return new WaitForSeconds(.0f);
		}		

		public void SetState(int[,] newState)
		{
				int blackPawnCounter = 0;
				List<string> usedPawns = new List<string>();

				for (int i = 0; i < 8; i++)
				{
						for (int j = 0; j < 8; j++)
						{
								if (newState[i, j] == 1)
								{
										for (int k = 0; k < Controller.BlackPawns.Length; k++)
										{
												if (Controller.BlackPawns[k].activeSelf && !usedPawns.Contains(Controller.BlackPawns[k].name))
												{
														Controller.BlackPawns[k].transform.position = new Vector3(j * -2.7f, 0.6f, -18.9f - (i * -2.7f));
														usedPawns.Add(Controller.BlackPawns[k].name);
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

		public void HitCheck()
		{
				bool hit = false;

				for (int i = 0; i < 16; i++)
				{
						if (Controller.WhitePawns[i].active)
						{
								for (int j = 0; j < 16; j++)
								{
										if (Controller.BlackPawns[j].active)
										{
												if (Mathf.Approximately(Controller.WhitePawns[i].transform.position.x, Mathf.Clamp(Controller.BlackPawns[j].transform.position.x, Controller.BlackPawns[j].transform.position.x + Mathf.Epsilon, Controller.BlackPawns[j].transform.position.x - Mathf.Epsilon))
												&& Mathf.Approximately(Controller.WhitePawns[i].transform.position.z, Mathf.Clamp(Controller.BlackPawns[j].transform.position.z, Controller.BlackPawns[j].transform.position.z + Mathf.Epsilon, Controller.BlackPawns[j].transform.position.z - Mathf.Epsilon)))
												{
														if (Controller.playersTurn)
														{
																Controller.BlackPawns[j].transform.position = new Vector3(Random.Range(50f, 250f), Random.Range(50f, 250f), Random.Range(50f, 250f));
																Controller.BlackPawns[j].SetActive(false);
																hit = true;
																break;
														}
														else
														{
																Controller.WhitePawns[i].transform.position = new Vector3(Random.Range(300f, 500f), Random.Range(300f, 500f), Random.Range(300f, 500f));
																Controller.WhitePawns[i].SetActive(false);
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
