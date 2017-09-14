﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelLayout
{
    //public int rows = 8;
    //public int cols = 5;
    public int rows;
    public int cols;
    public char[,] levelArray = new char[,] {{'+', '-', '+', '-', '+'},
                                             {'|', '0', '|', '0', '|'},
                                             {'|', '0', '|', '0', '|'},
                                             {'+', '-', '+', '-', '+'},
                                             {'|', '0', '0', '0', '|'},
                                             {'|', '0', '0', '0', '|'},
                                             {'|', '0', '0', '0', '|'},
                                             {'|', '0', '0', '0', '|'},
                                             {'+', '-', '-', '-', '+'}};

    public LevelLayout()
    {
        rows = levelArray.GetLength(0);
        cols = levelArray.GetLength(1);
    }
    


    
}
