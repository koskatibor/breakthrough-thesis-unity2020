using Assets.Scripts.menu_scripts.play_scripts;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;
using UnityEngine;

public class Move : MonoBehaviour
{		
		FieldController Controller;
		AIDetails AiDetails;
		private Material WhitePawnMaterial;		
		private Material GreenCubeMaterial;
		private static List<string> UsedStates = new List<string>();
		private int StepRandomizer;

		void Start()
		{
				GameObject materialLoader;

				materialLoader = GameObject.Find("GreenCube");
				GreenCubeMaterial = materialLoader.GetComponent<Renderer>().material;				
				materialLoader = GameObject.Find("WhitePawn");
				WhitePawnMaterial = materialLoader.GetComponent<Renderer>().material;

				GameObject ChessTable = GameObject.Find("chess-table");
				Controller = ChessTable.GetComponent<FieldController>();
				GameObject ScriptHolder = GameObject.Find("ScriptHolder");
				AiDetails = ScriptHolder.GetComponent<AIDetails>();

				StepRandomizer = Random.Range(2, 10);
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
												Controller.WhitePawns[i].GetComponent<Renderer>().material = WhitePawnMaterial;
												break;
										}
								}

								Controller.HitCheck();
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
				//Thread minimax = new Thread(new ThreadStart(root.AddTerminalMinimaxValues));
				treebuild.Start();
				while (true)
				{
						if (!(treebuild.ThreadState == ThreadState.Running))
								break;
				}
				root.AddTerminalMinimaxValues(AiDetails);
				//minimax.Start();
				//while (true)
				//{
				//		if (!(minimax.ThreadState == ThreadState.Running))
				//				break;
				//}
				//root.BuildTree();
				//root.AddTerminalMinimaxValues();
				root.FillTreeWithMinimax(int.MinValue, int.MaxValue);
				int x = root.GetCounter();
				int[,] newState = new int[8, 8];
				int bestMinimax;
				int choosen_best_id;				
				StringBuilder sb = new StringBuilder();
				//using (StreamWriter sr = new StreamWriter("developer_log.txt", true))
				//{
						if(StepCounter < StepRandomizer) //StepCounter < 4
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
						Debug.Log("Black: Best ID / Best MiniMax:" + choosen_best_id + " / " + bestMinimax);						
						Controller.SetState(newState);
						Controller.HitCheck();						
						Controller.Table = newState;
				
						//sr.WriteLine("Best ID" + choosen_best_id + " MiniMax = " + bestMinimax);
						//for (int i = 0; i < 8; i++)
						//{
						//		for (int j = 0; j < 8; j++)
						//		{
						//				sr.Write(Controller.Table[i, j].ToString() + "  ");
						//		}
						//		sr.WriteLine();
						//}
						//sr.WriteLine();						
				//}
				StepCounter++;
				Controller.Start();
				Controller.playersTurn = true;
				yield return new WaitForSeconds(.0f);
		}		
}
