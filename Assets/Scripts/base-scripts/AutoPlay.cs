using Assets.Scripts.menu_scripts.play_scripts;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;
using UnityEngine;

public class AutoPlay : MonoBehaviour
{		
		public bool AutoPlayActive = false;		
		private FieldController Controller;
		private AIDetails AiDetails;
		private static List<string> UsedWhiteStates = new List<string>();
		private static List<string> UsedStates = new List<string>();
		private static int StepCounter = 0;
		private int StepRandomizer;		

		private void Start()
		{
				GameObject ChessTable =	GameObject.Find("chess-table");
				Controller = ChessTable.GetComponent<FieldController>();
				ChessTable = GameObject.Find("chess-table");
				GameObject ScriptHolder = GameObject.Find("ScriptHolder");
				AiDetails = ScriptHolder.GetComponent<AIDetails>();
				StepRandomizer = Random.Range(2, 20);				
		}
		void Update()
		{
				if (AutoPlayActive && Time.timeScale != 0)
				{
						AutomaticPlay();
				}
		}

		public void AutomaticPlay()
		{				
				if (!Controller.PlayerLostGame && !Controller.AILostGame)
				{
						if (Controller.playersTurn)
						{
								StartCoroutine(WhiteAIPlay());
						}
						else
						{
								StartCoroutine(EnemyMove());
						}
				}
				else
				{
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
						else
						{
								using (StreamWriter sw = new StreamWriter("secretknowledge_white.txt", true))
								{
										foreach (string state in UsedWhiteStates)
										{
												sw.WriteLine(state);
										}
								}
						}
				}				
		}

		IEnumerator WhiteAIPlay()
		{
				Node root = new Node(Controller.Table, null, false, 0);
				root.BuildTree(AiDetails.MaxLevelWhite);
				//Thread treebuild = new Thread(new ThreadStart(root.BuildTree));
				//Thread minimax = new Thread(new ThreadStart(root.AddTerminalMinimaxValuesOfWhiteAI));
				//treebuild.Start();
				//while (true)
				//{
				//		if (!(treebuild.ThreadState == ThreadState.Running))
				//				break;
				//}
				//minimax.Start();
				//while (true)
				//{
				//		if (!(minimax.ThreadState == ThreadState.Running))
				//				break;
				//}
				root.AddTerminalMinimaxValuesOfWhiteAI(AiDetails);
				//root.BuildTree();
				//root.AddTerminalMinimaxValues();
				root.FillTreeWithMinimax(int.MinValue, int.MaxValue);
				int x = root.GetCounter();
				int[,] newState = new int[8, 8];
				int bestMinimax;
				int choosen_best_id;
				StringBuilder sb = new StringBuilder();

				using (StreamWriter sr = new StreamWriter("developer_log_whitestep.txt", true))
				{
						if (StepCounter < StepRandomizer) //StepCounter < 4 vagy StepCounter < StepRandomizer
						{
								newState = root.GetStateByID(choosen_best_id = root.GetBestIDForWhiteAi(true));
						}
						else
						{
								if (AiDetails.UseStepIgnore)
								{
										newState = root.GetStateByID(choosen_best_id = root.GetBestIDForWhiteAi(false));
								}
								else
								{
										newState = root.GetStateByID(choosen_best_id = root.GetBestIDForWhiteAi(true));
								}
						}

						for (int i = 0; i < 8; i++)
						{
								for (int j = 0; j < 8; j++)
								{
										sb.Append(newState[i, j]);
								}
						}
						UsedWhiteStates.Add(sb.ToString());
						sb.Clear();

						bestMinimax = root.GetMinimaxByID(choosen_best_id);
						Debug.Log("White: Best ID / Best MiniMax:" + choosen_best_id + " / " + bestMinimax);
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
				}

				StepCounter++;
				Controller.Start();
				Controller.SetDefaultCubeColors();
				Controller.playersTurn = false;
				yield return new WaitForSeconds(.0f);
		}

		IEnumerator EnemyMove()
		{
				Node root = new Node(Controller.Table, null, true, 0);
				root.BuildTree(AiDetails.MaxLevel);
				//Thread treebuild = new Thread(new ThreadStart(root.BuildTree));
				//Thread minimax = new Thread(new ThreadStart(root.AddTerminalMinimaxValues));
				//treebuild.Start();
				//while (true)
				//{
				//		if (!(treebuild.ThreadState == ThreadState.Running))
				//				break;
				//}
				//root.AddTerminalMinimaxValues(AiDetails);
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
						if (StepCounter < StepRandomizer) //StepCounter < 4
						{
								newState = root.GetStateByID(choosen_best_id = root.GetBestID(true));
						}
						else
						{
								if (AiDetails.UseStepIgnore)
								{
										newState = root.GetStateByID(choosen_best_id = root.GetBestIDForWhiteAi(false));
								}
								else
								{
										newState = root.GetStateByID(choosen_best_id = root.GetBestIDForWhiteAi(true));
								}
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