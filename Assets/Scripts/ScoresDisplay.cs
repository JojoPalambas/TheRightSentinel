using System.Collections;
using System.Collections.Generic;
using System.Runtime;
using UnityEngine;

public class ScoresDisplay : MonoBehaviour
{
    public TextDisplayer congratulations;
    public TextDisplayer victoryScore;
    public TextDisplayer score2;
    public TextDisplayer score3;
    public TextDisplayer score4;

    public void DisplayScores(int[] scores)
    {
        // Ordering the players and the scores
        int[] orderedPlayers = { 0, 1, 2, 3 };
        for (int i = 3; i >= 0; i--)
        {
            for (int j = 0; j < i; j++)
            {
                if (scores[j] < scores[i])
                {
                    int tmp = scores[j];
                    scores[j] = scores[i];
                    scores[i] = tmp;

                    tmp = orderedPlayers[j];
                    orderedPlayers[j] = orderedPlayers[i];
                    orderedPlayers[i] = tmp;
                }
            }
        }

        congratulations.ChangeText("Congratulations, player " + (orderedPlayers[0] + 1).ToString() + "!");
        victoryScore.ChangeText("You won the game with " + (scores[0]).ToString() + " points!");
        score2.ChangeText("Player " + (orderedPlayers[1] + 1).ToString() + ": " + (scores[1]).ToString() + " points");
        score3.ChangeText("Player " + (orderedPlayers[2] + 1).ToString() + ": " + (scores[2]).ToString() + " points");
        score4.ChangeText("Player " + (orderedPlayers[3] + 1).ToString() + ": " + (scores[3]).ToString() + " points");
    }
}
