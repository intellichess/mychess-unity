using UnityEngine;
using System.Collections;

public class Pawn : Piece
{
	public override bool[,] PossibleMove ()
	{
		bool[,] r = new bool[8, 8];

		Piece c, c2;
        int[] e = BoardManager.Instance.EnPassant;


        //white teams move 
        if (isWhite) {
			//straight move 1 space 
			if (CurrentY != 7) {
				c = BoardManager.Instance.Pieces [CurrentX, CurrentY + 1];
				if (c == null) {
					r [CurrentX, CurrentY + 1] = true;
				}
			}


			//straight move 2 space on intial move 

			if (CurrentY == 1) {
				c = BoardManager.Instance.Pieces [CurrentX, CurrentY + 1];
				c2 = BoardManager.Instance.Pieces [CurrentX, CurrentY + 2]; 
				if (c == null && c2 == null) {
					r [CurrentX, CurrentY + 2] = true; 
				}
			}
			//diagonal left to capture a piece
			if (CurrentX != 0 && CurrentY != 7) {

                if(e[0] == CurrentX - 1 && e[1] == CurrentY + 1)
                    r[CurrentX - 1, CurrentY + 1] = true;
                    
                
                    
                c = BoardManager.Instance.Pieces [CurrentX - 1, CurrentY + 1];
				if (c != null && !c.isWhite) {
					r [CurrentX - 1, CurrentY + 1] = true;
				}
			}

			//diagonal right to capture a piece
			if (CurrentX != 7 && CurrentY != 7) {
                if (e[0] == CurrentX + 1 && e[1] == CurrentY + 1)
                    r[CurrentX + 1, CurrentY + 1] = true;

                c = BoardManager.Instance.Pieces [CurrentX + 1, CurrentY + 1];
				if (c != null && !c.isWhite) {
					r [CurrentX + 1, CurrentY + 1] = true;
				}
			}
		} else {
			//straight move 1 space 
			if (CurrentY != 0) {
				c = BoardManager.Instance.Pieces [CurrentX, CurrentY - 1];
				if (c == null) {
					r [CurrentX, CurrentY - 1] = true;
				}
			}


			//straight move 2 space on intial move 

			if (CurrentY == 6) {
				c = BoardManager.Instance.Pieces [CurrentX, CurrentY - 1];
				c2 = BoardManager.Instance.Pieces [CurrentX, CurrentY - 2]; 
				if (c == null && c2 == null) {
					r [CurrentX, CurrentY - 2] = true; 
				}
			}
			//diagonal left (but as black)
			if (CurrentX != 0 && CurrentY != 0) {
                if (e[0] == CurrentX - 1 && e[1] == CurrentY -1)
                    r[CurrentX - 1, CurrentY - 1] = true;

                c = BoardManager.Instance.Pieces [CurrentX - 1, CurrentY - 1];
				if (c != null && c.isWhite) {
					r [CurrentX - 1, CurrentY - 1] = true;
				}
			}

			//diagonal right (black)
			if (CurrentX != 7 && CurrentY != 0) {
                if (e[0] == CurrentX + 1 && e[1] == CurrentY - 1)
                    r[CurrentX + 1, CurrentY - 1] = true;
                c = BoardManager.Instance.Pieces [CurrentX + 1, CurrentY - 1];
				if (c != null && c.isWhite) {
					r [CurrentX + 1, CurrentY - 1] = true;
				}
			}
		}

		return r; 
	}
}
