using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TrainingSet
{
    public double[] input;
    public double output;
}

public class Perceptron : MonoBehaviour
{
    public TrainingSet[] ts;
    public double[] weights = { 0, 0 };
    public double bias = 0;
    public double totalError = 0;

    //public SimpleGrafer sg;

    double DotProductBias(double[] v1, double[] v2)
    {
        if (v1 == null || v2 == null) return -1;
        if (v1.Length != v2.Length) return -1;
        double d = 0;
        for (int x = 0; x < v1.Length; x++)
        {
            d += v1[x] * v2[x];
        }
        d += bias;
        return d;
    }

    void InitializeWeights()
    {
        for (int i = 0; i < weights.Length; i++)
        {
            weights[i] = UnityEngine.Random.Range(-1.0f, 1.0f);
        }
        bias = UnityEngine.Random.Range(-1.0f, 1.0f);

    }

    void Train(int epochs)
    {
        InitializeWeights();
        for (int e = 0; e < epochs; e++)
        {
            totalError = 0;
            for (int t = 0; t < ts.Length; t++)
            {
                UpdateWeights(t);
                Debug.Log("W1: " + weights[0] + "W2: " + weights[1] + "Bias: " + bias);
            }
            Debug.Log("Total Error: " + totalError);
        }
    }

    void UpdateWeights(int j)
    {
        double error = ts[j].output - CalcOutput(j);
        totalError += Mathf.Abs((float)error);
        for (int i = 0; i < weights.Length; i++)
        {
            weights[i] = weights[i] + error * ts[j].input[i];
        }
        bias += error;
    }

    double CalcOutput(int i)
    {
        double dp = DotProductBias(weights, ts[i].input);
        if (dp > 0) return 1;
        return 0;
    }

    //void DrawAllPoints()
    //{
    //    for (int t = 0; t < ts.Length; t++)
    //    {
    //        if (ts[t].output == 0) sg.DrawPoint((float)ts[t].input[0], (float)ts[t].input[1], Color.magenta);
    //        else sg.DrawPoint((float)ts[t].input[0], (float)ts[t].input[1], Color.green);
    //    }
    //}

    void Start()
    {
        //DrawAllPoints();
        Train(10);
        //sg.DrawRay((float)(-bias / weights[1] / (bias / weights[0])), (float)(-bias / weights[1]), Color.red);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
