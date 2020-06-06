using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class Node
{
		Stack<Node> NodeStack = new Stack<Node>();
		Node Parent;
    public List<Node> Children;
    public int[,] table = new int[8, 8];
    bool IsWhiteNode;
    public static int counter = 0;
    int id;
    int minimax;
		int nodeLevel;

    public Node(int[,] table, Node parent, bool isWhiteNode, int nodeLevel)
    {
				if (parent == null)
				{
						counter = 0;
						id = 0;
						minimax = 0;
						//level = 0;
						best_id = 0;
						best_mm = 0;
						max_minimax = int.MinValue;
						this.nodeLevel = 0;
				}						
        this.Children = new List<Node>();
				this.id = counter;						
        counter++;
        this.Parent = parent;
				this.nodeLevel = nodeLevel;
        for (int i = 0; i < 8; i++)
        {
            for (int j = 0; j < 8; j++)
            {
                this.table[i, j] = table[i, j];
            }
        }
        this.IsWhiteNode = isWhiteNode;
    }

    public int getCounter()
    {
        return counter;
    }

//    static int level = 0;

    //A fa maximum mélysége
    static int max_level = 4;

		public void BuildTree()
		{
				bool outOfNodes = false;
				NodeStack.Push(this);
				Node currentNode;
				int level = 0;
				while( NodeStack.Count != 0 )
				{
						currentNode = NodeStack.Pop();
						int[,] nextTable = new int[8, 8];
						Node Child;

						while (currentNode.nodeLevel == max_level)
						{
								if (NodeStack.Count == 0)
								{
										outOfNodes = true;
										break;
								}										
								currentNode = NodeStack.Pop();
						}
						if (outOfNodes)
								break;

						level = currentNode.nodeLevel + 1;

						for (int i = 0; i < 8; i++)
						{
								for (int j = 0; j < 8; j++)
								{
										if (currentNode.IsWhiteNode)
										{
												if (currentNode.table[i, j] == 1)
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
																						nextTable[z, w] = currentNode.table[z, w];
																				}
																		}
																		if (currentNode.table[i + 1, j] == 0)
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
																						nextTable[z, w] = currentNode.table[z, w];
																				}
																		}
																		if (currentNode.table[i + 1, j + 1] == 0 || currentNode.table[i + 1, j + 1] == 2)
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
																						nextTable[z, w] = currentNode.table[z, w];
																				}
																		}
																		if (currentNode.table[i + 1, j] == 0)
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
																						nextTable[z, w] = currentNode.table[z, w];
																				}
																		}
																		//Jobbra-előre lépés
																		if (currentNode.table[i + 1, j + 1] == 0 || currentNode.table[i + 1, j + 1] == 2)
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
																						nextTable[z, w] = currentNode.table[z, w];
																				}
																		}
																		if (currentNode.table[i + 1, j - 1] == 0 || currentNode.table[i + 1, j - 1] == 2)
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
																						nextTable[z, w] = currentNode.table[z, w];
																				}
																		}
																		if (currentNode.table[i + 1, j - 1] == 0 || currentNode.table[i + 1, j - 1] == 2)
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
																						nextTable[z, w] = currentNode.table[z, w];
																				}
																		}
																		if (currentNode.table[i + 1, j] == 0)
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
												if (currentNode.table[i, j] == 2)
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
																						nextTable[z, w] = currentNode.table[z, w];
																				}
																		}
																		if (currentNode.table[i - 1, j] == 0)
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
																						nextTable[z, w] = currentNode.table[z, w];
																				}
																		}
																		if (currentNode.table[i - 1, j + 1] == 0 || currentNode.table[i - 1, j + 1] == 1)
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
																						nextTable[z, w] = currentNode.table[z, w];
																				}
																		}
																		if (currentNode.table[i - 1, j - 1] == 0 || currentNode.table[i - 1, j - 1] == 1)
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
																						nextTable[z, w] = currentNode.table[z, w];
																				}
																		}
																		if (currentNode.table[i - 1, j] == 0)
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
																						nextTable[z, w] = currentNode.table[z, w];
																				}
																		}
																		if (currentNode.table[i - 1, j + 1] == 0 || currentNode.table[i - 1, j + 1] == 1)
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
																						nextTable[z, w] = currentNode.table[z, w];
																				}
																		}
																		if (currentNode.table[i - 1, j - 1] == 0 || currentNode.table[i - 1, j - 1] == 1)
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
																						nextTable[z, w] = currentNode.table[z, w];
																				}
																		}
																		if (currentNode.table[i - 1, j] == 0)
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

    /*public Node AddNode(bool isBlackNode)
    {
        int[,] nextTable = new int[8, 8];
        Node Child;

        if (level == max_level)
            return null;

        for (int i = 0; i < 8; i++)
        {
            for (int j = 0; j < 8; j++)
            {
								if (isBlackNode)
								{
										if (this.table[i, j] == 1)
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
																				nextTable[z, w] = this.table[z, w];
																		}
																}
																if (this.table[i + 1, j] == 0)
																{
																		nextTable[i, j] = 0;
																		nextTable[i + 1, j] = 1;
																		Child = new Node(nextTable, this, false);
																		this.Children.Add(Child);
																		level++;
																		Child.AddNode(false);
																		level--;
																}
																if (level == max_level)
																		return null;
																//Jobbra-előre lépés
																for (int z = 0; z < 8; z++)
																{
																		for (int w = 0; w < 8; w++)
																		{
																				nextTable[z, w] = this.table[z, w];
																		}
																}
																if (this.table[i + 1, j + 1] == 0 || this.table[i + 1, j + 1] == 2)
																{
																		nextTable[i, j] = 0;
																		nextTable[i + 1, j + 1] = 1;
																		Child = new Node(nextTable, this, false);
																		this.Children.Add(Child);
																		level++;
																		Child.AddNode(false);
																		level--;
																}
																if (level == max_level)
																		return null;
														}
														//Ha a nincs a tábla egyik szélén sem
														if (j > 0 && j < 7)
														{
																//Egyenes lépés
																for (int z = 0; z < 8; z++)
																{
																		for (int w = 0; w < 8; w++)
																		{
																				nextTable[z, w] = this.table[z, w];
																		}
																}
																if (this.table[i + 1, j] == 0)
																{
																		nextTable[i, j] = 0;
																		nextTable[i + 1, j] = 1;
																		Child = new Node(nextTable, this, false);
																		this.Children.Add(Child);
																		level++;
																		Child.AddNode(false);
																		level--;
																}
																if (level == max_level)																		
																		return null;
																//Balra-előre lépés
																for (int z = 0; z < 8; z++)
																{
																		for (int w = 0; w < 8; w++)
																		{
																				nextTable[z, w] = this.table[z, w];
																		}
																}
																//Jobbra-előre lépés
																if (this.table[i + 1, j + 1] == 0 || this.table[i + 1, j + 1] == 2)
																{
																		nextTable[i, j] = 0;
																		nextTable[i + 1, j + 1] = 1;
																		Child = new Node(nextTable, this, false);
																		this.Children.Add(Child);
																		level++;
																		Child.AddNode(false);
																		level--;
																}
																if (level == max_level)
																		return null;
																//Balra előre lépés
																for (int z = 0; z < 8; z++)
																{
																		for (int w = 0; w < 8; w++)
																		{
																				nextTable[z, w] = this.table[z, w];
																		}
																}
																if (this.table[i + 1, j - 1] == 0 || this.table[i + 1, j - 1] == 2)
																{
																		nextTable[i, j] = 0;
																		nextTable[i + 1, j - 1] = 1;
																		Child = new Node(nextTable, this, false);
																		this.Children.Add(Child);
																		level++;
																		Child.AddNode(false);
																		level--;
																}
																if (level == max_level)
																		return null;
														}
														//Ha a tábla jobb szélén van
														if (j == 7)
														{
																//Balra-előre lépés
																for (int z = 0; z < 8; z++)
																{
																		for (int w = 0; w < 8; w++)
																		{
																				nextTable[z, w] = this.table[z, w];
																		}
																}
																if (this.table[i + 1, j - 1] == 0 || this.table[i + 1, j - 1] == 2)
																{
																		nextTable[i, j] = 0;
																		nextTable[i + 1, j - 1] = 1;
																		Child = new Node(nextTable, this, false);
																		this.Children.Add(Child);
																		level++;
																		Child.AddNode(false);
																		level--;
																}
																if (level == max_level)
																		return null;
																//Egyenes lépés
																for (int z = 0; z < 8; z++)
																{
																		for (int w = 0; w < 8; w++)
																		{
																				nextTable[z, w] = this.table[z, w];
																		}
																}
																if (this.table[i + 1, j] == 0)
																{
																		nextTable[i, j] = 0;
																		nextTable[i + 1, j] = 1;
																		Child = new Node(nextTable, this, false);
																		this.Children.Add(Child);
																		level++;
																		Child.AddNode(false);
																		level--;
																}
																if (level == max_level)
																		return null;
														}
												}
										}
								}
								else
								{
										if (this.table[i, j] == 2)
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
																				nextTable[z, w] = this.table[z, w];
																		}
																}
																if (this.table[i - 1, j] == 0)
																{
																		nextTable[i, j] = 0;
																		nextTable[i - 1, j] = 2;
																		Child = new Node(nextTable, this, true);
																		this.Children.Add(Child);
																		level++;
																		Child.AddNode(true);																		
																		level--;
																}
																if (level == max_level)
																		return null;
																//Jobbra-előre lépés
																for (int z = 0; z < 8; z++)
																{
																		for (int w = 0; w < 8; w++)
																		{
																				nextTable[z, w] = this.table[z, w];
																		}
																}
																if (this.table[i - 1, j + 1] == 0 || this.table[i - 1, j + 1] == 1)
																{
																		nextTable[i, j] = 0;
																		nextTable[i - 1, j + 1] = 2;
																		Child = new Node(nextTable, this, true);
																		this.Children.Add(Child);
																		level++;
																		Child.AddNode(true);
																		level--;
																}
																if (level == max_level)
																		return null;
														}
														//Ha nincs a tábla egyik szélén sem
														if (j > 0 && j < 7)
														{
																//Balra-előre lépés
																for (int z = 0; z < 8; z++)
																{
																		for (int w = 0; w < 8; w++)
																		{
																				nextTable[z, w] = this.table[z, w];
																		}
																}
																if (this.table[i - 1, j - 1] == 0 || this.table[i - 1, j - 1] == 1)
																{
																		nextTable[i, j] = 0;
																		nextTable[i - 1, j - 1] = 2;
																		Child = new Node(nextTable, this, true);
																		this.Children.Add(Child);
																		level++;
																		Child.AddNode(true);
																		level--;
																}
																if (level == max_level)
																		return null;
																//Egyenes lépés
																for (int z = 0; z < 8; z++)
																{
																		for (int w = 0; w < 8; w++)
																		{
																				nextTable[z, w] = this.table[z, w];
																		}
																}
																if (this.table[i - 1, j] == 0)
																{
																		nextTable[i, j] = 0;
																		nextTable[i - 1, j] = 2;
																		Child = new Node(nextTable, this, true);
																		this.Children.Add(Child);
																		level++;
																		Child.AddNode(true);
																		level--;
																}
																if (level == max_level)
																		return null;
																//Jobbra-előre lépés
																for (int z = 0; z < 8; z++)
																{
																		for (int w = 0; w < 8; w++)
																		{
																				nextTable[z, w] = this.table[z, w];
																		}
																}
																if (this.table[i - 1, j + 1] == 0 || this.table[i - 1, j + 1] == 1)
																{
																		nextTable[i, j] = 0;
																		nextTable[i - 1, j + 1] = 2;
																		Child = new Node(nextTable, this, true);
																		this.Children.Add(Child);
																		level++;
																		Child.AddNode(true);
																		level--;
																}
																if (level == max_level)
																		return null;
														}
														//Ha a tábla jobb szélén van
														if (j == 7)
														{
																//Balra-előre lépés
																for (int z = 0; z < 8; z++)
																{
																		for (int w = 0; w < 8; w++)
																		{
																				nextTable[z, w] = this.table[z, w];
																		}
																}
																if (this.table[i - 1, j - 1] == 0 || this.table[i - 1, j - 1] == 1)
																{
																		nextTable[i, j] = 0;
																		nextTable[i - 1, j - 1] = 2;
																		Child = new Node(nextTable, this, true);
																		this.Children.Add(Child);
																		level++;
																		Child.AddNode(true);
																		level--;
																}
																if (level == max_level)
																		return null;
																//Egyenes lépés
																for (int z = 0; z < 8; z++)
																{
																		for (int w = 0; w < 8; w++)
																		{
																				nextTable[z, w] = this.table[z, w];
																		}
																}
																if (this.table[i - 1, j] == 0)
																{
																		nextTable[i, j] = 0;
																		nextTable[i - 1, j] = 2;
																		Child = new Node(nextTable, this, true);
																		this.Children.Add(Child);
																		level++;
																		Child.AddNode(true);
																		level--;
																}
																if (level == max_level)
																		return null;
														}
												}
										}
								}                
            }
        }
        return this;
    }*/

    public void addTerminalMinimaxValues()
    {
        if (this.Children.Count == 0)
        {
            this.minimax = calculateMiniMax();
        }
        else
        {
            foreach (Node item in this.Children)
            {
                item.addTerminalMinimaxValues();
            }
        }
    }

    //Minimax számoló
    public int calculateMiniMax()
    {
        int mm = 0;
				int placeValue = 0;
        for (int i = 0; i < 8; i++)
        {
            for (int j = 0; j < 8; j++)
            {
                //Ha fekete bábut találunk egy helyen
                if (this.table[i, j] == 1)
                {
                    switch (i)
                    {                                                
                        case 2: { mm += 2; placeValue = 2; break; }
                        case 3: { mm += 3; placeValue = 3; break; }
                        case 4: { mm += 5; placeValue = 5; break; }
                        case 5: { mm += 7; placeValue = 7; break; }
                        case 6: { mm += 11; placeValue = 11; break; }
                        case 7: { mm += 1000; break; }
                    }
										if (i > 0 && i < 7)
										{
												if (j != 0 && j != 7)
												{
														// Ha biztonságos mezőre lép az dupla pont.
														if (table[i + 1, j + 1] == 0 && table[i + 1, j - 1] == 0)
																mm += (i + 1) * 2;											
														//Védő társak +1 pont
														if (table[i - 1, j + 1] == 1)
																mm++;
														if (table[i - 1, j - 1] == 1)
																mm++;
														//Ellenfél ütő mezője - Nem biztonságos mező
														if (table[i + 1, j + 1] == 2)
																mm -= (i + 1) * 2;
														if (table[i + 1, j - 1] == 2)
																mm -= (i + 1) * 2;
												}
												else
												{
														if (j == 0)
														{
																if (table[i + 1, j + 1] == 0)
																		mm += (i + 1) * 2;					
																
																if (table[i - 1, j + 1] == 1)
																		mm++;

																if (table[i + 1, j + 1] == 2)
																		mm -= (i + 1) * 2;
														}
														else
														{																
																if (table[i + 1, j - 1] == 0)
																		mm += (i + 1) * 2;
																
																if (table[i - 1, j - 1] == 1)
																		mm++;

																if (table[i + 1, j - 1] == 2)
																		mm -= (i + 1) * 2;
														}
												}

												/*if (j != 0 && j != 7) //++ Ha van védő társa az +1 pont (középen)
												{
														if (table[i - 1, j + 1] == 1)
																mm++;
														if (table[i - 1, j - 1] == 1)
																mm++;
														//if (table[i, j + 1] == 1 || table[i, j - 1] == 1) //Ha mellette van az nem védő társ (-1 pont)
														//		mm--;
														if (table[i + 1, j + 1] == 2) // Ha ellenfél ütő mezőjére lép (-mm / 2 pont)
																mm -= mm / 2;
														if (table[i + 1, j - 1] == 2)
																mm -= mm / 2;
												}
												if (j == 0) //++ Ha van védő társa az +1 pont (bal oldal)
												{
														if (table[i - 1, j + 1] == 1)
																mm++;
														//if (table[i, j + 1] == 1) //Ha mellette van az sem védő társ (-1 pont)
														//		mm--;
														if (table[i + 1, j + 1] == 2) // Ha ellenfél ütő mezőjére lép (-1 pont)
																mm -= mm / 2;
												}
												if (j == 7) //++ Ha van védő társa az +1 pont (jobb oldal)
												{
														if (table[i - 1, j - 1] == 1)
																mm++;
														//if (table[i, j - 1] == 1) //Ha mellette van az sem védő társ (-1 pont)
														//		mm--;
														if (table[i + 1, j - 1] == 2) // Ha ellenfél ütő mezőjére lép (-mm/2 pont)
																mm -= mm/2;
												}
												if (table[i - 1, j] == 1) //Ha mögötte van a bábu (nem védő, akkor -1 pont)
														mm--;
												if (i<7)
												{
														if (table[i + 1, j] == 1) //Ha előtte van a bábu (nem védő, akkor -1 pont)
																mm--;
														if (table[i + 1, j] == 2) //Ha előtte van ellenséges bábu(block) (ellenfél akkor +1 pont)
																mm++;
												}								*/
										}												
								}
                //Ha fehér bábut találunk egy helyen
                /*else
                {
                    if (this.table[i, j] == 2)
                    {
                        switch (i)
                        {
                            case 0: { mm -= 1000; break; }
                            case 1: { mm -= 11; break; }
                            case 2: { mm -= 6; break; }
                            case 3: { mm -= 5; break; }
                            case 4: { mm -= 3; break; }
                            case 5: { mm -= 2; break; }
                        }
										if (i > 0 && i < 7)
										{
												if (j != 0 && j != 7) //++ Ha van védő társa az -1 pont (középen)
												{
														if (table[i + 1, j + 1] == 2)
																mm--;
														if (table[i + 1, j - 1] == 2)
																mm--;
														//if (table[i, j + 1] == 2 || table[i, j - 1] == 2) //Ha mellette van az sem védő társ (+1 pont)
														//		mm++;
														if (table[i - 1, j + 1] == 1) // Ha ellenfél ütő mezőjére lép (-mm / 2 pont)
																mm += mm / 2;
														if (table[i - 1, j - 1] == 1)
																mm += mm / 2;
												}
												else if (j == 0) //++ Ha van védő társa az -1 pont (bal oldal)
												{
														if (table[i + 1, j + 1] == 2)
																mm--;
														//if (table[i, j + 1] == 2) //Ha mellette van az sem védő társ (+1 pont)
														//		mm++;
														if (table[i - 1, j + 1] == 1) // Ha ellenfél ütő mezőjére lép (-mm / 2 pont)
																mm += mm / 2;
												}
												else if (j == 7) //++ Ha van védő társa az -1 pont (jobb oldal)
												{
														if (table[i + 1, j - 1] == 2)
																mm--;
														//if (table[i, j - 1] == 2) //Ha mellette van az sem védő társ (+1 pont)
														//		mm++;
														if (table[i - 1, j - 1] == 1) // Ha ellenfél ütő mezőjére lép (-mm / 2 pont)
																mm += mm / 2;
												}
												if (table[i + 1, j] == 2) //Ha mögötte van a bábu (nem védő, akkor +1 pont)
														mm++;
												if (i>0)
														{
																if (table[i - 1, j] == 2) //Ha előtte van a bábu (block) (ellenfél akkor +1 pont)
																		mm++;
																if (table[i - 1, j] == 1) //Ha előtte van a bábu (block) (nem védő, akkor -1 pont)
																		mm--;
														}												
												}
								}
                }*/
            }
        }
        return mm;
    }

    static int best_id;
    static int best_mm;

    public int FillTreeWithMinimax()
    {
        if (this.Children.Count == 0)
        {
            return this.minimax;
        }
        else
        {
            if (this.IsWhiteNode)
            {
                best_mm = int.MaxValue;
                foreach (Node node in this.Children)
                {
                    best_mm = Mathf.Min(best_mm, node.FillTreeWithMinimax());												
												this.minimax = best_mm;
                }
                return best_mm;
            }
            else
            {
                best_mm = int.MinValue;
                foreach (Node node in this.Children)
                {
                    best_mm = Mathf.Max(best_mm, node.FillTreeWithMinimax());												
												this.minimax = best_mm;										
                }
                return best_mm;
            }
        }
    }

    public int getMinimaxByID(int id)
    {
        if (this.id == id)
            return this.minimax;
        else
        {
            foreach (Node item in this.Children)
            {
                item.getMinimaxByID(id);
            }
        }
        return this.minimax;
    }

    static int[,] State = new int[8, 8];
    public int[,] getStateByID(int givenID)
    {
        if (this.id == givenID)
        {
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    State[i, j] = this.table[i, j];
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
                    this.Children[k].getStateByID(givenID);
                }
            }
        }
        return State;
    }

    static int max_minimax = int.MinValue;
    public int getBestID()
    {
        if (this.Parent != null && this.Parent.id == 0)
        {
            if (this.minimax > max_minimax)
            {
                max_minimax = this.minimax;
                best_id = this.id;
            } else if (this.minimax == max_minimax)
						{
								if (Random.Range(1, 3) == 2) //Ha két csomópont értéke egyenlő, véletlenszerűen választunk.
								{
										max_minimax = this.minimax;
										best_id = this.id;
								}
						}
            return best_id;
        }
        else
        {
            if (this.Children.Count != 0)
            {
                for (int k = 0; k < this.Children.Count; k++)
                {
                    this.Children[k].getBestID();
                }
            }
        }
        return best_id;
    }
}
