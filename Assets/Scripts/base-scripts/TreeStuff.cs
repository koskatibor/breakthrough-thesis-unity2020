using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;

class Node
{
		Stack<Node> NodeStack = new Stack<Node>();
		Node Parent;
		public List<Node> Children;
		public int[,] Table = new int[8, 8];
		bool IsWhiteNode;
		public static int Counter = 0;
		int Id;
		int Minimax;
		int NodeLevel;

		public Node(int[,] Table, Node parent, bool isWhiteNode, int NodeLevel)
		{
				if (parent == null)
				{
						Counter = 0;
						Id = 0;
						Minimax = 0;
						BestId = 0;
						BestMinimax = 0;						
						this.NodeLevel = 0;
				}
				this.Children = new List<Node>();
				this.Id = Counter;
				Counter++;
				this.Parent = parent;
				this.NodeLevel = NodeLevel;
				for (int i = 0; i < 8; i++)
				{
						for (int j = 0; j < 8; j++)
						{
								this.Table[i, j] = Table[i, j];
						}
				}
				this.IsWhiteNode = isWhiteNode;
		}

		public int GetCounter()
		{
				return Counter;
		}

		//    static int level = 0;

		//A fa maximum mélysége
		static int MaxLevel = 4;

		public void BuildTree()
		{
				bool outOfNodes = false;				
				NodeStack.Push(this);
				Node currentNode;
				int level = 0;
				while (NodeStack.Count != 0)
				{
						bool noNeedForMoreNodes = false;
						currentNode = NodeStack.Pop();
						int[,] nextTable = new int[8, 8];
						Node Child;

						for (int i = 0; i < 8; i++)
						{
								if (currentNode.Table[0, i] == 2 || currentNode.Table[7, i] == 1)
								{
										noNeedForMoreNodes = true;
								}
						}

						while (currentNode.NodeLevel == MaxLevel || noNeedForMoreNodes)
						{
								if (NodeStack.Count == 0)
								{
										outOfNodes = true;
										break;
								}
								currentNode = NodeStack.Pop();
								noNeedForMoreNodes = false;
						}
						if (outOfNodes)
								break;

						level = currentNode.NodeLevel + 1;

						for (int i = 0; i < 8; i++)
						{
								for (int j = 0; j < 8; j++)
								{
										if (currentNode.IsWhiteNode)
										{
												if (currentNode.Table[i, j] == 1)
												{
														if (i < 7)
														{
																//Ha a tábla bal szélén van
																if (j == 0)
																{
																		//Egyenes lépés
																		for (int z = 0; z < 8; z++)
																		{
																				for (int w = 0; w < 8; w++)
																				{
																						nextTable[z, w] = currentNode.Table[z, w];
																				}
																		}
																		if (currentNode.Table[i + 1, j] == 0)
																		{
																				nextTable[i, j] = 0;
																				nextTable[i + 1, j] = 1;
																				Child = new Node(nextTable, currentNode, false, level);
																				currentNode.Children.Add(Child);
																				NodeStack.Push(Child);
																		}
																		//Jobbra-előre lépés
																		for (int z = 0; z < 8; z++)
																		{
																				for (int w = 0; w < 8; w++)
																				{
																						nextTable[z, w] = currentNode.Table[z, w];
																				}
																		}
																		if (currentNode.Table[i + 1, j + 1] == 0 || currentNode.Table[i + 1, j + 1] == 2)
																		{
																				nextTable[i, j] = 0;
																				nextTable[i + 1, j + 1] = 1;
																				Child = new Node(nextTable, currentNode, false, level);
																				currentNode.Children.Add(Child);
																				NodeStack.Push(Child);
																		}
																}
																//Ha a nincs a tábla egyik szélén sem
																if (j > 0 && j < 7)
																{
																		//Egyenes lépés
																		for (int z = 0; z < 8; z++)
																		{
																				for (int w = 0; w < 8; w++)
																				{
																						nextTable[z, w] = currentNode.Table[z, w];
																				}
																		}
																		if (currentNode.Table[i + 1, j] == 0)
																		{
																				nextTable[i, j] = 0;
																				nextTable[i + 1, j] = 1;
																				Child = new Node(nextTable, currentNode, false, level);
																				currentNode.Children.Add(Child);
																				NodeStack.Push(Child);
																		}
																		//Balra-előre lépés
																		for (int z = 0; z < 8; z++)
																		{
																				for (int w = 0; w < 8; w++)
																				{
																						nextTable[z, w] = currentNode.Table[z, w];
																				}
																		}
																		//Jobbra-előre lépés
																		if (currentNode.Table[i + 1, j + 1] == 0 || currentNode.Table[i + 1, j + 1] == 2)
																		{
																				nextTable[i, j] = 0;
																				nextTable[i + 1, j + 1] = 1;
																				Child = new Node(nextTable, currentNode, false, level);
																				currentNode.Children.Add(Child);
																				NodeStack.Push(Child);
																		}
																		//Balra előre lépés
																		for (int z = 0; z < 8; z++)
																		{
																				for (int w = 0; w < 8; w++)
																				{
																						nextTable[z, w] = currentNode.Table[z, w];
																				}
																		}
																		if (currentNode.Table[i + 1, j - 1] == 0 || currentNode.Table[i + 1, j - 1] == 2)
																		{
																				nextTable[i, j] = 0;
																				nextTable[i + 1, j - 1] = 1;
																				Child = new Node(nextTable, currentNode, false, level);
																				currentNode.Children.Add(Child);
																				NodeStack.Push(Child);
																		}
																}
																//Ha a tábla jobb szélén van
																if (j == 7)
																{
																		//Balra-előre lépés
																		for (int z = 0; z < 8; z++)
																		{
																				for (int w = 0; w < 8; w++)
																				{
																						nextTable[z, w] = currentNode.Table[z, w];
																				}
																		}
																		if (currentNode.Table[i + 1, j - 1] == 0 || currentNode.Table[i + 1, j - 1] == 2)
																		{
																				nextTable[i, j] = 0;
																				nextTable[i + 1, j - 1] = 1;
																				Child = new Node(nextTable, currentNode, false, level);
																				currentNode.Children.Add(Child);
																				NodeStack.Push(Child);
																		}
																		//Egyenes lépés
																		for (int z = 0; z < 8; z++)
																		{
																				for (int w = 0; w < 8; w++)
																				{
																						nextTable[z, w] = currentNode.Table[z, w];
																				}
																		}
																		if (currentNode.Table[i + 1, j] == 0)
																		{
																				nextTable[i, j] = 0;
																				nextTable[i + 1, j] = 1;
																				Child = new Node(nextTable, currentNode, false, level);
																				currentNode.Children.Add(Child);
																				NodeStack.Push(Child);
																		}
																}
														}
												}
										}
										else
										{
												if (currentNode.Table[i, j] == 2)
												{
														if (i > 0)
														{
																//Ha a tábla bal szélén van
																if (j == 0)
																{
																		//Egyenes lépés
																		for (int z = 0; z < 8; z++)
																		{
																				for (int w = 0; w < 8; w++)
																				{
																						nextTable[z, w] = currentNode.Table[z, w];
																				}
																		}
																		if (currentNode.Table[i - 1, j] == 0)
																		{
																				nextTable[i, j] = 0;
																				nextTable[i - 1, j] = 2;
																				Child = new Node(nextTable, currentNode, true, level);
																				currentNode.Children.Add(Child);
																				NodeStack.Push(Child);
																		}
																		//Jobbra-előre lépés
																		for (int z = 0; z < 8; z++)
																		{
																				for (int w = 0; w < 8; w++)
																				{
																						nextTable[z, w] = currentNode.Table[z, w];
																				}
																		}
																		if (currentNode.Table[i - 1, j + 1] == 0 || currentNode.Table[i - 1, j + 1] == 1)
																		{
																				nextTable[i, j] = 0;
																				nextTable[i - 1, j + 1] = 2;
																				Child = new Node(nextTable, currentNode, true, level);
																				currentNode.Children.Add(Child);
																				NodeStack.Push(Child);
																		}
																}
																//Ha nincs a tábla egyik szélén sem
																if (j > 0 && j < 7)
																{
																		//Balra-előre lépés
																		for (int z = 0; z < 8; z++)
																		{
																				for (int w = 0; w < 8; w++)
																				{
																						nextTable[z, w] = currentNode.Table[z, w];
																				}
																		}
																		if (currentNode.Table[i - 1, j - 1] == 0 || currentNode.Table[i - 1, j - 1] == 1)
																		{
																				nextTable[i, j] = 0;
																				nextTable[i - 1, j - 1] = 2;
																				Child = new Node(nextTable, currentNode, true, level);
																				currentNode.Children.Add(Child);
																				NodeStack.Push(Child);
																		}
																		//Egyenes lépés
																		for (int z = 0; z < 8; z++)
																		{
																				for (int w = 0; w < 8; w++)
																				{
																						nextTable[z, w] = currentNode.Table[z, w];
																				}
																		}
																		if (currentNode.Table[i - 1, j] == 0)
																		{
																				nextTable[i, j] = 0;
																				nextTable[i - 1, j] = 2;
																				Child = new Node(nextTable, currentNode, true, level);
																				currentNode.Children.Add(Child);
																				NodeStack.Push(Child);
																		}
																		//Jobbra-előre lépés
																		for (int z = 0; z < 8; z++)
																		{
																				for (int w = 0; w < 8; w++)
																				{
																						nextTable[z, w] = currentNode.Table[z, w];
																				}
																		}
																		if (currentNode.Table[i - 1, j + 1] == 0 || currentNode.Table[i - 1, j + 1] == 1)
																		{
																				nextTable[i, j] = 0;
																				nextTable[i - 1, j + 1] = 2;
																				Child = new Node(nextTable, currentNode, true, level);
																				currentNode.Children.Add(Child);
																				NodeStack.Push(Child);
																		}
																}
																//Ha a tábla jobb szélén van
																if (j == 7)
																{
																		//Balra-előre lépés
																		for (int z = 0; z < 8; z++)
																		{
																				for (int w = 0; w < 8; w++)
																				{
																						nextTable[z, w] = currentNode.Table[z, w];
																				}
																		}
																		if (currentNode.Table[i - 1, j - 1] == 0 || currentNode.Table[i - 1, j - 1] == 1)
																		{
																				nextTable[i, j] = 0;
																				nextTable[i - 1, j - 1] = 2;
																				Child = new Node(nextTable, currentNode, true, level);
																				currentNode.Children.Add(Child);
																				NodeStack.Push(Child);
																		}
																		//Egyenes lépés
																		for (int z = 0; z < 8; z++)
																		{
																				for (int w = 0; w < 8; w++)
																				{
																						nextTable[z, w] = currentNode.Table[z, w];
																				}
																		}
																		if (currentNode.Table[i - 1, j] == 0)
																		{
																				nextTable[i, j] = 0;
																				nextTable[i - 1, j] = 2;
																				Child = new Node(nextTable, currentNode, true, level);
																				currentNode.Children.Add(Child);
																				NodeStack.Push(Child);
																		}
																}
														}
												}
										}
								}
						}
				}
		}

		public void AddTerminalMinimaxValues()
		{
				if (this.Children.Count == 0)
				{
						this.Minimax = CalculateMiniMax();
				}
				else
				{
						for (int i = 0; i < this.Children.Count; i++)
						{
								this.Children[i].AddTerminalMinimaxValues();
						}

						//foreach (Node item in this.Children)
						//{
						//		item.AddTerminalMinimaxValues();
						//}
				}
		}

		public void AddTerminalMinimaxValuesOfWhiteAI()
		{
				if (this.Children.Count == 0)
				{
						this.Minimax = CalculateWhiteMinimax();
				}
				else
				{
						for (int i = 0; i < this.Children.Count; i++)
						{
								this.Children[i].AddTerminalMinimaxValuesOfWhiteAI();
						}
						
				}
		}

		//CurrentMinimax számoló
		public int CalculateMiniMax()
		{
				int mm = 0;				

				int[,] TableChanges = new int[8, 8];			

				//Fekete pontjainak kiszámítása
				for (int i = 0; i < 8; i++)
				{
						for (int j = 0; j < 8; j++)
						{
								if (this.Table[i, j] == 1)
								{										
										switch (i)
										{
												case 0: { mm += 1; break; }
												case 1: { mm += 2; break; }
												case 2: { mm += 4; break; }
												case 3: { mm += 8; break; }
												case 4: { mm += 16; break; }
												case 5: { mm += 20; break; }
												case 6: { mm += 24; break; }
												case 7: { mm += 1000; break; }
										}

										if (j != 0 && j != 7)
										{
												// Ha biztonságos mezőre lép +1 pont.
												if (i != 7 && this.Table[i + 1, j + 1] != 2 && this.Table[i + 1, j - 1] != 2)
														mm += 4;
												//Alakzat bónusz, ha egymás mellett vagy alatt vannak +1 pont.
												//Sor
												if (this.Table[i, j - 1] == 1)
														mm += 8;
												if (this.Table[i, j + 1] == 1)
														mm += 8;
												if (i != 0 && this.Table[i, j - 1] == 1 && this.Table[i, j + 1] == 1)
												{
														if (this.Table[i - 1, j - 1] == 1)
																mm += 4;
														if (this.Table[i - 1, j + 1] == 1)
																mm += 4;
												}

												//Oszlop
												if (i != 0 && this.Table[i - 1, j] == 1)
														mm += 4;
												if (i != 7 && this.Table[i + 1, j] == 1)
														mm += 4;
												if (i != 7 && i != 0 && this.Table[i - 1, j] == 1 && this.Table[i + 1, j] == 1)
												{
														if (this.Table[i - 1, j - 1] == 1)
																mm += 4;
														if (this.Table[i - 1, j + 1] == 1)
																mm += 4;
												}

												//Ellenfél ütő mezője - Nem biztonságos mező -1 pont
												if (i != 7 && i > 2 && (this.Table[i + 1, j + 1] == 2 || this.Table[i + 1, j - 1] == 2))
												{
														if (this.Table[i + 1, j + 1] == 2)
														{
																mm -= 18;
														}
														if (this.Table[i + 1, j - 1] == 2)
														{
																mm -= 18;
														}
														if (this.Table[i - 1, j - 1] == 1)
														{
																mm += 16;
														}																
														if (this.Table[i - 1, j + 1] == 1)
														{
																mm += 16;
														}														
														if (i != 0 && this.Table[i - 1, j] == 1)
														{
																mm += 16;
														}														
												}												
										}
										else
										{
												if (j == 0)
												{
														// Ha biztonságos mezőre lép +1 pont.
														if (i != 7 && this.Table[i + 1, j + 1] != 2)
																mm += 4;

														//Alakzat bónusz, ha egymás mellett vagy alatt vannak +1 pont.
														//Sor
														if (this.Table[i, j + 1] == 1)
														{
																mm += 16;
																if (i != 0 && this.Table[i - 1, j + 1] == 1)
																		mm += 8;
														}

														//Oszlop
														if (i != 0 && this.Table[i - 1, j] == 1)
																mm += 6;
														if (i != 7 && this.Table[i + 1, j] == 1)
																mm += 6;
														if (i != 7 && i != 0 && this.Table[i - 1, j] == 1 && this.Table[i + 1, j] == 1)
														{
																if (this.Table[i - 1, j + 1] == 1)
																		mm += 4;
														}

														//Ellenfél ütő mezője - Nem biztonságos mező -1 pont
														if (i != 7 && i > 2 && this.Table[i + 1, j + 1] == 2)
														{
																mm -= 21;
																if (this.Table[i - 1, j + 1] == 1)
																{
																		mm += 16;
																}
																if (i != 0 && this.Table[i - 1, j] == 1)
																{
																		mm += 16;
																}
														}														
												}
												else
												{
														// Ha biztonságos mezőre lép +1 pont.
														if (i != 7 && this.Table[i + 1, j - 1] != 2)
																mm += 4;

														//Alakzat bónusz, ha egymás mellett vagy alatt vannak +1 pont.
														//Sor
														if (this.Table[i, j - 1] == 1)
														{
																mm += 16;
																if (i != 0 && this.Table[i - 1, j - 1] == 1)
																		mm += 8;
														}

														//Oszlop
														if (i != 0 && this.Table[i - 1, j] == 1)
																mm += 6;
														if (i != 7 && this.Table[i + 1, j] == 1)
																mm += 6;
														if (i != 7 && i != 0 && this.Table[i - 1, j] == 1 && this.Table[i + 1, j] == 1)
														{
																if (this.Table[i - 1, j - 1] == 1)
																		mm += 4;
														}

														//Ellenfél ütő mezője - Nem biztonságos mező -1 pont
														if (i != 7 && i > 2 && this.Table[i + 1, j - 1] == 2)
														{
																mm -= 21;
																if (this.Table[i - 1, j - 1] == 1)
																{
																		mm += 16;
																}
																if (i != 0 && this.Table[i - 1, j] == 1)
																{
																		mm += 16;
																}
														}														
												}
										}
								}
								//-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
								//-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
								//-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
								else
								{
										//Fehérek összpontszáma
										if (this.Table[i, j] == 2)
										{
												switch (i)
												{
														case 0: { mm -= 1500; break; }
														case 1: { mm -= 256; break; }
														case 2: { mm -= 128; break; }
														case 3: { mm -= 64; break; }
														case 4: { mm -= 32; break; }
														case 5: { mm -= 16; break; }
														case 6: { mm -= 8; break; }
														case 7: { mm -= 4; break; }
												}												
										}
								}
						}
				}

				return mm;
		}

		public int CalculateWhiteMinimax()
		{
				int mm = 0;

				int[,] TableChanges = new int[8, 8];

				//Fekete pontjainak kiszámítása
				for (int i = 0; i < 8; i++)
				{
						for (int j = 0; j < 8; j++)
						{
								if (this.Table[i, j] == 1)
								{
										switch (i)
										{
												case 0: { mm += 4; break; }
												case 1: { mm += 8; break; }
												case 2: { mm += 16; break; }
												case 3: { mm += 32; break; }
												case 4: { mm += 64; break; }
												case 5: { mm += 128; break; }
												case 6: { mm += 256; break; }
												case 7: { mm += 1500; break; }
										}																				
								}
								//-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
								//-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
								//-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
								else
								{
										//Fehérek összpontszáma
										if (this.Table[i, j] == 2)
										{
												switch (i)
												{
														case 0: { mm -= 1000; break; }
														case 1: { mm -= 24; break; }
														case 2: { mm -= 20; break; }
														case 3: { mm -= 16; break; }
														case 4: { mm -= 8; break; }
														case 5: { mm -= 4; break; }
														case 6: { mm -= 2; break; }
														case 7: { mm -= 1; break; }
												}

												if (j != 0 && j != 7)
												{
														// Ha biztonságos mezőre lép +1 pont.
														if (i != 0 && this.Table[i - 1, j + 1] != 1 && this.Table[i - 1, j - 1] != 1)
																mm -= 4;
														//Alakzat bónusz, ha egymás mellett vagy alatt vannak +1 pont.
														//Sor
														if (this.Table[i, j - 1] == 2)
																mm -= 8;
														if (this.Table[i, j + 1] == 2)
																mm -= 8;
														if (i != 7 && this.Table[i, j - 1] == 2 && this.Table[i, j + 1] == 2)
														{
																if (this.Table[i + 1, j - 1] == 2)
																		mm -= 4;
																if (this.Table[i + 1, j + 1] == 2)
																		mm -= 4;
														}

														//Oszlop
														if (i != 0 && this.Table[i - 1, j] == 2)
																mm -= 4;
														if (i != 7 && this.Table[i + 1, j] == 2)
																mm -= 4;
														if (i != 7 && i != 0 && this.Table[i - 1, j] == 2 && this.Table[i + 1, j] == 2)
														{
																if (this.Table[i + 1, j - 1] == 2)
																		mm -= 4;
																if (this.Table[i + 1, j + 1] == 2)
																		mm -= 4;
														}

														//Ellenfél ütő mezője - Nem biztonságos mező -1 pont
														if (i != 0 && i < 5 && (this.Table[i - 1, j + 1] == 1 || this.Table[i - 1, j - 1] == 1))
														{
																if (this.Table[i - 1, j + 1] == 1)
																{
																		mm += 18;
																}
																if (this.Table[i - 1, j - 1] == 1)
																{
																		mm += 18;
																}
																if (this.Table[i + 1, j - 1] == 2)
																{
																		mm -= 16;
																}
																if (this.Table[i + 1, j + 1] == 2)
																{
																		mm -= 16;
																}
																if (i != 0 && this.Table[i + 1, j] == 2)
																{
																		mm -= 16;
																}
														}
												}
												else
												{
														if (j == 0)
														{
																// Ha biztonságos mezőre lép +1 pont.
																if (i != 0 && this.Table[i - 1, j + 1] != 1)
																		mm -= 4;

																//Alakzat bónusz, ha egymás mellett vagy alatt vannak +1 pont.
																//Sor
																if (this.Table[i, j + 1] == 2)
																{
																		mm -= 16;
																		if (i != 7 && this.Table[i + 1, j + 1] == 2)
																				mm -= 8;
																}

																//Oszlop
																if (i != 0 && this.Table[i - 1, j] == 2)
																		mm -= 6;
																if (i != 7 && this.Table[i + 1, j] == 2)
																		mm -= 6;
																if (i != 7 && i != 0 && this.Table[i - 1, j] == 2 && this.Table[i + 1, j] == 2)
																{
																		if (this.Table[i + 1, j + 1] == 2)
																				mm -= 4;
																}

																//Ellenfél ütő mezője - Nem biztonságos mező -1 pont
																if (i != 0 && i < 5 && this.Table[i - 1, j + 1] == 1)
																{
																		mm += 21;
																		if (this.Table[i + 1, j + 1] == 2)
																		{
																				mm -= 16;
																		}
																		if (i != 7 && this.Table[i + 1, j] == 2)
																		{
																				mm -= 16;
																		}
																}
														}
														else
														{
																// Ha biztonságos mezőre lép +1 pont.
																if (i != 0 && this.Table[i - 1, j - 1] != 1)
																		mm -= 4;

																//Alakzat bónusz, ha egymás mellett vagy alatt vannak +1 pont.
																//Sor
																if (this.Table[i, j - 1] == 2)
																{
																		mm -= 16;
																		if (i != 7 && this.Table[i + 1, j - 1] == 2)
																				mm -= 8;
																}

																//Oszlop
																if (i != 0 && this.Table[i - 1, j] == 2)
																		mm -= 6;
																if (i != 7 && this.Table[i + 1, j] == 2)
																		mm -= 6;
																if (i != 7 && i != 0 && this.Table[i - 1, j] == 2 && this.Table[i + 1, j] == 2)
																{
																		if (this.Table[i + 1, j - 1] == 2)
																				mm -= 4;
																}

																//Ellenfél ütő mezője - Nem biztonságos mező -1 pont
																if (i != 0 && i < 5 && this.Table[i - 1, j - 1] == 1)
																{
																		mm += 21;
																		if (this.Table[i + 1, j - 1] == 2)
																		{
																				mm -= 16;
																		}
																		if (i != 7 && this.Table[i + 1, j] == 2)
																		{
																				mm -= 16;
																		}
																}
														}
												}
										}
								}
						}
				}

				return mm;
		}

		static int BestId;
		static int BestMinimax;

		public int FillTreeWithMinimax(int alpha, int beta)
		{
				if (this.Children.Count == 0)
				{
						return this.Minimax;
				}
				else
				{
						if (this.IsWhiteNode)
						{
								BestMinimax = int.MinValue;
								for (int i = 0; i < this.Children.Count; i++)
								{
										BestMinimax = Mathf.Max(BestMinimax, this.Children[i].FillTreeWithMinimax(alpha, beta));
										this.Minimax = BestMinimax;
										alpha = Mathf.Max(alpha, BestMinimax);
										if (alpha >= beta)
												break;
								}

								//foreach (Node node in this.Children)
								//{
								//		BestMinimax = Mathf.Max(BestMinimax, node.FillTreeWithMinimax(alpha, beta));
								//		this.Minimax = BestMinimax;
								//		alpha = Mathf.Max(alpha, BestMinimax);
								//		if (alpha >= beta)
								//				break;
								//}
								return BestMinimax;
						}
						else
						{
								BestMinimax = int.MaxValue;
								for (int i = 0; i < this.Children.Count; i++)
								{
										BestMinimax = Mathf.Min(BestMinimax, this.Children[i].FillTreeWithMinimax(alpha, beta));
										this.Minimax = BestMinimax;
										beta = Mathf.Min(beta, BestMinimax);
										if (beta <= alpha)
												break;
								}

								//foreach (Node node in this.Children)
								//{
								//		BestMinimax = Mathf.Min(BestMinimax, node.FillTreeWithMinimax(alpha, beta));
								//		this.Minimax = BestMinimax;
								//		beta = Mathf.Min(beta, BestMinimax);
								//		if (beta <= alpha)
								//				break;
								//}
								return BestMinimax;
						}
				}

				//if (this.Children.Count == 0)
				//{
				//		return this.Minimax;
				//}
				//else
				//{
				//		if (this.IsWhiteNode)
				//		{
				//				BestMinimax = int.MinValue;
				//				foreach (Node node in this.Children)
				//				{
				//						BestMinimax = Mathf.Max(BestMinimax, node.FillTreeWithMinimax());
				//						this.Minimax = BestMinimax;
				//				}
				//				return BestMinimax;
				//		}
				//		else
				//		{
				//				BestMinimax = int.MaxValue;
				//				foreach (Node node in this.Children)
				//				{
				//						BestMinimax = Mathf.Min(BestMinimax, node.FillTreeWithMinimax());
				//						this.Minimax = BestMinimax;
				//				}
				//				return BestMinimax;
				//		}
				//}
		}

		static int CurrentMinimax;
		public int GetMinimaxByID(int Id)
		{
				if (this.Id == Id)
				{
						CurrentMinimax = this.Minimax;
						return CurrentMinimax;
				}
				else
				{
						for (int i = 0; i < this.Children.Count; i++)
						{
								this.Children[i].GetMinimaxByID(Id);
						}

						//foreach (Node item in this.Children)
						//{
						//		item.GetMinimaxByID(Id);
						//}
				}
				return CurrentMinimax;
		}

		static int[,] State = new int[8, 8];
		public int[,] GetStateByID(int givenID)
		{
				if (this.Id == givenID)
				{
						for (int i = 0; i < 8; i++)
						{
								for (int j = 0; j < 8; j++)
								{
										State[i, j] = this.Table[i, j];
								}
						}
						return State;
				}
				else
				{
						if (this.Children.Count != 0)
						{
								for (int k = 0; k < this.Children.Count; k++)
								{
										this.Children[k].GetStateByID(givenID);
								}
						}
				}
				return State;
		}	
		
		public int GetBestID(bool firstStep)
		{
				int MinMinimax = int.MinValue;
				for (int i = 0; i < this.Children.Count; i++)
				{												
						if (this.Children[i].Minimax > MinMinimax)
						{
								MinMinimax = this.Children[i].Minimax;
								BestId = this.Children[i].Id;
						}
						else
						{
								if (this.Children[i].Minimax == MinMinimax)
								{
										bool loserState = false;
										if (!firstStep)
										{
												using (StreamReader sr = new StreamReader("secretknowledge.txt", true))
												{
														StringBuilder sb = new StringBuilder();
														for (int j = 0; j < 8; j++)
														{
																for (int k = 0; k < 8; k++)
																{
																		sb.Append(this.Children[i].Table[j, k]);
																}
														}
														string stateString = sb.ToString();
														while (!sr.EndOfStream)
														{
																if (string.Equals(sr.ReadLine(), stateString))
																{
																		loserState = true;
																		break;
																}
														}
												}
										}

										if (!loserState) //Ha két csomópont értéke egyenlő, véletlenszerűen választunk.
										{
												System.Random random = new System.Random();
												double rand1 = ((random.NextDouble() * random.NextDouble()) % (random.NextDouble() * random.NextDouble()));
												double rand2 = ((random.NextDouble() * random.NextDouble()) % (random.NextDouble() * random.NextDouble()));
												if (rand1 > rand2)
												{
														MinMinimax = this.Children[i].Minimax;
														BestId = this.Children[i].Id;
												}
										}
								}
						}
				}

				return BestId;
		}

		public int GetBestIDForWhiteAi(bool firstStep)
		{
				int MinMinimax = int.MaxValue;
				for (int i = 0; i < this.Children.Count; i++)
				{
						if (this.Children[i].Minimax < MinMinimax)
						{
								MinMinimax = this.Children[i].Minimax;
								BestId = this.Children[i].Id;
						}
						else
						{
								if (this.Children[i].Minimax == MinMinimax)
								{
										bool loserState = false;
										if (!firstStep)
										{
												using (StreamReader sr = new StreamReader("secretknowledge_white.txt", true))
												{
														StringBuilder sb = new StringBuilder();
														for (int j = 0; j < 8; j++)
														{
																for (int k = 0; k < 8; k++)
																{
																		sb.Append(this.Children[i].Table[j, k]);
																}
														}
														string stateString = sb.ToString();
														while (!sr.EndOfStream)
														{
																if (string.Equals(sr.ReadLine(), stateString))
																{
																		loserState = true;
																		break;
																}
														}
												}
										}

										if (!loserState) //Ha két csomópont értéke egyenlő, véletlenszerűen választunk.
										{
												System.Random random = new System.Random();
												double rand1 = ((random.NextDouble() * random.NextDouble()) % (random.NextDouble() * random.NextDouble()));
												double rand2 = ((random.NextDouble() * random.NextDouble()) % (random.NextDouble() * random.NextDouble()));
												if (rand1 > rand2)
												{
														MinMinimax = this.Children[i].Minimax;
														BestId = this.Children[i].Id;
												}
										}
								}
						}
				}

				return BestId;
		}
}
