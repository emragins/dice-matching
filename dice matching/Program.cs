using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace dice_matching
{
	class Program
	{
		/// <summary>
		/// 
		/// </summary>
		/// <param name="args"></param>
		static void Main(string[] args)
		{

			int numDice = 0;
			int numSides = 0;
			//int numMatchesDesired = 0;
			bool another = true; ;
			while (another)
			{
				Console.Write("Number of dice: ");
				numDice = Convert.ToInt32(Console.ReadLine());          //bad form here. very bad.

				Console.Write("Number of sides on each die: ");
				numSides = Convert.ToInt32(Console.ReadLine());          //bad form here. very bad.

				int[] matches = new int[numDice];	//[1, 2, 3, 4, ....] ; note that this is one MORE than the index.
				FillArrayWithZeros(ref matches);


				int[] sets = new int[numDice];
				FillArrayWithZeros(ref sets);


				int[] roll = new int[numDice];

				Random rand = new Random();
				int maxForRandom = numSides + 1;

				long count = 0;
				while (count < 1000000)	//100000 times...
				{
					for (int i = 0; i < numDice; i++)	//roll a number of dice
					{
						roll[i] = rand.Next(1, maxForRandom);	//(inclusive, exclusive)
					}

					//determine single max number of matches only

					int maxMatched = 1;

					for (int i = 0; i < numDice; i++)	//determine maximum number matched
					{
						int currentNum = roll[i];
						int currentMatches = 1;

						for (int k = i + 1; k < numDice; k++)
						{
							if (roll[k] == currentNum)
							{
								currentMatches++;
							}
						}

						if (currentMatches > maxMatched)
						{
							maxMatched = currentMatches;
						}
					}


					int numSets = 0;

					for (int i = 0; i < numDice; i++)	//determine if there are any other sets of
					{
						int currentNum = roll[i];
						int currentMatches = 1;

						for (int k = i + 1; k < numDice; k++)
						{
							if (roll[k] == currentNum)
							{
								currentMatches++;
							}
						}

						if (currentMatches == maxMatched)
						{
							numSets++;
						}

						if (numSets >= 2)
						{
							sets[maxMatched - 1]++;
							break;
						}
					}


					matches[maxMatched - 1]++;
					count++;
				}

				Console.Write("Number of times total matched:\n");
				for (int i = 0; i < matches.Length; i++)
				{
					Console.Write(Convert.ToString(i + 1) + ":  " + matches[i] + "\n");
				}


				Console.Write("Number of times one or more sets of max match:\n");
				for (int i = 0; i < sets.Length; i++)
				{
					Console.Write(Convert.ToString(i + 1) + ":  " + sets[i] + "\n");
				}

				Console.Write("hit any key to continue, escape to exit\n");
				ConsoleKeyInfo a = Console.ReadKey();
				if (a.Key == ConsoleKey.Escape)
				{
					break;
				}

			}







			//Console.Write("Number of matches desired: ");
			//numMatchesDesired = Convert.ToInt32(Console.ReadLine());          //bad form here. very bad.

			//int[] matches = new int[100];
			//FillArrayWithZeros(ref matches);


			//determine all the sets of denominators available for this scenario
			//in the form: [following(4), 3, 3, 3, 1, following(6), 3, 3, 1, 1, 1, 1, following(5), 3, 3, 2, 1, 1, 0, 0, 0, 0, ...]
			//matches = DetermineDenominators(numDice, numMatchesDesired);



			///TODO: determine corresponding sides-of-dice denominator for each set.



			///TODO: multiply everything out



			///TODO: add up numbers




			///TODO: set up loop to not have to re-run program for entering multiple scenarios

		}

		private static void FillArrayWithZeros(ref int[] a)
		{
			for (int i = 0; i < a.Length; i++)
			{
				a[i] = 0;
			}
		}

		/// <summary>
		/// returns an array in the form: [following(4), 3, 3, 3, 1, following(6), 3, 3, 1, 1, 1, 1, following(5), 3, 3, 2, 1, 1, 0, 0, 0, 0, ...]
		/// </summary>
		/// <param name="numDice"></param>
		/// <param name="numMatchesDesired"></param>
		/// <returns></returns>
		private static int[] DetermineDenominators(int numDice, int numMatchesDesired)
		{
			int[] matches = new int[100];

			int i = 0;  //position in array 'matches'
			int offset = 1;

			//let's to pair+ first
			int remainingDice = numDice;
			int currentMatchDesired = numMatchesDesired;

			while (offset <= numDice)
			{
				matches[i + offset] = numMatchesDesired;
				remainingDice -= numMatchesDesired;
				if (remainingDice < numMatchesDesired)
				{
					numMatchesDesired--;
				}
				offset++;
			}

			matches[i] = offset;
			i += offset + 1;
			offset = 0;


			return matches;

		}


		public static int fact(int x)
		{
			int fact = 1;
			int i = 1;
			while (i <= x)
			{
				fact = fact * i;
				i++;

			}
			return fact;
		}
	}
}
