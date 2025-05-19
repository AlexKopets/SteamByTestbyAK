using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour
{
   public BoardManager board;

    public void OnReshuffleButtonClicked() {
        board.Reshuffle();
    }
}
